namespace GrabTheMoment.Forms
{
    partial class SettingsForm
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
            this.treeViewSettings = new System.Windows.Forms.TreeView();
            this.panelGeneral = new System.Windows.Forms.Panel();
            this.labelImgurAnon = new System.Windows.Forms.Label();
            this.labelDropbox = new System.Windows.Forms.Label();
            this.labelFTP = new System.Windows.Forms.Label();
            this.labelLocal = new System.Windows.Forms.Label();
            this.checkBoxStCImgurAnon = new System.Windows.Forms.CheckBox();
            this.checkBoxStCDropbox = new System.Windows.Forms.CheckBox();
            this.checkBoxStCFTP = new System.Windows.Forms.CheckBox();
            this.checkBoxStCLocal = new System.Windows.Forms.CheckBox();
            this.checkBoxOnOffImgurAnon = new System.Windows.Forms.CheckBox();
            this.checkBoxOnOffDropbox = new System.Windows.Forms.CheckBox();
            this.checkBoxOnOffFTP = new System.Windows.Forms.CheckBox();
            this.checkBoxOnOffLocal = new System.Windows.Forms.CheckBox();
            this.labelSaveToClipboard = new System.Windows.Forms.Label();
            this.labelOnOff = new System.Windows.Forms.Label();
            this.labelSettingsName = new System.Windows.Forms.Label();
            this.panelNotFound = new System.Windows.Forms.Panel();
            this.labelNotFound = new System.Windows.Forms.Label();
            this.panelGeneral.SuspendLayout();
            this.panelNotFound.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewSettings
            // 
            this.treeViewSettings.Location = new System.Drawing.Point(13, 13);
            this.treeViewSettings.Name = "treeViewSettings";
            this.treeViewSettings.Size = new System.Drawing.Size(121, 311);
            this.treeViewSettings.TabIndex = 0;
            this.treeViewSettings.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSettings_AfterSelect);
            // 
            // panelGeneral
            // 
            this.panelGeneral.Controls.Add(this.labelImgurAnon);
            this.panelGeneral.Controls.Add(this.labelDropbox);
            this.panelGeneral.Controls.Add(this.labelFTP);
            this.panelGeneral.Controls.Add(this.labelLocal);
            this.panelGeneral.Controls.Add(this.checkBoxStCImgurAnon);
            this.panelGeneral.Controls.Add(this.checkBoxStCDropbox);
            this.panelGeneral.Controls.Add(this.checkBoxStCFTP);
            this.panelGeneral.Controls.Add(this.checkBoxStCLocal);
            this.panelGeneral.Controls.Add(this.checkBoxOnOffImgurAnon);
            this.panelGeneral.Controls.Add(this.checkBoxOnOffDropbox);
            this.panelGeneral.Controls.Add(this.checkBoxOnOffFTP);
            this.panelGeneral.Controls.Add(this.checkBoxOnOffLocal);
            this.panelGeneral.Controls.Add(this.labelSaveToClipboard);
            this.panelGeneral.Controls.Add(this.labelOnOff);
            this.panelGeneral.Location = new System.Drawing.Point(141, 40);
            this.panelGeneral.Name = "panelGeneral";
            this.panelGeneral.Size = new System.Drawing.Size(572, 284);
            this.panelGeneral.TabIndex = 1;
            this.panelGeneral.Visible = false;
            // 
            // labelImgurAnon
            // 
            this.labelImgurAnon.AutoSize = true;
            this.labelImgurAnon.Location = new System.Drawing.Point(12, 98);
            this.labelImgurAnon.Name = "labelImgurAnon";
            this.labelImgurAnon.Size = new System.Drawing.Size(91, 13);
            this.labelImgurAnon.TabIndex = 13;
            this.labelImgurAnon.Text = "Imgur (Anonymus)";
            // 
            // labelDropbox
            // 
            this.labelDropbox.AutoSize = true;
            this.labelDropbox.Location = new System.Drawing.Point(12, 78);
            this.labelDropbox.Name = "labelDropbox";
            this.labelDropbox.Size = new System.Drawing.Size(47, 13);
            this.labelDropbox.TabIndex = 12;
            this.labelDropbox.Text = "Dropbox";
            // 
            // labelFTP
            // 
            this.labelFTP.AutoSize = true;
            this.labelFTP.Location = new System.Drawing.Point(12, 58);
            this.labelFTP.Name = "labelFTP";
            this.labelFTP.Size = new System.Drawing.Size(27, 13);
            this.labelFTP.TabIndex = 11;
            this.labelFTP.Text = "FTP";
            // 
            // labelLocal
            // 
            this.labelLocal.AutoSize = true;
            this.labelLocal.Location = new System.Drawing.Point(12, 38);
            this.labelLocal.Name = "labelLocal";
            this.labelLocal.Size = new System.Drawing.Size(33, 13);
            this.labelLocal.TabIndex = 10;
            this.labelLocal.Text = "Local";
            // 
            // checkBoxStCImgurAnon
            // 
            this.checkBoxStCImgurAnon.AutoSize = true;
            this.checkBoxStCImgurAnon.Enabled = false;
            this.checkBoxStCImgurAnon.Location = new System.Drawing.Point(230, 98);
            this.checkBoxStCImgurAnon.Name = "checkBoxStCImgurAnon";
            this.checkBoxStCImgurAnon.Size = new System.Drawing.Size(15, 14);
            this.checkBoxStCImgurAnon.TabIndex = 9;
            this.checkBoxStCImgurAnon.UseVisualStyleBackColor = true;
            this.checkBoxStCImgurAnon.CheckedChanged += new System.EventHandler(this.checkBoxStCImgurAnon_CheckedChanged);
            this.checkBoxStCImgurAnon.EnabledChanged += new System.EventHandler(this.checkBoxStCImgurAnon_EnabledChanged);
            // 
            // checkBoxStCDropbox
            // 
            this.checkBoxStCDropbox.AutoSize = true;
            this.checkBoxStCDropbox.Enabled = false;
            this.checkBoxStCDropbox.Location = new System.Drawing.Point(230, 78);
            this.checkBoxStCDropbox.Name = "checkBoxStCDropbox";
            this.checkBoxStCDropbox.Size = new System.Drawing.Size(15, 14);
            this.checkBoxStCDropbox.TabIndex = 8;
            this.checkBoxStCDropbox.UseVisualStyleBackColor = true;
            this.checkBoxStCDropbox.CheckedChanged += new System.EventHandler(this.checkBoxStCDropbox_CheckedChanged);
            this.checkBoxStCDropbox.EnabledChanged += new System.EventHandler(this.checkBoxStCDropbox_EnabledChanged);
            // 
            // checkBoxStCFTP
            // 
            this.checkBoxStCFTP.AutoSize = true;
            this.checkBoxStCFTP.Enabled = false;
            this.checkBoxStCFTP.Location = new System.Drawing.Point(230, 58);
            this.checkBoxStCFTP.Name = "checkBoxStCFTP";
            this.checkBoxStCFTP.Size = new System.Drawing.Size(15, 14);
            this.checkBoxStCFTP.TabIndex = 7;
            this.checkBoxStCFTP.UseVisualStyleBackColor = true;
            this.checkBoxStCFTP.CheckedChanged += new System.EventHandler(this.checkBoxStCFTP_CheckedChanged);
            this.checkBoxStCFTP.EnabledChanged += new System.EventHandler(this.checkBoxStCFTP_EnabledChanged);
            // 
            // checkBoxStCLocal
            // 
            this.checkBoxStCLocal.AutoSize = true;
            this.checkBoxStCLocal.Enabled = false;
            this.checkBoxStCLocal.Location = new System.Drawing.Point(230, 38);
            this.checkBoxStCLocal.Name = "checkBoxStCLocal";
            this.checkBoxStCLocal.Size = new System.Drawing.Size(15, 14);
            this.checkBoxStCLocal.TabIndex = 6;
            this.checkBoxStCLocal.UseVisualStyleBackColor = true;
            this.checkBoxStCLocal.CheckedChanged += new System.EventHandler(this.checkBoxStCLocal_CheckedChanged);
            this.checkBoxStCLocal.EnabledChanged += new System.EventHandler(this.checkBoxStCLocal_EnabledChanged);
            // 
            // checkBoxOnOffImgurAnon
            // 
            this.checkBoxOnOffImgurAnon.AutoSize = true;
            this.checkBoxOnOffImgurAnon.Location = new System.Drawing.Point(121, 98);
            this.checkBoxOnOffImgurAnon.Name = "checkBoxOnOffImgurAnon";
            this.checkBoxOnOffImgurAnon.Size = new System.Drawing.Size(15, 14);
            this.checkBoxOnOffImgurAnon.TabIndex = 5;
            this.checkBoxOnOffImgurAnon.UseVisualStyleBackColor = true;
            this.checkBoxOnOffImgurAnon.CheckedChanged += new System.EventHandler(this.checkBoxOnOffImgurAnon_CheckedChanged);
            // 
            // checkBoxOnOffDropbox
            // 
            this.checkBoxOnOffDropbox.AutoSize = true;
            this.checkBoxOnOffDropbox.Location = new System.Drawing.Point(121, 78);
            this.checkBoxOnOffDropbox.Name = "checkBoxOnOffDropbox";
            this.checkBoxOnOffDropbox.Size = new System.Drawing.Size(15, 14);
            this.checkBoxOnOffDropbox.TabIndex = 4;
            this.checkBoxOnOffDropbox.UseVisualStyleBackColor = true;
            this.checkBoxOnOffDropbox.CheckedChanged += new System.EventHandler(this.checkBoxOnOffDropbox_CheckedChanged);
            // 
            // checkBoxOnOffFTP
            // 
            this.checkBoxOnOffFTP.AutoSize = true;
            this.checkBoxOnOffFTP.Location = new System.Drawing.Point(121, 58);
            this.checkBoxOnOffFTP.Name = "checkBoxOnOffFTP";
            this.checkBoxOnOffFTP.Size = new System.Drawing.Size(15, 14);
            this.checkBoxOnOffFTP.TabIndex = 3;
            this.checkBoxOnOffFTP.UseVisualStyleBackColor = true;
            this.checkBoxOnOffFTP.CheckedChanged += new System.EventHandler(this.checkBoxOnOffFTP_CheckedChanged);
            // 
            // checkBoxOnOffLocal
            // 
            this.checkBoxOnOffLocal.AutoSize = true;
            this.checkBoxOnOffLocal.Location = new System.Drawing.Point(121, 38);
            this.checkBoxOnOffLocal.Name = "checkBoxOnOffLocal";
            this.checkBoxOnOffLocal.Size = new System.Drawing.Size(15, 14);
            this.checkBoxOnOffLocal.TabIndex = 2;
            this.checkBoxOnOffLocal.UseVisualStyleBackColor = true;
            this.checkBoxOnOffLocal.CheckedChanged += new System.EventHandler(this.checkBoxOnOffLocal_CheckedChanged);
            // 
            // labelSaveToClipboard
            // 
            this.labelSaveToClipboard.AutoSize = true;
            this.labelSaveToClipboard.Location = new System.Drawing.Point(193, 13);
            this.labelSaveToClipboard.Name = "labelSaveToClipboard";
            this.labelSaveToClipboard.Size = new System.Drawing.Size(91, 13);
            this.labelSaveToClipboard.TabIndex = 1;
            this.labelSaveToClipboard.Text = "Save to Clipboard";
            // 
            // labelOnOff
            // 
            this.labelOnOff.AutoSize = true;
            this.labelOnOff.Location = new System.Drawing.Point(108, 13);
            this.labelOnOff.Name = "labelOnOff";
            this.labelOnOff.Size = new System.Drawing.Size(48, 13);
            this.labelOnOff.TabIndex = 0;
            this.labelOnOff.Text = "ON/OFF";
            // 
            // labelSettingsName
            // 
            this.labelSettingsName.AutoSize = true;
            this.labelSettingsName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSettingsName.Location = new System.Drawing.Point(141, 13);
            this.labelSettingsName.Name = "labelSettingsName";
            this.labelSettingsName.Size = new System.Drawing.Size(184, 24);
            this.labelSettingsName.TabIndex = 2;
            this.labelSettingsName.Text = "labelSettingsName";
            // 
            // panelNotFound
            // 
            this.panelNotFound.Controls.Add(this.labelNotFound);
            this.panelNotFound.Location = new System.Drawing.Point(141, 40);
            this.panelNotFound.Name = "panelNotFound";
            this.panelNotFound.Size = new System.Drawing.Size(572, 284);
            this.panelNotFound.TabIndex = 0;
            this.panelNotFound.Visible = false;
            // 
            // labelNotFound
            // 
            this.labelNotFound.AutoSize = true;
            this.labelNotFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNotFound.Location = new System.Drawing.Point(56, 102);
            this.labelNotFound.Name = "labelNotFound";
            this.labelNotFound.Size = new System.Drawing.Size(454, 55);
            this.labelNotFound.TabIndex = 0;
            this.labelNotFound.Text = "under development";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 336);
            this.Controls.Add(this.labelSettingsName);
            this.Controls.Add(this.treeViewSettings);
            this.Controls.Add(this.panelGeneral);
            this.Controls.Add(this.panelNotFound);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.panelGeneral.ResumeLayout(false);
            this.panelGeneral.PerformLayout();
            this.panelNotFound.ResumeLayout(false);
            this.panelNotFound.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewSettings;
        private System.Windows.Forms.Panel panelGeneral;
        private System.Windows.Forms.Label labelSettingsName;
        private System.Windows.Forms.Panel panelNotFound;
        private System.Windows.Forms.Label labelNotFound;
        private System.Windows.Forms.Label labelOnOff;
        private System.Windows.Forms.Label labelSaveToClipboard;
        private System.Windows.Forms.Label labelImgurAnon;
        private System.Windows.Forms.Label labelDropbox;
        private System.Windows.Forms.Label labelFTP;
        private System.Windows.Forms.Label labelLocal;
        private System.Windows.Forms.CheckBox checkBoxStCImgurAnon;
        private System.Windows.Forms.CheckBox checkBoxStCDropbox;
        private System.Windows.Forms.CheckBox checkBoxStCFTP;
        private System.Windows.Forms.CheckBox checkBoxStCLocal;
        private System.Windows.Forms.CheckBox checkBoxOnOffImgurAnon;
        private System.Windows.Forms.CheckBox checkBoxOnOffDropbox;
        private System.Windows.Forms.CheckBox checkBoxOnOffFTP;
        private System.Windows.Forms.CheckBox checkBoxOnOffLocal;
    }
}