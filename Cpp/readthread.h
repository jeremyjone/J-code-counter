#pragma once
#include <QThread>
#include <vector>
#include <iostream>
#include <io.h>
#include <fstream>
#include <string>
#include <sstream>
#include <regex>
#include "read.h"
#include "counter.h"

#define MAX_PATH = 1024

class ReadThread : public QThread
{
	Q_OBJECT

public:
	ReadThread(std::string path, CodeCounter *counter, std::vector<QString> *extList, std::vector<QString> *ignoreList, QObject* parent = Q_NULLPTR);

signals:
	void updateLogTextSignal(const QString);
	void updateResTableSignal();

protected:
	void run();

private:
	void recPath_Win(std::string path, std::vector<std::string>& files);
	void readFiles(std::vector<std::string>& files);
	std::string getExt(std::string path, bool eraseDot = true);

	std::string rootPath = "";
	CodeCounter* counter;
	std::vector<QString> *extList;
	std::vector<QString> *ignoreList;
};

