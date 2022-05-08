namespace LeahsPlatinumTracker.Maps
{
    partial class Oreburgh
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
            this.button1 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.routeConnectorButton1 = new LeahsPlatinumTracker.RouteConnectorButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Power Clear", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.button1.Location = new System.Drawing.Point(7, 767);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 23);
            this.button1.TabIndex = 26;
            this.button1.Text = "?";
            this.button1.UseCompatibleTextRendering = true;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button24
            // 
            this.button24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(183)))), ((int)(((byte)(214)))));
            this.button24.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(146)))), ((int)(((byte)(190)))));
            this.button24.FlatAppearance.BorderSize = 2;
            this.button24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button24.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(82)))), ((int)(((byte)(129)))));
            this.button24.Location = new System.Drawing.Point(56, 767);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(107, 23);
            this.button24.TabIndex = 27;
            this.button24.Text = "Canalave";
            this.button24.UseCompatibleTextRendering = true;
            this.button24.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.pictureBox1.Image = global::LeahsPlatinumTracker.Properties.MapResources.Oreburgh;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(996, 802);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // routeConnectorButton1
            // 
            this.routeConnectorButton1.associatedVisualMapSector = null;
            this.routeConnectorButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(180)))), ((int)(((byte)(111)))));
            this.routeConnectorButton1.destinationVisualMapSectorID = "207";
            this.routeConnectorButton1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.routeConnectorButton1.FlatAppearance.BorderSize = 2;
            this.routeConnectorButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.routeConnectorButton1.Font = new System.Drawing.Font("Nirmala UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.routeConnectorButton1.ForeColor = System.Drawing.Color.White;
            this.routeConnectorButton1.Location = new System.Drawing.Point(627, 5);
            this.routeConnectorButton1.Name = "routeConnectorButton1";
            this.routeConnectorButton1.Size = new System.Drawing.Size(107, 23);
            this.routeConnectorButton1.TabIndex = 28;
            this.routeConnectorButton1.Text = "routeConnectorButton1";
            this.routeConnectorButton1.UseCompatibleTextRendering = true;
            this.routeConnectorButton1.UseVisualStyleBackColor = false;
            // 
            // Oreburgh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(996, 802);
            this.Controls.Add(this.routeConnectorButton1);
            this.Controls.Add(this.button24);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Oreburgh";
            this.Text = "Floaroma";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button button1;
        private Button button24;
        private PictureBox pictureBox1;
        private RouteConnectorButton routeConnectorButton1;
    }
}