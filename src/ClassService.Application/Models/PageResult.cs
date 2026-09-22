using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassService.Application.Models
{
    public class PageResult<T>
    {
        public List<T> Items = new List<T>();
        public int TotalCount { get; set; }
        public int TotalPage => (int)Math.Ceiling(TotalPage / (double)PageSize);
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int? AsOfId { get; set; }
    }
}
