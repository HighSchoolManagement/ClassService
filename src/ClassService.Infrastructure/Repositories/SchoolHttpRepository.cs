using ClassService.Application.Interfaces;
using ClassService.Application.Models;
using Refit;
using SchoolService.Contracts.Schools;
using System.Net;

namespace ClassService.Infrastructure.Repositories
{
    public class SchoolHttpRepository : ISchoolRepository
    {
        private readonly ISchoolsApi _schoolsApi;

        public SchoolHttpRepository(ISchoolsApi schoolsApi)
        {
            _schoolsApi = schoolsApi;
        }

        public async Task<SchoolReadModel?> GetSchoolReadModelByIdAsync(int id)
        {
            var response = await _schoolsApi.GetSchoolByIdAsync(id);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            // ApiResponse<T> không tự throw khi lỗi. Nếu SchoolService trả lỗi khác 404
            // (500, timeout...) thì throw ApiException để không bị nhầm với "School không tồn tại".
            await response.EnsureSuccessfulAsync();

            var school = response.Content!;
            return new SchoolReadModel
            {
                Id = school.Id,
                Name = school.Name,
                IsActive = school.IsActive
            };
        }
    }
}
