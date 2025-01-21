namespace Server
{
    partial class BidderClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.tbModel = new System.Windows.Forms.TextBox();
            this.tbManufacturer = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbSearchRes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbBid = new System.Windows.Forms.TextBox();
            this.tbIdBid = new System.Windows.Forms.TextBox();
            this.btnBid = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Year";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Model";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Manufacturer";
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(88, 90);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(100, 20);
            this.tbYear.TabIndex = 28;
            this.tbYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbIdBid_KeyPress);
            // 
            // tbModel
            // 
            this.tbModel.Location = new System.Drawing.Point(88, 64);
            this.tbModel.Name = "tbModel";
            this.tbModel.Size = new System.Drawing.Size(100, 20);
            this.tbModel.TabIndex = 26;
            // 
            // tbManufacturer
            // 
            this.tbManufacturer.Location = new System.Drawing.Point(88, 38);
            this.tbManufacturer.Name = "tbManufacturer";
            this.tbManufacturer.Size = new System.Drawing.Size(100, 20);
            this.tbManufacturer.TabIndex = 25;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(164, 116);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 23);
            this.btnSearch.TabIndex = 20;
            this.btnSearch.Text = "Search vehicles";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbSearchRes
            // 
            this.tbSearchRes.Location = new System.Drawing.Point(12, 145);
            this.tbSearchRes.Multiline = true;
            this.tbSearchRes.Name = "tbSearchRes";
            this.tbSearchRes.ReadOnly = true;
            this.tbSearchRes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSearchRes.Size = new System.Drawing.Size(515, 76);
            this.tbSearchRes.TabIndex = 32;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(450, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Bid";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(362, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Bid on vehicle with id:";
            // 
            // tbBid
            // 
            this.tbBid.Location = new System.Drawing.Point(478, 90);
            this.tbBid.Name = "tbBid";
            this.tbBid.Size = new System.Drawing.Size(49, 20);
            this.tbBid.TabIndex = 34;
            this.tbBid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBid_KeyPress);
            // 
            // tbIdBid
            // 
            this.tbIdBid.Location = new System.Drawing.Point(478, 64);
            this.tbIdBid.Name = "tbIdBid";
            this.tbIdBid.Size = new System.Drawing.Size(49, 20);
            this.tbIdBid.TabIndex = 33;
            this.tbIdBid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbIdBid_KeyPress);
            // 
            // btnBid
            // 
            this.btnBid.Location = new System.Drawing.Point(427, 116);
            this.btnBid.Name = "btnBid";
            this.btnBid.Size = new System.Drawing.Size(100, 23);
            this.btnBid.TabIndex = 37;
            this.btnBid.Text = "Bid";
            this.btnBid.UseVisualStyleBackColor = true;
            this.btnBid.Click += new System.EventHandler(this.btnBid_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(321, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 38;
            this.button1.Text = "Active auctions";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BidderClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 235);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnBid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbBid);
            this.Controls.Add(this.tbIdBid);
            this.Controls.Add(this.tbSearchRes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.tbModel);
            this.Controls.Add(this.tbManufacturer);
            this.Controls.Add(this.btnSearch);
            this.Name = "BidderClient";
            this.Text = "Bidder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbYear;
        private System.Windows.Forms.TextBox tbModel;
        private System.Windows.Forms.TextBox tbManufacturer;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbSearchRes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbBid;
        private System.Windows.Forms.TextBox tbIdBid;
        private System.Windows.Forms.Button btnBid;
        private System.Windows.Forms.Button button1;
    }
}

