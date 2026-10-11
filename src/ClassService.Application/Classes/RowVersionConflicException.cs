namespace ClassService.Application.Classes
{
    public class RowVersionConflitException: Exception
    {
        public RowVersionConflitException(): base("Version is conflic")
        {

        }
    }
}