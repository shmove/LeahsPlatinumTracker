namespace LeahsPlatinumTracker
{
    partial class Index
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Index));
            this.Logo = new System.Windows.Forms.PictureBox();
            this.LogoPanel = new System.Windows.Forms.Panel();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.LoadFileButton = new System.Windows.Forms.Button();
            this.NewFileButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.LogoPanel.SuspendLayout();
            this.ButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Logo
            // 
            this.Logo.Image = global::LeahsPlatinumTracker.Resources.PictureResources.Logo;
            this.Logo.Location = new System.Drawing.Point(15, 15);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(505, 298);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Logo.TabIndex = 0;
            this.Logo.TabStop = false;
            // 
            // LogoPanel
            // 
            this.LogoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.LogoPanel.Controls.Add(this.VersionLabel);
            this.LogoPanel.Controls.Add(this.Logo);
            this.LogoPanel.Location = new System.Drawing.Point(17, 17);
            this.LogoPanel.Name = "LogoPanel";
            this.LogoPanel.Size = new System.Drawing.Size(535, 328);
            this.LogoPanel.TabIndex = 1;
            // 
            // VersionLabel
            // 
            this.VersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Font = new System.Drawing.Font("Pokemon DPPt", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.VersionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.VersionLabel.Location = new System.Drawing.Point(464, 292);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(55, 20);
            this.VersionLabel.TabIndex = 3;
            this.VersionLabel.Text = "vN.N.N";
            this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.ButtonPanel.Controls.Add(this.LoadFileButton);
            this.ButtonPanel.Controls.Add(this.NewFileButton);
            this.ButtonPanel.Location = new System.Drawing.Point(17, 351);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(535, 98);
            this.ButtonPanel.TabIndex = 2;
            // 
            // LoadFileButton
            // 
            this.LoadFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(183)))), ((int)(((byte)(214)))));
            this.LoadFileButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(146)))), ((int)(((byte)(190)))));
            this.LoadFileButton.FlatAppearance.BorderSize = 2;
            this.LoadFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadFileButton.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LoadFileButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(82)))), ((int)(((byte)(129)))));
            this.LoadFileButton.Location = new System.Drawing.Point(15, 53);
            this.LoadFileButton.Name = "LoadFileButton";
            this.LoadFileButton.Size = new System.Drawing.Size(505, 35);
            this.LoadFileButton.TabIndex = 70;
            this.LoadFileButton.Text = "Load File";
            this.LoadFileButton.UseCompatibleTextRendering = true;
            this.LoadFileButton.UseVisualStyleBackColor = false;
            this.LoadFileButton.Click += new System.EventHandler(this.LoadFileButton_Click);
            // 
            // NewFileButton
            // 
            this.NewFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(183)))), ((int)(((byte)(214)))));
            this.NewFileButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(146)))), ((int)(((byte)(190)))));
            this.NewFileButton.FlatAppearance.BorderSize = 2;
            this.NewFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewFileButton.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.NewFileButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(82)))), ((int)(((byte)(129)))));
            this.NewFileButton.Location = new System.Drawing.Point(15, 10);
            this.NewFileButton.Name = "NewFileButton";
            this.NewFileButton.Size = new System.Drawing.Size(505, 35);
            this.NewFileButton.TabIndex = 69;
            this.NewFileButton.Text = "New File";
            this.NewFileButton.UseCompatibleTextRendering = true;
            this.NewFileButton.UseVisualStyleBackColor = false;
            this.NewFileButton.Click += new System.EventHandler(this.NewFileButton_Click);
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(569, 461);
            this.Controls.Add(this.ButtonPanel);
            this.Controls.Add(this.LogoPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Index";
            this.Text = "Leah\'s Platinum Tracker";
            this.Load += new System.EventHandler(this.Index_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.LogoPanel.ResumeLayout(false);
            this.LogoPanel.PerformLayout();
            this.ButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox Logo;
        private Panel LogoPanel;
        private Panel ButtonPanel;
        private Label VersionLabel;
        private Button LoadFileButton;
        private Button NewFileButton;
    }
}