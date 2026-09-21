using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Models
{
    // Không map từ DbContext local (ClassService không có bảng School).
    // Model này chỉ đại diện cho dữ liệu School mà ClassService cần,
    // được deserialize từ response HTTP của SchoolService.
    public class SchoolReadModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
