using AutoMapper;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.GetClassById
{
    public class GetClassByIdHandle : IRequestHandler<GetClassByIdQuery, GetClassByIdResponse>
    {
        private readonly IClassRepository classRepository;
        private readonly IMapper mapper;
        public GetClassByIdHandle(IClassRepository classRepository, IMapper mapper)
        {
            this.classRepository = classRepository;
            this.mapper = mapper;
        }
        public async Task<GetClassByIdResponse> Handle(GetClassByIdQuery request, CancellationToken cancellationToken = default)
        {
            if(request.ClassId <= 0 || request.SchoolYearId <=0 )
            {
                throw new ArgumentException("ClassId or SchoolYearId must be greater than 0");
            }
            var existingClass = classRepository.GetByIdAsync(request.SchoolYearId, request.ClassId);
            if (existingClass == null)
            {
                throw new ClassNotFoundException();
            }
            return mapper.Map<GetClassByIdResponse>(existingClass);
            
        }
    }
}
