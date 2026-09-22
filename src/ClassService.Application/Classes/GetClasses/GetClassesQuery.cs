using ClassService.Application.Common.Mediator;
using ClassService.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.GetClasses
{
    public class GetClassesQuery: IRequest<PageResult<GetClassesResponse>>
    {
        public int? AsOfId { get; set; }
        public int SchoolYearId { get; set; }
        public int PageSize { get; set; } = 1;
        public int PageNumber { get; set; } = 10;

    }
}
