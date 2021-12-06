using System.Collections.Generic;

namespace CodeCounter.handler
{
    /// <summary>
    /// 统计结果
    /// </summary>
    public class CountResult
    {
        /// <summary>
        /// 语言名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 统计结果
        /// </summary>
        public ReadResult Result { get; set; }

        /// <summary>
        /// 文件数
        /// </summary>
        public int Files { get; set; }
    }

    /// <summary>
    /// 统计类
    /// </summary>
    public class Counter
    {
        public List<CountResult> Results { get; set; } = new List<CountResult>();

        /// <summary>
        /// 添加结果
        /// </summary>
        /// <param name="name">语言名</param>
        /// <param name="result">结果集</param>
        public void AddResult(string name, ReadResult result)
        {
            var res = Results.Find(x => x.Name == name);
            if (res == null)
            {
                Results.Add(new CountResult
                {
                    Name = name,
                    Result = result,
                    Files = 1
                });
            }
            else
            {
                res.Result.Add(result);
                res.Files++;
            }
        }

        public void Clear()
        {
            Results.Clear();
        }
    }
}
