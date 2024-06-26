using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.Entities;
using MS.Entities.admin;
using MS.Services;
using Renci.SshNet.Compression;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Ubiety.Dns.Core;



namespace MS.WebApi.Controllers.admin
{
    [Route("admin/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IAliOssService _aliOssService;

        public CommonController(IAliOssService aliOssService)
        {
            _aliOssService = aliOssService;
        }

        [DllImport("D:\\program\\dotnet\\code\\final\\MSDemo\\src\\ImageProcess\\ImageProcess.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CompressImage(string inputPath, string outputPath);


        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest(new { code = false, msg = "上传文件不能为空" });
            }

            var objectName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            using (var stream = file.OpenReadStream())
            {
                var fileUrl = await _aliOssService.UploadFileAsync(stream, objectName);
                return Ok(new { code = true, data = fileUrl, msg = "文件上传成功" });
            }

            /*
            // 保存上传的图片到项目目录
            var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            var compressedDirectory = Path.Combine(Directory.GetCurrentDirectory(), "compressed");

            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            if (!Directory.Exists(compressedDirectory))
            {
                Directory.CreateDirectory(compressedDirectory);
            }

            var inputPath = Path.Combine(uploadsDirectory, objectName);
            var outputPath = Path.Combine(compressedDirectory, objectName);

            try
            {

                using (var stream = file.OpenReadStream())
                {
                    using (var fileStream = new FileStream(inputPath, FileMode.Create))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    if (CompressImage(inputPath, outputPath))
                    {
                        // 上传压缩后的文件到OSS或其他存储服务
                        var compressedStream = new FileStream(outputPath, FileMode.Open);
                        var fileUrl = await _aliOssService.UploadFileAsync(compressedStream, "compressed_" + objectName);

                        // 删除原始图片
                        if (System.IO.File.Exists(inputPath))
                        {
                            System.IO.File.Delete(inputPath);
                        }
                        return Ok(new { code = true, data = fileUrl, msg = "文件上传成功" });
                    }
                    else
                    {
                        return BadRequest(new { code = false, msg = "图片压缩失败" });
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                using (var stream = file.OpenReadStream())
                {
                    var fileUrl = await _aliOssService.UploadFileAsync(stream, objectName);
                    return Ok(new { code = true, data = fileUrl, msg = "文件上传成功" });
                }
            }
            */

                    

                   
        }

        /*
         * using ImageCompressorCLI; // 引用C++/CLI封装的命名空间

            [HttpPost("upload")]
            public async Task<IActionResult> UploadFile(IFormFile file)
            {
                if (file == null || file.Length <= 0)
                {
                    return BadRequest(new { code = false, msg = "上传文件不能为空" });
                }

                var objectName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var tempFilePath = Path.Combine(Path.GetTempPath(), objectName);
                var compressedFilePath = Path.Combine(Path.GetTempPath(), "compressed_" + objectName);

                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // 调用C++/CLI封装的压缩函数
                bool success = Compressor.CompressImage(tempFilePath, compressedFilePath);
                if (!success)
                {
                    return BadRequest(new { code = false, msg = "图片压缩失败" });
                }

                using (var stream = new FileStream(compressedFilePath, FileMode.Open))
                {
                    var fileUrl = await _aliOssService.UploadFileAsync(stream, objectName);
                    return Ok(new { code = true, data = fileUrl, msg = "文件上传成功" });
                }
            }
         */

        
    }
}
