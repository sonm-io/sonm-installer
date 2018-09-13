namespace SonmInstaller
{
    partial class WizardFormDesign
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.tabs = new SonmInstaller.WizardPages();
            this.step0 = new System.Windows.Forms.TabPage();
            this.lblHeader0 = new System.Windows.Forms.Label();
            this.step1choose = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.radioHasWallet = new System.Windows.Forms.RadioButton();
            this.radioNoWallet = new System.Windows.Forms.RadioButton();
            this.lblHeader1 = new System.Windows.Forms.Label();
            this.step2a1 = new System.Windows.Forms.TabPage();
            this.lblNewKeyErrorMessage = new System.Windows.Forms.Label();
            this.linkChooseDir = new System.Windows.Forms.LinkLabel();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbPasswordRepeat = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblHeader2a1 = new System.Windows.Forms.Label();
            this.step2a2 = new System.Windows.Forms.TabPage();
            this.linkOpenKeyFile = new System.Windows.Forms.LinkLabel();
            this.linkOpenKeyDir = new System.Windows.Forms.LinkLabel();
            this.lblSavedKeyPath = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblHeader2a2 = new System.Windows.Forms.Label();
            this.step2b1 = new System.Windows.Forms.TabPage();
            this.lblHeader2b = new System.Windows.Forms.Label();
            this.btnSelectKeyFile = new System.Windows.Forms.Button();
            this.step2b2 = new System.Windows.Forms.TabPage();
            this.label20 = new System.Windows.Forms.Label();
            this.tbSelectedKeyPassword = new System.Windows.Forms.TextBox();
            this.lblLoadedKeyPath = new System.Windows.Forms.Label();
            this.linkSelectKeyFile = new System.Windows.Forms.LinkLabel();
            this.lblHeader2b2 = new System.Windows.Forms.Label();
            this.step3out = new System.Windows.Forms.TabPage();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.tbThresholdAmount = new System.Windows.Forms.TextBox();
            this.tbAddressToSend = new System.Windows.Forms.TextBox();
            this.lblHeader3 = new System.Windows.Forms.Label();
            this.step4disk = new System.Windows.Forms.TabPage();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbDisk = new System.Windows.Forms.ComboBox();
            this.lblHeader4 = new System.Windows.Forms.Label();
            this.step5progress = new System.Windows.Forms.TabPage();
            this.progressBar = new SonmInstaller.UI.ProgressBar();
            this.lblHeader5 = new System.Windows.Forms.Label();
            this.step6fin = new System.Windows.Forms.TabPage();
            this.linkFaq = new System.Windows.Forms.LinkLabel();
            this.label25 = new System.Windows.Forms.Label();
            this.lblHeader6 = new System.Windows.Forms.Label();
            this.progressBarBottom = new SonmInstaller.UI.ProgressBar();
            this.panel1.SuspendLayout();
            this.tabs.SuspendLayout();
            this.step0.SuspendLayout();
            this.step1choose.SuspendLayout();
            this.step2a1.SuspendLayout();
            this.step2a2.SuspendLayout();
            this.step2b1.SuspendLayout();
            this.step2b2.SuspendLayout();
            this.step3out.SuspendLayout();
            this.step4disk.SuspendLayout();
            this.step5progress.SuspendLayout();
            this.step6fin.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBarBottom);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 405);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 70);
            this.panel1.TabIndex = 4;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(489, 15);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(130, 43);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(634, 15);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(130, 43);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.step0);
            this.tabs.Controls.Add(this.step1choose);
            this.tabs.Controls.Add(this.step2a1);
            this.tabs.Controls.Add(this.step2a2);
            this.tabs.Controls.Add(this.step2b1);
            this.tabs.Controls.Add(this.step2b2);
            this.tabs.Controls.Add(this.step3out);
            this.tabs.Controls.Add(this.step4disk);
            this.tabs.Controls.Add(this.step5progress);
            this.tabs.Controls.Add(this.step6fin);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(776, 405);
            this.tabs.TabIndex = 5;
            this.tabs.Tag = "header";
            // 
            // step0
            // 
            this.step0.BackColor = System.Drawing.SystemColors.Control;
            this.step0.Controls.Add(this.lblHeader0);
            this.step0.Location = new System.Drawing.Point(4, 25);
            this.step0.Name = "step0";
            this.step0.Padding = new System.Windows.Forms.Padding(3);
            this.step0.Size = new System.Drawing.Size(768, 376);
            this.step0.TabIndex = 0;
            this.step0.Text = "step0";
            // 
            // lblHeader0
            // 
            this.lblHeader0.AutoSize = true;
            this.lblHeader0.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader0.Location = new System.Drawing.Point(9, 16);
            this.lblHeader0.Name = "lblHeader0";
            this.lblHeader0.Size = new System.Drawing.Size(347, 31);
            this.lblHeader0.TabIndex = 0;
            this.lblHeader0.Tag = "header";
            this.lblHeader0.Text = "Welcome to SONM Installer";
            // 
            // step1choose
            // 
            this.step1choose.BackColor = System.Drawing.SystemColors.Control;
            this.step1choose.Controls.Add(this.label11);
            this.step1choose.Controls.Add(this.label10);
            this.step1choose.Controls.Add(this.radioHasWallet);
            this.step1choose.Controls.Add(this.radioNoWallet);
            this.step1choose.Controls.Add(this.lblHeader1);
            this.step1choose.Location = new System.Drawing.Point(4, 25);
            this.step1choose.Name = "step1choose";
            this.step1choose.Padding = new System.Windows.Forms.Padding(3);
            this.step1choose.Size = new System.Drawing.Size(768, 376);
            this.step1choose.TabIndex = 1;
            this.step1choose.Text = "step1choose";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(34, 174);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(333, 17);
            this.label11.TabIndex = 3;
            this.label11.Text = "You need to specify your json-file with key next step";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(294, 17);
            this.label10.TabIndex = 3;
            this.label10.Text = "We generate for you your personal wallet key";
            // 
            // radioHasWallet
            // 
            this.radioHasWallet.AutoSize = true;
            this.radioHasWallet.Location = new System.Drawing.Point(15, 150);
            this.radioHasWallet.Name = "radioHasWallet";
            this.radioHasWallet.Size = new System.Drawing.Size(99, 21);
            this.radioHasWallet.TabIndex = 2;
            this.radioHasWallet.Text = "Yes, I have";
            this.radioHasWallet.UseVisualStyleBackColor = true;
            // 
            // radioNoWallet
            // 
            this.radioNoWallet.AutoSize = true;
            this.radioNoWallet.Checked = true;
            this.radioNoWallet.Location = new System.Drawing.Point(15, 90);
            this.radioNoWallet.Name = "radioNoWallet";
            this.radioNoWallet.Size = new System.Drawing.Size(235, 21);
            this.radioNoWallet.TabIndex = 2;
            this.radioNoWallet.TabStop = true;
            this.radioNoWallet.Text = "No, I do not have etherium wallet";
            this.radioNoWallet.UseVisualStyleBackColor = true;
            // 
            // lblHeader1
            // 
            this.lblHeader1.AutoSize = true;
            this.lblHeader1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader1.Location = new System.Drawing.Point(9, 16);
            this.lblHeader1.Name = "lblHeader1";
            this.lblHeader1.Size = new System.Drawing.Size(647, 31);
            this.lblHeader1.TabIndex = 1;
            this.lblHeader1.Tag = "header";
            this.lblHeader1.Text = "Step {0} of {1}: Do you already have etherium wallet?";
            // 
            // step2a1
            // 
            this.step2a1.BackColor = System.Drawing.SystemColors.Control;
            this.step2a1.Controls.Add(this.lblNewKeyErrorMessage);
            this.step2a1.Controls.Add(this.linkChooseDir);
            this.step2a1.Controls.Add(this.label16);
            this.step2a1.Controls.Add(this.label15);
            this.step2a1.Controls.Add(this.label14);
            this.step2a1.Controls.Add(this.label13);
            this.step2a1.Controls.Add(this.label12);
            this.step2a1.Controls.Add(this.tbPasswordRepeat);
            this.step2a1.Controls.Add(this.tbPassword);
            this.step2a1.Controls.Add(this.lblHeader2a1);
            this.step2a1.Location = new System.Drawing.Point(4, 25);
            this.step2a1.Name = "step2a1";
            this.step2a1.Padding = new System.Windows.Forms.Padding(3);
            this.step2a1.Size = new System.Drawing.Size(768, 376);
            this.step2a1.TabIndex = 2;
            this.step2a1.Text = "step2a1";
            // 
            // lblNewKeyErrorMessage
            // 
            this.lblNewKeyErrorMessage.AutoSize = true;
            this.lblNewKeyErrorMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNewKeyErrorMessage.Location = new System.Drawing.Point(246, 124);
            this.lblNewKeyErrorMessage.Name = "lblNewKeyErrorMessage";
            this.lblNewKeyErrorMessage.Size = new System.Drawing.Size(100, 17);
            this.lblNewKeyErrorMessage.TabIndex = 7;
            this.lblNewKeyErrorMessage.Text = "error message";
            this.lblNewKeyErrorMessage.Visible = false;
            // 
            // linkChooseDir
            // 
            this.linkChooseDir.AutoSize = true;
            this.linkChooseDir.Location = new System.Drawing.Point(12, 301);
            this.linkChooseDir.Name = "linkChooseDir";
            this.linkChooseDir.Size = new System.Drawing.Size(168, 17);
            this.linkChooseDir.TabIndex = 6;
            this.linkChooseDir.TabStop = true;
            this.linkChooseDir.Text = "Choose another directory";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 274);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(90, 17);
            this.label16.TabIndex = 5;
            this.label16.Text = "c:/users/user";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 245);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(293, 17);
            this.label15.TabIndex = 5;
            this.label15.Text = "json file with key will be saved to this location:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 60);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(219, 17);
            this.label14.TabIndex = 5;
            this.label14.Text = "Set password for your private key";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 156);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(118, 17);
            this.label13.TabIndex = 4;
            this.label13.Text = "Repeat password";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 98);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 17);
            this.label12.TabIndex = 4;
            this.label12.Text = "Password";
            // 
            // tbPasswordRepeat
            // 
            this.tbPasswordRepeat.Location = new System.Drawing.Point(15, 179);
            this.tbPasswordRepeat.Name = "tbPasswordRepeat";
            this.tbPasswordRepeat.Size = new System.Drawing.Size(191, 22);
            this.tbPasswordRepeat.TabIndex = 3;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(15, 121);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(191, 22);
            this.tbPassword.TabIndex = 3;
            // 
            // lblHeader2a1
            // 
            this.lblHeader2a1.AutoSize = true;
            this.lblHeader2a1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader2a1.Location = new System.Drawing.Point(9, 16);
            this.lblHeader2a1.Name = "lblHeader2a1";
            this.lblHeader2a1.Size = new System.Drawing.Size(377, 31);
            this.lblHeader2a1.TabIndex = 2;
            this.lblHeader2a1.Tag = "header";
            this.lblHeader2a1.Text = "Step {0} of {1}: Key generation";
            // 
            // step2a2
            // 
            this.step2a2.BackColor = System.Drawing.SystemColors.Control;
            this.step2a2.Controls.Add(this.linkOpenKeyFile);
            this.step2a2.Controls.Add(this.linkOpenKeyDir);
            this.step2a2.Controls.Add(this.lblSavedKeyPath);
            this.step2a2.Controls.Add(this.label17);
            this.step2a2.Controls.Add(this.lblHeader2a2);
            this.step2a2.Location = new System.Drawing.Point(4, 25);
            this.step2a2.Name = "step2a2";
            this.step2a2.Padding = new System.Windows.Forms.Padding(3);
            this.step2a2.Size = new System.Drawing.Size(768, 376);
            this.step2a2.TabIndex = 3;
            this.step2a2.Text = "step2a2";
            // 
            // linkOpenKeyFile
            // 
            this.linkOpenKeyFile.AutoSize = true;
            this.linkOpenKeyFile.Location = new System.Drawing.Point(15, 167);
            this.linkOpenKeyFile.Name = "linkOpenKeyFile";
            this.linkOpenKeyFile.Size = new System.Drawing.Size(65, 17);
            this.linkOpenKeyFile.TabIndex = 4;
            this.linkOpenKeyFile.TabStop = true;
            this.linkOpenKeyFile.Text = "Open file";
            // 
            // linkOpenKeyDir
            // 
            this.linkOpenKeyDir.AutoSize = true;
            this.linkOpenKeyDir.Location = new System.Drawing.Point(15, 139);
            this.linkOpenKeyDir.Name = "linkOpenKeyDir";
            this.linkOpenKeyDir.Size = new System.Drawing.Size(102, 17);
            this.linkOpenKeyDir.TabIndex = 4;
            this.linkOpenKeyDir.TabStop = true;
            this.linkOpenKeyDir.Text = "Open directory";
            // 
            // lblSavedKeyPath
            // 
            this.lblSavedKeyPath.AutoSize = true;
            this.lblSavedKeyPath.Location = new System.Drawing.Point(15, 103);
            this.lblSavedKeyPath.Name = "lblSavedKeyPath";
            this.lblSavedKeyPath.Size = new System.Drawing.Size(162, 17);
            this.lblSavedKeyPath.TabIndex = 3;
            this.lblSavedKeyPath.Text = "c:\\users\\user\\...\\key.json";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 62);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(195, 17);
            this.label17.TabIndex = 3;
            this.label17.Text = "json-file with key saved to file:";
            // 
            // lblHeader2a2
            // 
            this.lblHeader2a2.AutoSize = true;
            this.lblHeader2a2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader2a2.Location = new System.Drawing.Point(9, 16);
            this.lblHeader2a2.Name = "lblHeader2a2";
            this.lblHeader2a2.Size = new System.Drawing.Size(367, 31);
            this.lblHeader2a2.TabIndex = 2;
            this.lblHeader2a2.Tag = "header";
            this.lblHeader2a2.Text = "Step {0} of {1}: Save your key";
            // 
            // step2b1
            // 
            this.step2b1.BackColor = System.Drawing.SystemColors.Control;
            this.step2b1.Controls.Add(this.lblHeader2b);
            this.step2b1.Controls.Add(this.btnSelectKeyFile);
            this.step2b1.Location = new System.Drawing.Point(4, 25);
            this.step2b1.Name = "step2b1";
            this.step2b1.Padding = new System.Windows.Forms.Padding(3);
            this.step2b1.Size = new System.Drawing.Size(768, 376);
            this.step2b1.TabIndex = 4;
            this.step2b1.Text = "step2b1";
            // 
            // lblHeader2b
            // 
            this.lblHeader2b.AutoSize = true;
            this.lblHeader2b.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader2b.Location = new System.Drawing.Point(9, 16);
            this.lblHeader2b.Name = "lblHeader2b";
            this.lblHeader2b.Size = new System.Drawing.Size(538, 31);
            this.lblHeader2b.TabIndex = 2;
            this.lblHeader2b.Tag = "header";
            this.lblHeader2b.Text = "Step {0} of {1}: Select json-file with your key";
            // 
            // btnSelectKeyFile
            // 
            this.btnSelectKeyFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectKeyFile.Location = new System.Drawing.Point(15, 73);
            this.btnSelectKeyFile.Name = "btnSelectKeyFile";
            this.btnSelectKeyFile.Size = new System.Drawing.Size(130, 43);
            this.btnSelectKeyFile.TabIndex = 0;
            this.btnSelectKeyFile.Text = "Import key";
            this.btnSelectKeyFile.UseVisualStyleBackColor = true;
            // 
            // step2b2
            // 
            this.step2b2.BackColor = System.Drawing.SystemColors.Control;
            this.step2b2.Controls.Add(this.label20);
            this.step2b2.Controls.Add(this.tbSelectedKeyPassword);
            this.step2b2.Controls.Add(this.lblLoadedKeyPath);
            this.step2b2.Controls.Add(this.linkSelectKeyFile);
            this.step2b2.Controls.Add(this.lblHeader2b2);
            this.step2b2.Location = new System.Drawing.Point(4, 25);
            this.step2b2.Name = "step2b2";
            this.step2b2.Size = new System.Drawing.Size(768, 376);
            this.step2b2.TabIndex = 9;
            this.step2b2.Text = "step2b2";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(15, 151);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(69, 17);
            this.label20.TabIndex = 10;
            this.label20.Text = "Password";
            // 
            // tbSelectedKeyPassword
            // 
            this.tbSelectedKeyPassword.Location = new System.Drawing.Point(15, 174);
            this.tbSelectedKeyPassword.Name = "tbSelectedKeyPassword";
            this.tbSelectedKeyPassword.Size = new System.Drawing.Size(191, 22);
            this.tbSelectedKeyPassword.TabIndex = 9;
            // 
            // lblLoadedKeyPath
            // 
            this.lblLoadedKeyPath.AutoSize = true;
            this.lblLoadedKeyPath.Location = new System.Drawing.Point(12, 73);
            this.lblLoadedKeyPath.Name = "lblLoadedKeyPath";
            this.lblLoadedKeyPath.Size = new System.Drawing.Size(146, 17);
            this.lblLoadedKeyPath.TabIndex = 8;
            this.lblLoadedKeyPath.Text = "c:\\users\\user\\key.json";
            // 
            // linkSelectKeyFile
            // 
            this.linkSelectKeyFile.AutoSize = true;
            this.linkSelectKeyFile.Location = new System.Drawing.Point(12, 99);
            this.linkSelectKeyFile.Name = "linkSelectKeyFile";
            this.linkSelectKeyFile.Size = new System.Drawing.Size(109, 17);
            this.linkSelectKeyFile.TabIndex = 7;
            this.linkSelectKeyFile.TabStop = true;
            this.linkSelectKeyFile.Text = "Choose another";
            // 
            // lblHeader2b2
            // 
            this.lblHeader2b2.AutoSize = true;
            this.lblHeader2b2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader2b2.Location = new System.Drawing.Point(9, 16);
            this.lblHeader2b2.Name = "lblHeader2b2";
            this.lblHeader2b2.Size = new System.Drawing.Size(387, 31);
            this.lblHeader2b2.TabIndex = 3;
            this.lblHeader2b2.Tag = "header";
            this.lblHeader2b2.Text = "Step {0} of {1}: Enter Password";
            // 
            // step3out
            // 
            this.step3out.BackColor = System.Drawing.SystemColors.Control;
            this.step3out.Controls.Add(this.label22);
            this.step3out.Controls.Add(this.label21);
            this.step3out.Controls.Add(this.tbThresholdAmount);
            this.step3out.Controls.Add(this.tbAddressToSend);
            this.step3out.Controls.Add(this.lblHeader3);
            this.step3out.Location = new System.Drawing.Point(4, 25);
            this.step3out.Name = "step3out";
            this.step3out.Padding = new System.Windows.Forms.Padding(3);
            this.step3out.Size = new System.Drawing.Size(768, 376);
            this.step3out.TabIndex = 5;
            this.step3out.Text = "step3out";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(15, 181);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(231, 17);
            this.label22.TabIndex = 4;
            this.label22.Text = "The amount by which to draw funds";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(15, 88);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(60, 17);
            this.label21.TabIndex = 4;
            this.label21.Text = "Address";
            // 
            // tbThresholdAmount
            // 
            this.tbThresholdAmount.Location = new System.Drawing.Point(15, 201);
            this.tbThresholdAmount.Name = "tbThresholdAmount";
            this.tbThresholdAmount.Size = new System.Drawing.Size(385, 22);
            this.tbThresholdAmount.TabIndex = 3;
            // 
            // tbAddressToSend
            // 
            this.tbAddressToSend.Location = new System.Drawing.Point(15, 111);
            this.tbAddressToSend.Name = "tbAddressToSend";
            this.tbAddressToSend.Size = new System.Drawing.Size(385, 22);
            this.tbAddressToSend.TabIndex = 3;
            // 
            // lblHeader3
            // 
            this.lblHeader3.AutoSize = true;
            this.lblHeader3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader3.Location = new System.Drawing.Point(9, 16);
            this.lblHeader3.Name = "lblHeader3";
            this.lblHeader3.Size = new System.Drawing.Size(473, 31);
            this.lblHeader3.TabIndex = 2;
            this.lblHeader3.Text = "Step {0} of {1}: Where to send money?";
            // 
            // step4disk
            // 
            this.step4disk.BackColor = System.Drawing.SystemColors.Control;
            this.step4disk.Controls.Add(this.label23);
            this.step4disk.Controls.Add(this.cmbDisk);
            this.step4disk.Controls.Add(this.lblHeader4);
            this.step4disk.Location = new System.Drawing.Point(4, 25);
            this.step4disk.Name = "step4disk";
            this.step4disk.Padding = new System.Windows.Forms.Padding(3);
            this.step4disk.Size = new System.Drawing.Size(768, 376);
            this.step4disk.TabIndex = 6;
            this.step4disk.Text = "step4disk";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(15, 103);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(76, 17);
            this.label23.TabIndex = 4;
            this.label23.Text = "Select disk";
            // 
            // cmbDisk
            // 
            this.cmbDisk.FormattingEnabled = true;
            this.cmbDisk.Location = new System.Drawing.Point(15, 126);
            this.cmbDisk.Name = "cmbDisk";
            this.cmbDisk.Size = new System.Drawing.Size(296, 24);
            this.cmbDisk.TabIndex = 3;
            // 
            // lblHeader4
            // 
            this.lblHeader4.AutoSize = true;
            this.lblHeader4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader4.Location = new System.Drawing.Point(9, 16);
            this.lblHeader4.Name = "lblHeader4";
            this.lblHeader4.Size = new System.Drawing.Size(337, 31);
            this.lblHeader4.TabIndex = 2;
            this.lblHeader4.Tag = "header";
            this.lblHeader4.Text = "Step {0} of {1}: Select drive";
            // 
            // step5progress
            // 
            this.step5progress.BackColor = System.Drawing.SystemColors.Control;
            this.step5progress.Controls.Add(this.progressBar);
            this.step5progress.Controls.Add(this.lblHeader5);
            this.step5progress.Location = new System.Drawing.Point(4, 25);
            this.step5progress.Name = "step5progress";
            this.step5progress.Padding = new System.Windows.Forms.Padding(3);
            this.step5progress.Size = new System.Drawing.Size(768, 376);
            this.step5progress.TabIndex = 7;
            this.step5progress.Text = "step5progress";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.LabelTpl = null;
            this.progressBar.Location = new System.Drawing.Point(15, 187);
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressCurrent = 0;
            this.progressBar.ProgressTotal = 0;
            this.progressBar.Size = new System.Drawing.Size(736, 45);
            this.progressBar.TabIndex = 3;
            // 
            // lblHeader5
            // 
            this.lblHeader5.AutoSize = true;
            this.lblHeader5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader5.Location = new System.Drawing.Point(9, 16);
            this.lblHeader5.Name = "lblHeader5";
            this.lblHeader5.Size = new System.Drawing.Size(249, 31);
            this.lblHeader5.TabIndex = 2;
            this.lblHeader5.Tag = "header";
            this.lblHeader5.Text = "Step {0} of {1}: Wait";
            // 
            // step6fin
            // 
            this.step6fin.BackColor = System.Drawing.SystemColors.Control;
            this.step6fin.Controls.Add(this.linkFaq);
            this.step6fin.Controls.Add(this.label25);
            this.step6fin.Controls.Add(this.lblHeader6);
            this.step6fin.Location = new System.Drawing.Point(4, 25);
            this.step6fin.Name = "step6fin";
            this.step6fin.Padding = new System.Windows.Forms.Padding(3);
            this.step6fin.Size = new System.Drawing.Size(768, 376);
            this.step6fin.TabIndex = 8;
            this.step6fin.Text = "step6fin";
            // 
            // linkFaq
            // 
            this.linkFaq.AutoSize = true;
            this.linkFaq.LinkArea = new System.Windows.Forms.LinkArea(93, 3);
            this.linkFaq.Location = new System.Drawing.Point(18, 88);
            this.linkFaq.Name = "linkFaq";
            this.linkFaq.Size = new System.Drawing.Size(572, 20);
            this.linkFaq.TabIndex = 4;
            this.linkFaq.TabStop = true;
            this.linkFaq.Text = "Image successfully written to flash drive. Now you can load from it. For more det" +
    "ails see in FAQ.";
            this.linkFaq.UseCompatibleTextRendering = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(15, 51);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(0, 17);
            this.label25.TabIndex = 3;
            // 
            // lblHeader6
            // 
            this.lblHeader6.AutoSize = true;
            this.lblHeader6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader6.Location = new System.Drawing.Point(9, 16);
            this.lblHeader6.Name = "lblHeader6";
            this.lblHeader6.Size = new System.Drawing.Size(342, 31);
            this.lblHeader6.TabIndex = 2;
            this.lblHeader6.Tag = "header";
            this.lblHeader6.Text = "Setup successfully finished";
            // 
            // progressBarBottom
            // 
            this.progressBarBottom.LabelTpl = "Downloading: {0} of {1} bytes";
            this.progressBarBottom.Location = new System.Drawing.Point(19, 15);
            this.progressBarBottom.Name = "progressBarBottom";
            this.progressBarBottom.ProgressCurrent = 0;
            this.progressBarBottom.ProgressTotal = 100;
            this.progressBarBottom.Size = new System.Drawing.Size(454, 45);
            this.progressBarBottom.TabIndex = 1;
            // 
            // WizardFormDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 475);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "WizardFormDesign";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.tabs.ResumeLayout(false);
            this.step0.ResumeLayout(false);
            this.step0.PerformLayout();
            this.step1choose.ResumeLayout(false);
            this.step1choose.PerformLayout();
            this.step2a1.ResumeLayout(false);
            this.step2a1.PerformLayout();
            this.step2a2.ResumeLayout(false);
            this.step2a2.PerformLayout();
            this.step2b1.ResumeLayout(false);
            this.step2b1.PerformLayout();
            this.step2b2.ResumeLayout(false);
            this.step2b2.PerformLayout();
            this.step3out.ResumeLayout(false);
            this.step3out.PerformLayout();
            this.step4disk.ResumeLayout(false);
            this.step4disk.PerformLayout();
            this.step5progress.ResumeLayout(false);
            this.step5progress.PerformLayout();
            this.step6fin.ResumeLayout(false);
            this.step6fin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnBack;
        public System.Windows.Forms.Button btnNext;
        public UI.ProgressBar progressBarBottom;
        public System.Windows.Forms.TabPage step6fin;
        public System.Windows.Forms.LinkLabel linkFaq;
        public System.Windows.Forms.Label label25;
        public System.Windows.Forms.Label lblHeader6;
        public System.Windows.Forms.TabPage step5progress;
        public UI.ProgressBar progressBar;
        public System.Windows.Forms.Label lblHeader5;
        public System.Windows.Forms.TabPage step4disk;
        public System.Windows.Forms.Label label23;
        public System.Windows.Forms.ComboBox cmbDisk;
        public System.Windows.Forms.Label lblHeader4;
        public System.Windows.Forms.TabPage step3out;
        public System.Windows.Forms.Label label22;
        public System.Windows.Forms.Label label21;
        public System.Windows.Forms.TextBox tbThresholdAmount;
        public System.Windows.Forms.TextBox tbAddressToSend;
        public System.Windows.Forms.Label lblHeader3;
        public System.Windows.Forms.TabPage step2a2;
        public System.Windows.Forms.LinkLabel linkOpenKeyFile;
        public System.Windows.Forms.LinkLabel linkOpenKeyDir;
        public System.Windows.Forms.Label lblSavedKeyPath;
        public System.Windows.Forms.Label label17;
        public System.Windows.Forms.Label lblHeader2a2;
        public System.Windows.Forms.TabPage step2a1;
        public System.Windows.Forms.Label lblNewKeyErrorMessage;
        public System.Windows.Forms.LinkLabel linkChooseDir;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.Label label15;
        public System.Windows.Forms.Label label14;
        public System.Windows.Forms.Label label13;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox tbPasswordRepeat;
        public System.Windows.Forms.TextBox tbPassword;
        public System.Windows.Forms.Label lblHeader2a1;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.RadioButton radioHasWallet;
        public System.Windows.Forms.RadioButton radioNoWallet;
        public System.Windows.Forms.Label lblHeader1;
        public System.Windows.Forms.TabPage step0;
        public System.Windows.Forms.Label lblHeader0;
        public WizardPages tabs;
        public System.Windows.Forms.TabPage step2b1;
        public System.Windows.Forms.Label lblHeader2b;
        public System.Windows.Forms.Button btnSelectKeyFile;
        public System.Windows.Forms.TabPage step2b2;
        public System.Windows.Forms.Label label20;
        public System.Windows.Forms.TextBox tbSelectedKeyPassword;
        public System.Windows.Forms.Label lblLoadedKeyPath;
        public System.Windows.Forms.LinkLabel linkSelectKeyFile;
        public System.Windows.Forms.Label lblHeader2b2;
        public System.Windows.Forms.TabPage step1choose;
    }
}

