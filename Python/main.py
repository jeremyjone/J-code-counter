# -*-coding: utf8 -*-

import os
import sys
from threading import Thread
from PySide import QtGui, QtCore

from ui_load import UILoader
from read import readFile
from lang import langEnum, string
from counter import CodeCounter


# 读取当前文件路径，并且拼接，用于资源文件，所有资源文件路径都是用该函数装饰，它服务于PyInstaller
def resource(path):
    if getattr(sys, 'frozen', False):  # 是否Bundle Resource
        base_path = sys._MEIPASS
    else:
        base_path = os.path.dirname(os.path.abspath(__file__))
    return os.path.join(base_path, path)


class MainWindow(QtGui.QMainWindow):
    # 定义需要的信号 ↓↓↓↓↓
    # 更新日志文本信息的信号，需要携带一个需要显示在日志中的文本信息 -> str
    updateLogTextSignal = QtCore.Signal(str)
    # 更新表格信息的信号，这是当读取文件完成后调用该信号，可以更新表格信息
    updateResTableSignal = QtCore.Signal()

    def __init__(self, lang, counter):
        super(MainWindow, self).__init__()
        self._lang = lang
        self._counter = counter

        # 绑定信号
        self.updateLogTextSignal.connect(self.updateLogTextHandle)
        self.updateResTableSignal.connect(self.updateResTableHandle)

        # 制定一些参数
        self._ignoreList = [".git", ".vscode", "dist", "node_modules", ".vs"]
        self._root = ""
        self._extList = []
        self._ignore = True
        self._logText = string("准备...")
        self._readThread = None

        # 一些控制小部件的flag
        self._selectAllFlag = False  # 全选语言时手动 / 自动，True为自动
        self._selectOneFlag = False  # 单选语言时手动 / 自动，True为自动

        # 最后加载UI
        self.initUI()

    def initUI(self):
        # 加载UI
        uiFile = resource("resource\\mainWindow.ui")
        win = UILoader(uiFile)
        self.setCentralWidget(win)
        self.setWindowIcon(QtGui.QIcon(resource("resource\\cc_icon.png")))

        # 获取布局
        self.langSelectLayout = self.findChild(
            QtGui.QLayout, "langSelectLayout")
        self.pathSelectLayout = self.findChild(
            QtGui.QLayout, "pathSelectLayout")
        self.startLayout = self.findChild(QtGui.QLayout, "startLayout")

        # 获取小部件
        self.selectAllCkBox = self.findChild(QtGui.QCheckBox, "selectAllCkBox")
        self.selectPathLine = self.findChild(QtGui.QLineEdit, "selectPathLine")
        self.selectPathBtn = self.findChild(QtGui.QPushButton, "selectPathBtn")

        self.ignoreSysCkBox = self.findChild(QtGui.QCheckBox, "ignoreSysCkBox")
        self.startBtn = self.findChild(QtGui.QPushButton, "startBtn")

        self.resultTable = self.findChild(QtGui.QTableWidget, "resultTable")
        self.logTextArea = self.findChild(QtGui.QTextEdit, "logTextArea")

        # 获取菜单
        self.selectPathAction = self.findChild(
            QtGui.QAction, "selectPathAction")
        self.exitAction = self.findChild(QtGui.QAction, "exitAction")

        # 给选择语言栏添加语言复选框
        for lang in self._lang.supportLang():
            cb = QtGui.QCheckBox(self._lang.getLang(lang), self)
            cb.stateChanged.connect(self.checkLangHandle)
            self.langSelectLayout.addWidget(cb)

        # 给各种绑定事件
        self.startBtn.clicked.connect(self.startHandle)
        self.selectPathBtn.clicked.connect(self.selectPathHandle)
        self.ignoreSysCkBox.stateChanged.connect(self.ignoreSysHandle)
        self.selectAllCkBox.stateChanged.connect(self.selectAllLangHandle)

        # 绑定菜单事件
        self.selectPathAction.triggered.connect(self.selectPathHandle)
        self.exitAction.triggered.connect(self.exitHandle)

        # 设置文本变量
        self.logTextArea.setText(self._logText)

        self.setWindowTitle("JCodeCounter")
        self.resize(800, 600)

        self.show()

    def clearHandle(self):
        # 每次启动前把表格清空
        self.resultTable.setRowCount(0)

        # 清空counter的数据内容
        self._counter.clear()

        # 清空log
        self.logTextArea.clear()
        self._logText = string("准备...")
        self.logTextArea.setText(self._logText)

    def startHandle(self):
        # 首先判断是否可以启动
        if not self._root:
            self.updateLogTextHandle("选择一个路径...")
            return

        if not self._extList:
            self.updateLogTextHandle("选择需要汇总的语言...")
            return

        # 每次启动前清空所有数据
        self.clearHandle()

        self.updateLogTextHandle("开始...")

        # 开一个线程，用于读取文件内容，否则主线程（界面）就卡死了
        self._readThread = Thread(target=readFile, args=(self, self._counter,
                                                         self._root, self._extList, self._ignoreList if self._ignore else []))
        self._readThread.start()
        # readFile(self.logTextArea, self._counter,
        #          self._root, self._extList, self._ignoreList if self._ignore else [])

    def selectPathHandle(self):
        # 打开对话框，位置为上一次打开的位置
        file = QtGui.QFileDialog.getExistingDirectory(
            self, string("选择路径..."), self._root)
        if file:
            self._root = file

        # 将路径显示在界面中
        self.selectPathLine.setText(self._root)

    def checkLangHandle(self, state):
        # 这里使用sender动态获取点击的checkbox
        cb = self.sender()
        key = self._lang.getKeyByValue(cb.text())

        # 没找到的话直接返回
        if key == None:
            return

        if state == QtCore.Qt.Unchecked:
            # 取消选中，删除
            self._extList.remove(key)
        elif state == QtCore.Qt.Checked:
            # 选中添加
            self._extList.append(key)

        # 自动调整，这本身就是点击全选按钮时的操作，不需要再修改全选按钮的状态
        if self._selectAllFlag:
            return

        # 如果已经全部选中，则将全选按钮选中
        # 先将改变一个的状态修改为True，这是自动调整，不需要再触发全部
        self._selectOneFlag = True
        if len(self._lang.supportLang()) == len(self._extList):
            # 数量相等，已经全选
            self.selectAllCkBox.setCheckState(QtCore.Qt.CheckState.Checked)
        else:
            self.selectAllCkBox.setCheckState(QtCore.Qt.CheckState.Unchecked)

        # 调整之后把状态该为初始
        self._selectOneFlag = False

    def ignoreSysHandle(self, state):
        if state == QtCore.Qt.Unchecked:
            self._ignore = False
        elif state == QtCore.Qt.Checked:
            self._ignore = True

    def selectAllLangHandle(self, state):
        # 全选/全不选 语言的控制函数
        if state == QtCore.Qt.Unchecked:
            state = QtCore.Qt.CheckState.Unchecked
        elif state == QtCore.Qt.Checked:
            state = QtCore.Qt.CheckState.Checked

        # 如果是因为全部手动选择语言之后触发的全选，那么不会触发自动调整
        if self._selectOneFlag:
            return

        # 将flag该为True，自动调整模式
        self._selectAllFlag = True
        for index in range(1, self.langSelectLayout.count()):
            # print self.langSelectLayout.itemAt(index).widget().text()
            self.langSelectLayout.itemAt(index).widget().setCheckState(state)

        # 调整之后改为默认状态
        self._selectAllFlag = False

    def exitHandle(self):
        # 退出程序，首先关闭线程
        try:
            self._readThread.stop()
        except:
            pass
        sys.exit(0)

    def updateLogTextHandle(self, text):
        # 插入文本
        self.logTextArea.append(string(text))
        # 始终滚动到最下方
        self.logTextArea.verticalScrollBar().setValue(
            self.logTextArea.verticalScrollBar().maximum())

    def updateResTableHandle(self):
        # 将结果添加到表格中
        header = ["file", "code", "comment", "blank"]
        for key in self._counter.getCounter().keys():
            self.resultTable.insertRow(0)
            self.resultTable.setItem(
                0, 0, QtGui.QTableWidgetItem(self._lang.getLang(key)))

            # 循环将数据添加到表格中，枚举循环一次header，省代码量
            for i, h in enumerate(header):
                self.resultTable.setItem(0, i + 1, QtGui.QTableWidgetItem(
                    "%d" % self._counter.getCounter()[key].get(h)))

        # 打一个完成的log
        self.updateLogTextHandle("完成!")


if __name__ == "__main__":
    # 创建枚举实例
    enum = langEnum()

    # 创建一个保存数据的类实例
    cc = CodeCounter()

    app = QtGui.QApplication(sys.argv)
    win = MainWindow(enum, cc)
    sys.exit(app.exec_())
