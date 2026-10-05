namespace ClassService.Application.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<int> GetStudentEnrollmentTotal(int classId);
    }
}