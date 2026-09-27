namespace ClassService.Application.Classes.GetClassDetail
{
    public class GetClassDetailResponse
    {
        public string ClassName { get; set; } = string.Empty;
        public int NumberOfStudents { get; set; }
        public string SubjectName { get; set; }= string.Empty;
        public DateOnly Date { get; set; }
        public TimeOnly StartTime {get; set;}
        public TimeOnly EndTime {get; set;}
        public string RoomName { get; set; }= string.Empty;
        public List<ClassSchedule> ClassSchedules { get; set; } = new List<ClassSchedule>();
        public OtherInformaiton OtherInformaiton { get; set; } = new OtherInformaiton();
    }

    public class ClassSchedule
    {
        public int PeriodId {get; set;}
        public int PeriodNumber { get; set;}
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string SubjectName { get; set; }= string.Empty;
        public string Topic { get; set; }= string.Empty;
        public string RoomName { get; set; }= string.Empty;
    }

    public class OtherInformaiton
    {
        public string? HomeRoomTeacher { get; set; } 
        public string? SubjectTeacher { get; set; } 
        public int RoomCapacity { get; set; }
        public int PeriodClassCount  {get; set;}
          public string SchoolYear  {get; set;}= string.Empty;
    }