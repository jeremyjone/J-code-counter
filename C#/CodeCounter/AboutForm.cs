using System;
using System.Windows.Forms;

namespace CodeCounter
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            titleLabel.Text = "代码计数器(Code Counter)";
            versionLabel.Text = "V 1.0";
            copyrightLabel.Text = $"©Desgin By JeremyJone {DateTime.Now.Year}";
            aboutTextBox.Text = @"代码文档 说明
I  本程序采用 .net 5 框架开发，使用前需要安装该框架。
II 本程序仅仅作为学习交流使用。
";
        }
    }
}
