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

    // 读取输入文件的像素数据
    std::vector<unsigned char> pixels;
    inputFile.seekg(0, std::ios::end);
    size_t fileSize = inputFile.tellg();
    pixels.resize(fileSize);
    inputFile.seekg(0, std::ios::beg);
    inputFile.read(reinterpret_cast<char*>(&pixels[0]), fileSize);
    inputFile.close();

    // 压缩算法：简单地将每个像素值减小一点
    const int compressionFactor = 10; // 压缩因子，可以根据需要调整
    for (size_t i = 0; i < pixels.size(); ++i) {
        pixels[i] -= compressionFactor;
    }

    // 写入压缩后的像素数据到输出文件
    outputFile.write(reinterpret_cast<const char*>(&pixels[0]), fileSize);
    outputFile.close();

    return true;
}