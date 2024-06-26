using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.Entities;
using MS.Entities.admin;
using MS.Services;
using Renci.SshNet.Compression;
using System;
using System.IO;
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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest(new { code = false, msg = "上传文件不能为空" });
            }

            var objectName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            /*
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
            */

            /*

            try
            {
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

            }
            catch (Exception ex)
            {
                using (var stream = file.OpenReadStream())
                {
                    var fileUrl = await _aliOssService.UploadFileAsync(stream, objectName);
                    return Ok(new { code = true, data = fileUrl, msg = "文件上传成功" });
                }
            }
            */


            using (var stream = file.OpenReadStream())
            {
                var fileUrl = await _aliOssService.UploadFileAsync(stream, objectName);
                return Ok(new { code = true, data = fileUrl, msg = "文件上传成功" });
            }
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
