#pragma once
#include <iostream>
#include <map>
#include <QString>

class CodeCounter
{
private:
	std::map<QString, std::map<QString, int>> counter;

public:
	CodeCounter();
	~CodeCounter();

	// 获取counter
	std::map<QString, std::map<QString, int>> getCounter();
	// 添加counter
	void setCounter(QString lang, QString key, int value);
	// 清空counter
	void clear();
};

