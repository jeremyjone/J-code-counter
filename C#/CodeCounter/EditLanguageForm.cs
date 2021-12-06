using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CodeCounter.languages;

namespace CodeCounter
{
    public partial class EditLanguageForm : Form
    {
        private const string Sep = ",";
        private const string Undefined = "(undefined)";
        private readonly Languages _languages;
        private Language _selectLanguage;
        private Language _tempLanguage;
        private string _separator;

        public EditLanguageForm(Languages languages)
        {
            _languages = languages;
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            _separator = Sep;
            _selectLanguage = null;
            _tempLanguage = new Language { Name = Undefined };
            separatorText.Text = _separator;

            UpdateList();
            Clear();
        }

        private void Clear()
        {
            nameText.Text = "";
            extText.Text = "";
            singleCommentText.Text = "";
            multiCommentText.Text = "";
        }

        private void UpdateList()
        {
            listBox.Items.Clear();
            var all = _languages.GetAll();
            var index = -1;
            for (var i = 0; i < all.Count; i++)
            {
                var language = all[i];
                listBox.Items.Add(language.Name);

                if (_selectLanguage != null &&
                    string.Equals(_selectLanguage.Name, language.Name, StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                }
            }

            if (index >= 0)
            {
                listBox.SetSelected(index, true);
            }
            else
            {
                _selectLanguage = null;
                okBtn.Enabled = false;
                okBtn.Cursor = Cursors.No;
            }
        }

        private void UpdateMultiCommentText()
        {
            if (_selectLanguage == null) return;

            multiCommentText.Text = string.Join(Environment.NewLine,
                _selectLanguage.MultiLineComment.Select(l => $"{l.Start}{_separator}{l.End}"));
        }

        /// <summary>
        /// 点击左侧列表获取语言
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var box = (ListBox)sender;
            if (box.SelectedItems.Count > 0)
            {
                okBtn.Enabled = true;
                okBtn.Cursor = Cursors.Default;

                var lang = _languages.Get(box.SelectedItem.ToString());

                if (ReferenceEquals(box.SelectedItem, Undefined))
                {
                    lang = _tempLanguage;
                    okBtn.Text = "创建";
                }
                else
                {
                    okBtn.Text = "更新";
                }

                if (lang == null)
                {
                    MessageBox.Show("选择的语言没有值");
                    return;
                }

                _selectLanguage = lang;

                nameText.Text = lang.Name;
                extText.Text = string.Join(Environment.NewLine, lang.Ext);
                singleCommentText.Text = string.Join(Environment.NewLine, lang.SingleLineComment);
                UpdateMultiCommentText();
            }
            else
            {
                MessageBox.Show("未选择语言");
            }
        }

        /// <summary>
        /// 分隔符输入框失去焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void separatorText_FocusChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(box.Text))
            {
                if (string.IsNullOrWhiteSpace(_separator))
                {
                    _separator = Sep;
                }

                separatorText.Text = _separator;
            }
            else
            {
                _separator = separatorText.Text;
            }

            UpdateMultiCommentText();
        }

        /// <summary>
        /// 删除按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (_selectLanguage == null) return;

            _languages.Remove(_selectLanguage);
            UpdateList();
            Clear();
        }

        /// <summary>
        /// 重置按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetBtn_Click(object sender, EventArgs e)
        {
            if (_selectLanguage == null)
            {
                Init();
                return;
            }

            var index = _languages.GetAll().FindIndex(x => x.Name == _selectLanguage.Name);

            Init();

            if (index >= 0)
            {
                listBox.SetSelected(index, true);
            }
        }

        /// <summary>
        /// 确认按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (_selectLanguage == null) return;

            if (listBox.Items.Contains(Undefined))
            {
                // 创建新的
                if (_tempLanguage.Name == Undefined)
                {
                    MessageBox.Show("填写正确的名称");
                    return;
                }

                var lang = _languages.Get(_tempLanguage.Name);
                if (lang == null ||
                    MessageBox.Show("语言已存在，是否替换？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                    DialogResult.OK)
                {
                    _languages.Update(_tempLanguage);
                    _selectLanguage = _tempLanguage;
                }
                else
                {
                    return;
                }
            }
            else
            {
                // 根据新数据生成一个新对象
                var lang = new Language
                {
                    Name = nameText.Text,
                    Ext = new List<string>(extText.Text.Split(Environment.NewLine)),
                    SingleLineComment = new List<string>(singleCommentText.Text.Split(Environment.NewLine)),
                    MultiLineComment = new List<MultiLineComment>()
                };

                foreach (var s in multiCommentText.Text.Split(Environment.NewLine))
                {
                    var comments = s.Split(_separator);
                    if (comments.Length == 2)
                    {
                        lang.MultiLineComment.Add(new MultiLineComment
                        {
                            Start = comments[0],
                            End = comments[1]
                        });
                    }
                }

                _languages.Update(lang);
            }

            UpdateList();
            _tempLanguage = new Language { Name = Undefined };
        }

        /// <summary>
        /// 关闭对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            // 有临时变量，要删除
            if (listBox.Items.Contains(Undefined))
            {
                var lang = _languages.Get(Undefined);
                _languages.Remove(lang);
            }

            Close();
        }

        /// <summary>
        /// 添加按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addBtn_Click(object sender, EventArgs e)
        {
            if (listBox.Items.Contains(Undefined)) return;

            // 创建临时变量
            listBox.Items.Add(Undefined);
            listBox.SetSelected(listBox.Items.Count - 1, true);
        }

        #region 输入框失去焦点，用于处理新建的临时对象

        private void nameText_FocusChanged(object sender, EventArgs e)
        {
            if (ReferenceEquals(listBox.SelectedItem, Undefined))
            {
                _tempLanguage.Name = ((TextBox)sender).Text;
            }
        }

        private void extText_FocusChanged(object sender, EventArgs e)
        {
            if (ReferenceEquals(listBox.SelectedItem, Undefined))
            {
                _tempLanguage.Ext = new List<string>(((TextBox)sender).Text.Split(Environment.NewLine));
            }
        }

        private void singleCommentText_FocusChanged(object sender, EventArgs e)
        {
            if (ReferenceEquals(listBox.SelectedItem, Undefined))
            {
                _tempLanguage.SingleLineComment = new List<string>(((TextBox)sender).Text.Split(Environment.NewLine));
            }
        }

        private void multiCommentText_FocusChanged(object sender, EventArgs e)
        {
            if (!ReferenceEquals(listBox.SelectedItem, Undefined)) return;

            var comments = ((TextBox)sender).Text.Split(Environment.NewLine);
            foreach (var comment in comments)
            {
                var c = comment.Split(_separator);
                if (c.Length == 2)
                {
                    _tempLanguage.MultiLineComment.Add(new MultiLineComment
                    {
                        Start = c[0],
                        End = c[1]
                    });
                }
            }
        }

        #endregion
    }
}
