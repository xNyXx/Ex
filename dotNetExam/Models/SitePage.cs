using System.Text.RegularExpressions;

namespace dotNetExam.Models
{
    public class SitePage
    {
        public SitePage(string url, string body)
        {
            Url = url;
            Body = Regex.Replace(body, "<[^>]+>", string.Empty);
        }
        
        public int Id { get; set; }
        public string Url { get; set; }
        public string Body { get; set; }
    }
}