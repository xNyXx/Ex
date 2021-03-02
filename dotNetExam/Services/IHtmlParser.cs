using System.Collections.Generic;

namespace dotNetExam.Services
{
    public interface IHtmlParser
    {
        public IEnumerable<string> GetLinks(string link, string body, int maxCount);
    }
}