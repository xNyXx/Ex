using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace dotNetExam.Services
{
    public class HtmlParser : IHtmlParser
    {
        public IEnumerable<string> GetLinks(string link, string body, int maxCount)
        {
            var html = new HtmlDocument();
            html.LoadHtml(body);
            var links = html
                .DocumentNode
                .SelectNodes("//a[@href]")?
                .Select(node => node.Attributes["href"].Value);

            if (links == null)
                yield break;
            
            var count = 0;
            foreach (var newLink in links)
            {
                if (count == maxCount)
                    yield break;
                
                if (newLink.StartsWith("mailto"))
                    continue;

                if (newLink.StartsWith("http") && !IsLinkExternal(link, newLink))
                {
                    yield return newLink;
                }
                else
                {
                    yield return link + newLink;
                }

                count++;
            }
        }

        private static bool IsLinkExternal(string link, string newLink)
            => (new Uri(link)).Host != (new Uri(newLink)).Host;
    }
}