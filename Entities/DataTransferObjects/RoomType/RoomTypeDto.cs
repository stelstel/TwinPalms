namespace Entities.DataTransferObjects
{
    public class RoomTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // public int HotelId { get; set; } HotelId (parent) is already set before getting roomtype (child)
    }
}
