using System.IO;
using System.Linq;
using CodeCounter.languages;

namespace CodeCounter
{
    public class ReadResult
    {
        public int Lines { get; set; }
        public int Annotation { get; set; }
        public int Empty { get; set; }
        public int Code { get; set; }

        public void Add(ReadResult result)
        {
            Lines += result.Lines;
            Annotation += result.Annotation;
            Empty += result.Empty;
            Code += result.Code;
        }
    }

    /// <summary>
    /// 读取文件内容的类
    /// </summary>
    public class Read
    {
        private readonly string _path;
        private readonly Language _language;
        public Read(string path, Language language)
        {
            _path = path;
            _language = language;
        }

        public ReadResult ReadFileByLine()
        {
            var lines = File.ReadAllLines(_path);
            var res = new ReadResult { Lines = lines.Length };
            var comment = string.Empty;

            // 把所有行分类
            foreach (var line in lines)
            {
                var s = line.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(comment))
                {
                    var end = "";
                    // 空行
                    if (string.IsNullOrWhiteSpace(s)) res.Empty++;
                    // 单行注释
                    else if (_language.SingleLineComment.Any(cmt => s.StartsWith(cmt)))
                    {
                        res.Annotation++;
                    }
                    // 多行注释
                    else if (_language.MultiLineComment.Any(cmt =>
                    {
                        end = cmt.End;
                        return s.StartsWith(cmt.Start);
                    }))
                    {
                        res.Annotation++;

                        // 多行注释也可能是一行的，一行就不需要多行统计了
                        if (!s.Contains(end))
                        {
                            comment = end;
                        }
                    }
                    // 代码
                    else
                    {
                        res.Code++;
                    }
                }
                else
                {
                    // 多行注释中，空行单独计算
                    if (string.IsNullOrWhiteSpace(s))
                    {
                        res.Empty++;
                    }
                    else
                    {
                        // 多行注释
                        res.Annotation++;
                        if (s.Contains(comment)) comment = string.Empty;
                    }
                }
            }

            return res;
        }
    }
}
