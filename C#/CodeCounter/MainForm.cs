using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeCounter.handler;
using CodeCounter.languages;

namespace CodeCounter
{
    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    public partial class MainForm : Form
    {
        private delegate void UpdateText(string txt);
        private readonly UpdateText _updatePrompt;
        private delegate void UpdatePb(int number);
        private readonly UpdatePb _updateProgress;
        private delegate void UpdateTableData();
        private readonly UpdateTableData _updateTable;
        private CancellationTokenSource _cts;


        private readonly List<string> _extCheckList;
        private readonly List<string> _fileList;
        private readonly Languages _languages;
        private readonly List<CheckBox> _languageBoxes;
        private bool _triggeredBySingleSelect;
        private bool _triggeredByMultiSelect;
        private readonly Counter _counter;
        private List<string> _ignores = new List<string> { ".vs", ".vscode", ".idea", ".git", "node_modules", "dist", "release" };

        private bool _showPath;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _updatePrompt = UpdatePrompt;
            _updateProgress = UpdateProgress;
            _updateTable = UpdateTable;

            _extCheckList = new List<string>();
            _languageBoxes = new List<CheckBox>();
            _triggeredBySingleSelect = false;
            _triggeredByMultiSelect = false;
            _fileList = new List<string>();
            _counter = new Counter();

            _languages = new Languages();

            AddLanguageBoxes();

            ignoreText.Text = string.Join(Environment.NewLine, _ignores);

            _showPath = false;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void Init()
        {
            promptBox.Clear();
            progressBar.Value = 0;
            table.Rows.Clear();

            _fileList.Clear();
            _counter.Clear();
        }

        /// <summary>
        /// 添加一堆语言选择框
        /// </summary>
        /// <param name="boxes"></param>
        private void AddLanguageBoxes()
        {
            if (langPanel.Controls[0] is CheckBox all)
            {
                all.Checked = false;
                langPanel.Controls.Clear();
                _languageBoxes.Clear();
                _triggeredBySingleSelect = false;
                _triggeredByMultiSelect = false;
                langPanel.Controls.Add(all);
            }

            foreach (var box in _languages.GetAll().Select(CreateCheckBox))
            {
                AddLanguageBox(box);
                _languageBoxes.Add(box);
            }
            UpdatePrompt("语言已加载");
        }

        /// <summary>
        /// 添加一个语言选择框
        /// </summary>
        /// <param name="box"></param>
        private void AddLanguageBox(Control box)
        {
            // 添加语言选择框
            langPanel.Controls.Add(box);

            // 动态调整 panel 的大小。我也不知道为什么 FlowLayoutPanel 不设计成换行自动大小的。。。
            var step = box.Size.Height + box.Margin.Top + box.Margin.Bottom;
            if (box.Location.Y + step + 6 <= panel7.Height) return;
            panel7.Height += step;
            panel7.Update();
        }

        /// <summary>
        /// 创建检测语言选择框
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        private CheckBox CreateCheckBox(Language language)
        {
            var checkBox = new CheckBox
            {
                AutoSize = true,
                Name = language.Name,
                TabIndex = 1,
                Text = language.Name,
                UseVisualStyleBackColor = true
            };
            checkBox.CheckedChanged += langbox_CheckedChanged;

            return checkBox;
        }

        /// <summary>
        /// 添加语言信息（扩展自定义功能）
        /// </summary>
        private void AddCheckLanguage()
        {
            var language = new Language
            {
                Name = "Vue",
                Ext = new List<string> { "vue" },
                SingleLineComment = new List<string> { "//" },
                MultiLineComment = new List<MultiLineComment>
                {
                    new MultiLineComment {Start = "/*", End = "/*"},
                    new MultiLineComment {Start = "<!--", End = "-->"}
                }
            };
            _languages.Add(language);
            AddLanguageBox(CreateCheckBox(language));
        }

        /// <summary>
        /// 更新提示信息
        /// </summary>
        /// <param name="txt"></param>
        private void UpdatePrompt(string txt)
        {
            UpdatePrompt(txt, false);
        }

        /// <summary>
        /// 更新提示信息
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="clear"></param>
        private void UpdatePrompt(string txt, bool clear)
        {
            if (clear) promptBox.Clear();
            promptBox.AppendText(txt + Environment.NewLine);
        }

        /// <summary>
        /// 更新进度条
        /// </summary>
        /// <param name="number"></param>
        private void UpdateProgress(int number)
        {
            progressBar.Value = number;
            progressBar.Update();
        }

        /// <summary>
        /// 更新表格
        /// </summary>
        private void UpdateTable()
        {
            // 将数据添加到表格中
            var total = new ReadResult();
            var files = 0;
            foreach (var result in _counter.Results)
            {
                var index = table.Rows.Add();
                table.Rows[index].Cells["lang"].Value = result.Name;
                table.Rows[index].Cells["line"].Value = result.Result.Lines;
                table.Rows[index].Cells["code"].Value = result.Result.Code;
                table.Rows[index].Cells["empty"].Value = result.Result.Empty;
                table.Rows[index].Cells["annotation"].Value = result.Result.Annotation;
                table.Rows[index].Cells["file"].Value = result.Files;

                total.Add(result.Result);
                files += result.Files;
            }

            // 总计
            var t = table.Rows.Add();
            table.Rows[t].DefaultCellStyle.Font = new Font("宋体", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            table.Rows[t].Cells["lang"].Value = "总计";
            table.Rows[t].Cells["line"].Value = total.Lines;
            table.Rows[t].Cells["code"].Value = total.Code;
            table.Rows[t].Cells["empty"].Value = total.Empty;
            table.Rows[t].Cells["annotation"].Value = total.Annotation;
            table.Rows[t].Cells["file"].Value = files;
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        private void OpenFileDialog()
        {
            using var dialog = new OpenFileDialog()
            {
                InitialDirectory = Environment.CurrentDirectory,
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var name = dialog.FileName;
                selectPathLine.Text = name;
            }
            else
            {
                selectPathLine.Text = "";
            }
        }

        /// <summary>
        /// 打开文件夹
        /// </summary>
        private void OpenFolderDialog()
        {
            var dialog = new FolderBrowserDialog { Description = "请选择文件路径" };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var path = dialog.SelectedPath;
                selectPathLine.Text = path;
            }
            else
            {
                selectPathLine.Text = "";
            }
        }

        /// <summary>
        /// 递归文件夹
        /// </summary>
        /// <param name="path"></param>
        private void RecursiveFolder(string path)
        {
            var dir = new DirectoryInfo(path);
            var files = dir.GetFiles();
            var dirs = dir.GetDirectories();

            // 不仅入忽略的文件夹
            if (path.Split(Path.DirectorySeparatorChar).Any(s => _ignores.Contains(s)))
            {
                return;
            }

            // 会取消，不要用 foreach
            for (var i = 0; i < files.Length; i++)
            {
                if (_cts.Token.IsCancellationRequested) return;

                // 忽略文件
                if (_ignores.Contains(files[i].Name)) continue;

                foreach (var _ in _extCheckList.Where(ext =>
                    // 对应扩展名
                    files[i].Name.EndsWith($".{ext}", StringComparison.OrdinalIgnoreCase)))
                {
                    _fileList.Add(files[i].FullName.ToLower());
                }
            }

            for (var i = 0; i < dirs.Length; i++)
            {
                RecursiveFolder(dirs[i].FullName);
            }
        }

        /// <summary>
        /// 读取文件的调用方法
        /// </summary>
        /// <param name="path"></param>
        private void ReadFileFunc(string path)
        {
            var fileTask = new Task(() => ReadFiles(path), _cts.Token);
            fileTask.Start();

            fileTask.ContinueWith(task1 => BeginInvoke(_updateTable))
                .ContinueWith(task3 => _cts = null)
                .ContinueWith(task2 => BeginInvoke(_updatePrompt, "完成！"));
        }

        /// <summary>
        /// 统计文件内容
        /// </summary>
        /// <param name="root"></param>
        private void ReadFiles(string root)
        {
            for (var i = 0; i < _fileList.Count; i++)
            {
                // 取消
                if (_cts.Token.IsCancellationRequested) return;

                // 更新进度条
                BeginInvoke(_updateProgress, Convert.ToInt32((i + 1.0f) / _fileList.Count * 100));

                // 找到文件对应的语言
                var language = _languages.Get(_fileList[i].Split('.').Last());
                var read = new Read(_fileList[i], language);
                var res = read.ReadFileByLine();

                // 打印文件路径
                if (_showPath)
                {
                    BeginInvoke(_updatePrompt,
                        _fileList[i].Replace(root, ".", StringComparison.InvariantCultureIgnoreCase));
                }

                // 添加数量到对应的语言中
                _counter.AddResult(language.Name, res);
            }
        }

        /// <summary>
        /// 选择按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectBtn_Click(object sender, EventArgs e)
        {
            //OpenFileDialog();
            OpenFolderDialog();
        }

        /// <summary>
        /// 开始按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startBtn_Click(object sender, EventArgs e)
        {
            // 必须要选择路径
            if (string.IsNullOrWhiteSpace(selectPathLine.Text))
            {
                UpdatePrompt("请选择一个文件或文件夹", true);
                return;
            }

            // 先重置数据
            Init();

            // 整理忽略文件夹
            _ignores = ignoreText.Text.Split(Environment.NewLine).Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();
            ignoreText.Text = string.Join(Environment.NewLine, _ignores);

            // 整理要统计的后缀
            if (_extCheckList.Count == 0)
            {
                UpdatePrompt("至少选择一个要统计的语言", true);
                return;
            }

            UpdatePrompt("开始!");
            UpdatePrompt("检索文件中，请稍后...");

            // 递归目录，找到虽有符合查找条件的文件
            var path = selectPathLine.Text;
            _cts = new CancellationTokenSource();

            if (File.Exists(path))
            {
                // 文件路径
                _fileList.Add(path.ToLower());
                ReadFileFunc(path);
            }
            else if (Directory.Exists(path))
            {
                // 文件夹路径
                var folderTask = new Task(() => RecursiveFolder(path), _cts.Token);
                folderTask.Start();

                // 等待找到所有文件之后，依次读取
                folderTask.ContinueWith(t =>
                {
                    if (!folderTask.IsCompleted || _cts.Token.IsCancellationRequested) return;

                    BeginInvoke(_updatePrompt, $"检索到 {_fileList.Count} 个文件。");

                    if (!_showPath) BeginInvoke(_updatePrompt, "分析中，请稍后...");

                    ReadFileFunc(path);
                });
            }
            else
            {
                // 路径错误，直接返回
                UpdatePrompt("检索文件中，请稍后...");
                _cts = null;
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            if (_cts == null) return;

            _cts.Cancel();
            UpdatePrompt("已取消");
        }

        /// <summary>
        /// 勾选语言触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void langbox_CheckedChanged(object sender, EventArgs e)
        {
            var name = (sender as CheckBox)?.Name;
            var language = _languages.Get(name);

            foreach (var ext in language.Ext)
            {
                var r = _extCheckList.FindIndex(x => x == ext);
                if (r == -1)
                    _extCheckList.Add(ext);
                else
                    _extCheckList.RemoveAt(r);
            }

            // 如果是全选操作的，则不继续
            if (_triggeredByMultiSelect)
            {
                _triggeredByMultiSelect = false;
                return;
            }

            // 判断是否已经全选
            var c = ((CheckBox)sender).Checked;
            if (!c)
            {
                // 取消，肯定不会是全选
                if (!selectAllBox.Checked) return;
                _triggeredBySingleSelect = true;
                selectAllBox.Checked = false;
            }
            else
            {
                // 选择一个，检查是否已经全选，如果全选了，选中全选按钮
                _triggeredBySingleSelect = true;
                for (var i = 0; i < _languageBoxes.Count; i++)
                {
                    if (_languageBoxes[i].Checked) continue;
                    _triggeredBySingleSelect = false;
                    break;
                }

                if (_triggeredBySingleSelect)
                {
                    selectAllBox.Checked = true;
                }
            }
        }

        /// <summary>
        /// 全选按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAllBox_CheckedChanged(object sender, EventArgs e)
        {
            // 单选触发，不做后续操作
            if (_triggeredBySingleSelect)
            {
                _triggeredBySingleSelect = false;
                return;
            }

            var c = ((CheckBox)sender).Checked;

            foreach (var box in _languageBoxes)
            {
                _triggeredByMultiSelect = true;
                box.Checked = c;
            }
        }

        /// <summary>
        /// 统计时打印文件路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showPathBox_CheckedChanged(object sender, EventArgs e)
        {
            _showPath = ((CheckBox)sender).Checked;
        }

        /// <summary>
        /// 打开文件菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectFileMenuItem_Clicked(object sender, EventArgs e)
        {
            OpenFileDialog();
        }

        /// <summary>
        /// 打开文件夹菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectPathMenuItem_Clicked(object sender, EventArgs e)
        {
            OpenFolderDialog();
        }

        /// <summary>
        /// 退出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitMenuItem_Clicked(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// 关于菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutMenuItem_Clicked(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm { StartPosition = FormStartPosition.CenterParent };
            aboutForm.ShowDialog();
        }

        /// <summary>
        /// 编辑语言菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editLanguageMenuItem_Clicked(object sender, EventArgs e)
        {
            var form = new EditLanguageForm(_languages) { StartPosition = FormStartPosition.CenterParent };
            form.ShowDialog();

            AddLanguageBoxes();

        }
    }
}
