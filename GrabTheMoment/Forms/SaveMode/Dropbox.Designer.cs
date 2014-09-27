namespace GrabTheMoment.Savemode.Forms
{
    partial class Dropbox
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
            this.RequestAccessButton = new System.Windows.Forms.Button();
            this.CheckAccessButton = new System.Windows.Forms.Button();
            this.UploadLabel = new System.Windows.Forms.CheckBox();
            this.ReceivedData = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RequestAccessButton
            // 
            this.RequestAccessButton.Location = new System.Drawing.Point(12, 12);
            this.RequestAccessButton.Name = "RequestAccessButton";
            this.RequestAccessButton.Size = new System.Drawing.Size(103, 23);
            this.RequestAccessButton.TabIndex = 3;
            this.RequestAccessButton.Text = "Access to GTM";
            this.RequestAccessButton.UseVisualStyleBackColor = true;
            this.RequestAccessButton.Click += new System.EventHandler(this.RequestAccessButton_Click);
            // 
            // CheckAccessButton
            // 
            this.CheckAccessButton.Location = new System.Drawing.Point(12, 41);
            this.CheckAccessButton.Name = "CheckAccessButton";
            this.CheckAccessButton.Size = new System.Drawing.Size(103, 22);
            this.CheckAccessButton.TabIndex = 4;
            this.CheckAccessButton.Text = "Check the access";
            this.CheckAccessButton.UseVisualStyleBackColor = true;
            this.CheckAccessButton.Click += new System.EventHandler(this.CheckAccessButton_Click);
            // 
            // UploadLabel
            // 
            this.UploadLabel.AutoSize = true;
            this.UploadLabel.Enabled = false;
            this.UploadLabel.Location = new System.Drawing.Point(32, 69);
            this.UploadLabel.Name = "UploadLabel";
            this.UploadLabel.Size = new System.Drawing.Size(60, 17);
            this.UploadLabel.TabIndex = 6;
            this.UploadLabel.Text = "Upload";
            this.UploadLabel.UseVisualStyleBackColor = true;
            this.UploadLabel.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ReceivedData
            // 
            this.ReceivedData.AutoSize = true;
            this.ReceivedData.Location = new System.Drawing.Point(46, 46);
            this.ReceivedData.Name = "ReceivedData";
            this.ReceivedData.Size = new System.Drawing.Size(35, 13);
            this.ReceivedData.TabIndex = 7;
            this.ReceivedData.Text = "label1";
            this.ReceivedData.Visible = false;
            // 
            // Dropbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(127, 89);
            this.Controls.Add(this.ReceivedData);
            this.Controls.Add(this.UploadLabel);
            this.Controls.Add(this.CheckAccessButton);
            this.Controls.Add(this.RequestAccessButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Dropbox";
            this.Text = "Dropbox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Dropbox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RequestAccessButton;
        private System.Windows.Forms.Button CheckAccessButton;
        private System.Windows.Forms.CheckBox UploadLabel;
        private System.Windows.Forms.Label ReceivedData;
    }
}