namespace Server
{
    public class SUV : Vehicle
    {
        public int Seats { get; set; }

        public SUV(int id, string manufacturer, string model, int year, decimal startingBid, int seats)
            : base(id, manufacturer, model, year, startingBid)
        {
            Seats = seats;
        }
    }
}
