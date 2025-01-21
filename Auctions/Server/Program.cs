using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    internal static class Program
    {
        [STAThread]
        public static void Main()
        {
            var auctionManager = new AuctionManager();
            // Add some test vehicles
            var sedan = new Sedan(0, "Toyota", "Corolla", 2019, 20000, 4);
            var suv = new SUV(1, "Nissan", "Qashqai", 2024, 45000, 5);
            var suv2 = new SUV(2, "Nissan", "XTrail", 2024, 15000, 5);
            var suv3 = new SUV(3, "Nissan", "Qashqai", 2019, 5000, 5);
            var truck = new Truck(4, "MAN", "BuildSpec", 2019, 50000, 35000);
            auctionManager.AddVehicle(sedan);
            auctionManager.AddVehicle(suv);
            auctionManager.AddVehicle(suv2);
            auctionManager.AddVehicle(suv3);
            auctionManager.AddVehicle(truck);
            auctionManager.ShowDialog();


        }
    }
}
