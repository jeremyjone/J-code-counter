#pragma once
#include <iostream>
#include <vector>
#include <regex>
#include <algorithm>

class Read
{
public:
	Read(std::string ext, std::vector<std::string>* lines);
	
	int getCode();
	int getBlank();
	int getComment();

private:
	std::vector<std::string>* lines = nullptr;
	int code = 0;
	int blank = 0;
	int comment = 0;

	void cpp();
	void html();
	void java();
	void python();
	void vue();
};

