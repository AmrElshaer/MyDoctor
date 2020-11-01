using PagedList.Core.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Areas.Admin.Models
{
    public class SitePagedListRenderOptions
    {
        public static PagedListRenderOptions Boostrap4
        {
            get
            {
                var option = PagedListRenderOptions.Classic;
                option.MaximumPageNumbersToDisplay = 5;

                return option;
            }
        }
    }
}
