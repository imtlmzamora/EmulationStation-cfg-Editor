
namespace EmulatioStation_cfg_Editor
{
    partial class mainForm
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
            this.btnAddLauncher = new System.Windows.Forms.Button();
            this.btnDltLauncher = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstbx_Launchers = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadEsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setRomPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstbx_Systems = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_fullscrn = new System.Windows.Forms.CheckBox();
            this.chkbox_bash = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtbx_theme = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtbx_platform = new System.Windows.Forms.TextBox();
            this.cmbx_libretro = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbx_launcher = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbx_Extensions = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbx_FullName = new System.Windows.Forms.TextBox();
            this.cmbx_platform = new System.Windows.Forms.ComboBox();
            this.txtbx_GamesPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_GamesPath = new System.Windows.Forms.Button();
            this.lblSystemName = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.rTxtBx_SystemPreview = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddLauncher
            // 
            this.btnAddLauncher.Enabled = false;
            this.btnAddLauncher.Location = new System.Drawing.Point(6, 19);
            this.btnAddLauncher.Name = "btnAddLauncher";
            this.btnAddLauncher.Size = new System.Drawing.Size(98, 23);
            this.btnAddLauncher.TabIndex = 1;
            this.btnAddLauncher.Text = "add Launcher";
            this.btnAddLauncher.UseVisualStyleBackColor = true;
            this.btnAddLauncher.Click += new System.EventHandler(this.btnAddLauncher_Click);
            // 
            // btnDltLauncher
            // 
            this.btnDltLauncher.Enabled = false;
            this.btnDltLauncher.Location = new System.Drawing.Point(6, 48);
            this.btnDltLauncher.Name = "btnDltLauncher";
            this.btnDltLauncher.Size = new System.Drawing.Size(98, 23);
            this.btnDltLauncher.TabIndex = 2;
            this.btnDltLauncher.Text = "Delete Launcher";
            this.btnDltLauncher.UseVisualStyleBackColor = true;
            this.btnDltLauncher.Click += new System.EventHandler(this.btnDltLauncher_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstbx_Launchers);
            this.groupBox1.Controls.Add(this.btnAddLauncher);
            this.groupBox1.Controls.Add(this.btnDltLauncher);
            this.groupBox1.Location = new System.Drawing.Point(12, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 151);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Launchers";
            // 
            // lstbx_Launchers
            // 
            this.lstbx_Launchers.FormattingEnabled = true;
            this.lstbx_Launchers.Location = new System.Drawing.Point(110, 19);
            this.lstbx_Launchers.Name = "lstbx_Launchers";
            this.lstbx_Launchers.Size = new System.Drawing.Size(168, 121);
            this.lstbx_Launchers.Sorted = true;
            this.lstbx_Launchers.TabIndex = 4;
            this.lstbx_Launchers.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(834, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadEsToolStripMenuItem,
            this.setRomPathToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadEsToolStripMenuItem
            // 
            this.loadEsToolStripMenuItem.Name = "loadEsToolStripMenuItem";
            this.loadEsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.loadEsToolStripMenuItem.Text = "set es_systems.cfg";
            this.loadEsToolStripMenuItem.Click += new System.EventHandler(this.loadEsToolStripMenuItem_Click);
            // 
            // setRomPathToolStripMenuItem
            // 
            this.setRomPathToolStripMenuItem.Name = "setRomPathToolStripMenuItem";
            this.setRomPathToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.setRomPathToolStripMenuItem.Text = "set Rom Path";
            // 
            // lstbx_Systems
            // 
            this.lstbx_Systems.FormattingEnabled = true;
            this.lstbx_Systems.Location = new System.Drawing.Point(6, 19);
            this.lstbx_Systems.Name = "lstbx_Systems";
            this.lstbx_Systems.Size = new System.Drawing.Size(98, 368);
            this.lstbx_Systems.TabIndex = 5;
            this.lstbx_Systems.SelectedIndexChanged += new System.EventHandler(this.lstbx_Systems_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_fullscrn);
            this.groupBox2.Controls.Add(this.chkbox_bash);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtbx_theme);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtbx_platform);
            this.groupBox2.Controls.Add(this.cmbx_libretro);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbx_launcher);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtbx_Extensions);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtbx_FullName);
            this.groupBox2.Controls.Add(this.cmbx_platform);
            this.groupBox2.Controls.Add(this.txtbx_GamesPath);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btn_GamesPath);
            this.groupBox2.Controls.Add(this.lblSystemName);
            this.groupBox2.Controls.Add(this.lstbx_Systems);
            this.groupBox2.Location = new System.Drawing.Point(12, 201);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 398);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Setting Systems";
            // 
            // chk_fullscrn
            // 
            this.chk_fullscrn.AutoSize = true;
            this.chk_fullscrn.Location = new System.Drawing.Point(175, 350);
            this.chk_fullscrn.Name = "chk_fullscrn";
            this.chk_fullscrn.Size = new System.Drawing.Size(79, 17);
            this.chk_fullscrn.TabIndex = 25;
            this.chk_fullscrn.Text = "Full Screen";
            this.chk_fullscrn.UseVisualStyleBackColor = true;
            // 
            // chkbox_bash
            // 
            this.chkbox_bash.AutoSize = true;
            this.chkbox_bash.Location = new System.Drawing.Point(114, 350);
            this.chkbox_bash.Name = "chkbox_bash";
            this.chkbox_bash.Size = new System.Drawing.Size(55, 17);
            this.chkbox_bash.TabIndex = 24;
            this.chkbox_bash.Text = "bash?";
            this.chkbox_bash.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(111, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "theme";
            // 
            // txtbx_theme
            // 
            this.txtbx_theme.Location = new System.Drawing.Point(114, 289);
            this.txtbx_theme.Name = "txtbx_theme";
            this.txtbx_theme.ReadOnly = true;
            this.txtbx_theme.Size = new System.Drawing.Size(179, 20);
            this.txtbx_theme.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(111, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "platform";
            // 
            // txtbx_platform
            // 
            this.txtbx_platform.Location = new System.Drawing.Point(114, 234);
            this.txtbx_platform.Name = "txtbx_platform";
            this.txtbx_platform.ReadOnly = true;
            this.txtbx_platform.Size = new System.Drawing.Size(179, 20);
            this.txtbx_platform.TabIndex = 20;
            // 
            // cmbx_libretro
            // 
            this.cmbx_libretro.FormattingEnabled = true;
            this.cmbx_libretro.Location = new System.Drawing.Point(278, 135);
            this.cmbx_libretro.Name = "cmbx_libretro";
            this.cmbx_libretro.Size = new System.Drawing.Size(121, 21);
            this.cmbx_libretro.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(277, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Core";
            // 
            // cmbx_launcher
            // 
            this.cmbx_launcher.FormattingEnabled = true;
            this.cmbx_launcher.Location = new System.Drawing.Point(112, 135);
            this.cmbx_launcher.Name = "cmbx_launcher";
            this.cmbx_launcher.Size = new System.Drawing.Size(121, 21);
            this.cmbx_launcher.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Launcher";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Extensions";
            // 
            // txtbx_Extensions
            // 
            this.txtbx_Extensions.Location = new System.Drawing.Point(114, 185);
            this.txtbx_Extensions.Name = "txtbx_Extensions";
            this.txtbx_Extensions.Size = new System.Drawing.Size(285, 20);
            this.txtbx_Extensions.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Full Name";
            // 
            // txtbx_FullName
            // 
            this.txtbx_FullName.Location = new System.Drawing.Point(220, 39);
            this.txtbx_FullName.Name = "txtbx_FullName";
            this.txtbx_FullName.ReadOnly = true;
            this.txtbx_FullName.Size = new System.Drawing.Size(179, 20);
            this.txtbx_FullName.TabIndex = 12;
            // 
            // cmbx_platform
            // 
            this.cmbx_platform.FormattingEnabled = true;
            this.cmbx_platform.Location = new System.Drawing.Point(110, 38);
            this.cmbx_platform.Name = "cmbx_platform";
            this.cmbx_platform.Size = new System.Drawing.Size(86, 21);
            this.cmbx_platform.TabIndex = 11;
            this.cmbx_platform.SelectedIndexChanged += new System.EventHandler(this.cmbx_platform_SelectedIndexChanged);
            // 
            // txtbx_GamesPath
            // 
            this.txtbx_GamesPath.Location = new System.Drawing.Point(110, 84);
            this.txtbx_GamesPath.Name = "txtbx_GamesPath";
            this.txtbx_GamesPath.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtbx_GamesPath.Size = new System.Drawing.Size(223, 20);
            this.txtbx_GamesPath.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Games Path";
            // 
            // btn_GamesPath
            // 
            this.btn_GamesPath.Enabled = false;
            this.btn_GamesPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GamesPath.Location = new System.Drawing.Point(371, 83);
            this.btn_GamesPath.Name = "btn_GamesPath";
            this.btn_GamesPath.Size = new System.Drawing.Size(28, 20);
            this.btn_GamesPath.TabIndex = 8;
            this.btn_GamesPath.Text = "...";
            this.btn_GamesPath.UseVisualStyleBackColor = true;
            // 
            // lblSystemName
            // 
            this.lblSystemName.AutoSize = true;
            this.lblSystemName.Location = new System.Drawing.Point(109, 21);
            this.lblSystemName.Name = "lblSystemName";
            this.lblSystemName.Size = new System.Drawing.Size(76, 13);
            this.lblSystemName.TabIndex = 6;
            this.lblSystemName.Text = "Platform Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(428, 201);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "System Preview";
            // 
            // rTxtBx_SystemPreview
            // 
            this.rTxtBx_SystemPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rTxtBx_SystemPreview.Location = new System.Drawing.Point(431, 217);
            this.rTxtBx_SystemPreview.Name = "rTxtBx_SystemPreview";
            this.rTxtBx_SystemPreview.ReadOnly = true;
            this.rTxtBx_SystemPreview.Size = new System.Drawing.Size(391, 293);
            this.rTxtBx_SystemPreview.TabIndex = 28;
            this.rTxtBx_SystemPreview.Text = "";
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(724, 516);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(98, 23);
            this.button3.TabIndex = 29;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(428, 576);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(98, 23);
            this.button4.TabIndex = 30;
            this.button4.Text = "Delete";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(428, 516);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(98, 23);
            this.btnUpdate.TabIndex = 31;
            this.btnUpdate.Text = "update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 611);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.rTxtBx_SystemPreview);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAddLauncher;
        private System.Windows.Forms.Button btnDltLauncher;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstbx_Launchers;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadEsToolStripMenuItem;
        private System.Windows.Forms.ListBox lstbx_Systems;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem setRomPathToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmbx_platform;
        private System.Windows.Forms.TextBox txtbx_GamesPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_GamesPath;
        private System.Windows.Forms.Label lblSystemName;
        private System.Windows.Forms.ComboBox cmbx_launcher;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbx_Extensions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbx_FullName;
        private System.Windows.Forms.ComboBox cmbx_libretro;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chk_fullscrn;
        private System.Windows.Forms.CheckBox chkbox_bash;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtbx_theme;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtbx_platform;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox rTxtBx_SystemPreview;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button btnUpdate;
    }
}

