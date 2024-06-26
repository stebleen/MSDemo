#include <iostream>
#include <fstream>
#include <vector>

extern "C" __declspec(dllexport) bool CompressImage(const char* inputPath, const char* outputPath)
{
    std::ifstream inputFile(inputPath, std::ios::binary);
    if (!inputFile.is_open()) {
        std::cerr << "Error opening input file" << std::endl;
        return false;
    }

    std::ofstream outputFile(outputPath, std::ios::binary);
    if (!outputFile.is_open()) {
        std::cerr << "Error opening output file" << std::endl;
        inputFile.close();
        return false;
    }

    // ��ȡ�����ļ�����������
    std::vector<unsigned char> pixels;
    inputFile.seekg(0, std::ios::end);
    size_t fileSize = inputFile.tellg();
    pixels.resize(fileSize);
    inputFile.seekg(0, std::ios::beg);
    inputFile.read(reinterpret_cast<char*>(&pixels[0]), fileSize);
    inputFile.close();

    // ѹ���㷨���򵥵ؽ�ÿ������ֵ��Сһ��
    const int compressionFactor = 10; // ѹ�����ӣ����Ը�����Ҫ����
    for (size_t i = 0; i < pixels.size(); ++i) {
        pixels[i] -= compressionFactor;
    }

    // д��ѹ������������ݵ�����ļ�
    outputFile.write(reinterpret_cast<const char*>(&pixels[0]), fileSize);
    outputFile.close();

    return true;
}