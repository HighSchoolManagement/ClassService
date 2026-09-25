using ClassService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Models
{
    public class RoomScheduleReadModel
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int PeriodId { get; set; }
        public BookingType Type { get; set; }      
        public string? Title { get; set; }
        public int? ClassId { get; set; }
        public string? ClassName { get; set; }
        public int? StudentCount { get; set; }
    }
}
