using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.GetClassById
{
    public class GetClassByIdResponse
    {
        public int ClassId { get; set; }
        public int SchoolId { get; set; }
        public int SchoolYearId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
