#pragma execution_character_set("utf-8")  // 解决中文编码问题
#include "qtguiapplication1.h"
#include <QFileDialog>

QtGuiApplication1::QtGuiApplication1(QWidget *parent)
	: QMainWindow(parent)
{
	// 装载UI
	ui.setupUi(this);
	this->resize(1200, 840);
	this->setWindowTitle("JCodeCounter");

	QIcon* qicon = new QIcon(".\\Resources\\cc_icon.png");
	this->setWindowIcon(*qicon);

	// 变量赋值
	QString ig[6] = { tr(".git"), tr(".vscode"), tr("dist"), tr("node_modules"), tr(".vs"), tr("venv") };
	ignoreList.assign(ig, ig + 6);

	QString hv[4] = { tr("file"), tr("code"), tr("comment"), tr("blank") };
	headerV.assign(hv, hv + 4);

	// 获取小部件
	langSelectLayout = this->findChild<QLayout*>("langSelectLayout");
	pathSelectLayout = this->findChild<QLayout*>("pathSelectLayout");
	startLayout = this->findChild<QLayout*>("startLayout");

	selectAllCkBox = this->findChild<QCheckBox*>("selectAllCkBox");
	selectPathLine = this->findChild<QLineEdit*>("selectPathLine");
	selectPathBtn = this->findChild<QPushButton*>("selectPathBtn");

	ignoreSysCkBox = this->findChild<QCheckBox*>("ignoreSysCkBox");
	startBtn = this->findChild<QPushButton*>("startBtn");

	resultTable = this->findChild<QTableWidget*>("resultTable");
	logTextArea = this->findChild<QTextEdit*>("logTextArea");

	selectPathAction = this->findChild<QAction*>("selectPathAction");
	exitAction = this->findChild<QAction*>("exitAction");

	// 添加语言栏复选框
	for (int i = 0; i < lang.supportLang().size(); ++i)
	{
		QCheckBox* cb = new QCheckBox(tr(lang.supportLang()[i].c_str()), this);
		connect(cb, &QCheckBox::stateChanged, this, &QtGuiApplication1::onSelectLangStateChanged);
		langSelectLayout->addWidget(cb);
	}

	//绑定事件
	connect(startBtn, &QPushButton::clicked, this, &QtGuiApplication1::onStartClick);
	connect(selectPathBtn, &QPushButton::clicked, this, &QtGuiApplication1::onSelectPathClick);
	connect(ignoreSysCkBox, &QCheckBox::stateChanged, this, &QtGuiApplication1::onIgnoreStateChanged);
	connect(selectAllCkBox, &QCheckBox::stateChanged, this, &QtGuiApplication1::onSelectAllLangStateChanged);
	connect(selectPathAction, &QAction::triggered, this, &QtGuiApplication1::onSelectPathTriggered);
	connect(exitAction, &QAction::triggered, this, &QtGuiApplication1::onExitTriggered);

	// 设置初始文本
	logTextArea->setText(logText);
}

void QtGuiApplication1::clear()
{
	// 清空表格
	resultTable->setRowCount(0);

	// 清空counter
	counter.clear();

	// 清空log
	logText = START_LOG_TEXT;
	logTextArea->clear();
	logTextArea->setText(logText);
}

void QtGuiApplication1::openPath()
{
	QString dir = QFileDialog::getExistingDirectory(this, tr("选择路径..."), root, QFileDialog::ShowDirsOnly);
	if (dir != "")
	{
		root = dir;
	}

	// 显示在路径展示框中
	selectPathLine->setText(root);
}

void QtGuiApplication1::close()
{
	updateLogText(tr("exit"));
	exit(0);
}

void QtGuiApplication1::updateLogText(QString text)
{
	logTextArea->append(text);
}

void QtGuiApplication1::updateExt()
{
	for (std::vector<QString>::iterator it = extList.begin(); it < extList.end(); ++it)
	{
		// C/Cpp需要添加头文件扩展名
		if ((*it) == "c" || (*it) == "cpp")
		{
			extList.push_back(tr("h"));
			break;
		}
	}
}

// ****************************
// ***** 下面是信号槽函数 *****
// ****************************
void QtGuiApplication1::onSelectLangStateChanged(int state)
{
	QCheckBox* cb = qobject_cast<QCheckBox*>(sender());
	QString key = lang.getExtByLang((cb->text()).toStdString()).c_str();
	
	if (key == "")
	{
		return;
	}

	if (state == Qt::Checked)
	{
		// 选中添加，循环为了保证不重复添加
		bool r = false;
		for (std::vector<QString>::iterator it = extList.begin(); it < extList.end(); ++it)
		{
			if (*it == key)
			{
				r = true;
				break;
			}
		}

		if (!r)
		{
			extList.push_back(key);
		}
	}
	else if(state == Qt::Unchecked)
	{
		// 取消，删除指定元素
		for (std::vector<QString>::iterator it = extList.begin(); it < extList.end(); ++it)
		{
			if (*it == key)
			{
				extList.erase(it);
				break;
			}
		}
	}

	// 自动调整，这是点击全选时的操作，不需要执行下面的操作
	if (selectAllFlag)
	{
		return;
	}

	// 下面是单独调整
	selectOneFlag = true;
	// 如果长度相等，表示已经全选，需要勾选全选框，否则取消勾选
	if (lang.supportLang().size() == extList.size())
	{
		selectAllCkBox->setCheckState(Qt::CheckState::Checked);
	}
	else
	{
		selectAllCkBox->setCheckState(Qt::CheckState::Unchecked);
	}
	// 调整完成，恢复flag的状态
	selectOneFlag = false;
}

void QtGuiApplication1::onStartClick()
{
	// 判断是否可以启动
	if (root == "")
	{
		updateLogText(tr("选择一个路径..."));
		return;
	}

	if (extList.size() == 0)
	{
		updateLogText(tr("选择需要汇总的语言..."));
		return;
	}

	// 清空
	clear();

	// 开始执行
	updateLogText(tr("开始..."));

	// 如果不勾选忽略系统文件，则需要一个空向量传入线程
	std::vector<QString>* _ignoreList;
	if (ignore == false)
	{
		_ignoreList = new std::vector<QString>;
	}
	else {
		_ignoreList = &ignoreList;
	}

	// 还需要做一些针对文件扩展名的辅助性工作，如果选择了某个语言，改语言并不一定完全只有被选择的一个扩展名，比如C语言还应该包含头文件.h
	updateExt();

	// 开启线程读取文件
	readThread = new ReadThread(root.toStdString(), &counter, &extList, _ignoreList);
	connect(readThread, &ReadThread::updateLogTextSignal, this, &QtGuiApplication1::onUpdateLogText);
	connect(readThread, &ReadThread::finished, this, &QtGuiApplication1::onUpdateResTable);

	readThread->start();
}

void QtGuiApplication1::onSelectPathClick()
{
	openPath();
}

void QtGuiApplication1::onIgnoreStateChanged(int state)
{
	if (state)
	{
		ignore = true;
	}
	else
	{
		ignore = false;
	}
}

void QtGuiApplication1::onSelectAllLangStateChanged(int state)
{
	// 单独选择触发的全选不执行任何操作
	if (selectOneFlag)
	{
		return;
	}

	Qt::CheckState cs;
	if (state)
	{
		cs = Qt::CheckState::Checked;
	}
	else
	{
		cs = Qt::CheckState::Unchecked;
	}

	// 全选模式
	selectAllFlag = true;

	for (int i = 1; i < langSelectLayout->count(); i++)
	{
		QCheckBox* cb = qobject_cast<QCheckBox*>(langSelectLayout->itemAt(i)->widget());
		cb->setCheckState(cs);
	}

	selectAllFlag = false;
}

void QtGuiApplication1::onSelectPathTriggered()
{
	openPath();
}

void QtGuiApplication1::onExitTriggered()
{
	close();
}

void QtGuiApplication1::onUpdateLogText(QString text)
{
	updateLogText(text);
}

void QtGuiApplication1::onUpdateResTable()
{
	// 不知道为什么直接在for内部使用counter.getCounter()一直报错，赋值一个变量就解决了，可能是C++的玄学。
	std::map<QString, std::map<QString, int>> c = counter.getCounter();
	for (auto it = c.begin(); it != c.end(); ++it)
	{
		// 读取出对应的文件类型，如果为空，说明不是当前程序所支持的内容，直接返回
		std::string n = lang.getLang((it->first).toStdString());

		if (n == "") continue;

		resultTable->insertRow(0);

		// 添加文件类型到第一列
		QTableWidgetItem *headerItem = new QTableWidgetItem(tr(n.c_str()));
		resultTable->setItem(0, 0, headerItem);

		// 添加数据到后面的列中
		for (int i = 0; i < headerV.size(); i++)
		{
			char buffer[10];
			_itoa((it->second)[headerV[i]], buffer, 10);
			QTableWidgetItem *contentItem = new QTableWidgetItem(tr(buffer));
			resultTable->setItem(0, i + 1, contentItem);
		}
	}

	// 输出完成
	updateLogText(tr("完成！"));
}
