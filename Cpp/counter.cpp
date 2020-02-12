#include "counter.h"

CodeCounter::CodeCounter()
{

}

CodeCounter::~CodeCounter()
{

}

std::map<QString, std::map<QString, int>> CodeCounter::getCounter()
{
	return counter;
}

void CodeCounter::setCounter(QString lang, QString key, int value)
{
	if (counter.find(lang) == counter.end())
	{
		std::map<QString, int> r;
		r[key] = value;
		counter[lang] = r;
	}
	else
	{
		if (counter[lang].find(key) == counter[lang].end())
		{
			counter[lang][key] = value;
		}
		else
		{
			counter[lang][key] += value;
		}
	}
}

void CodeCounter::clear()
{
	counter.clear();
}
