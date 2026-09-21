using ClassService.Application.Interfaces;
using ClassService.Application.Models;
using System;
using System.Net;
using System.Net.Http.Json;

namespace ClassService.Infrastructure.Repositories
{
    // Không dùng ClassDbContext vì ClassService không sở hữu bảng School.
    // Implementation này gọi HTTP sang SchoolService để lấy dữ liệu.
    public class SchoolHttpRepository : ISchoolRepository
    {
        private readonly HttpClient _httpClient;

        public SchoolHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SchoolReadModel?> GetSchoolReadModelByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/schools/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            // Nếu SchoolService trả lỗi khác 404 (500, timeout...) thì throw ra ngoài
            // để không bị nhầm lẫn với trường hợp "School không tồn tại".
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<SchoolReadModel>();
        }
    }
}
