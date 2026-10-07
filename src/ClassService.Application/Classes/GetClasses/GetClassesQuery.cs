using ClassService.Application.Common.Mediator;
using ClassService.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.GetClasses
{
    public class GetClassesQuery: IRequest<PageResult<GetClassesResponse>>, ISchoolScope
    {
        public int SchoolYearId { get; set; }
        public int SchoolId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; } 

    }
}
