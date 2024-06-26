#pragma once

using namespace System;

namespace ImageCompressor {
    public ref class Compressor
    {
    public:
        static bool CompressImage(String^ inputPath, String^ outputPath);
    };
}