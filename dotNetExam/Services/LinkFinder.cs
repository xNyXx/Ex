using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using dotNetExam.Data;
using dotNetExam.Models;

namespace dotNetExam.Services
{
    public class LinkFinder : ILinkFinder
    {
        private readonly HashSet<string> processedLinks;
        private readonly HttpClient client;
        private readonly ConcurrentQueue<SitePage> sitePages;
        
        private readonly ApplicationDbContext db;
        private readonly IHtmlParser htmlParser;

        public LinkFinder(ApplicationDbContext db, IHtmlParser htmlParser)
        {
            this.db = db;
            this.htmlParser = htmlParser;
            
            processedLinks = new HashSet<string>();
            sitePages = new ConcurrentQueue<SitePage>();
            client = new HttpClient();
        }
        
        public async Task<ConcurrentQueue<SitePage>> GetSitePages(string link, int lastLevel, int maxCount)
        {
            await Recursive(link, lastLevel, 1, maxCount);
            await db.SaveChangesAsync();
            return sitePages;
        }

        private async Task Recursive(string link, int lastLevel, int currentLevel, int maxCount)
        {
            if (processedLinks.Contains(link))
                return;
            processedLinks.Add(link);
            
            string body;
            try
            {
                body = await client.GetStringAsync(link);
            }
            catch
            {
                return;
            }

            var newSitePage = new SitePage(link, body);
            sitePages.Enqueue(newSitePage);
            var adding = db.SitePages.AddAsync(newSitePage);

            if (currentLevel == lastLevel)
                return;
            
            var newLinks = htmlParser.GetLinks(link, body, maxCount);
            var tasks = new List<Task>(newLinks
                .Select(newLink => Recursive(newLink, lastLevel, currentLevel + 1, maxCount)));

            await Task.WhenAll(tasks);
            await adding;
        }
    }
}