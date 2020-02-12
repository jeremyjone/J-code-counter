#include "lang.h"

LangEnum::LangEnum()
{
	LangEnum::langMap["C"] = "c";
	LangEnum::langMap["Cpp"] = "cpp";
	LangEnum::langMap["CSharp"] = "cs";
	LangEnum::langMap["HTML"] = "html";
	LangEnum::langMap["Java"] = "java";
	LangEnum::langMap["JavaScript"] = "js";
	LangEnum::langMap["Python"] = "py";
	LangEnum::langMap["Vue"] = "vue";
}

LangEnum::~LangEnum()
{

}

std::string LangEnum::getLang(std::string ext)
{
	std::map<std::string, std::string>::iterator it;

	for (it = langMap.begin(); it != langMap.end(); ++it)
	{
		if (it->second == ext)
		{
			return it->first;
		}
	}

	return "";
}

std::vector<std::string> LangEnum::supportLang()
{
	std::vector<std::string> res;
	// 迭代获取key值的list
    for(std::map<std::string, std::string>::iterator it=langMap.begin(); it != langMap.end(); ++it)
    {
        res.push_back(it->first);
    }
    return res;
}

std::string LangEnum::getExtByLang(std::string value)
{
	std::map<std::string, std::string>::iterator it;
	it = langMap.find(value);

	if (it == langMap.end())
	{
		return "";
	}
	else
	{
		return it->second;
	}
}

