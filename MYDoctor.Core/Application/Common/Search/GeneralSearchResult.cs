using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Core.Application.Common.Search
{
    public class GeneralSearchResult
    {
       
        public GeneralSearchResult(int id,string controller,string action,string title,string image,string content)
        {
            Id = id;
            Controller = controller;
            Action = action;
            Title = title;
            Image = image;
            Content = content;
        }
        public int Id { get; private set; }
        public string Controller { get; private set; }
        public string Action { get;private set; }
        public string Title { get;private set; }
        public string Image { get;private set; }
        public string Content { get;private set; }
    }
}
