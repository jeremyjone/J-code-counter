# -*- coding:utf8 -*-
# 整个系统的数据保存类


class CodeCounter:
    def __init__(self):
        # {lang: { key, value }}
        # lang: js, py, html, ...
        # key: code, blank, comment, ...
        # value: number
        self._counter = {}

    def getCounter(self):
        return self._counter

    def setCounter(self, lang, key, value):
        if not self._counter.get(lang):
            self._counter[lang] = {key: value}
        else:
            if not self._counter[lang].get(key):
                self._counter[lang][key] = value
            else:
                self._counter[lang][key] += value

    def clear(self):
        self._counter = {}
