using Aliyun.OSS;
using Microsoft.Extensions.Options;
using MS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public class AliOssService : IAliOssService
    {
        private readonly AliOssOptions _ossOptions;

        // 构造函数，注入阿里云OSS配置
        public AliOssService(IOptions<AliOssOptions> ossOptions)
        {
            _ossOptions = ossOptions.Value;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string objectName)
        {
            var client = new OssClient(_ossOptions.Endpoint, _ossOptions.AccessKeyId, _ossOptions.AccessKeySecret);
            try
            {
                // 使用 Task.Run 来对同步方法进行异步封装
                var result = await Task.Run(() =>
                {
                    client.PutObject(_ossOptions.BucketName, objectName, fileStream);
                    return $"https://{_ossOptions.BucketName}.{_ossOptions.Endpoint}/{objectName}";
                });

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("文件上传失败", ex);
            }
        }
    }
}
