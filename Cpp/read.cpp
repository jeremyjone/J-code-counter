#include "read.h"

void strip(std::string& str)
{
	std::string blanks("\f\v\r\t\n ");
	str.erase(0, str.find_first_not_of(blanks));
	str.erase(str.find_last_not_of(blanks) + 1);
}

Read::Read(std::string ext, std::vector<std::string>* lines)
{
	this->lines = lines;

	// 根据ext使用调用不同执行函数
	if (ext == "c" || ext == "cpp" || ext == "cs")
	{
		cpp();
	}
	else if (ext == "html")
	{
		html();
	}
	else if (ext == "java" || ext == "js")
	{
		java();
	}
	else if (ext == "py")
	{
		python();
	}
	else if (ext == "vue")
	{
		vue();
	}
}

int Read::getCode()
{
	return code;
}

int Read::getBlank()
{
	return blank;
}

int Read::getComment()
{
	return comment;
}

void Read::cpp()
{
	std::string cmt = "";

	for (int i = 0; i < (*lines).size(); i++)
	{
		std::string line = (*lines)[i];
		strip(line); // 去掉空白字符
		transform(line.begin(), line.end(), line.begin(), ::tolower); // 转为小写

		// 空行
		if (line == "")
		{
			blank++;
		}
		else
		{
			if (cmt == "")
			{
				if (std::regex_search(line, std::regex("^#if")))
				{
					comment++;
					cmt = "^#endif";
				}
				else if (std::regex_search(line, std::regex("^/\\*")))
				{
					comment++;
					// 单行判断
					if (!(std::regex_search(line, std::regex("\\*/$"))))
					{
						cmt = "\\*/$";
					}
				}
				else if (std::regex_search(line, std::regex("^/")))
				{
					comment++;
				}
				else
				{
					// 都不是，就是代码
					code++;
				}
			}
			else
			{
				comment++;

				if (std::regex_search(line, std::regex(cmt)))
				{
					cmt = "";
				}
			}
		}
	}
}

void Read::html()
{
	std::string cmt = "";

	for (int i = 0; i < (*lines).size(); i++)
	{
		std::string line = (*lines)[i];
		strip(line); // 去掉空白字符
		transform(line.begin(), line.end(), line.begin(), ::tolower); // 转为小写

		// 空行
		if (line == "")
		{
			blank++;
		}
		else
		{
			if (cmt == "")
			{
				if (std::regex_search(line, std::regex("^<!--")))
				{
					comment++;
					// 单行判断
					if (!(std::regex_search(line, std::regex("-->$"))))
					{
						cmt = "-->";
					}
				}
				else
				{
					// 都不是，就是代码
					code++;
				}
			}
			else
			{
				comment++;

				if (std::regex_search(line, std::regex(cmt)))
				{
					cmt = "";
				}
			}
		}
	}
}

void Read::java()
{
	std::string cmt = "";

	for (int i = 0; i < (*lines).size(); i++)
	{
		std::string line = (*lines)[i];
		strip(line); // 去掉空白字符
		transform(line.begin(), line.end(), line.begin(), ::tolower); // 转为小写

		// 空行
		if (line == "")
		{
			blank++;
		}
		else
		{
			if (cmt == "")
			{
				if (std::regex_search(line, std::regex("^/\\*")))
				{
					comment++;
					// 单行判断
					if (!(std::regex_search(line, std::regex("\\*/$"))))
					{
						cmt = "\\*/$";
					}
				}
				else if (std::regex_search(line, std::regex("^/")))
				{
					comment++;
				}
				else
				{
					// 都不是，就是代码
					code++;
				}
			}
			else
			{
				comment++;

				if (std::regex_search(line, std::regex(cmt)))
				{
					cmt = "";
				}
			}
		}
	}
}

void Read::python()
{
	std::string cmt = "";

	for (int i = 0; i < (*lines).size(); i++)
	{
		std::string line = (*lines)[i];
		strip(line); // 去掉空白字符
		transform(line.begin(), line.end(), line.begin(), ::tolower); // 转为小写

		// 空行
		if (line == "")
		{
			blank++;
		}
		else
		{
			if (cmt == "")
			{
				if (std::regex_search(line, std::regex("^#")))
				{
					comment++;
				}
				else if (std::regex_search(line, std::regex("^\"\"\"")))
				{
					comment++;
					// 单行判断
					if (!(std::regex_search(line, std::regex("\"\"\"$"))))
					{
						cmt = "\"\"\"$";
					}
				}
				else if (std::regex_search(line, std::regex("^\'\'\'")))
				{
					comment++;
					// 单行判断
					if (!(std::regex_search(line, std::regex("\'\'\'$"))))
					{
						cmt = "\'\'\'$";
					}
				}
				else
				{
					// 都不是，就是代码
					code++;
				}
			}
			else
			{
				comment++;

				if (std::regex_search(line, std::regex(cmt)))
				{
					cmt = "";
				}
			}
		}
	}
}

void Read::vue()
{
	std::string cmt = "";

	for (int i = 0; i < (*lines).size(); i++)
	{
		std::string line = (*lines)[i];
		strip(line); // 去掉空白字符
		transform(line.begin(), line.end(), line.begin(), ::tolower); // 转为小写

		// 空行
		if (line == "")
		{
			blank++;
		}
		else
		{
			if (cmt == "")
			{
				if (std::regex_search(line, std::regex("^<!--")))
				{
					comment++;
					// 单行判断
					if (!(std::regex_search(line, std::regex("-->$"))))
					{
						cmt = "-->";
					}
				}
				else if (std::regex_search(line, std::regex("^/\\*")))
				{
					comment++;
					// 单行判断
					if (!(std::regex_search(line, std::regex("\\*/$"))))
					{
						cmt = "\\*/$";
					}
				}
				else if (std::regex_search(line, std::regex("^/")))
				{
					comment++;
				}
				else
				{
					// 都不是，就是代码
					code++;
				}
			}
			else
			{
				comment++;

				if (std::regex_search(line, std::regex(cmt)))
				{
					cmt = "";
				}
			}
		}
	}
}
