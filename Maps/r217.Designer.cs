﻿namespace LeahsPlatinumTracker.Maps
{
    partial class r217
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.routeConnectorButton1 = new LeahsPlatinumTracker.RouteConnectorButton();
            this.routeConnectorButton2 = new LeahsPlatinumTracker.RouteConnectorButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.pictureBox1.Image = global::LeahsPlatinumTracker.Properties.MapResources.Route217;
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
            this.routeConnectorButton1.destinationVisualMapSectorID = "AcuityLake";
            this.routeConnectorButton1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.routeConnectorButton1.FlatAppearance.BorderSize = 2;
            this.routeConnectorButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.routeConnectorButton1.Font = new System.Drawing.Font("Nirmala UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.routeConnectorButton1.ForeColor = System.Drawing.Color.White;
            this.routeConnectorButton1.Location = new System.Drawing.Point(322, 5);
            this.routeConnectorButton1.Name = "routeConnectorButton1";
            this.routeConnectorButton1.Size = new System.Drawing.Size(107, 23);
            this.routeConnectorButton1.TabIndex = 1;
            this.routeConnectorButton1.Text = "routeConnectorButton1";
            this.routeConnectorButton1.UseCompatibleTextRendering = true;
            this.routeConnectorButton1.UseVisualStyleBackColor = false;
            // 
            // routeConnectorButton2
            // 
            this.routeConnectorButton2.associatedVisualMapSector = null;
            this.routeConnectorButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(180)))), ((int)(((byte)(111)))));
            this.routeConnectorButton2.destinationVisualMapSectorID = "216";
            this.routeConnectorButton2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.routeConnectorButton2.FlatAppearance.BorderSize = 2;
            this.routeConnectorButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.routeConnectorButton2.Font = new System.Drawing.Font("Nirmala UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.routeConnectorButton2.ForeColor = System.Drawing.Color.White;
            this.routeConnectorButton2.Location = new System.Drawing.Point(227, 747);
            this.routeConnectorButton2.Name = "routeConnectorButton2";
            this.routeConnectorButton2.Size = new System.Drawing.Size(107, 23);
            this.routeConnectorButton2.TabIndex = 2;
            this.routeConnectorButton2.Text = "routeConnectorButton2";
            this.routeConnectorButton2.UseCompatibleTextRendering = true;
            this.routeConnectorButton2.UseVisualStyleBackColor = false;
            // 
            // r217
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(996, 802);
            this.Controls.Add(this.routeConnectorButton2);
            this.Controls.Add(this.routeConnectorButton1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "r217";
            this.Text = "Floaroma";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private PictureBox pictureBox1;
        private RouteConnectorButton routeConnectorButton1;
        private RouteConnectorButton routeConnectorButton2;
    }
}