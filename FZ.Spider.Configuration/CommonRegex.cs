using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Configuration
{
    /// <summary>
    /// 通用正则表达式
    /// </summary>
    public class CommonRegex
    {
        /// <summary>
        /// 提取URL正则表达式
        /// </summary>
        public const string CommonUrlReg = @"<a[^>]*?href=(""(?<Href>[^""]*?)""|'(?<Href>[^']*?)'|(?<Href>[^>\s]*))[^>]*?>(?<Title>[\s\S]*?)</a>";
        /// <summary>
        /// 提取Email正则表达式
        /// </summary>
        public const string CommonEmailReg = @"(?<Email>\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";
    }
}
