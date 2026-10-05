using ClassService.Application.Common.Mediator;

namespace ClassService.Application.Classes.DeleteClass
{
    public class DeleteClassCommand : IRequest<Unit>, IClassScope
    {
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int SchoolYearId { get; set; }
    }
}