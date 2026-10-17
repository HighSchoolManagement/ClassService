using AutoMapper;
using ClassService.Application.Classes.GetClassById;
using ClassService.Application.Classes.Specifications;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;


namespace ClassService.Application.Classes.UpdateClass
{
    public class UpdateClassHandle : IRequestHandler<UpdateClassCommand, UpdateClassResponse>
    {
        private readonly IClassRepository classRepository;
        private readonly IMapper _mapper;
        public UpdateClassHandle(IClassRepository classRepository, IMapper mapper)
        {
            this.classRepository = classRepository;
            _mapper = mapper;
        }
        public async Task<UpdateClassResponse> Handle(UpdateClassCommand request, CancellationToken cancellationToken = default)
        {
            if (request?.UpdateClassRequest == null)
                throw new ArgumentException("Request is null");
            if (request.RowVersion == null)
            {
                throw new ArgumentException("RowVersion is null");
            }
            var req = request.UpdateClassRequest;
            var name = ClassValidator.NormalizeAndValidateName(req.Name);
            if (req.Capacity <= 0)
            {
                throw new ArgumentException("Capacity must be greater than 0"); 
            }
            var spec = new ClassActiveBySchoolYearIdAndSchoolIdAndClassIdSpec(request.SchoolYearId, request.SchoolId,
                request.ClassId);
            var classExisting = await ClassValidator.GetOrThrowAsync(
                () => classRepository.GetByIdTrackedAsync(spec, cancellationToken),
                () => new ClassNotFoundException());

            if (!request.RowVersion.SequenceEqual(classExisting.RowVersion))
            {
                throw new RowVersionConflitException();
            }

          
            if (name == classExisting.Name && req.Capacity == classExisting.Capacity)
                throw new ArgumentException("Nothing to update");
      

            classExisting.Name = name;
            classExisting.Capacity = req.Capacity;
            classExisting.ModifiedDate = DateTime.UtcNow;  

            await classRepository.SaveChangesAsync(cancellationToken);
            return new UpdateClassResponse()
            {
                ClassId = classExisting.Id,
                Capacity = classExisting.Capacity,
                ModifiedDate = classExisting.ModifiedDate,
                Name = classExisting.Name,
                RowVersion = classExisting.RowVersion,
                SchoolYearId = classExisting.SchoolYearId,
                SchoolId = classExisting.SchoolId
            };
        }
    }
}
