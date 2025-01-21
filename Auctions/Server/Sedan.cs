namespace Server
{
    public class Sedan : Vehicle
    {
        public int Doors { get; set; }

        public Sedan(int id, string manufacturer, string model, int year, decimal startingBid, int doors)
            : base(id, manufacturer, model, year, startingBid)
        {
            Doors = doors;
        }

}

}