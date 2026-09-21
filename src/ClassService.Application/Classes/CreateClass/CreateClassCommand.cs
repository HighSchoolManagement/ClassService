using ClassService.Application.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.CreateClass
{
    public class CreateClassCommand :IRequest<CreateClassResponse>
    {
        public CreateClassRequest CreateClassRequest { get; set; } = new CreateClassRequest();
    }
}
