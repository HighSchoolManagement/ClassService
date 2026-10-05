using ClassService.Application.Classes.GetClassById;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;

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
            var classExisting = await ClassValidator.GetOrThrowAsync(
                () => _classRepository.GetByIdTrackedAsync(request.SchoolYearId, request.SchoolId, request.ClassId),
                () => new ClassNotFoundException());

            classExisting.IsActive = false;
            await _classRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}