#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace CodeCounter.languages
{
    /// <summary>
    /// 多行注释类型
    /// </summary>
    public class MultiLineComment
    {
        /// <summary>
        /// 多行注释起始字符
        /// </summary>
        public string Start { get; set; } = "";

        /// <summary>
        /// 多行注释结束字符
        /// </summary>
        public string End { get; set; } = "";
    }

    /// <summary>
    /// 语言类
    /// </summary>
    public class Language : IComparable
    {
        /// <summary>
        /// 单行注释字符
        /// </summary>
        public List<string> SingleLineComment { get; set; } = new List<string>();

        /// <summary>
        /// 多行注释字符
        /// </summary>
        public List<MultiLineComment> MultiLineComment { get; set; } = new List<MultiLineComment>();

        /// <summary>
        /// 语言的名字
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 语言的扩展名
        /// </summary>
        public List<string> Ext { get; set; } = new List<string>();

        public void Update(Language lang)
        {
            Name = lang.Name;
            Ext = lang.Ext.FindAll(x => !string.IsNullOrWhiteSpace(x));
            SingleLineComment = lang.SingleLineComment.FindAll(x => !string.IsNullOrWhiteSpace(x));
            MultiLineComment = lang.MultiLineComment;
        }

        /// <summary>
        /// 自定义排序规则
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public int CompareTo(object? x)
        {
            if (x is Language language)
            {
                return string.Compare(Name, language.Name, StringComparison.Ordinal);
            }

            throw new ArgumentException($"arguments are not {GetType()} type.");
        }
    }
}
