using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.Entities;
using MS.Entities.admin;
using MS.Services;
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
                return BadRequest(new { code=false,msg="上传文件不能为空" });
            }

            var objectName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                var fileUrl = await _aliOssService.UploadFileAsync(stream, objectName);
                return Ok(new { code = true, data = fileUrl, msg = "文件上传成功" });
            }
        }
    }
}
