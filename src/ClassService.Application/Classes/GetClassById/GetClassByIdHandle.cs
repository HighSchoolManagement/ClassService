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
            var existingClass = await ClassValidator.GetOrThrowAsync(
                (() => classRepository.GetByIdTrackedAsync(request.SchoolYearId, request.SchoolId, request.ClassId)),
                (() => new ClassNotFoundException()));
           
            return mapper.Map<GetClassByIdResponse>(existingClass);
            
        }
    }
}
