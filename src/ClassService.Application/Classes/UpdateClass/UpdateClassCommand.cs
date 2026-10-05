using ClassService.Application.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.UpdateClass
{
    public class UpdateClassCommand: IRequest<Unit>,  IClassScope
    {
        public int SchoolYearId { get; set; }
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public UpdateClassRequest UpdateClassRequest { get; set; } = new UpdateClassRequest();
    }
}
