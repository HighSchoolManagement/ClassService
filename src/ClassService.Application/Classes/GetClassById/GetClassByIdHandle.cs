using AutoMapper;
using ClassService.Application.Classes.Specifications;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;


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
            var spec = new ClassActiveBySchoolYearIdAndSchoolIdAndClassIdSpec(request.SchoolYearId, request.SchoolId,
                request.ClassId);
            var existingClass = await ClassValidator.GetOrThrowAsync(
                (() => classRepository.GetByIdAsync(spec, cancellationToken)),
                (() => new ClassNotFoundException()));
           
            return mapper.Map<GetClassByIdResponse>(existingClass);
            
        }
    }
}
