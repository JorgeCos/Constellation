using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Server
{
    public class AuctionManager : Form
    {
        public AuctionManager()
        {
            _vehicles = new List<Vehicle>();
            _activeAuctions = new Dictionary<int, Auction>();
            InitializeComponent();
        }

        private readonly List<Vehicle> _vehicles;
        private Dictionary<int, Auction> _activeAuctions;

        public List<Auction> GetActiveAuctions()
        {
            return _activeAuctions.Values.ToList();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (_vehicles.Any(v => v.Id == vehicle.Id))
                throw new ArgumentException("Vehicle with this ID already exists.");

            _vehicles.Add(vehicle);

            cbInventory.Items.Add(new KeyValuePair<string, object>(string.Concat(vehicle.Id, '-', vehicle.Manufacturer, ' ', vehicle.Model, ' ', vehicle.Year), vehicle));

        }

        public List<Vehicle> SearchVehicles(string manufacturer = null, string model = null, int? year = null)
        {
            return _vehicles.Where(v =>
                (manufacturer == null || v.Manufacturer.ToLower().Contains(manufacturer.ToLower())) &&
                (model == null || v.Model.ToLower().Contains(model.ToLower())) &&
                (!year.HasValue || v.Year == year)
            ).ToList();
        }

        public void StartAuction(int vehicleId)
        {
            var vehicle = _vehicles.FirstOrDefault(v => v.Id == vehicleId);
            if (vehicle == null)
                throw new ArgumentException("Vehicle not found.");

            if (_activeAuctions.ContainsKey(vehicleId))
                throw new InvalidOperationException("Auction already started for this vehicle.");
            _activeAuctions[vehicleId] = new Auction(vehicle);
        }

        public void PlaceBid(int vehicleId, decimal bid)
        {
            if (!_activeAuctions.ContainsKey(vehicleId))
                throw new InvalidOperationException("Auction is not active for this vehicle.");

            _activeAuctions[vehicleId].PlaceBid(bid);
        }

        public void CloseAuction(int vehicleId)
        {
            if (!_activeAuctions.ContainsKey(vehicleId))
                throw new InvalidOperationException("Auction is not active for this vehicle.");

            _activeAuctions[vehicleId].CloseAuction();
            _activeAuctions.Remove(vehicleId);

            _vehicles.RemoveAll(v => v.Id == vehicleId);
        }

        #region UI Events
        private void rbVehicle_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHatchback.Checked
                || rbSedan.Checked)
            {
                tbDoors.Enabled = true;

                tbLoadingCapacity.Enabled =
                    tbSeats.Enabled = false;
            }
            else if (rbSuv.Checked)
            {
                tbDoors.Enabled =
                    tbLoadingCapacity.Enabled = false;

                tbSeats.Enabled = true;
            }
            else // is a Truck
            {
                tbLoadingCapacity.Enabled = true;

                tbDoors.Enabled =
                    tbSeats.Enabled = false;
            }

        }

        private void btnAddAuction_Click(object sender, EventArgs e)
        {
            Vehicle vehicle = null;
            int id = _vehicles.Any() ? _vehicles.OrderBy(v => v.Id).ElementAt(_vehicles.Count - 1).Id + 1 : 0;

            if (rbHatchback.Checked)
            {
                if (string.IsNullOrEmpty(tbManufacturer.Text)
                    || string.IsNullOrEmpty(tbModel.Text)
                    || string.IsNullOrEmpty(tbYear.Text)
                    || string.IsNullOrEmpty(TbStartBid.Text)
                    || string.IsNullOrEmpty(tbDoors.Text))
                {
                    tbOutput.AppendText("All vehicles parameters must be inserted!\n");
                }
                else
                {
                    vehicle = new Hatchback(id, tbManufacturer.Text, tbModel.Text, Convert.ToInt32(tbYear.Text), Convert.ToDecimal(TbStartBid.Text), Convert.ToInt32(tbDoors.Text));
                }
            }
            else if (rbSedan.Checked)
            {
                if (string.IsNullOrEmpty(tbManufacturer.Text)
                    || string.IsNullOrEmpty(tbModel.Text)
                    || string.IsNullOrEmpty(tbYear.Text)
                    || string.IsNullOrEmpty(TbStartBid.Text)
                    || string.IsNullOrEmpty(tbDoors.Text))
                {
                    tbOutput.AppendText("All vehicles parameters must be inserted!\n");
                }
                else
                {
                    vehicle = new Sedan(id, tbManufacturer.Text, tbModel.Text, Convert.ToInt32(tbYear.Text), Convert.ToDecimal(TbStartBid.Text), Convert.ToInt32(tbDoors.Text));
                }
            }
            else if (rbSuv.Checked)
            {
                if (string.IsNullOrEmpty(tbManufacturer.Text)
                    || string.IsNullOrEmpty(tbModel.Text)
                    || string.IsNullOrEmpty(tbYear.Text)
                    || string.IsNullOrEmpty(TbStartBid.Text)
                    || string.IsNullOrEmpty(tbDoors.Text))
                {
                    tbOutput.AppendText("All vehicles parameters must be inserted!\n");
                }
                else
                {
                    vehicle = new SUV(id, tbManufacturer.Text, tbModel.Text, Convert.ToInt32(tbYear.Text), Convert.ToDecimal(TbStartBid.Text), Convert.ToInt32(tbSeats.Text));
                }
            }
            else // is a Truck
            {
                if (string.IsNullOrEmpty(tbManufacturer.Text)
                    || string.IsNullOrEmpty(tbModel.Text)
                    || string.IsNullOrEmpty(tbYear.Text)
                    || string.IsNullOrEmpty(TbStartBid.Text)
                    || string.IsNullOrEmpty(tbDoors.Text))
                {
                    tbOutput.AppendText("All vehicles parameters must be inserted!\n");
                }
                else
                {
                    vehicle = new Truck(id, tbManufacturer.Text, tbModel.Text, Convert.ToInt32(tbYear.Text), Convert.ToDecimal(TbStartBid.Text), Convert.ToInt32(tbLoadingCapacity.Text));
                }
            }

            if (vehicle != null)
            {
                try
                {
                    AddVehicle(vehicle);
                    tbOutput.AppendText("Vehicle added successfully.\n");
                }
                catch (Exception ex)
                {
                    tbOutput.AppendText($"Unable to add vehicle. {ex.Message}.\n");
                }
            }
            else
            {
                tbOutput.AppendText("Invalid vehicle type.\n");
            }
        }

        private void btnStartAuction_Click(object sender, EventArgs e)
        {
            if (_activeAuctions.Count > 0)
            {
                tbOutput.AppendText("Auction still occurring.\n");
                return;
            }

            if (cbInventory.SelectedItem != null)
            {
                int id = (((KeyValuePair<string, object>)cbInventory.SelectedItem).Value as Vehicle).Id;
                string desc = ((KeyValuePair<string, object>)cbInventory.SelectedItem).Key;

                try
                {
                    StartAuction(id);
                    tbCurrentAuction.Text = desc;
                    cbInventory.Enabled = false;
                    tbOutput.AppendText($"Auction started.\n");
                }
                catch (Exception ex)
                {
                    tbOutput.AppendText($"Unable to start auction. {ex.Message}.\n");
                }
            }
            else
                tbOutput.AppendText("No vehicle selected to start auction.\n");
        }

        private void btnStopAuction_Click(object sender, EventArgs e)
        {
            if (_activeAuctions.Count > 0)
            {
                int vehicleId = _activeAuctions.First().Key;

                if (_activeAuctions.First().Value.WinningBid > _activeAuctions.First().Value.StartingBid)
                {
                    tbOutput.AppendText($"\nAuction for {_activeAuctions.First().Value.Vehicle.Manufacturer} {_activeAuctions.First().Value.Vehicle.Model} from {_activeAuctions.First().Value.Vehicle.Year} finished.\n" +
                        $"Winning bid: {_activeAuctions.First().Value.WinningBid}\n\n");
                }
                else
                {
                    tbOutput.AppendText($"\nFailed auction for {_activeAuctions.First().Value.Vehicle.Manufacturer} {_activeAuctions.First().Value.Vehicle.Model} from {_activeAuctions.First().Value.Vehicle.Year} finished.\n" +
                        $"No bid bigger than starting bid\n\n");
                }

                try
                {
                    CloseAuction(vehicleId);
                    tbCurrentAuction.Text = string.Empty;
                    cbInventory.Items.RemoveAt(cbInventory.SelectedIndex);
                    cbInventory.Refresh();
                    cbInventory.Enabled = true;
                }
                catch (Exception ex)
                {
                    tbOutput.AppendText($"Unable to stop auction. {ex.Message}.\n");
                }
            }
        }

        private void tbCurrentAuction_TextChanged(object sender, EventArgs e)
        {
            btnStopAuction.Enabled = !string.IsNullOrEmpty(tbCurrentAuction.Text);

        }

        private void btnNewBidder_Click(object sender, EventArgs e)
        {
            BidderClient clt = new BidderClient(this);
            clt.Show();
        }

        private void tbInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbDecimal_KeyPress(object sender, KeyPressEventArgs e)
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

        #endregion

        #region UI
        private System.Windows.Forms.Button btnAddInventory;
        private RadioButton rbHatchback;
        private RadioButton rbSedan;
        private GroupBox groupBox1;
        private RadioButton rbTruck;
        private RadioButton rbSuv;
        private System.Windows.Forms.TextBox tbManufacturer;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private System.Windows.Forms.TextBox tbYear;
        private System.Windows.Forms.TextBox TbStartBid;
        private System.Windows.Forms.TextBox tbModel;
        private Label lblSeats;
        private System.Windows.Forms.TextBox tbSeats;
        private Label label5;
        private System.Windows.Forms.TextBox tbDoors;
        private Label label6;
        private System.Windows.Forms.TextBox tbLoadingCapacity;
        private System.Windows.Forms.TextBox tbOutput;
        private Label label7;
        private GroupBox groupBox2;
        private Label label8;
        private System.Windows.Forms.Button btnStartAuction;
        private System.Windows.Forms.ComboBox cbInventory;
        private System.Windows.Forms.Button btnStopAuction;
        private System.Windows.Forms.TextBox tbCurrentAuction;
        private Label label9;
        private Button btnNewBidder;

        private void InitializeComponent()
        {
            this.btnAddInventory = new System.Windows.Forms.Button();
            this.rbHatchback = new System.Windows.Forms.RadioButton();
            this.rbSedan = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbLoadingCapacity = new System.Windows.Forms.TextBox();
            this.lblSeats = new System.Windows.Forms.Label();
            this.tbSeats = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDoors = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.TbStartBid = new System.Windows.Forms.TextBox();
            this.tbModel = new System.Windows.Forms.TextBox();
            this.tbManufacturer = new System.Windows.Forms.TextBox();
            this.rbTruck = new System.Windows.Forms.RadioButton();
            this.rbSuv = new System.Windows.Forms.RadioButton();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStopAuction = new System.Windows.Forms.Button();
            this.tbCurrentAuction = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnStartAuction = new System.Windows.Forms.Button();
            this.cbInventory = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnNewBidder = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddInventory
            // 
            this.btnAddInventory.Location = new System.Drawing.Point(303, 123);
            this.btnAddInventory.Name = "btnAddInventory";
            this.btnAddInventory.Size = new System.Drawing.Size(100, 23);
            this.btnAddInventory.TabIndex = 0;
            this.btnAddInventory.Text = "Add to inventory";
            this.btnAddInventory.UseVisualStyleBackColor = true;
            this.btnAddInventory.Click += new System.EventHandler(this.btnAddAuction_Click);
            // 
            // rbHatchback
            // 
            this.rbHatchback.AutoSize = true;
            this.rbHatchback.Location = new System.Drawing.Point(6, 19);
            this.rbHatchback.Name = "rbHatchback";
            this.rbHatchback.Size = new System.Drawing.Size(78, 17);
            this.rbHatchback.TabIndex = 1;
            this.rbHatchback.TabStop = true;
            this.rbHatchback.Text = "Hatchback";
            this.rbHatchback.UseVisualStyleBackColor = true;
            this.rbHatchback.CheckedChanged += new System.EventHandler(this.rbVehicle_CheckedChanged);
            // 
            // rbSedan
            // 
            this.rbSedan.AutoSize = true;
            this.rbSedan.Location = new System.Drawing.Point(90, 19);
            this.rbSedan.Name = "rbSedan";
            this.rbSedan.Size = new System.Drawing.Size(56, 17);
            this.rbSedan.TabIndex = 2;
            this.rbSedan.TabStop = true;
            this.rbSedan.Text = "Sedan";
            this.rbSedan.UseVisualStyleBackColor = true;
            this.rbSedan.CheckedChanged += new System.EventHandler(this.rbVehicle_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbLoadingCapacity);
            this.groupBox1.Controls.Add(this.lblSeats);
            this.groupBox1.Controls.Add(this.tbSeats);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbDoors);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbYear);
            this.groupBox1.Controls.Add(this.TbStartBid);
            this.groupBox1.Controls.Add(this.tbModel);
            this.groupBox1.Controls.Add(this.tbManufacturer);
            this.groupBox1.Controls.Add(this.rbTruck);
            this.groupBox1.Controls.Add(this.btnAddInventory);
            this.groupBox1.Controls.Add(this.rbSuv);
            this.groupBox1.Controls.Add(this.rbHatchback);
            this.groupBox1.Controls.Add(this.rbSedan);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 159);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add vehicles to inventory";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(227, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Load Capacity";
            // 
            // tbLoadingCapacity
            // 
            this.tbLoadingCapacity.Enabled = false;
            this.tbLoadingCapacity.Location = new System.Drawing.Point(303, 97);
            this.tbLoadingCapacity.Name = "tbLoadingCapacity";
            this.tbLoadingCapacity.Size = new System.Drawing.Size(100, 20);
            this.tbLoadingCapacity.TabIndex = 18;
            this.tbLoadingCapacity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInt_KeyPress);
            // 
            // lblSeats
            // 
            this.lblSeats.AutoSize = true;
            this.lblSeats.Location = new System.Drawing.Point(227, 74);
            this.lblSeats.Name = "lblSeats";
            this.lblSeats.Size = new System.Drawing.Size(34, 13);
            this.lblSeats.TabIndex = 17;
            this.lblSeats.Text = "Seats";
            // 
            // tbSeats
            // 
            this.tbSeats.Enabled = false;
            this.tbSeats.Location = new System.Drawing.Point(303, 71);
            this.tbSeats.Name = "tbSeats";
            this.tbSeats.Size = new System.Drawing.Size(100, 20);
            this.tbSeats.TabIndex = 16;
            this.tbSeats.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInt_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(227, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Doors";
            // 
            // tbDoors
            // 
            this.tbDoors.Enabled = false;
            this.tbDoors.Location = new System.Drawing.Point(303, 45);
            this.tbDoors.Name = "tbDoors";
            this.tbDoors.Size = new System.Drawing.Size(100, 20);
            this.tbDoors.TabIndex = 14;
            this.tbDoors.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInt_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Starting bid";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Year";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Model";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Manufacturer";
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(82, 97);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(100, 20);
            this.tbYear.TabIndex = 9;
            this.tbYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInt_KeyPress);
            // 
            // TbStartBid
            // 
            this.TbStartBid.Location = new System.Drawing.Point(82, 123);
            this.TbStartBid.Name = "TbStartBid";
            this.TbStartBid.Size = new System.Drawing.Size(100, 20);
            this.TbStartBid.TabIndex = 8;
            this.TbStartBid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDecimal_KeyPress);
            // 
            // tbModel
            // 
            this.tbModel.Location = new System.Drawing.Point(82, 71);
            this.tbModel.Name = "tbModel";
            this.tbModel.Size = new System.Drawing.Size(100, 20);
            this.tbModel.TabIndex = 7;
            // 
            // tbManufacturer
            // 
            this.tbManufacturer.Location = new System.Drawing.Point(82, 45);
            this.tbManufacturer.Name = "tbManufacturer";
            this.tbManufacturer.Size = new System.Drawing.Size(100, 20);
            this.tbManufacturer.TabIndex = 6;
            // 
            // rbTruck
            // 
            this.rbTruck.AutoSize = true;
            this.rbTruck.Location = new System.Drawing.Point(205, 19);
            this.rbTruck.Name = "rbTruck";
            this.rbTruck.Size = new System.Drawing.Size(53, 17);
            this.rbTruck.TabIndex = 5;
            this.rbTruck.TabStop = true;
            this.rbTruck.Text = "Truck";
            this.rbTruck.UseVisualStyleBackColor = true;
            this.rbTruck.CheckedChanged += new System.EventHandler(this.rbVehicle_CheckedChanged);
            // 
            // rbSuv
            // 
            this.rbSuv.AutoSize = true;
            this.rbSuv.Location = new System.Drawing.Point(152, 19);
            this.rbSuv.Name = "rbSuv";
            this.rbSuv.Size = new System.Drawing.Size(47, 17);
            this.rbSuv.TabIndex = 4;
            this.rbSuv.TabStop = true;
            this.rbSuv.Text = "SUV";
            this.rbSuv.UseVisualStyleBackColor = true;
            this.rbSuv.CheckedChanged += new System.EventHandler(this.rbVehicle_CheckedChanged);
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.Location = new System.Drawing.Point(12, 377);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutput.Size = new System.Drawing.Size(413, 124);
            this.tbOutput.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 361);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Output";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStopAuction);
            this.groupBox2.Controls.Add(this.tbCurrentAuction);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.btnStartAuction);
            this.groupBox2.Controls.Add(this.cbInventory);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(12, 177);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(413, 137);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Manage Auctions";
            // 
            // btnStopAuction
            // 
            this.btnStopAuction.Enabled = false;
            this.btnStopAuction.Location = new System.Drawing.Point(303, 106);
            this.btnStopAuction.Name = "btnStopAuction";
            this.btnStopAuction.Size = new System.Drawing.Size(100, 23);
            this.btnStopAuction.TabIndex = 23;
            this.btnStopAuction.Text = "Stop Auction";
            this.btnStopAuction.UseVisualStyleBackColor = true;
            this.btnStopAuction.Click += new System.EventHandler(this.btnStopAuction_Click);
            // 
            // tbCurrentAuction
            // 
            this.tbCurrentAuction.Location = new System.Drawing.Point(95, 80);
            this.tbCurrentAuction.Name = "tbCurrentAuction";
            this.tbCurrentAuction.ReadOnly = true;
            this.tbCurrentAuction.Size = new System.Drawing.Size(308, 20);
            this.tbCurrentAuction.TabIndex = 20;
            this.tbCurrentAuction.TextChanged += new System.EventHandler(this.tbCurrentAuction_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Under Auction";
            // 
            // btnStartAuction
            // 
            this.btnStartAuction.Location = new System.Drawing.Point(303, 40);
            this.btnStartAuction.Name = "btnStartAuction";
            this.btnStartAuction.Size = new System.Drawing.Size(100, 23);
            this.btnStartAuction.TabIndex = 20;
            this.btnStartAuction.Text = "Start Auction";
            this.btnStartAuction.UseVisualStyleBackColor = true;
            this.btnStartAuction.Click += new System.EventHandler(this.btnStartAuction_Click);
            // 
            // cbInventory
            // 
            this.cbInventory.FormattingEnabled = true;
            this.cbInventory.Location = new System.Drawing.Point(95, 13);
            this.cbInventory.Name = "cbInventory";
            this.cbInventory.Size = new System.Drawing.Size(308, 21);
            this.cbInventory.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Active inventory";
            // 
            // btnNewBidder
            // 
            this.btnNewBidder.Location = new System.Drawing.Point(242, 337);
            this.btnNewBidder.Name = "btnNewBidder";
            this.btnNewBidder.Size = new System.Drawing.Size(173, 23);
            this.btnNewBidder.TabIndex = 24;
            this.btnNewBidder.Text = "Open New Bidder Client";
            this.btnNewBidder.UseVisualStyleBackColor = true;
            this.btnNewBidder.Click += new System.EventHandler(this.btnNewBidder_Click);
            // 
            // AuctionManager
            // 
            this.ClientSize = new System.Drawing.Size(443, 513);
            this.Controls.Add(this.btnNewBidder);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.groupBox1);
            this.Name = "AuctionManager";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}
