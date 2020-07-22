using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreExtention
{
    public static class SharedHelpers
    {
        /// <summary>
        /// 验证邮件
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmail(string source)
        {
            return Regex.IsMatch(source, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", RegexOptions.IgnoreCase);
        }

        public static string EncodeBase64(this string source, Encoding encode)
        {
            byte[] bytes = encode.GetBytes(source);
            string re ;
            try
            {
                re = Convert.ToBase64String(bytes);
            }
            catch
            {
                re = source;
            }
            return re;
        }

        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string EncodeBase64(this string source)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(source);
            try
            {
                decode = Encoding.UTF8.GetString(bytes);
            }
            catch
            {

            }
            return decode;
        }
    }
}
