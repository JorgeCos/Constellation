using NUnit.Framework;
using Server;
using System;
using System.Linq;

namespace UnitTest
{

    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void AddVehicle_ShouldAddVehicleSuccessfully()
        {
            AuctionManager _auctionManager = new AuctionManager();
            var sedan = new Sedan(0, "VW", "Passat", 2020, 10000, 4);

            _auctionManager.AddVehicle(sedan);

            var vehicles = _auctionManager.SearchVehicles();
            NUnit.Framework.Legacy.ClassicAssert.AreEqual(1, vehicles.Count);
            NUnit.Framework.Legacy.ClassicAssert.AreEqual(0, vehicles.First().Id);
        }

        [Test]
        public void AddVehicle_ShouldThrowException_WhenVehicleIDAlreadyExists()
        {
            AuctionManager _auctionManager = new AuctionManager();
            var sedan1 = new Sedan(1, "VW", "Passat", 2020, 10000, 4);
            var sedan2 = new Sedan(1, "Honda", "Civic", 2021, 12000, 4);
            _auctionManager.AddVehicle(sedan1);

            var ex = NUnit.Framework.Legacy.ClassicAssert.Throws<ArgumentException>(() => _auctionManager.AddVehicle(sedan2));
            NUnit.Framework.Legacy.ClassicAssert.AreEqual("Vehicle with this ID already exists.", ex.Message);
        }

        [Test]
        public void SearchVehicles_ShouldReturnMatchingVehicles()
        {
            AuctionManager _auctionManager = new AuctionManager();
            var sedan = new Sedan(1, "VW", "Passat", 2020, 10000, 4);
            var suv = new SUV(2, "Ford", "Explorer", 2021, 25000, 7);
            _auctionManager.AddVehicle(sedan);
            _auctionManager.AddVehicle(suv);

            var searchResults = _auctionManager.SearchVehicles(manufacturer: "VW");

            NUnit.Framework.Legacy.ClassicAssert.AreEqual(1, searchResults.Count);
            NUnit.Framework.Legacy.ClassicAssert.AreEqual("1", searchResults.First().Id);
        }

        [Test]
        public void StartAuction_ShouldThrowException_WhenVehicleNotFound()
        {
            AuctionManager _auctionManager = new AuctionManager();
            var ex = NUnit.Framework.Legacy.ClassicAssert.Throws<ArgumentException>(() => _auctionManager.StartAuction(5));
            NUnit.Framework.Legacy.ClassicAssert.AreEqual("Vehicle not found.", ex.Message);
        }

        [Test]
        public void StartAuction_ShouldThrowException_WhenAuctionAlreadyActive()
        {
            AuctionManager _auctionManager = new AuctionManager();
            var sedan = new Sedan(1, "VW", "Passat", 2020, 10000, 4);
            _auctionManager.AddVehicle(sedan);
            _auctionManager.StartAuction(1);

            var ex = NUnit.Framework.Legacy.ClassicAssert.Throws<InvalidOperationException>(() => _auctionManager.StartAuction(1));
            NUnit.Framework.Legacy.ClassicAssert.AreEqual("Auction already started for this vehicle.", ex.Message);
        }

        [Test]
        public void PlaceBid_ShouldPlaceBidSuccessfully()
        {
            AuctionManager _auctionManager = new AuctionManager();
            var sedan = new Sedan(1, "VW", "Passat", 2020, 10000, 4);
            _auctionManager.AddVehicle(sedan);
            _auctionManager.StartAuction(1);

            _auctionManager.PlaceBid(1, 12000);

            Auction activeAuction = _auctionManager.GetActiveAuctions().First();
            NUnit.Framework.Legacy.ClassicAssert.AreEqual(12000, activeAuction.WinningBid);
        }

        [Test]
        public void PlaceBid_ShouldThrowException_WhenAuctionIsNotActive()
        {
            AuctionManager _auctionManager = new AuctionManager();
            var sedan = new Sedan(1, "VW", "Passat", 2020, 10000, 4);
            _auctionManager.AddVehicle(sedan);

            var ex = NUnit.Framework.Legacy.ClassicAssert.Throws<InvalidOperationException>(() => _auctionManager.PlaceBid(1, 12000));
            NUnit.Framework.Legacy.ClassicAssert.AreEqual("Auction is not active for this vehicle.", ex.Message);
        }

        [Test]
        public void PlaceBid_ShouldThrowException_WhenBidIsNotHigherThanCurrentBid()
        {
            AuctionManager _auctionManager = new AuctionManager();
            var sedan = new Sedan(1, "VW", "Passat", 2020, 10000, 4);
            _auctionManager.AddVehicle(sedan);
            _auctionManager.StartAuction(1);

            var ex = NUnit.Framework.Legacy.ClassicAssert.Throws<ArgumentException>(() => _auctionManager.PlaceBid(1, 10000));
            NUnit.Framework.Legacy.ClassicAssert.IsTrue(ex.Message.StartsWith("Bid must be higher than the starting bid."));
        }

        [Test]
        public void CloseAuction_ShouldCloseAuctionSuccessfully()
        {
            AuctionManager _auctionManager = new AuctionManager();
            var sedan = new Sedan(1, "VW", "Passat", 2020, 10000, 4);
            _auctionManager.AddVehicle(sedan);
            _auctionManager.StartAuction(1);

            _auctionManager.CloseAuction(1);

            var activeAuctions = _auctionManager.GetActiveAuctions();
            NUnit.Framework.Legacy.ClassicAssert.AreEqual(0, activeAuctions.Count);
        }

        [Test]
        public void CloseAuction_ShouldThrowException_WhenAuctionIsNotActive()
        {
            AuctionManager _auctionManager = new AuctionManager();
            var sedan = new Sedan(0, "VW", "Passat", 2020, 10000, 4);
            _auctionManager.AddVehicle(sedan);

            var ex = NUnit.Framework.Legacy.ClassicAssert.Throws<InvalidOperationException>(() => _auctionManager.CloseAuction(0));
            NUnit.Framework.Legacy.ClassicAssert.AreEqual("Auction is not active for this vehicle.", ex.Message);
        }
    }
}