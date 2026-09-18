using ClassService.Application.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.UpdateClass
{
    public class UpdateClassCommand: IRequest<Unit>
    {
        public int SchoolYearId { get; set; }
        public int ClassId { get; set; }
        public UpdateClassRequest UpdateClassRequest { get; set; } = new UpdateClassRequest();
    }
}
