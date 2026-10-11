using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.UpdateClass
{
    public class UpdateClassRequest
    {
        public string? Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
}
