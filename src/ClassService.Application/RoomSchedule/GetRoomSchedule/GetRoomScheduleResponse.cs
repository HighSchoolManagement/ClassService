using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.ClassRoomSchedule.GetClassRoomSchedule
{
    public class GetRoomScheduleResponse
    {
        public RoomInfo Room { get; set; } = null!;
        public SchoolYearInfo SchoolYear { get; set; } = null!;

        /// <summary>The period grid of the school year (rows of the table).</summary>
        public List<PeriodInfo> Periods { get; set; } = new();

        /// <summary>Booked cells only. Points to a period by PeriodId.</summary>
        public List<ItemInfo> Items { get; set; } = new();

        public SummaryInfo Summary { get; set; } = null!;


        public class RoomInfo
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int Capacity { get; set; }
            public bool IsActive { get; set; }
        }

        public class SchoolYearInfo
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateOnly StartDate { get; set; }
            public DateOnly EndDate { get; set; }
        }

        public class PeriodInfo
        {
            public int Id { get; set; }
            public int Number { get; set; }
            public TimeOnly StartTime { get; set; }
            public TimeOnly EndTime { get; set; }
        }

        public class ItemInfo
        {
            public int Id { get; set; }
            public DateOnly Date { get; set; }
            public int PeriodId { get; set; }

            /// <summary>"Class", "Meeting" or "Reserved" (BookingType name).</summary>
            public string Type { get; set; } = string.Empty;

            /// <summary>Label for Meeting / Reserved (e.g. "Staff meeting"). Null for a class.</summary>
            public string? Title { get; set; }

            public int? ClassId { get; set; }
            public string? ClassName { get; set; }
            public int? StudentCount { get; set; }
        }

        public class SummaryInfo
        {
            public int ClassCount { get; set; }
            public int BookedPeriodCount { get; set; }
            public int FreePeriodCount { get; set; }
        }
    }
}
