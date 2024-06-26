#include "ImageCompressor.h"
#include <msclr/marshal_cppstd.h>

// ʹ��Ԥ������ָ���ų��ض�ͷ�ļ�
#ifndef __cplusplus_cli
#include <servprov.h>
#include <urlmon.h>
#endif


// ʹ�� #pragma unmanaged ָ�����������й�ͷ�ļ�
#pragma unmanaged
#include <iostream>
#include <fstream>

//bool CompressImage(const char* inputPath, const char* outputPath); // ����C++����
extern "C" __declspec(dllimport) bool CompressImage(const char* inputPath, const char* outputPath);

// �ָ��й�ģʽ
#pragma managed

bool ImageCompressor::Compressor::CompressImage(String^ inputPath, String^ outputPath)
{
    std::string input = msclr::interop::marshal_as<std::string>(inputPath);
    std::string output = msclr::interop::marshal_as<std::string>(outputPath);

    return ::CompressImage(input.c_str(), output.c_str());
}

