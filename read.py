# -*-coding: utf8 -*-
# 不同语言有不同的规则，分别文件读取，返回相应的数量
import os
import re

from lang import string


# 读取文件的线程函数
def readFile(widget, counter, rootPath, extList, ignoreList):
    for root, dirs, files in os.walk(rootPath):

        # 判断文件夹是否为需要忽略的文件夹，提供自定义忽略列表
        igFlag = False
        for igName in ignoreList:
            if re.findall(igName, root):
                igFlag = True
                break
        # 前缀为'.'开始的文件夹都视为系统文件夹，也需要隐藏
        # 这里将路径切开后，使用切片进行翻转，首先判断最末尾的文件夹，这样效率更高
        for r in root.split(os.path.sep)[::-1]:
            if re.findall(r"^\.", r):
                igFlag = True
                break

        # 忽略的文件夹直接继续下一层
        if igFlag:
            continue

        # 操作文件
        for fName in files:
            # 去掉系统的隐藏文件
            if re.findall(r"^\.", fName):
                continue

            # 生成完整的文件路径
            _path = os.path.join(root, fName)

            # 判断文件扩展名，如果在需求列表，读取内容
            try:
                extName = os.path.splitext(fName)[1].replace(".", "")
                if extName in extList:
                    # 到这里就是需要读取的文件
                    # 打印log到textarea
                    widget.updateLogTextSignal.emit(string(_path))

                    # 具体的读取文件的操作，根据文件类型调用不同方法读取，分别计算
                    with open(_path, "r") as f:
                        read = Read(extName, f.readlines())

                    # 将数据添加到counter中
                    counter.setCounter(extName, "file", 1)  # 成功读取文件后，文件数量 +1
                    counter.setCounter(extName, "code", read.code)
                    counter.setCounter(extName, "blank", read.blank)
                    counter.setCounter(extName, "comment", read.comment)
            except Exception as e:
                info = string(
                    "{0} - 该文件已忽略。\n\t\t出现如下问题，无法完成解析：{1}".format(_path, string(e)))
                widget.updateLogTextSignal.emit(info)
                continue

    # 完成后发送信号
    widget.updateResTableSignal.emit()


# 具体的读取文件类，确定某个语言的读取规则，并读取计数。
class Read:
    def __init__(self, ext, lines):
        self._code = 0
        self._blank = 0
        self._comment = 0

        self._lines = lines

        if ext in ["c", "cpp", "cs"]:
            self.__cpp()
        elif ext == "html":
            self.__html()
        elif ext in ["java", "js"]:
            self.__java()
        elif ext == "py":
            self.__python()
        elif ext == "vue":
            self.__vue()

    @property
    def code(self):
        return self._code

    @property
    def blank(self):
        return self._blank

    @property
    def comment(self):
        return self._comment

    def __cpp(self):
        # 标记一个注释的结尾符，如果为空，说明可以正常判断，如果不为空，则在之前遇到了一个多行注释，需要找到结尾符，同时其间所有行都标记为注释
        cmt = ""

        for line in self._lines:
            line = line.strip()

            if cmt == "":
                if line is "":
                    self._blank += 1
                # 需要删除 if - endif 的部分等...
                elif re.findall("^#if", line.lower()):
                    self._comment += 1
                    cmt = "^#endif"

                # 该判断需要放在单斜杠前面，否则查找不到
                # 要不然的话，下面一条需要写双斜杠也可以，这样方便一些
                elif re.findall(r"^/\*", line):
                    self._comment += 1
                    # 判断是否为单行
                    if not re.findall(r"\*/$", line):
                        cmt = r"\*/"
                # 双杠注释和三杠注释都可以概括为下面的情况
                elif line.startswith("/"):
                    self._comment += 1
                else:
                    self._code += 1
            else:
                # 首先注释行数 +1
                self._comment += 1

                if re.findall(cmt, line.lower()):
                    # 清空注释标记符
                    cmt = ""

    def __html(self):
        cmt = ""

        for line in self._lines:
            line = line.strip()

            if cmt == "":
                if line is "":
                    self._blank += 1
                elif re.findall("^<!--", line):
                    self._comment += 1
                    # 判断是否为单行，如果当前行找不到注释结尾符，则向下查找
                    if not re.findall("-->$", line):
                        cmt = "-->"
                else:
                    self._code += 1
            else:
                # 首先注释行数 +1
                self._comment += 1

                if re.findall(cmt, line):
                    # 清空注释标记符
                    cmt = ""

    def __java(self):
        cmt = ""

        for line in self._lines:
            line = line.strip()

            if cmt == "":
                if line is "":
                    self._blank += 1
                # 该判断需要放在单斜杠前面，否则查找不到
                # 要不然的话，下面一条需要写双斜杠也可以，这样方便一些
                elif re.findall(r"^/\*", line):
                    self._comment += 1
                    # 判断是否为单行
                    if not re.findall(r"\*/$", line):
                        cmt = r"\*/"
                elif line.startswith("/"):
                    self._comment += 1
                else:
                    self._code += 1
            else:
                # 首先注释行数 +1
                self._comment += 1

                if re.findall(cmt, line):
                    # 清空注释标记符
                    cmt = ""

    def __python(self):
        cmt = ""

        for line in self._lines:
            line = line.strip()

            if cmt == "":
                if line is "":
                    self._blank += 1
                elif line.startswith("#"):
                    self._comment += 1
                # python的多行注释，使用单引号或双引号，即：下面两种情况
                elif re.findall("^\"\"\"", line):
                    self._comment += 1
                    # 判断是否为单行
                    if not re.findall("\"\"\"$", line):
                        cmt = "\"\"\""
                elif re.findall("^\'\'\'", line):
                    self._comment += 1
                    # 判断是否为单行
                    if not re.findall("\'\'\'$", line):
                        cmt = "\'\'\'"
                else:
                    self._code += 1
            else:
                # 首先注释行数 +1
                self._comment += 1

                if re.findall(cmt, line):
                    # 清空注释标记符
                    cmt = ""

    def __vue(self):
        # Vue比较多了，需要同时判断js和html的格式
        cmt = ""

        for line in self._lines:
            line = line.strip()

            if cmt == "":
                if line is "":
                    self._blank += 1
                # 判断html的注释格式
                elif re.findall(r"^<!--", line):
                    self._comment += 1
                    if not re.findall("-->$", line):
                        cmt = "-->"
                # 判断js的注释格式
                elif re.findall(r"^/\*", line):
                    self._comment += 1
                    # 判断是否为单行
                    if not re.findall(r"\*/$", line):
                        cmt = r"\*/"
                elif line.startswith("/"):
                    self._comment += 1
                else:
                    self._code += 1
            else:
                # 首先注释行数 +1
                self._comment += 1

                if re.findall(cmt, line):
                    # 清空注释标记符
                    cmt = ""
