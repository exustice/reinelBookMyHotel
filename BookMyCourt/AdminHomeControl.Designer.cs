namespace BookMyCourt
{
    partial class AdminHomeControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewCompletedBookings = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCompletedBookings)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCompletedBookings
            // 
            this.dataGridViewCompletedBookings.AllowUserToAddRows = false;
            this.dataGridViewCompletedBookings.AllowUserToDeleteRows = false;
            this.dataGridViewCompletedBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCompletedBookings.Location = new System.Drawing.Point(44, 31);
            this.dataGridViewCompletedBookings.Name = "dataGridViewCompletedBookings";
            this.dataGridViewCompletedBookings.Size = new System.Drawing.Size(708, 297);
            this.dataGridViewCompletedBookings.TabIndex = 1;
            this.dataGridViewCompletedBookings.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCompletedBookings_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(44, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(708, 297);
            this.panel1.TabIndex = 2;
            this.panel1.Visible = false;
            // 
            // AdminHomeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridViewCompletedBookings);
            this.Name = "AdminHomeControl";
            this.Size = new System.Drawing.Size(834, 380);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCompletedBookings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewCompletedBookings;
        private System.Windows.Forms.Panel panel1;
    }
}
