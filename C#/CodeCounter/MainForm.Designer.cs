
using System;

namespace CodeCounter
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ignoreText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.table = new System.Windows.Forms.DataGridView();
            this.lang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.annotation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.file = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.promptBox = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.showPathBox = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.startBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.langBox = new System.Windows.Forms.GroupBox();
            this.langPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.selectAllBox = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.selectPathLine = new System.Windows.Forms.TextBox();
            this.selectPathBtn = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectFileBtnMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.selectPathBtnMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.panel6 = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectFileStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectPathStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editLanguageStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.langBox.SuspendLayout();
            this.langPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 536);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel5.Controls.Add(this.splitContainer2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 124);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3);
            this.panel5.Size = new System.Drawing.Size(784, 392);
            this.panel5.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel3);
            this.splitContainer2.Size = new System.Drawing.Size(778, 386);
            this.splitContainer2.SplitterDistance = 192;
            this.splitContainer2.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ignoreText);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.table);
            this.splitContainer1.Size = new System.Drawing.Size(778, 192);
            this.splitContainer1.SplitterDistance = 259;
            this.splitContainer1.TabIndex = 2;
            // 
            // ignoreText
            // 
            this.ignoreText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ignoreText.Location = new System.Drawing.Point(0, 17);
            this.ignoreText.Multiline = true;
            this.ignoreText.Name = "ignoreText";
            this.ignoreText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ignoreText.Size = new System.Drawing.Size(259, 175);
            this.ignoreText.TabIndex = 2;
            this.ignoreText.WordWrap = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "需要忽略的文件夹(按行)";
            // 
            // table
            // 
            this.table.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lang,
            this.line,
            this.code,
            this.empty,
            this.annotation,
            this.file});
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(0, 0);
            this.table.Name = "table";
            this.table.ReadOnly = true;
            this.table.RowTemplate.Height = 25;
            this.table.Size = new System.Drawing.Size(515, 192);
            this.table.TabIndex = 1;
            // 
            // lang
            // 
            this.lang.HeaderText = "语言";
            this.lang.Name = "lang";
            this.lang.ReadOnly = true;
            // 
            // line
            // 
            this.line.HeaderText = "行数";
            this.line.Name = "line";
            this.line.ReadOnly = true;
            // 
            // code
            // 
            this.code.HeaderText = "代码";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            // 
            // empty
            // 
            this.empty.HeaderText = "空行";
            this.empty.Name = "empty";
            this.empty.ReadOnly = true;
            // 
            // annotation
            // 
            this.annotation.HeaderText = "注释";
            this.annotation.Name = "annotation";
            this.annotation.ReadOnly = true;
            // 
            // file
            // 
            this.file.HeaderText = "文件数";
            this.file.Name = "file";
            this.file.ReadOnly = true;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.Controls.Add(this.promptBox);
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(778, 190);
            this.panel3.TabIndex = 3;
            // 
            // promptBox
            // 
            this.promptBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.promptBox.Location = new System.Drawing.Point(3, 28);
            this.promptBox.Margin = new System.Windows.Forms.Padding(10);
            this.promptBox.Multiline = true;
            this.promptBox.Name = "promptBox";
            this.promptBox.ReadOnly = true;
            this.promptBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.promptBox.Size = new System.Drawing.Size(772, 159);
            this.promptBox.TabIndex = 0;
            this.promptBox.TabStop = false;
            this.promptBox.WordWrap = false;
            // 
            // panel8
            // 
            this.panel8.AutoSize = true;
            this.panel8.Controls.Add(this.showPathBox);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(772, 25);
            this.panel8.TabIndex = 2;
            // 
            // showPathBox
            // 
            this.showPathBox.AutoSize = true;
            this.showPathBox.Location = new System.Drawing.Point(7, 1);
            this.showPathBox.Name = "showPathBox";
            this.showPathBox.Size = new System.Drawing.Size(167, 21);
            this.showPathBox.TabIndex = 1;
            this.showPathBox.Text = "显示文件路径(可能会卡顿)";
            this.showPathBox.UseVisualStyleBackColor = true;
            this.showPathBox.CheckedChanged += new System.EventHandler(this.showPathBox_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.startBtn);
            this.panel4.Controls.Add(this.cancelBtn);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 91);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3);
            this.panel4.Size = new System.Drawing.Size(784, 33);
            this.panel4.TabIndex = 4;
            // 
            // startBtn
            // 
            this.startBtn.AutoSize = true;
            this.startBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.startBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startBtn.Location = new System.Drawing.Point(3, 3);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(678, 27);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "开始";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.AutoSize = true;
            this.cancelBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.cancelBtn.Location = new System.Drawing.Point(681, 3);
            this.cancelBtn.MinimumSize = new System.Drawing.Size(100, 0);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(100, 27);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "取消";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // panel7
            // 
            this.panel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel7.Controls.Add(this.langBox);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 30);
            this.panel7.MinimumSize = new System.Drawing.Size(0, 61);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(3);
            this.panel7.Size = new System.Drawing.Size(784, 61);
            this.panel7.TabIndex = 2;
            // 
            // langBox
            // 
            this.langBox.AutoSize = true;
            this.langBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.langBox.Controls.Add(this.langPanel);
            this.langBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.langBox.Location = new System.Drawing.Point(3, 3);
            this.langBox.Name = "langBox";
            this.langBox.Size = new System.Drawing.Size(778, 55);
            this.langBox.TabIndex = 0;
            this.langBox.TabStop = false;
            this.langBox.Text = "选择要统计的语言";
            // 
            // langPanel
            // 
            this.langPanel.AutoSize = true;
            this.langPanel.Controls.Add(this.selectAllBox);
            this.langPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.langPanel.Location = new System.Drawing.Point(3, 19);
            this.langPanel.Name = "langPanel";
            this.langPanel.Padding = new System.Windows.Forms.Padding(3);
            this.langPanel.Size = new System.Drawing.Size(772, 33);
            this.langPanel.TabIndex = 6;
            // 
            // selectAllBox
            // 
            this.selectAllBox.AutoSize = true;
            this.selectAllBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.selectAllBox.Location = new System.Drawing.Point(6, 6);
            this.selectAllBox.Name = "selectAllBox";
            this.selectAllBox.Size = new System.Drawing.Size(75, 21);
            this.selectAllBox.TabIndex = 0;
            this.selectAllBox.Text = "选择全部";
            this.selectAllBox.UseVisualStyleBackColor = true;
            this.selectAllBox.CheckedChanged += new System.EventHandler(this.selectAllBox_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.selectPathLine);
            this.panel2.Controls.Add(this.selectPathBtn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.MinimumSize = new System.Drawing.Size(210, 30);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(784, 30);
            this.panel2.TabIndex = 1;
            // 
            // selectPathLine
            // 
            this.selectPathLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectPathLine.Location = new System.Drawing.Point(3, 3);
            this.selectPathLine.MinimumSize = new System.Drawing.Size(100, 4);
            this.selectPathLine.Name = "selectPathLine";
            this.selectPathLine.ReadOnly = true;
            this.selectPathLine.Size = new System.Drawing.Size(678, 23);
            this.selectPathLine.TabIndex = 1;
            this.selectPathLine.TabStop = false;
            // 
            // selectPathBtn
            // 
            this.selectPathBtn.ContextMenuStrip = this.contextMenuStrip1;
            this.selectPathBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.selectPathBtn.Location = new System.Drawing.Point(681, 3);
            this.selectPathBtn.Name = "selectPathBtn";
            this.selectPathBtn.Size = new System.Drawing.Size(100, 24);
            this.selectPathBtn.TabIndex = 0;
            this.selectPathBtn.Text = "选择...";
            this.selectPathBtn.UseVisualStyleBackColor = true;
            this.selectPathBtn.Click += new System.EventHandler(this.selectBtn_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectFileBtnMenu,
            this.selectPathBtnMenu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 48);
            // 
            // selectFileBtnMenu
            // 
            this.selectFileBtnMenu.Name = "selectFileBtnMenu";
            this.selectFileBtnMenu.Size = new System.Drawing.Size(136, 22);
            this.selectFileBtnMenu.Text = "选择文件";
            this.selectFileBtnMenu.Click += new System.EventHandler(this.selectFileMenuItem_Clicked);
            // 
            // selectPathBtnMenu
            // 
            this.selectPathBtnMenu.Name = "selectPathBtnMenu";
            this.selectPathBtnMenu.Size = new System.Drawing.Size(136, 22);
            this.selectPathBtnMenu.Text = "选择文件夹";
            this.selectPathBtnMenu.Click += new System.EventHandler(this.selectPathMenuItem_Clicked);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.progressBar);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 516);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3);
            this.panel6.Size = new System.Drawing.Size(784, 20);
            this.panel6.TabIndex = 6;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(3, 3);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(778, 14);
            this.progressBar.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(784, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectFileStripMenuItem,
            this.selectPathStripMenuItem,
            this.toolStripSeparator,
            this.exitStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.fileToolStripMenuItem.Text = "文件(&F)";
            // 
            // selectFileStripMenuItem
            // 
            this.selectFileStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("selectFileStripMenuItem.Image")));
            this.selectFileStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectFileStripMenuItem.Name = "selectFileStripMenuItem";
            this.selectFileStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.selectFileStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.selectFileStripMenuItem.Text = "选择文件(&F)";
            this.selectFileStripMenuItem.Click += new System.EventHandler(this.selectFileMenuItem_Clicked);
            // 
            // selectPathStripMenuItem
            // 
            this.selectPathStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("selectPathStripMenuItem.Image")));
            this.selectPathStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectPathStripMenuItem.Name = "selectPathStripMenuItem";
            this.selectPathStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.selectPathStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.selectPathStripMenuItem.Text = "选择文件夹(&P)";
            this.selectPathStripMenuItem.Click += new System.EventHandler(this.selectPathMenuItem_Clicked);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(192, 6);
            // 
            // exitStripMenuItem
            // 
            this.exitStripMenuItem.Name = "exitStripMenuItem";
            this.exitStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exitStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.exitStripMenuItem.Text = "退出(&E)";
            this.exitStripMenuItem.Click += new System.EventHandler(this.exitMenuItem_Clicked);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editLanguageStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.editToolStripMenuItem.Text = "编辑(&E)";
            // 
            // editLanguageStripMenuItem
            // 
            this.editLanguageStripMenuItem.Name = "editLanguageStripMenuItem";
            this.editLanguageStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.editLanguageStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.editLanguageStripMenuItem.Text = "编辑语言(&L)";
            this.editLanguageStripMenuItem.Click += new System.EventHandler(this.editLanguageMenuItem_Clicked);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.helpToolStripMenuItem.Text = "帮助(&H)";
            // 
            // aboutStripMenuItem
            // 
            this.aboutStripMenuItem.Name = "aboutStripMenuItem";
            this.aboutStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutStripMenuItem.Text = "关于(&A)";
            this.aboutStripMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Clicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "Code Counter";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.langBox.ResumeLayout(false);
            this.langBox.PerformLayout();
            this.langPanel.ResumeLayout(false);
            this.langPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button selectPathBtn;
        private System.Windows.Forms.TextBox selectPathLine;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.FlowLayoutPanel langPanel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox ignoreText;
        private System.Windows.Forms.DataGridView table;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox promptBox;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox langBox;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.CheckBox selectAllBox;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.CheckBox showPathBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editLanguageStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectFileStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectPathStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem selectFileBtnMenu;
        private System.Windows.Forms.ToolStripMenuItem selectPathBtnMenu;
        private System.Windows.Forms.DataGridViewTextBoxColumn lang;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn line;
        private System.Windows.Forms.DataGridViewTextBoxColumn empty;
        private System.Windows.Forms.DataGridViewTextBoxColumn annotation;
        private System.Windows.Forms.DataGridViewTextBoxColumn file;
    }
}

