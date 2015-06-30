using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Configuration
{
    /// <summary>
    /// 爬虫模板配置切割字符
    /// </summary>
    public class TemplateSeparator
    {
        /// <summary>
        /// 文本替换分割符====> (如：str1==>str2 str1替换为str2),也用于正则替换
        /// </summary>
        public const string SplitStringForReplace = "====>";
        /// <summary>
        /// 文本替换分割符----> (如：str1---->str2 str1替换为str2),也用于正则替换
        /// </summary>
        public const string SplitStringForReplace_Child = "---->";
        /// <summary>
        /// 配置字符数组分割符=====,某配置项有一个配置组构成则以该符号分割(=====)
        /// </summary>
        public const string SplitStringForArray = "=====";
        /// <summary>
        /// 配置字符数组分割符-----,某配置项有一个配置组构成则以该符号分割(-----)
        /// </summary>
        public const string SplitStringForArray_Child = "-----";
        /// <summary>
        /// 字符Key value 对 ===
        /// </summary>
        public const string SplitStringForKeyValue = "===";
        /// <summary>
        /// 整数数组分隔符 | (如字段 DataTypeIDS 格式为 "|")
        /// </summary>
        public const char SplitCharForIDS = '|';
    }
}
