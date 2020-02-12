#pragma once
#include <iostream>
#include <map>
#include <vector>
#include <string>

class LangEnum
{
private:
	// {Language, Ext}
	std::map<std::string, std::string> langMap;

public:
	LangEnum();
	~LangEnum();

	// 根据扩展名获取语言名称
	std::string getLang(std::string ext);
	// 获取所有支持的语言的列表
	std::vector<std::string> supportLang();
	// 根据语言查找扩展名
	std::string getExtByLang(std::string value);
};

