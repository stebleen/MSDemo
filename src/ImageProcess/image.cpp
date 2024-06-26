#include <iostream>
#include <fstream>

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

    char buffer[1024];
    while (inputFile.read(buffer, sizeof(buffer))) {
        // 简单的压缩算法：每个字节减半
        for (std::streamsize i = 0; i < inputFile.gcount(); ++i) {
            buffer[i] /= 2;
        }
        outputFile.write(buffer, inputFile.gcount());
    }

    // 处理最后一块数据
    if (inputFile.gcount() > 0) {
        for (std::streamsize i = 0; i < inputFile.gcount(); ++i) {
            buffer[i] /= 2;
        }
        outputFile.write(buffer, inputFile.gcount());
    }

    inputFile.close();
    outputFile.close();

    return true;
}