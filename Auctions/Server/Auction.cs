using System;

namespace Server
{
    public class Auction
    {
        public Vehicle Vehicle { get; set; }
        public decimal StartingBid { get; set; }
        public decimal WinningBid { get; set; } = 0;
        public bool IsActive { get; set; } = false;

        public Auction(Vehicle vehicle)
        {
            Vehicle = vehicle;
            StartingBid = vehicle.StartingBid;
            IsActive = true;
        }

        public void PlaceBid(decimal bid)
        {
            if (!IsActive)
                throw new InvalidOperationException("Auction is not active.");

            if (bid <= StartingBid)
                throw new InvalidOperationException($"Bid must be higher than the starting bid. Starting bid: {StartingBid}");

            if (bid <= WinningBid)
                throw new InvalidOperationException($"Bid must be higher than the current bid. Current bid: {WinningBid}");

            WinningBid = bid;

        }

        public void CloseAuction()
        {
            IsActive = false;
        }
    }
}
