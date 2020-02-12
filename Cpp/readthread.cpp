#include "readthread.h"

ReadThread::ReadThread(std::string path, CodeCounter* counter, std::vector<QString> *extList, std::vector<QString>* ignoreList, QObject* parent)
{
	this->rootPath = path;
	this->counter = counter;
	this->extList = extList;
	this->ignoreList = ignoreList;
}

void ReadThread::run()
{
	// 保存所有查找到的文件路径
	std::vector<std::string> files;
	recPath_Win(rootPath, files);

	readFiles(files);
}

void ReadThread::recPath_Win(std::string path, std::vector<std::string>& files)
{
	//文件句柄
	/**
	* 这里有个坑，在win10，vs2019（看解决方案vs2017以上都需要，但是我的sv2017可以运行）中，
	* 句柄 hFile 必须是long long格式，或者intptr_t格式。否则就一直报
	* _findnext(hFile, &fileinfo) == 0 这一句中 ntdll.dll写入冲突 的错。
	* 如果是vs2015或者以下，并且不是win10，那么long格式就可以使用。
	*/
	long long hFile = 0;

	//文件信息  
	struct _finddata_t fileinfo;  //很少用的文件信息读取结构
	std::string p;  //string类很有意思的一个赋值函数:assign()，有很多重载版本
	if ((hFile = _findfirst(p.assign(path).append("\\*").c_str(), &fileinfo)) != -1)
	{
		do
		{
			if ((fileinfo.attrib & _A_SUBDIR))  //比较文件类型是否是文件夹
			{
				// 如果文件夹以'.'点开头，默认不读取，因为这是隐藏文件，很可能是系统文件
				if (std::regex_search(fileinfo.name, std::regex("^\\.")))
				{
					continue;
				}

				// 判断文件夹是否为需要忽略的文件夹，如果是，直接跳过
				std::string lastDir = "";
				size_t last_slash_idx = path.rfind('\\');
				if (last_slash_idx == std::string::npos)
				{
					// 目录如果'\\'找不到那么就是'/'，如果都找不到，则跳过判断
					last_slash_idx = path.rfind('/');
				}
				if (std::string::npos != last_slash_idx)
				{
					lastDir = path.substr(last_slash_idx + 1, path.size());
				}

				// 判断当前目录是否是需要忽略的文件夹
				bool con = false;
				for (std::vector<QString>::iterator it = ignoreList->begin(); it < ignoreList->end(); ++it)
				{
					if ((*it) == tr(lastDir.c_str()))
					{
						con = true;
						break;
					}
				}

				if (con)
				{
					// 当前文件夹是需要忽略的，直接下一次循环
					continue;
				}

				if (strcmp(fileinfo.name, ".") != 0 && strcmp(fileinfo.name, "..") != 0)
				{
					// 递归下一层文件夹
					recPath_Win(p.assign(path).append("/").append(fileinfo.name), files);
				}
			}
			else
			{
				// 如果文件在需要操作的列表中，将文件路径添加到files中
				QString _ext = tr(getExt(fileinfo.name).c_str());
				for (std::vector<QString>::iterator it = extList->begin(); it < extList->end(); ++it)
				{
					if ((*it) == _ext)
					{
						files.push_back(p.assign(path).append("/").append(fileinfo.name));
					}
				}
			}
		} while (_findnext(hFile, &fileinfo) == 0);  //寻找下一个，成功返回0，否则-1
		
		_findclose(hFile);
	}
}

void ReadThread::readFiles(std::vector<std::string>& files)
{
	// 循环读取文件
	for (int i = 0; i < files.size(); i++)
	{
		// 打印文件信息到log中
		emit updateLogTextSignal(tr(files[i].c_str()));

		// 打开文件
		std::fstream f(files[i]);
		std::vector<std::string> words;
		std::string line;

		// 读取文件的每一行，并保存到words中
		while (std::getline(f, line))
		{
			words.push_back(line);
		}

		// 处理每一行
		// 先获取文件扩展名
		std::string ext = getExt(files[i]);

		// 创建一个Read实例，并交给其操作
		Read read(ext, &words);

		counter->setCounter(tr(ext.c_str()), "file", 1);
		counter->setCounter(tr(ext.c_str()), "code", read.getCode());
		counter->setCounter(tr(ext.c_str()), "blank", read.getBlank());
		counter->setCounter(tr(ext.c_str()), "comment", read.getComment());
	}
}

std::string ReadThread::getExt(std::string path, bool eraseDot)
{
	char _drive[_MAX_DRIVE];
	char _dir[_MAX_DIR];
	char _fname[_MAX_FNAME];
	char _ext[_MAX_EXT];
	_splitpath(path.c_str(), _drive, _dir, _fname, _ext);
	std::string ext(_ext);

	if (eraseDot)
	{
		ext.erase(0, 1); // 删除获取扩展名的第一个点(.)
	}

	return ext;
}
