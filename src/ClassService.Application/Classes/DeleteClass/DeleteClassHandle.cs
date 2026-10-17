using ClassService.Application.Classes.GetClassById;
using ClassService.Application.Classes.Specifications;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;
using System.Linq;


namespace ClassService.Application.Classes.DeleteClass
{
    public class DeleteClassHandle : IRequestHandler<DeleteClassCommand, Unit>
    {
        private readonly IClassRepository _classRepository;

        public DeleteClassHandle(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }
        public async Task<Unit> Handle(DeleteClassCommand request, CancellationToken cancellationToken = default)
        {
            if (request.RowVersion == null)
            {
                throw new ArgumentException("RowVersion is not valid");
            }

            var spec = new ClassActiveBySchoolYearIdAndSchoolIdAndClassIdSpec(request.SchoolYearId, request.SchoolId,
                request.ClassId);
            var classExisting = await ClassValidator.GetOrThrowAsync(
                () => _classRepository.GetByIdTrackedAsync(spec, cancellationToken),
                () => new ClassNotFoundException());
            if (!request.RowVersion.SequenceEqual(classExisting.RowVersion))
            {
                throw new RowVersionConflitException();
            }
            
            classExisting.IsActive = false;
            await _classRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}