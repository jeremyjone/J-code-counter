# -*-coding: utf8 -*-


# 处理文本字符串，将字符转换成指定格式，默认utf8，使其支持中文
def string(text, code="utf8"):
    return text.decode(code)


class langEnum:
    def __init__(self):
        self.enum = {
            "c": "C",
            "cpp": "Cpp",
            "cs": "CSharp",
            "html": "HTML",
            "java": "Java",
            "js": "JavaScript",
            "py": "Python",
            "vue": "Vue"
        }

    def getLang(self, ext):
        return self.enum.get(ext)

    def supportLang(self):
        return sorted(self.enum.keys())

    def getKeyByValue(self, value):
        for k, v in self.enum.items():
            if v == value:
                return k
        return None
