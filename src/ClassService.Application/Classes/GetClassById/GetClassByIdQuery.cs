using ClassService.Application.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.GetClassById
{
    public class GetClassByIdQuery : IRequest<GetClassByIdResponse>
    {
        public int SchoolYearId { get; set; }
        public int ClassId { get; set; }
    }
}
