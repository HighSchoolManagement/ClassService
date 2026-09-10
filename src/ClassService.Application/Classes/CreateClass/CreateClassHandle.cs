using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.CreateClass
{
    public class CreateClassHandle : IRequestHandler<CreateClassCommand, CreateClassResponse>
    {
        private readonly IClassRepository repository;
        public CreateClassHandle (IClassRepository repository)
        {
            this.repository = repository;
        }
        public Task<CreateClassResponse> Handle(CreateClassCommand request, CancellationToken cancellationToken = default)
        {
            var existingSchool = 
            throw new NotImplementedException();
        }
    }
}
