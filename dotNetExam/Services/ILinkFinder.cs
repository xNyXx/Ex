using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotNetExam.Models;

namespace dotNetExam.Services
{
    public interface ILinkFinder
    {
        public Task<ConcurrentQueue<SitePage>> GetSitePages(string link, int lastLevel, int maxCount);
    }
}