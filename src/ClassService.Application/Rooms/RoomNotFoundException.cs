namespace ClassService.Application.Rooms
{
    public class RoomNotFoundException: Exception
    {
        public RoomNotFoundException(): base("Room not found")
        {

        }
    }
}