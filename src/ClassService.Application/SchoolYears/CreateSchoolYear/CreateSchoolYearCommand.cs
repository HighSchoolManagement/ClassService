using ClassService.Application.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.SchoolYears.CreateSchoolYear
{
    public class CreateSchoolYearCommand : IRequest<CreateSchoolYearResponse>
    {
        public CreateSchoolYearRequest CreateSchoolYearRequest { get; set; } = new CreateSchoolYearRequest();
    }
}
