using ClassService.Application.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.CreateClass
{
    public class CreateClassCommand : IRequest<CreateClassResponse>, ISchoolScope
    {
        public int SchoolYearId { get; set; }
        public int SchoolId { get; set; }
        public CreateClassRequest CreateClassRequest { get; set; } = new CreateClassRequest();
    }
}
