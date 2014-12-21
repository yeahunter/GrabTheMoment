using GrabTheMoment;
using GrabTheMoment.Properties;
using System;
using System.Windows.Forms;

namespace GrabTheMoment.Forms
{
    public partial class SettingsForm : Form
    {
        private CheckBox _SelectedCheckBox;

        public SettingsForm()
        {
            InitializeComponent();
            System.Windows.Forms.TreeNode treeNodeGeneral = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Local");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("FTP");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Dropbox");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Imgur (Anonymus)");
            System.Windows.Forms.TreeNode treeNodeSaveMode = new System.Windows.Forms.TreeNode("SaveMode", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            treeNodeGeneral.Name = "NodeGeneral";
            treeNodeGeneral.Text = "General";
            treeNode2.Name = "NodeSaveModeLocal";
            treeNode2.Text = "Local";
            treeNode3.Name = "NodeSaveModeFTP";
            treeNode3.Text = "FTP";
            treeNode4.Name = "NodeSaveModeDropbox";
            treeNode4.Text = "Dropbox";
            treeNode5.Name = "NodeSaveModeImgurAnon";
            treeNode5.Text = "ImgurAnon";
            treeNodeSaveMode.Name = "NodeSaveMode";
            treeNodeSaveMode.Text = "SaveMode";
            this.treeViewSettings.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNodeGeneral,
            treeNodeSaveMode});
            treeNodeSaveMode.ExpandAll();

            checkBoxOnOffLocal.Tag = General.SaveMode.Local;
            checkBoxOnOffFTP.Tag = General.SaveMode.FTP;
            checkBoxOnOffDropbox.Tag = General.SaveMode.Dropbox;
            checkBoxOnOffImgurAnon.Tag = General.SaveMode.ImgurAnon;

            checkBoxStCLocal.Tag = General.SaveMode.Local;
            checkBoxStCFTP.Tag = General.SaveMode.FTP;
            checkBoxStCDropbox.Tag = General.SaveMode.Dropbox;
            checkBoxStCImgurAnon.Tag = General.SaveMode.ImgurAnon;

            // IDEIGLENESEN
            // treeNodeSaveMode.Remove();
            // treeNodeGeneral.Remove();
            LoadConfig();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            //labelSettingsName.Text = treeViewSettings.SelectedNode.Text;
        }

        private void treeViewSettings_MouseClick(object sender, MouseEventArgs e)
        {
        }

        // Beallitja a labelSettingsName-t a szulo node-ok alapjan
        private void SettingsNameGenerator (TreeNode Node)
        {
            if (Node != null && Node.GetType() == typeof(TreeNode))
            {
                labelSettingsName.Text = Node.Text + " - " + labelSettingsName.Text;
                SettingsNameGenerator(Node.Parent);
            }
        }

        // Azt a Panel-t helyezi eloterbe, amelyik Node aktiv
        private void ShowPanel(TreeNode Node)
        {
            panelGeneral.Visible = false;
            panelNotFound.Visible = false;

            switch (Node.Text)
            {
                case "General":
                    panelGeneral.Visible = true;
                    break;
                default:
                    panelNotFound.Visible = true;
                    break;
            }
        }

        private void LoadConfig()
        {
            checkBoxStCLocal.Enabled = checkBoxOnOffLocal.Checked = Settings.Default.MLocal;
            checkBoxStCFTP.Enabled = checkBoxOnOffFTP.Checked = Settings.Default.MFtp;
            checkBoxStCDropbox.Enabled = checkBoxOnOffDropbox.Checked = Settings.Default.MDropbox;
            checkBoxStCImgurAnon.Enabled = checkBoxOnOffImgurAnon.Checked = Settings.Default.MImgur;

            switch ((General.SaveMode)Settings.Default.CopyLink)
            {
                case General.SaveMode.Local:
                    checkBoxStCLocal.Checked = true;
                    break;
                case General.SaveMode.FTP:
                    checkBoxStCFTP.Checked = true;
                    break;
                case General.SaveMode.Dropbox:
                    checkBoxStCDropbox.Checked = true;
                    break;
                case General.SaveMode.ImgurAnon:
                    checkBoxStCImgurAnon.Checked = true;
                    break;
            }
        }

        private void LoadFunction(CheckBox CB)
        {
            General.SaveMode Type = (General.SaveMode)Enum.Parse(typeof(General.SaveMode), CB.Tag.ToString());

            switch (Type)
            {
                case General.SaveMode.Local:
                    Settings.Default.MLocal = checkBoxStCLocal.Enabled = CB.Checked;
                    break;
                case General.SaveMode.FTP:
                    Settings.Default.MFtp = checkBoxStCFTP.Enabled = CB.Checked;
                    break;
                case General.SaveMode.Dropbox:
                    Settings.Default.MDropbox = checkBoxStCDropbox.Enabled = CB.Checked;
                    break;
                case General.SaveMode.ImgurAnon:
                    Settings.Default.MImgur = checkBoxStCImgurAnon.Enabled = CB.Checked;
                    break;
            }

            Settings.Default.Save();
        }

        // A kikapcsolt CheckBox-okbol kiszedi a pipat
        private void UncheckedAfterDisable (CheckBox CB)
        {
            if (!CB.Enabled && CB.Checked)
                CB.Checked = false;
        }

        // Kivalasztja a megadott tipust, es az alapjan hasznaljuk a vagolapot
        private void SelectClipboard(CheckBox CB)
        {
            if (_SelectedCheckBox == null)
                _SelectedCheckBox = CB;

            if (_SelectedCheckBox == CB)
            {
                General.SaveMode Type = General.SaveMode.Disabled;

                // a _SelectedCheckBox kivetelevel mindegyik Vagolapos checkbox pipajat kiszedjuk
                if (_SelectedCheckBox != checkBoxStCLocal)
                    checkBoxStCLocal.Checked = false;
                if (_SelectedCheckBox != checkBoxStCFTP)
                    checkBoxStCFTP.Checked = false;
                if (_SelectedCheckBox != checkBoxStCDropbox)
                    checkBoxStCDropbox.Checked = false;
                if (_SelectedCheckBox != checkBoxOnOffImgurAnon)
                    checkBoxOnOffImgurAnon.Checked = false;

                if (CB.Checked)
                    Type = (General.SaveMode)Enum.Parse(typeof(General.SaveMode), CB.Tag.ToString());

                General.SetClipboardType(Type);

                _SelectedCheckBox = null;
            }
        }

        private void treeViewSettings_AfterSelect(object sender, TreeViewEventArgs e)
        {
            labelSettingsName.Text = e.Node.Text;
            SettingsNameGenerator(e.Node.Parent);
            ShowPanel(e.Node);
        }

        private void checkBoxOnOffLocal_CheckedChanged(object sender, EventArgs e)
        {
            LoadFunction((CheckBox)sender);
        }

        private void checkBoxOnOffFTP_CheckedChanged(object sender, EventArgs e)
        {
            LoadFunction((CheckBox)sender);
        }

        private void checkBoxOnOffDropbox_CheckedChanged(object sender, EventArgs e)
        {
            LoadFunction((CheckBox)sender);
        }

        private void checkBoxOnOffImgurAnon_CheckedChanged(object sender, EventArgs e)
        {
            LoadFunction((CheckBox)sender);
        }

        private void checkBoxStCLocal_CheckedChanged(object sender, EventArgs e)
        {
            SelectClipboard((CheckBox)sender);
        }

        private void checkBoxStCFTP_CheckedChanged(object sender, EventArgs e)
        {
            SelectClipboard((CheckBox)sender);
        }

        private void checkBoxStCDropbox_CheckedChanged(object sender, EventArgs e)
        {
            SelectClipboard((CheckBox)sender);
        }

        private void checkBoxStCImgurAnon_CheckedChanged(object sender, EventArgs e)
        {
            SelectClipboard((CheckBox)sender);
        }

        private void checkBoxStCLocal_EnabledChanged(object sender, EventArgs e)
        {
            UncheckedAfterDisable((CheckBox)sender);
        }

        private void checkBoxStCFTP_EnabledChanged(object sender, EventArgs e)
        {
            UncheckedAfterDisable((CheckBox)sender);
        }

        private void checkBoxStCDropbox_EnabledChanged(object sender, EventArgs e)
        {
            UncheckedAfterDisable((CheckBox)sender);
        }

        private void checkBoxStCImgurAnon_EnabledChanged(object sender, EventArgs e)
        {
            UncheckedAfterDisable((CheckBox)sender);
        }
    }
}
