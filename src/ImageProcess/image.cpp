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
        // �򵥵�ѹ���㷨��ÿ���ֽڼ���
        for (std::streamsize i = 0; i < inputFile.gcount(); ++i) {
            buffer[i] /= 2;
        }
        outputFile.write(buffer, inputFile.gcount());
    }

    // �������һ������
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