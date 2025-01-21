namespace Server
{
    public class Hatchback : Vehicle
    {
        public int Doors { get; set; }

        public Hatchback(int id, string manufacturer, string model, int year, decimal startingBid, int doors)
            : base(id, manufacturer, model, year, startingBid)
        {
            Doors = doors;
        }
    }
}
