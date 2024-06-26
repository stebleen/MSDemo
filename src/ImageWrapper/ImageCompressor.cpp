#include "ImageCompressor.h"
#include <msclr/marshal_cppstd.h>

// 使用预处理器指令排除特定头文件
#ifndef __cplusplus_cli
#include <servprov.h>
#include <urlmon.h>
#endif


// 使用 #pragma unmanaged 指令来包含非托管头文件
#pragma unmanaged
#include <iostream>
#include <fstream>

//bool CompressImage(const char* inputPath, const char* outputPath); // 声明C++函数
extern "C" __declspec(dllimport) bool CompressImage(const char* inputPath, const char* outputPath);

// 恢复托管模式
#pragma managed

bool ImageCompressor::Compressor::CompressImage(String^ inputPath, String^ outputPath)
{
    std::string input = msclr::interop::marshal_as<std::string>(inputPath);
    std::string output = msclr::interop::marshal_as<std::string>(outputPath);

    return ::CompressImage(input.c_str(), output.c_str());
}

