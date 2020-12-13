using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Core.Application.ViewModel
{
    public class InboxMessageViewModel
    {
        public int Id { get; set; }
        public string FromName { get; set; }
        public string FromImage { get; set; }
        public string ToName { get; set; }
        public string ToImage { get; set; }
        public string Content { get; set; }
        public string CreateDate { get; set; }
    }
}
