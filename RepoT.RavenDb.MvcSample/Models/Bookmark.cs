using System;
using System.Collections.Generic;

namespace RepoT.RavenDb.MvcSample.Models
{
    public class Bookmark
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public DateTime DateCreated { get; set; }

        public Bookmark()
        {
            Tags = new List<string>();
        }
    }
}