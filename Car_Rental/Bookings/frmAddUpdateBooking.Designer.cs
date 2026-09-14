namespace Car_Rental.Bookings
{
    partial class frmAddUpdateBooking
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
            this.components = new System.ComponentModel.Container();
            this.tcCustomerInfo = new System.Windows.Forms.TabControl();
            this.tpCustomerInfo = new System.Windows.Forms.TabPage();
            this.btnPersonInfoNext = new System.Windows.Forms.Button();
            this.tpBookingInfo = new System.Windows.Forms.TabPage();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbVehicles = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblBookingID = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.lblInitialTotal = new System.Windows.Forms.Label();
            this.btnClaculate = new System.Windows.Forms.Button();
            this.ctrlCustomerCardWithFilter1 = new Car_Rental.Controls.ctrlCustomerCardWithFilter();
            this.ctrlCustomerCard1 = new Car_Rental.Controls.ctrlCustomerCard();
            this.tcCustomerInfo.SuspendLayout();
            this.tpCustomerInfo.SuspendLayout();
            this.tpBookingInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tcCustomerInfo
            // 
            this.tcCustomerInfo.Controls.Add(this.tpCustomerInfo);
            this.tcCustomerInfo.Controls.Add(this.tpBookingInfo);
            this.tcCustomerInfo.Location = new System.Drawing.Point(10, 65);
            this.tcCustomerInfo.Name = "tcCustomerInfo";
            this.tcCustomerInfo.SelectedIndex = 0;
            this.tcCustomerInfo.Size = new System.Drawing.Size(690, 603);
            this.tcCustomerInfo.TabIndex = 125;
            // 
            // tpCustomerInfo
            // 
            this.tpCustomerInfo.BackColor = System.Drawing.Color.OldLace;
            this.tpCustomerInfo.Controls.Add(this.ctrlCustomerCardWithFilter1);
            this.tpCustomerInfo.Controls.Add(this.ctrlCustomerCard1);
            this.tpCustomerInfo.Controls.Add(this.btnPersonInfoNext);
            this.tpCustomerInfo.Location = new System.Drawing.Point(4, 29);
            this.tpCustomerInfo.Name = "tpCustomerInfo";
            this.tpCustomerInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpCustomerInfo.Size = new System.Drawing.Size(682, 570);
            this.tpCustomerInfo.TabIndex = 0;
            this.tpCustomerInfo.Text = "Customer Info";
            // 
            // btnPersonInfoNext
            // 
            this.btnPersonInfoNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPersonInfoNext.Image = global::Car_Rental.Properties.Resources.Next_32;
            this.btnPersonInfoNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPersonInfoNext.Location = new System.Drawing.Point(535, 518);
            this.btnPersonInfoNext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPersonInfoNext.Name = "btnPersonInfoNext";
            this.btnPersonInfoNext.Size = new System.Drawing.Size(126, 37);
            this.btnPersonInfoNext.TabIndex = 119;
            this.btnPersonInfoNext.Text = "Next";
            this.btnPersonInfoNext.UseVisualStyleBackColor = true;
            this.btnPersonInfoNext.Click += new System.EventHandler(this.btnPersonInfoNext_Click);
            // 
            // tpBookingInfo
            // 
            this.tpBookingInfo.BackColor = System.Drawing.Color.OldLace;
            this.tpBookingInfo.Controls.Add(this.btnClaculate);
            this.tpBookingInfo.Controls.Add(this.lblInitialTotal);
            this.tpBookingInfo.Controls.Add(this.dtpEndDate);
            this.tpBookingInfo.Controls.Add(this.dtpStartDate);
            this.tpBookingInfo.Controls.Add(this.pictureBox4);
            this.tpBookingInfo.Controls.Add(this.label5);
            this.tpBookingInfo.Controls.Add(this.cbVehicles);
            this.tpBookingInfo.Controls.Add(this.pictureBox2);
            this.tpBookingInfo.Controls.Add(this.lblBookingID);
            this.tpBookingInfo.Controls.Add(this.label4);
            this.tpBookingInfo.Controls.Add(this.label1);
            this.tpBookingInfo.Controls.Add(this.label3);
            this.tpBookingInfo.Controls.Add(this.label2);
            this.tpBookingInfo.Controls.Add(this.pictureBox1);
            this.tpBookingInfo.Controls.Add(this.pictureBox8);
            this.tpBookingInfo.Controls.Add(this.pictureBox3);
            this.tpBookingInfo.Location = new System.Drawing.Point(4, 29);
            this.tpBookingInfo.Name = "tpBookingInfo";
            this.tpBookingInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpBookingInfo.Size = new System.Drawing.Size(682, 570);
            this.tpBookingInfo.TabIndex = 1;
            this.tpBookingInfo.Text = "Booking Info";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(231, 168);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(167, 26);
            this.dtpEndDate.TabIndex = 136;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "dd/MM/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(231, 131);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(167, 26);
            this.dtpStartDate.TabIndex = 135;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Car_Rental.Properties.Resources.Number_32;
            this.pictureBox4.Location = new System.Drawing.Point(193, 209);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(31, 26);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 134;
            this.pictureBox4.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(70, 211);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 20);
            this.label5.TabIndex = 132;
            this.label5.Text = "Intial Total:";
            // 
            // cbVehicles
            // 
            this.cbVehicles.FormattingEnabled = true;
            this.cbVehicles.Location = new System.Drawing.Point(230, 91);
            this.cbVehicles.Name = "cbVehicles";
            this.cbVehicles.Size = new System.Drawing.Size(167, 28);
            this.cbVehicles.TabIndex = 131;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Car_Rental.Properties.Resources.Number_32;
            this.pictureBox2.Location = new System.Drawing.Point(193, 58);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 130;
            this.pictureBox2.TabStop = false;
            // 
            // lblBookingID
            // 
            this.lblBookingID.AutoSize = true;
            this.lblBookingID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookingID.Location = new System.Drawing.Point(233, 58);
            this.lblBookingID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBookingID.Name = "lblBookingID";
            this.lblBookingID.Size = new System.Drawing.Size(39, 20);
            this.lblBookingID.TabIndex = 129;
            this.lblBookingID.Text = "???";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(70, 58);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 20);
            this.label4.TabIndex = 128;
            this.label4.Text = "Booking ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(100, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 120;
            this.label1.Text = "Vehicle:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(82, 168);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 20);
            this.label3.TabIndex = 125;
            this.label3.Text = "End Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 131);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.TabIndex = 121;
            this.label2.Text = "Start Date:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Car_Rental.Properties.Resources.Calendar_32;
            this.pictureBox1.Location = new System.Drawing.Point(193, 168);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 126;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::Car_Rental.Properties.Resources.Person_32;
            this.pictureBox8.Location = new System.Drawing.Point(193, 94);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(31, 26);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 123;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Car_Rental.Properties.Resources.Calendar_32;
            this.pictureBox3.Location = new System.Drawing.Point(193, 131);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(31, 26);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 122;
            this.pictureBox3.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(697, 39);
            this.lblTitle.TabIndex = 126;
            this.lblTitle.Text = "Add New Booking";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.CausesValidation = false;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = global::Car_Rental.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(415, 676);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 37);
            this.btnClose.TabIndex = 124;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Image = global::Car_Rental.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(574, 676);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(126, 37);
            this.btnSave.TabIndex = 123;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblInitialTotal
            // 
            this.lblInitialTotal.AutoSize = true;
            this.lblInitialTotal.Location = new System.Drawing.Point(233, 215);
            this.lblInitialTotal.Name = "lblInitialTotal";
            this.lblInitialTotal.Size = new System.Drawing.Size(19, 20);
            this.lblInitialTotal.TabIndex = 137;
            this.lblInitialTotal.Text = "0";
            // 
            // btnClaculate
            // 
            this.btnClaculate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClaculate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClaculate.Location = new System.Drawing.Point(193, 276);
            this.btnClaculate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClaculate.Name = "btnClaculate";
            this.btnClaculate.Size = new System.Drawing.Size(94, 37);
            this.btnClaculate.TabIndex = 138;
            this.btnClaculate.Text = "Calculate";
            this.btnClaculate.UseVisualStyleBackColor = true;
            this.btnClaculate.Click += new System.EventHandler(this.btnClaculate_Click);
            // 
            // ctrlCustomerCardWithFilter1
            // 
            this.ctrlCustomerCardWithFilter1.BackColor = System.Drawing.Color.OldLace;
            this.ctrlCustomerCardWithFilter1.FilterEnabled = true;
            this.ctrlCustomerCardWithFilter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlCustomerCardWithFilter1.Location = new System.Drawing.Point(4, 5);
            this.ctrlCustomerCardWithFilter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlCustomerCardWithFilter1.Name = "ctrlCustomerCardWithFilter1";
            this.ctrlCustomerCardWithFilter1.ShowAddCustomer = true;
            this.ctrlCustomerCardWithFilter1.Size = new System.Drawing.Size(680, 503);
            this.ctrlCustomerCardWithFilter1.TabIndex = 121;
            // 
            // ctrlCustomerCard1
            // 
            this.ctrlCustomerCard1.BackColor = System.Drawing.Color.OldLace;
            this.ctrlCustomerCard1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlCustomerCard1.Location = new System.Drawing.Point(435, 256);
            this.ctrlCustomerCard1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlCustomerCard1.Name = "ctrlCustomerCard1";
            this.ctrlCustomerCard1.Size = new System.Drawing.Size(8, 8);
            this.ctrlCustomerCard1.TabIndex = 120;
            // 
            // frmAddUpdateBooking
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(716, 752);
            this.Controls.Add(this.tcCustomerInfo);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmAddUpdateBooking";
            this.Text = "frmAddUpdateBooking";
            this.Activated += new System.EventHandler(this.frmAddUpdateBooking_Activated);
            this.Load += new System.EventHandler(this.frmAddUpdateBooking_Load);
            this.tcCustomerInfo.ResumeLayout(false);
            this.tpCustomerInfo.ResumeLayout(false);
            this.tpBookingInfo.ResumeLayout(false);
            this.tpBookingInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcCustomerInfo;
        private System.Windows.Forms.TabPage tpCustomerInfo;
        private System.Windows.Forms.Button btnPersonInfoNext;
        private System.Windows.Forms.TabPage tpBookingInfo;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblBookingID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnSave;
        private Controls.ctrlCustomerCard ctrlCustomerCard1;
        private Controls.ctrlCustomerCardWithFilter ctrlCustomerCardWithFilter1;
        private System.Windows.Forms.ComboBox cbVehicles;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lblInitialTotal;
        private System.Windows.Forms.Button btnClaculate;
    }
}