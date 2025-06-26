

namespace Entity
{
    public class Room
    {
        public int id { get; set; }
        public int roomTypeId { get; set; }
        public int bedId { get; set; }
        public int hotelId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal priceByNight { get; set; }
        public int numberOfPeople { get; set; }
        public string hasPool { get; set; }
        public string  hasWifi { get; set; }
        public string hasSeaView { get; set; }
    }
}
