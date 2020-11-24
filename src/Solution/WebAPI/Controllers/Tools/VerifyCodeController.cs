using Library.Tool;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.DrawingCore.Imaging;
using System.IO;

namespace WebAPI.Controllers.Tools
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class VerifyCodeController : ControllerBase
    {
        /// <summary>
        /// //把随机码存入Session
        /// </summary>
        /// <param name="code">随机代码</param>
        private void SetSession(string code)
        {
            HttpContext.Session.SetString("code", code.ToLower());
        }

        /// <summary>
        /// 数字验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("NumberVerifyCode")]
        public FileContentResult NumberVerifyCode()
        {
            string code = VerifyCodeTool.GetSingleObj().CreateVerifyCode(VerifyCodeTool.VerifyCodeType.NumberVerifyCode);
            byte[] codeImage = VerifyCodeTool.GetSingleObj().CreateByteByImgVerifyCode(code, 100, 40);
            SetSession(code);
            return File(codeImage, @"image/jpeg");
        }

        /// <summary>
        /// 字母验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("AbcVerifyCode")]
        public FileContentResult AbcVerifyCode()
        {
            string code = VerifyCodeTool.GetSingleObj().CreateVerifyCode(VerifyCodeTool.VerifyCodeType.AbcVerifyCode);
            var bitmap = VerifyCodeTool.GetSingleObj().CreateBitmapByImgVerifyCode(code, 100, 40);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Png);
            SetSession(code);
            return File(stream.ToArray(), "image/png");
        }

        /// <summary>
        /// 混合验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("MixVerifyCode")]
        public FileContentResult MixVerifyCode()
        {
            string code = VerifyCodeTool.GetSingleObj().CreateVerifyCode(VerifyCodeTool.VerifyCodeType.MixVerifyCode);
            var bitmap = VerifyCodeTool.GetSingleObj().CreateBitmapByImgVerifyCode(code, 100, 40);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Gif);
            SetSession(code);
            return File(stream.ToArray(), "image/gif");
        }
    }
}