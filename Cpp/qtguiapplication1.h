#pragma once
#define START_LOG_TEXT tr("准备...")

#include <QMainWindow>
#include "ui_qtguiapplication1.h"
#include "counter.h"
#include "lang.h"
#include "readthread.h"
#include <QTextEdit>
#include <QLayout>
#include <QCheckBox>
#include <QLineEdit>
#include <QPushButton>
#include <QTableWidget>
#include <QAction>

class QtGuiApplication1 : public QMainWindow
{
	Q_OBJECT

public:
	QtGuiApplication1(QWidget *parent = Q_NULLPTR);

private slots:
	void onSelectLangStateChanged(int state);
	void onStartClick();
	void onSelectPathClick();
	void onIgnoreStateChanged(int state);
	void onSelectAllLangStateChanged(int state);
	void onSelectPathTriggered();
	void onExitTriggered();
	void onUpdateLogText(QString text);
	void onUpdateResTable();

private:
	void clear();
	void close();
	void openPath();
	void updateLogText(QString text);
	void updateExt();

private:
	Ui::QtGuiApplication1Class ui;

	// ****************************
	// ***** 语言类型，计数器 *****
	// ****************************
	LangEnum lang;
	CodeCounter counter;

	// ********************
	// ***** 一些参数 *****
	// ********************
	// 忽略内容
	std::vector<QString> ignoreList;
	bool ignore = true;
	// 根目录
	QString root = "";
	// 结果表格中需要展示的内容头
	std::vector<QString> headerV;
	// 扩展名列表
	std::vector<QString> extList;
	// 下方的显示文字
	QString logText = START_LOG_TEXT;
	// 线程
	ReadThread *readThread;

	// ****************************
	// ***** 控制小部件的flag *****
	// ****************************
	// 全选语言的flag，用于内部函数控制流使用
	bool selectAllFlag = false;
	// 单独选择语言的flag，用于内部函数控制流使用
	bool selectOneFlag = false;

	// ********************
	// ***** 小部件们 *****
	// ********************
	// 样式布局
	QLayout* langSelectLayout = Q_NULLPTR;
	QLayout* pathSelectLayout = Q_NULLPTR;
	QLayout* startLayout = Q_NULLPTR;
	// 选择语言
	QCheckBox* selectAllCkBox = Q_NULLPTR;
	// 选择的路径
	QLineEdit* selectPathLine = Q_NULLPTR;
	QPushButton* selectPathBtn = Q_NULLPTR;
	// 启动选项
	QCheckBox* ignoreSysCkBox = Q_NULLPTR;
	QPushButton* startBtn = Q_NULLPTR;
	// 结果的显示表格
	QTableWidget* resultTable = Q_NULLPTR;
	// 日志文本区域
	QTextEdit* logTextArea = Q_NULLPTR;
	//菜单
	QAction* selectPathAction = Q_NULLPTR;
	QAction* exitAction = Q_NULLPTR;
};
