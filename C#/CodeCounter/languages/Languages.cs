using System;
using System.Collections.Generic;
using CodeCounter.handler;

namespace CodeCounter.languages
{
    public class Languages
    {
        private readonly List<Language> _languages;

        public Languages()
        {
            _languages = new List<Language>();

            // 初始化默认支持的语言
            Init();
        }

        /// <summary>
        /// 初始化语言信息
        /// </summary>
        /// <returns></returns>
        private void Init()
        {
            #region 手动生成语言列表（已弃用）

            // 生成语言列表
            //_languages.AddRange(DefaultLanguages());
            //_languages.Sort();

            #endregion

            _languages.Clear();
            _languages.AddRange(Config.Deserialize());
        }

        public void Add(Language lang)
        {
            _languages.Add(lang);

            // 排个序
            _languages.Sort();

            // 保存到配置文件
            Config.Serialize(_languages);
        }

        public List<Language> GetAll() =>
            _languages;

        public Language Get(string name) =>
            _languages.Find(x =>
                string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase) || x.Ext.Contains(name.ToLower()));

        public bool Remove(Language lang)
        {
            var b = _languages.Remove(lang);
            if (b)
            {
                // 删除成功后保存
                Config.Serialize(_languages);
            }

            return b;
        }

        public void Update(Language lang)
        {
            var index = _languages.FindIndex(x => string.Equals(x.Name, lang.Name, StringComparison.OrdinalIgnoreCase));
            if (index == -1)
            {
                Add(lang);
            }
            else
            {
                _languages[index].Update(lang);
                // 保存到配置文件
                Config.Serialize(_languages);
            }
        }

        public static List<Language> DefaultLanguages()
        {
            return new List<Language>
            {
                // C
                new Language()
                {
                    Name = "C",
                    Ext = new List<string> {"c"},
                    SingleLineComment = new List<string> {"//"},
                    MultiLineComment = new List<MultiLineComment>
                    {
                        new MultiLineComment {Start = "#if", End = "#endif"},
                        new MultiLineComment {Start = "/*", End = "*/"}
                    }
                },
                // CPP
                new Language()
                {
                    Name = "C++",
                    Ext = new List<string> {"cpp"},
                    SingleLineComment = new List<string> {"//"},
                    MultiLineComment = new List<MultiLineComment>
                    {
                        new MultiLineComment {Start = "#if", End = "#endif"},
                        new MultiLineComment {Start = "/*", End = "*/"}
                    }
                },
                // C#
                new Language()
                {
                    Name = "C#",
                    Ext = new List<string> {"cs"},
                    SingleLineComment = new List<string> {"//", "#"},
                    MultiLineComment = new List<MultiLineComment>
                    {
                        new MultiLineComment {Start = "#if", End = "#endif"},
                        new MultiLineComment {Start = "/*", End = "*/"}
                    }
                },
                // CSS
                new Language()
                {
                    Name = "CSS",
                    Ext = new List<string> {"css", "scss", "sass", "styl"},
                    SingleLineComment = new List<string> {"//"},
                    MultiLineComment = new List<MultiLineComment>
                    {
                        new MultiLineComment {Start = "/*", End = "*/"}
                    }
                },
                // Html
                new Language()
                {
                    Name = "Html",
                    Ext = new List<string> {"htm", "html"},
                    SingleLineComment = new List<string>(),
                    MultiLineComment = new List<MultiLineComment>
                    {
                        new MultiLineComment {Start = "<!--", End = "-->"}
                    }
                },
                // Java
                new Language()
                {
                    Name = "Java",
                    Ext = new List<string> {"java"},
                    SingleLineComment = new List<string> {"//"},
                    MultiLineComment = new List<MultiLineComment>
                    {
                        new MultiLineComment {Start = "/*", End = "*/"}
                    }
                },
                // JS
                new Language()
                {
                    Name = "JavaScript",
                    Ext = new List<string> {"js", "jsx"},
                    SingleLineComment = new List<string> {"//"},
                    MultiLineComment = new List<MultiLineComment>
                    {
                        new MultiLineComment {Start = "/*", End = "*/"}
                    }
                },
                // Python
                new Language()
                {
                    Name = "Python",
                    Ext = new List<string> {"py"},
                    SingleLineComment = new List<string> {"#"},
                    MultiLineComment = new List<MultiLineComment>
                    {
                        new MultiLineComment {Start = "\"\"\"", End = "\"\"\""},
                        new MultiLineComment {Start = "\'\'\'", End = "\'\'\'"}
                    }
                },
                // TS
                new Language()
                {
                    Name = "TypeScript",
                    Ext = new List<string> {"ts", "tsx"},
                    SingleLineComment = new List<string> {"//"},
                    MultiLineComment = new List<MultiLineComment>
                    {
                        new MultiLineComment {Start = "/*", End = "*/"}
                    }
                },
                // Vue
                new Language()
                {
                    Name = "Vue",
                    Ext = new List<string> {"vue"},
                    SingleLineComment = new List<string> {"//"},
                    MultiLineComment = new List<MultiLineComment>
                    {
                        new MultiLineComment {Start = "/*", End = "*/"},
                        new MultiLineComment {Start = "<!--", End = "-->"}
                    }
                },
            };
        }
    }
}
