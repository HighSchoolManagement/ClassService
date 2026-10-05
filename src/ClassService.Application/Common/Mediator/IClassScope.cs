namespace ClassService.Application.Common.Mediator
{
    public interface IClassScope: ISchoolScope
    {
        public int ClassId { get; set; }
    }
}