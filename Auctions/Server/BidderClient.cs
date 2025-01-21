using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Server
{
    public partial class BidderClient : Form
    {
        private AuctionManager _auctionManager;
        public BidderClient(AuctionManager parent)
        {
            InitializeComponent();
            _auctionManager = parent;
        }

        private void btnBid_Click(object sender, EventArgs e)
        {
            try
            {
                _auctionManager.PlaceBid(Convert.ToInt32(tbIdBid.Text), Convert.ToDecimal(tbBid.Text));
                tbSearchRes.Text = $"{tbBid.Text} bid placed on auction Id {tbIdBid.Text}.\n";
            }
            catch (Exception ex)
            {
                tbSearchRes.Text = $"An error occurred '{ex.Message}'. Confirm your values and try again.\n";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Vehicle> res = _auctionManager.SearchVehicles(tbManufacturer.Text, tbModel.Text, ToNullableInt(tbYear.Text));

            if (res != null && res.Count > 0)
            {
                tbSearchRes.Text = $"Vehicles found:\n";
                foreach (Vehicle v in res)
                {
                    tbSearchRes.AppendText($"Auction ID: {v.Id} - {v.Manufacturer} {v.Model} from {v.Year}. Starting bid: {v.StartingBid}.\n");
                }
            }
            else
            {
                tbSearchRes.Text = $"No vehicles found matching the searched criteria.";
            }
        }

        public int? ToNullableInt(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        private void tbIdBid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbBid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Auction> res = _auctionManager.GetActiveAuctions();

            if (res != null && res.Count > 0)
            {
                tbSearchRes.Text = $"Auctions found:\n";
                foreach (Auction v in res)
                {
                    tbSearchRes.AppendText($"Auction ID: {v.Vehicle.Id} - {v.Vehicle.Manufacturer} {v.Vehicle.Model} from {v.Vehicle.Year}. Starting bid: {v.StartingBid}. Current high bid: {v.WinningBid}.\n");
                }
            }
            else
            {
                tbSearchRes.Text = $"No active auctions found.";
            }
        }
    }
}

