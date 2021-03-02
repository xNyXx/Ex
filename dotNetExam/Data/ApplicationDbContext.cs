using System;
using System.Collections.Generic;
using System.Text;
using dotNetExam.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotNetExam.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<SitePage> SitePages { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}