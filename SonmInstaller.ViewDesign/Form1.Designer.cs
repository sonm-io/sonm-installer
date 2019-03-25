namespace SonmInstaller
{
    partial class WizardFormDesign
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

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
        public void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardFormDesign));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.progressBarBottom = new SonmInstaller.UI.ProgressBar();
            this.btnNext = new System.Windows.Forms.Button();
            this.saveNewKey = new System.Windows.Forms.SaveFileDialog();
            this.loader = new System.Windows.Forms.ProgressBar();
            this.openKeystore = new System.Windows.Forms.OpenFileDialog();
            this.tabs = new SonmInstaller.WizardPages();
            this.step0 = new System.Windows.Forms.TabPage();
            this.linkWelcome = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblHeader0 = new System.Windows.Forms.Label();
            this.lblWelcomeError = new System.Windows.Forms.Label();
            this.step1choose = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.radioHasWallet = new System.Windows.Forms.RadioButton();
            this.radioNoWallet = new System.Windows.Forms.RadioButton();
            this.lblHeader1 = new System.Windows.Forms.Label();
            this.step2a1 = new System.Windows.Forms.TabPage();
            this.lblNewKeyErrorMessage = new System.Windows.Forms.Label();
            this.linkNewKeyPath = new System.Windows.Forms.LinkLabel();
            this.lblNewKeyPath = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbPasswordRepeat = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblHeader2a1 = new System.Windows.Forms.Label();
            this.step2a2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.tbThresholdAmount = new System.Windows.Forms.TextBox();
            this.tbAddressToSend = new System.Windows.Forms.TextBox();
            this.lblHeader3 = new System.Windows.Forms.Label();
            this.step4disk = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbDisk = new System.Windows.Forms.ComboBox();
            this.lblHeader4 = new System.Windows.Forms.Label();
            this.step5progress = new System.Windows.Forms.TabPage();
            this.pnlErrorDownload = new System.Windows.Forms.Panel();
            this.btnTryAgainProgress = new System.Windows.Forms.Button();
            this.lblDownloadError = new System.Windows.Forms.Label();
            this.progressBar = new SonmInstaller.UI.ProgressBar();
            this.lblHeader5 = new System.Windows.Forms.Label();
            this.step6fin = new System.Windows.Forms.TabPage();
            this.linkFaq = new System.Windows.Forms.LinkLabel();
            this.label25 = new System.Windows.Forms.Label();
            this.lblHeader6 = new System.Windows.Forms.Label();
            this.messagePage = new System.Windows.Forms.TabPage();
            this.btnMessagePageTryAgain = new System.Windows.Forms.Button();
            this.lblMessagePageText = new System.Windows.Forms.Label();
            this.lblMessagePageHeader = new System.Windows.Forms.Label();
            this.checkUpdateDist = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.tabs.SuspendLayout();
            this.step0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.step1choose.SuspendLayout();
            this.step2a1.SuspendLayout();
            this.step2a2.SuspendLayout();
            this.step2b1.SuspendLayout();
            this.step2b2.SuspendLayout();
            this.step3out.SuspendLayout();
            this.step4disk.SuspendLayout();
            this.step5progress.SuspendLayout();
            this.pnlErrorDownload.SuspendLayout();
            this.step6fin.SuspendLayout();
            this.messagePage.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.progressBarBottom);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 286);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 63);
            this.panel1.TabIndex = 4;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(294, 12);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(78, 41);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // progressBarBottom
            // 
            this.progressBarBottom.LabelTpl = "Download in progress: {0:0.0} of {1:0.0} ({2:0}%)";
            this.progressBarBottom.Location = new System.Drawing.Point(8, 12);
            this.progressBarBottom.Margin = new System.Windows.Forms.Padding(2);
            this.progressBarBottom.Name = "progressBarBottom";
            this.progressBarBottom.ProgressCurrent = 0D;
            this.progressBarBottom.ProgressTotal = 100D;
            this.progressBarBottom.Size = new System.Drawing.Size(281, 41);
            this.progressBarBottom.TabIndex = 1;
            this.progressBarBottom.Visible = false;
            this.progressBarBottom.Load += new System.EventHandler(this.progressBarBottom_Load);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(374, 12);
            this.btnNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(78, 41);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // loader
            // 
            this.loader.Location = new System.Drawing.Point(294, 260);
            this.loader.Margin = new System.Windows.Forms.Padding(2);
            this.loader.MarqueeAnimationSpeed = 25;
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(158, 19);
            this.loader.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.loader.TabIndex = 6;
            this.loader.Visible = false;
            // 
            // openKeystore
            // 
            this.openKeystore.FileName = "openFileDialog1";
            this.openKeystore.Filter = "Json files|*.json|All files|*.*";
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
            this.tabs.Controls.Add(this.messagePage);
            this.tabs.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Margin = new System.Windows.Forms.Padding(2);
            this.tabs.Multiline = true;
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(464, 349);
            this.tabs.TabIndex = 5;
            this.tabs.Tag = "header";
            // 
            // step0
            // 
            this.step0.BackColor = System.Drawing.SystemColors.Control;
            this.step0.Controls.Add(this.linkWelcome);
            this.step0.Controls.Add(this.pictureBox1);
            this.step0.Controls.Add(this.lblHeader0);
            this.step0.Controls.Add(this.lblWelcomeError);
            this.step0.Location = new System.Drawing.Point(4, 40);
            this.step0.Margin = new System.Windows.Forms.Padding(2);
            this.step0.Name = "step0";
            this.step0.Padding = new System.Windows.Forms.Padding(2);
            this.step0.Size = new System.Drawing.Size(456, 305);
            this.step0.TabIndex = 0;
            this.step0.Text = "step0";
            // 
            // linkWelcome
            // 
            this.linkWelcome.AutoSize = true;
            this.linkWelcome.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.linkWelcome.Location = new System.Drawing.Point(9, 166);
            this.linkWelcome.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkWelcome.Name = "linkWelcome";
            this.linkWelcome.Size = new System.Drawing.Size(302, 78);
            this.linkWelcome.TabIndex = 2;
            this.linkWelcome.Text = resources.GetString("linkWelcome.Text");
            this.linkWelcome.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 55);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(440, 83);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lblHeader0
            // 
            this.lblHeader0.AutoSize = true;
            this.lblHeader0.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader0.Location = new System.Drawing.Point(7, 13);
            this.lblHeader0.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeader0.Name = "lblHeader0";
            this.lblHeader0.Size = new System.Drawing.Size(282, 26);
            this.lblHeader0.TabIndex = 0;
            this.lblHeader0.Tag = "header";
            this.lblHeader0.Text = "Welcome to SONM Installer";
            // 
            // lblWelcomeError
            // 
            this.lblWelcomeError.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcomeError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblWelcomeError.Location = new System.Drawing.Point(11, 157);
            this.lblWelcomeError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWelcomeError.Name = "lblWelcomeError";
            this.lblWelcomeError.Size = new System.Drawing.Size(438, 91);
            this.lblWelcomeError.TabIndex = 3;
            this.lblWelcomeError.Text = "SONM Installer needs elevated rights to partition your USB drive. Please restart " +
    "installer as Administrator.";
            this.lblWelcomeError.Visible = false;
            // 
            // step1choose
            // 
            this.step1choose.BackColor = System.Drawing.SystemColors.Control;
            this.step1choose.Controls.Add(this.label11);
            this.step1choose.Controls.Add(this.label10);
            this.step1choose.Controls.Add(this.radioHasWallet);
            this.step1choose.Controls.Add(this.radioNoWallet);
            this.step1choose.Controls.Add(this.lblHeader1);
            this.step1choose.Location = new System.Drawing.Point(4, 40);
            this.step1choose.Margin = new System.Windows.Forms.Padding(2);
            this.step1choose.Name = "step1choose";
            this.step1choose.Padding = new System.Windows.Forms.Padding(2);
            this.step1choose.Size = new System.Drawing.Size(456, 305);
            this.step1choose.TabIndex = 1;
            this.step1choose.Text = "step1choose";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(26, 141);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(329, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "You will need your keystore (and password) from previous installation";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(26, 93);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(215, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "We will create new user profile and keystore";
            // 
            // radioHasWallet
            // 
            this.radioHasWallet.AutoSize = true;
            this.radioHasWallet.Location = new System.Drawing.Point(11, 122);
            this.radioHasWallet.Margin = new System.Windows.Forms.Padding(2);
            this.radioHasWallet.Name = "radioHasWallet";
            this.radioHasWallet.Size = new System.Drawing.Size(322, 17);
            this.radioHasWallet.TabIndex = 2;
            this.radioHasWallet.Text = "Yes, and I want to create installation USB-stick for existing user";
            this.radioHasWallet.UseVisualStyleBackColor = true;
            // 
            // radioNoWallet
            // 
            this.radioNoWallet.AutoSize = true;
            this.radioNoWallet.Checked = true;
            this.radioNoWallet.Location = new System.Drawing.Point(11, 73);
            this.radioNoWallet.Margin = new System.Windows.Forms.Padding(2);
            this.radioNoWallet.Name = "radioNoWallet";
            this.radioNoWallet.Size = new System.Drawing.Size(158, 17);
            this.radioNoWallet.TabIndex = 2;
            this.radioNoWallet.TabStop = true;
            this.radioNoWallet.Text = "No, this is my first installation";
            this.radioNoWallet.UseVisualStyleBackColor = true;
            // 
            // lblHeader1
            // 
            this.lblHeader1.AutoSize = true;
            this.lblHeader1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader1.Location = new System.Drawing.Point(7, 13);
            this.lblHeader1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeader1.Name = "lblHeader1";
            this.lblHeader1.Size = new System.Drawing.Size(434, 26);
            this.lblHeader1.TabIndex = 1;
            this.lblHeader1.Tag = "header";
            this.lblHeader1.Text = "Step {0} of {1}. Have you ever used SONM?";
            // 
            // step2a1
            // 
            this.step2a1.BackColor = System.Drawing.SystemColors.Control;
            this.step2a1.Controls.Add(this.lblNewKeyErrorMessage);
            this.step2a1.Controls.Add(this.linkNewKeyPath);
            this.step2a1.Controls.Add(this.lblNewKeyPath);
            this.step2a1.Controls.Add(this.label15);
            this.step2a1.Controls.Add(this.label14);
            this.step2a1.Controls.Add(this.label13);
            this.step2a1.Controls.Add(this.label12);
            this.step2a1.Controls.Add(this.tbPasswordRepeat);
            this.step2a1.Controls.Add(this.tbPassword);
            this.step2a1.Controls.Add(this.lblHeader2a1);
            this.step2a1.Location = new System.Drawing.Point(4, 40);
            this.step2a1.Margin = new System.Windows.Forms.Padding(2);
            this.step2a1.Name = "step2a1";
            this.step2a1.Padding = new System.Windows.Forms.Padding(2);
            this.step2a1.Size = new System.Drawing.Size(456, 305);
            this.step2a1.TabIndex = 2;
            this.step2a1.Text = "step2a1";
            // 
            // lblNewKeyErrorMessage
            // 
            this.lblNewKeyErrorMessage.AutoSize = true;
            this.lblNewKeyErrorMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNewKeyErrorMessage.Location = new System.Drawing.Point(9, 142);
            this.lblNewKeyErrorMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNewKeyErrorMessage.Name = "lblNewKeyErrorMessage";
            this.lblNewKeyErrorMessage.Size = new System.Drawing.Size(73, 13);
            this.lblNewKeyErrorMessage.TabIndex = 7;
            this.lblNewKeyErrorMessage.Text = "error message";
            this.lblNewKeyErrorMessage.Visible = false;
            // 
            // linkNewKeyPath
            // 
            this.linkNewKeyPath.AutoSize = true;
            this.linkNewKeyPath.Location = new System.Drawing.Point(7, 223);
            this.linkNewKeyPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkNewKeyPath.Name = "linkNewKeyPath";
            this.linkNewKeyPath.Size = new System.Drawing.Size(106, 13);
            this.linkNewKeyPath.TabIndex = 6;
            this.linkNewKeyPath.TabStop = true;
            this.linkNewKeyPath.Text = "Choose another path";
            // 
            // lblNewKeyPath
            // 
            this.lblNewKeyPath.Location = new System.Drawing.Point(7, 189);
            this.lblNewKeyPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNewKeyPath.Name = "lblNewKeyPath";
            this.lblNewKeyPath.Size = new System.Drawing.Size(449, 31);
            this.lblNewKeyPath.TabIndex = 5;
            this.lblNewKeyPath.Text = "c:/users/user";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 166);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(336, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "Your UTC/JSON keystore containing your pivate key will be saved to:";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.Location = new System.Drawing.Point(9, 49);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(440, 47);
            this.label14.TabIndex = 5;
            this.label14.Text = "Enter a password.\r\nThis password encrypts your public key. You will need this pas" +
    "sword and keystore to make transactions in SONM blockchain.";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(191, 103);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Repeat password";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 103);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Password";
            // 
            // tbPasswordRepeat
            // 
            this.tbPasswordRepeat.Location = new System.Drawing.Point(191, 122);
            this.tbPasswordRepeat.Margin = new System.Windows.Forms.Padding(2);
            this.tbPasswordRepeat.Name = "tbPasswordRepeat";
            this.tbPasswordRepeat.PasswordChar = '*';
            this.tbPasswordRepeat.Size = new System.Drawing.Size(163, 20);
            this.tbPasswordRepeat.TabIndex = 2;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(9, 122);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(2);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(163, 20);
            this.tbPassword.TabIndex = 1;
            // 
            // lblHeader2a1
            // 
            this.lblHeader2a1.AutoSize = true;
            this.lblHeader2a1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader2a1.Location = new System.Drawing.Point(7, 13);
            this.lblHeader2a1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeader2a1.Name = "lblHeader2a1";
            this.lblHeader2a1.Size = new System.Drawing.Size(352, 26);
            this.lblHeader2a1.TabIndex = 2;
            this.lblHeader2a1.Tag = "header";
            this.lblHeader2a1.Text = "Step {0} of {1}. Generating keystore";
            // 
            // step2a2
            // 
            this.step2a2.BackColor = System.Drawing.SystemColors.Control;
            this.step2a2.Controls.Add(this.label6);
            this.step2a2.Controls.Add(this.label5);
            this.step2a2.Controls.Add(this.label4);
            this.step2a2.Controls.Add(this.label3);
            this.step2a2.Controls.Add(this.label2);
            this.step2a2.Controls.Add(this.label1);
            this.step2a2.Controls.Add(this.linkOpenKeyFile);
            this.step2a2.Controls.Add(this.linkOpenKeyDir);
            this.step2a2.Controls.Add(this.lblSavedKeyPath);
            this.step2a2.Controls.Add(this.label17);
            this.step2a2.Controls.Add(this.lblHeader2a2);
            this.step2a2.Location = new System.Drawing.Point(4, 40);
            this.step2a2.Margin = new System.Windows.Forms.Padding(2);
            this.step2a2.Name = "step2a2";
            this.step2a2.Padding = new System.Windows.Forms.Padding(2);
            this.step2a2.Size = new System.Drawing.Size(456, 305);
            this.step2a2.TabIndex = 3;
            this.step2a2.Text = "step2a2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(101, 141);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(285, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Secure it like the millions of dollars it may one day be worth.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(101, 128);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(340, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Your funds will be stolen if you use this file on a malicious/phishing site.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 114);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "It cannot be recovered if you lose it.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 141);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Make a backup!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 128);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Do not share it!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 114);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Do not lose it!";
            // 
            // linkOpenKeyFile
            // 
            this.linkOpenKeyFile.AutoSize = true;
            this.linkOpenKeyFile.Location = new System.Drawing.Point(9, 188);
            this.linkOpenKeyFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkOpenKeyFile.Name = "linkOpenKeyFile";
            this.linkOpenKeyFile.Size = new System.Drawing.Size(49, 13);
            this.linkOpenKeyFile.TabIndex = 4;
            this.linkOpenKeyFile.TabStop = true;
            this.linkOpenKeyFile.Text = "Open file";
            // 
            // linkOpenKeyDir
            // 
            this.linkOpenKeyDir.AutoSize = true;
            this.linkOpenKeyDir.Location = new System.Drawing.Point(9, 169);
            this.linkOpenKeyDir.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkOpenKeyDir.Name = "linkOpenKeyDir";
            this.linkOpenKeyDir.Size = new System.Drawing.Size(62, 13);
            this.linkOpenKeyDir.TabIndex = 4;
            this.linkOpenKeyDir.TabStop = true;
            this.linkOpenKeyDir.Text = "Open folder";
            // 
            // lblSavedKeyPath
            // 
            this.lblSavedKeyPath.AutoSize = true;
            this.lblSavedKeyPath.Location = new System.Drawing.Point(11, 76);
            this.lblSavedKeyPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSavedKeyPath.Name = "lblSavedKeyPath";
            this.lblSavedKeyPath.Size = new System.Drawing.Size(129, 13);
            this.lblSavedKeyPath.TabIndex = 3;
            this.lblSavedKeyPath.Text = "c:\\users\\user\\...\\key.json";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(11, 50);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(351, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Your UTC/JSON keystore containing your pivate key has been saved to:";
            // 
            // lblHeader2a2
            // 
            this.lblHeader2a2.AutoSize = true;
            this.lblHeader2a2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader2a2.Location = new System.Drawing.Point(7, 13);
            this.lblHeader2a2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeader2a2.Name = "lblHeader2a2";
            this.lblHeader2a2.Size = new System.Drawing.Size(388, 26);
            this.lblHeader2a2.TabIndex = 2;
            this.lblHeader2a2.Tag = "header";
            this.lblHeader2a2.Text = "Step {0} of {1}. Save your Keystore File";
            // 
            // step2b1
            // 
            this.step2b1.BackColor = System.Drawing.SystemColors.Control;
            this.step2b1.Controls.Add(this.lblHeader2b);
            this.step2b1.Controls.Add(this.btnSelectKeyFile);
            this.step2b1.Location = new System.Drawing.Point(4, 40);
            this.step2b1.Margin = new System.Windows.Forms.Padding(2);
            this.step2b1.Name = "step2b1";
            this.step2b1.Padding = new System.Windows.Forms.Padding(2);
            this.step2b1.Size = new System.Drawing.Size(456, 305);
            this.step2b1.TabIndex = 4;
            this.step2b1.Text = "step2b1";
            // 
            // lblHeader2b
            // 
            this.lblHeader2b.AutoSize = true;
            this.lblHeader2b.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader2b.Location = new System.Drawing.Point(7, 13);
            this.lblHeader2b.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeader2b.Name = "lblHeader2b";
            this.lblHeader2b.Size = new System.Drawing.Size(338, 52);
            this.lblHeader2b.TabIndex = 2;
            this.lblHeader2b.Tag = "header";
            this.lblHeader2b.Text = "Step {0} of {1}.\r\nChoose your UTC/JSON keystore";
            // 
            // btnSelectKeyFile
            // 
            this.btnSelectKeyFile.Location = new System.Drawing.Point(11, 98);
            this.btnSelectKeyFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectKeyFile.Name = "btnSelectKeyFile";
            this.btnSelectKeyFile.Size = new System.Drawing.Size(116, 39);
            this.btnSelectKeyFile.TabIndex = 0;
            this.btnSelectKeyFile.Text = "Choose file";
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
            this.step2b2.Location = new System.Drawing.Point(4, 40);
            this.step2b2.Margin = new System.Windows.Forms.Padding(2);
            this.step2b2.Name = "step2b2";
            this.step2b2.Size = new System.Drawing.Size(456, 305);
            this.step2b2.TabIndex = 9;
            this.step2b2.Text = "step2b2";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(11, 123);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 13);
            this.label20.TabIndex = 10;
            this.label20.Text = "Password";
            // 
            // tbSelectedKeyPassword
            // 
            this.tbSelectedKeyPassword.Location = new System.Drawing.Point(11, 141);
            this.tbSelectedKeyPassword.Margin = new System.Windows.Forms.Padding(2);
            this.tbSelectedKeyPassword.Name = "tbSelectedKeyPassword";
            this.tbSelectedKeyPassword.PasswordChar = '*';
            this.tbSelectedKeyPassword.Size = new System.Drawing.Size(163, 20);
            this.tbSelectedKeyPassword.TabIndex = 9;
            // 
            // lblLoadedKeyPath
            // 
            this.lblLoadedKeyPath.AutoSize = true;
            this.lblLoadedKeyPath.Location = new System.Drawing.Point(9, 59);
            this.lblLoadedKeyPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLoadedKeyPath.Name = "lblLoadedKeyPath";
            this.lblLoadedKeyPath.Size = new System.Drawing.Size(115, 13);
            this.lblLoadedKeyPath.TabIndex = 8;
            this.lblLoadedKeyPath.Text = "c:\\users\\user\\key.json";
            // 
            // linkSelectKeyFile
            // 
            this.linkSelectKeyFile.AutoSize = true;
            this.linkSelectKeyFile.Location = new System.Drawing.Point(9, 80);
            this.linkSelectKeyFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkSelectKeyFile.Name = "linkSelectKeyFile";
            this.linkSelectKeyFile.Size = new System.Drawing.Size(82, 13);
            this.linkSelectKeyFile.TabIndex = 7;
            this.linkSelectKeyFile.TabStop = true;
            this.linkSelectKeyFile.Text = "Choose another";
            // 
            // lblHeader2b2
            // 
            this.lblHeader2b2.AutoSize = true;
            this.lblHeader2b2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader2b2.Location = new System.Drawing.Point(7, 13);
            this.lblHeader2b2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeader2b2.Name = "lblHeader2b2";
            this.lblHeader2b2.Size = new System.Drawing.Size(396, 26);
            this.lblHeader2b2.TabIndex = 3;
            this.lblHeader2b2.Tag = "header";
            this.lblHeader2b2.Text = "Step {0} of {1}. Enter keystore password";
            // 
            // step3out
            // 
            this.step3out.BackColor = System.Drawing.SystemColors.Control;
            this.step3out.Controls.Add(this.linkLabel1);
            this.step3out.Controls.Add(this.label8);
            this.step3out.Controls.Add(this.label7);
            this.step3out.Controls.Add(this.label22);
            this.step3out.Controls.Add(this.label21);
            this.step3out.Controls.Add(this.tbThresholdAmount);
            this.step3out.Controls.Add(this.tbAddressToSend);
            this.step3out.Controls.Add(this.lblHeader3);
            this.step3out.Location = new System.Drawing.Point(4, 40);
            this.step3out.Margin = new System.Windows.Forms.Padding(2);
            this.step3out.Name = "step3out";
            this.step3out.Padding = new System.Windows.Forms.Padding(2);
            this.step3out.Size = new System.Drawing.Size(456, 305);
            this.step3out.TabIndex = 5;
            this.step3out.Text = "step3out";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.linkLabel1.Location = new System.Drawing.Point(16, 197);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(268, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.Text = "You will be able to change these settings in SONM GUI";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 210);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(212, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "(you will need your keystore and password).";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 114);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(239, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "You may use your exchange address or any other";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(11, 147);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(116, 13);
            this.label22.TabIndex = 4;
            this.label22.Text = "Minimum payout (SNM)";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(11, 72);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(149, 13);
            this.label21.TabIndex = 4;
            this.label21.Text = "Ethereum address to withdraw";
            // 
            // tbThresholdAmount
            // 
            this.tbThresholdAmount.Location = new System.Drawing.Point(11, 163);
            this.tbThresholdAmount.Margin = new System.Windows.Forms.Padding(2);
            this.tbThresholdAmount.Name = "tbThresholdAmount";
            this.tbThresholdAmount.Size = new System.Drawing.Size(290, 20);
            this.tbThresholdAmount.TabIndex = 2;
            // 
            // tbAddressToSend
            // 
            this.tbAddressToSend.Location = new System.Drawing.Point(11, 90);
            this.tbAddressToSend.Margin = new System.Windows.Forms.Padding(2);
            this.tbAddressToSend.Name = "tbAddressToSend";
            this.tbAddressToSend.Size = new System.Drawing.Size(290, 20);
            this.tbAddressToSend.TabIndex = 1;
            // 
            // lblHeader3
            // 
            this.lblHeader3.AutoSize = true;
            this.lblHeader3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader3.Location = new System.Drawing.Point(7, 13);
            this.lblHeader3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeader3.Name = "lblHeader3";
            this.lblHeader3.Size = new System.Drawing.Size(329, 26);
            this.lblHeader3.TabIndex = 2;
            this.lblHeader3.Tag = "header";
            this.lblHeader3.Text = "Step {0} of {1}. Withdraw settings";
            // 
            // step4disk
            // 
            this.step4disk.BackColor = System.Drawing.SystemColors.Control;
            this.step4disk.Controls.Add(this.checkUpdateDist);
            this.step4disk.Controls.Add(this.label9);
            this.step4disk.Controls.Add(this.label23);
            this.step4disk.Controls.Add(this.cmbDisk);
            this.step4disk.Controls.Add(this.lblHeader4);
            this.step4disk.Location = new System.Drawing.Point(4, 40);
            this.step4disk.Margin = new System.Windows.Forms.Padding(2);
            this.step4disk.Name = "step4disk";
            this.step4disk.Padding = new System.Windows.Forms.Padding(2);
            this.step4disk.Size = new System.Drawing.Size(456, 305);
            this.step4disk.TabIndex = 6;
            this.step4disk.Text = "step4disk";
            this.step4disk.Click += new System.EventHandler(this.step4disk_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 93);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(302, 52);
            this.label9.TabIndex = 5;
            this.label9.Text = "You will need USB flash drive with minimum capacity of 16 GB.\r\n\r\nWe will clear yo" +
    "ur device and write SONM installation image. \r\nYou will loose any information st" +
    "ored on this device.";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(9, 168);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(78, 13);
            this.label23.TabIndex = 4;
            this.label23.Text = "Choose device";
            // 
            // cmbDisk
            // 
            this.cmbDisk.DisplayMember = "Text";
            this.cmbDisk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisk.FormattingEnabled = true;
            this.cmbDisk.Location = new System.Drawing.Point(9, 187);
            this.cmbDisk.Margin = new System.Windows.Forms.Padding(2);
            this.cmbDisk.Name = "cmbDisk";
            this.cmbDisk.Size = new System.Drawing.Size(441, 21);
            this.cmbDisk.TabIndex = 3;
            this.cmbDisk.ValueMember = "Value";
            // 
            // lblHeader4
            // 
            this.lblHeader4.AutoSize = true;
            this.lblHeader4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader4.Location = new System.Drawing.Point(7, 13);
            this.lblHeader4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeader4.Name = "lblHeader4";
            this.lblHeader4.Size = new System.Drawing.Size(473, 52);
            this.lblHeader4.TabIndex = 2;
            this.lblHeader4.Tag = "header";
            this.lblHeader4.Text = "Step {0} of {1}.\r\nChoose USB device to create installation image";
            // 
            // step5progress
            // 
            this.step5progress.BackColor = System.Drawing.SystemColors.Control;
            this.step5progress.Controls.Add(this.pnlErrorDownload);
            this.step5progress.Controls.Add(this.progressBar);
            this.step5progress.Controls.Add(this.lblHeader5);
            this.step5progress.Location = new System.Drawing.Point(4, 40);
            this.step5progress.Margin = new System.Windows.Forms.Padding(2);
            this.step5progress.Name = "step5progress";
            this.step5progress.Padding = new System.Windows.Forms.Padding(2);
            this.step5progress.Size = new System.Drawing.Size(456, 305);
            this.step5progress.TabIndex = 7;
            this.step5progress.Text = "step5progress";
            // 
            // pnlErrorDownload
            // 
            this.pnlErrorDownload.Controls.Add(this.btnTryAgainProgress);
            this.pnlErrorDownload.Controls.Add(this.lblDownloadError);
            this.pnlErrorDownload.Location = new System.Drawing.Point(11, 124);
            this.pnlErrorDownload.Margin = new System.Windows.Forms.Padding(2);
            this.pnlErrorDownload.Name = "pnlErrorDownload";
            this.pnlErrorDownload.Size = new System.Drawing.Size(434, 114);
            this.pnlErrorDownload.TabIndex = 5;
            this.pnlErrorDownload.Visible = false;
            // 
            // btnTryAgainProgress
            // 
            this.btnTryAgainProgress.Location = new System.Drawing.Point(360, 3);
            this.btnTryAgainProgress.Margin = new System.Windows.Forms.Padding(2);
            this.btnTryAgainProgress.Name = "btnTryAgainProgress";
            this.btnTryAgainProgress.Size = new System.Drawing.Size(72, 32);
            this.btnTryAgainProgress.TabIndex = 5;
            this.btnTryAgainProgress.Text = "Try Again";
            this.btnTryAgainProgress.UseVisualStyleBackColor = true;
            // 
            // lblDownloadError
            // 
            this.lblDownloadError.Location = new System.Drawing.Point(2, 3);
            this.lblDownloadError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDownloadError.Name = "lblDownloadError";
            this.lblDownloadError.Size = new System.Drawing.Size(353, 103);
            this.lblDownloadError.TabIndex = 4;
            this.lblDownloadError.Text = "label16";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.LabelTpl = "Download in progress: {0:0.0} of {1:0.0} ({2:0}%)";
            this.progressBar.Location = new System.Drawing.Point(11, 83);
            this.progressBar.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressCurrent = 0D;
            this.progressBar.ProgressTotal = 100D;
            this.progressBar.Size = new System.Drawing.Size(434, 37);
            this.progressBar.TabIndex = 3;
            // 
            // lblHeader5
            // 
            this.lblHeader5.AutoSize = true;
            this.lblHeader5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader5.Location = new System.Drawing.Point(7, 13);
            this.lblHeader5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeader5.Name = "lblHeader5";
            this.lblHeader5.Size = new System.Drawing.Size(426, 26);
            this.lblHeader5.TabIndex = 2;
            this.lblHeader5.Tag = "header";
            this.lblHeader5.Text = "Step {0} of {1}. Preparing installation image";
            // 
            // step6fin
            // 
            this.step6fin.BackColor = System.Drawing.SystemColors.Control;
            this.step6fin.Controls.Add(this.linkFaq);
            this.step6fin.Controls.Add(this.label25);
            this.step6fin.Controls.Add(this.lblHeader6);
            this.step6fin.Location = new System.Drawing.Point(4, 40);
            this.step6fin.Margin = new System.Windows.Forms.Padding(2);
            this.step6fin.Name = "step6fin";
            this.step6fin.Padding = new System.Windows.Forms.Padding(2);
            this.step6fin.Size = new System.Drawing.Size(456, 305);
            this.step6fin.TabIndex = 8;
            this.step6fin.Text = "step6fin";
            // 
            // linkFaq
            // 
            this.linkFaq.AutoSize = true;
            this.linkFaq.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.linkFaq.Location = new System.Drawing.Point(14, 72);
            this.linkFaq.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkFaq.Name = "linkFaq";
            this.linkFaq.Size = new System.Drawing.Size(250, 91);
            this.linkFaq.TabIndex = 4;
            this.linkFaq.Text = resources.GetString("linkFaq.Text");
            this.linkFaq.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(11, 41);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(0, 13);
            this.label25.TabIndex = 3;
            // 
            // lblHeader6
            // 
            this.lblHeader6.AutoSize = true;
            this.lblHeader6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader6.Location = new System.Drawing.Point(7, 13);
            this.lblHeader6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeader6.Name = "lblHeader6";
            this.lblHeader6.Size = new System.Drawing.Size(70, 26);
            this.lblHeader6.TabIndex = 2;
            this.lblHeader6.Tag = "header";
            this.lblHeader6.Text = "Done!";
            // 
            // messagePage
            // 
            this.messagePage.BackColor = System.Drawing.SystemColors.Control;
            this.messagePage.Controls.Add(this.btnMessagePageTryAgain);
            this.messagePage.Controls.Add(this.lblMessagePageText);
            this.messagePage.Controls.Add(this.lblMessagePageHeader);
            this.messagePage.Location = new System.Drawing.Point(4, 40);
            this.messagePage.Margin = new System.Windows.Forms.Padding(2);
            this.messagePage.Name = "messagePage";
            this.messagePage.Padding = new System.Windows.Forms.Padding(2);
            this.messagePage.Size = new System.Drawing.Size(456, 305);
            this.messagePage.TabIndex = 10;
            this.messagePage.Text = "messagePage";
            // 
            // btnMessagePageTryAgain
            // 
            this.btnMessagePageTryAgain.Location = new System.Drawing.Point(377, 56);
            this.btnMessagePageTryAgain.Margin = new System.Windows.Forms.Padding(2);
            this.btnMessagePageTryAgain.Name = "btnMessagePageTryAgain";
            this.btnMessagePageTryAgain.Size = new System.Drawing.Size(72, 32);
            this.btnMessagePageTryAgain.TabIndex = 6;
            this.btnMessagePageTryAgain.Text = "Try Again";
            this.btnMessagePageTryAgain.UseVisualStyleBackColor = true;
            this.btnMessagePageTryAgain.Visible = false;
            // 
            // lblMessagePageText
            // 
            this.lblMessagePageText.Location = new System.Drawing.Point(9, 56);
            this.lblMessagePageText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMessagePageText.Name = "lblMessagePageText";
            this.lblMessagePageText.Size = new System.Drawing.Size(364, 164);
            this.lblMessagePageText.TabIndex = 4;
            this.lblMessagePageText.Text = "label16";
            // 
            // lblMessagePageHeader
            // 
            this.lblMessagePageHeader.AutoSize = true;
            this.lblMessagePageHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessagePageHeader.Location = new System.Drawing.Point(7, 13);
            this.lblMessagePageHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMessagePageHeader.Name = "lblMessagePageHeader";
            this.lblMessagePageHeader.Size = new System.Drawing.Size(60, 26);
            this.lblMessagePageHeader.TabIndex = 3;
            this.lblMessagePageHeader.Tag = "header";
            this.lblMessagePageHeader.Text = "Error";
            // 
            // checkUpdateDist
            // 
            this.checkUpdateDist.AutoSize = true;
            this.checkUpdateDist.Location = new System.Drawing.Point(9, 213);
            this.checkUpdateDist.Name = "checkUpdateDist";
            this.checkUpdateDist.Size = new System.Drawing.Size(152, 17);
            this.checkUpdateDist.TabIndex = 6;
            this.checkUpdateDist.Text = "Update existing distribution";
            this.checkUpdateDist.UseVisualStyleBackColor = true;
            // 
            // WizardFormDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 349);
            this.Controls.Add(this.loader);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "WizardFormDesign";
            this.Text = "SONM Installer";
            this.panel1.ResumeLayout(false);
            this.tabs.ResumeLayout(false);
            this.step0.ResumeLayout(false);
            this.step0.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
            this.pnlErrorDownload.ResumeLayout(false);
            this.step6fin.ResumeLayout(false);
            this.step6fin.PerformLayout();
            this.messagePage.ResumeLayout(false);
            this.messagePage.PerformLayout();
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
        public System.Windows.Forms.LinkLabel linkNewKeyPath;
        public System.Windows.Forms.Label lblNewKeyPath;
        public System.Windows.Forms.Label label15;
        public System.Windows.Forms.Label label14;
        public System.Windows.Forms.Label label13;
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
        public System.Windows.Forms.LinkLabel linkWelcome;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.LinkLabel linkLabel1;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.SaveFileDialog saveNewKey;
        public System.Windows.Forms.Label lblDownloadError;
        public System.Windows.Forms.Panel pnlErrorDownload;
        public System.Windows.Forms.Button btnTryAgainProgress;
        public System.Windows.Forms.ProgressBar loader;
        public System.Windows.Forms.TabPage messagePage;
        public System.Windows.Forms.Button btnMessagePageTryAgain;
        public System.Windows.Forms.Label lblMessagePageText;
        public System.Windows.Forms.Label lblMessagePageHeader;
        public System.Windows.Forms.OpenFileDialog openKeystore;
        public System.Windows.Forms.Label lblWelcomeError;
        private System.Windows.Forms.CheckBox checkUpdateDist;
    }
}

