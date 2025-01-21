namespace Server
{
    public abstract class Vehicle
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal StartingBid { get; set; }

        public Vehicle(int id, string manufacturer, string model, int year, decimal startingBid)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
            StartingBid = startingBid;
        }
    }
}