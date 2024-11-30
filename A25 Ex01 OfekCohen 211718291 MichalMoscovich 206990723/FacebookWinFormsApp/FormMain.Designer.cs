namespace BasicFacebookFeatures
{
    partial class FormMain
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
            this.tabMainPage = new System.Windows.Forms.TabPage();
            this.buttonCancelPost = new System.Windows.Forms.Button();
            this.buttonAddPicture = new System.Windows.Forms.Button();
            this.buttonPost = new System.Windows.Forms.Button();
            this.comboBoxMain = new System.Windows.Forms.ComboBox();
            this.listBoxMain = new System.Windows.Forms.ListBox();
            this.listBoxFriends = new System.Windows.Forms.ListBox();
            this.listBoxEvents = new System.Windows.Forms.ListBox();
            this.pictureBoxProfile = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPost = new System.Windows.Forms.RichTextBox();
            this.labelAddPost = new System.Windows.Forms.Label();
            this.labelAppId = new System.Windows.Forms.Label();
            this.textBoxAppID = new System.Windows.Forms.TextBox();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.pictureBoxCover = new System.Windows.Forms.PictureBox();
            this.tabMainApp = new System.Windows.Forms.TabControl();
            this.openFileDialogPostPicture = new System.Windows.Forms.OpenFileDialog();
            this.checkBoxRememberMe = new System.Windows.Forms.CheckBox();
            this.tabMainPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).BeginInit();
            this.tabMainApp.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMainPage
            // 
            this.tabMainPage.Controls.Add(this.checkBoxRememberMe);
            this.tabMainPage.Controls.Add(this.buttonCancelPost);
            this.tabMainPage.Controls.Add(this.buttonAddPicture);
            this.tabMainPage.Controls.Add(this.buttonPost);
            this.tabMainPage.Controls.Add(this.comboBoxMain);
            this.tabMainPage.Controls.Add(this.listBoxMain);
            this.tabMainPage.Controls.Add(this.listBoxFriends);
            this.tabMainPage.Controls.Add(this.listBoxEvents);
            this.tabMainPage.Controls.Add(this.pictureBoxProfile);
            this.tabMainPage.Controls.Add(this.label2);
            this.tabMainPage.Controls.Add(this.label1);
            this.tabMainPage.Controls.Add(this.textBoxPost);
            this.tabMainPage.Controls.Add(this.labelAddPost);
            this.tabMainPage.Controls.Add(this.labelAppId);
            this.tabMainPage.Controls.Add(this.textBoxAppID);
            this.tabMainPage.Controls.Add(this.buttonLogout);
            this.tabMainPage.Controls.Add(this.buttonLogin);
            this.tabMainPage.Controls.Add(this.pictureBoxCover);
            this.tabMainPage.Location = new System.Drawing.Point(4, 35);
            this.tabMainPage.Name = "tabMainPage";
            this.tabMainPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabMainPage.Size = new System.Drawing.Size(1044, 627);
            this.tabMainPage.TabIndex = 0;
            this.tabMainPage.Text = "tabPage1";
            this.tabMainPage.UseVisualStyleBackColor = true;
            this.tabMainPage.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // buttonCancelPost
            // 
            this.buttonCancelPost.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonCancelPost.Enabled = false;
            this.buttonCancelPost.Location = new System.Drawing.Point(864, 331);
            this.buttonCancelPost.Name = "buttonCancelPost";
            this.buttonCancelPost.Size = new System.Drawing.Size(114, 38);
            this.buttonCancelPost.TabIndex = 72;
            this.buttonCancelPost.Text = "Cancel";
            this.buttonCancelPost.UseVisualStyleBackColor = false;
            this.buttonCancelPost.Click += new System.EventHandler(this.buttonCancelPost_Click);
            // 
            // buttonAddPicture
            // 
            this.buttonAddPicture.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonAddPicture.Enabled = false;
            this.buttonAddPicture.Location = new System.Drawing.Point(716, 331);
            this.buttonAddPicture.Name = "buttonAddPicture";
            this.buttonAddPicture.Size = new System.Drawing.Size(142, 38);
            this.buttonAddPicture.TabIndex = 71;
            this.buttonAddPicture.Text = "Add a Picture";
            this.buttonAddPicture.UseVisualStyleBackColor = false;
            this.buttonAddPicture.Click += new System.EventHandler(this.buttonAddPicture_Click);
            // 
            // buttonPost
            // 
            this.buttonPost.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonPost.Enabled = false;
            this.buttonPost.Location = new System.Drawing.Point(596, 331);
            this.buttonPost.Name = "buttonPost";
            this.buttonPost.Size = new System.Drawing.Size(114, 38);
            this.buttonPost.TabIndex = 70;
            this.buttonPost.Text = "Post";
            this.buttonPost.UseVisualStyleBackColor = false;
            this.buttonPost.Click += new System.EventHandler(this.buttonPost_Click);
            // 
            // comboBoxMain
            // 
            this.comboBoxMain.Enabled = false;
            this.comboBoxMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxMain.FormattingEnabled = true;
            this.comboBoxMain.Items.AddRange(new object[] {
            "My Feed",
            "My Likes",
            "My Albums",
            "My Groups"});
            this.comboBoxMain.Location = new System.Drawing.Point(8, 369);
            this.comboBoxMain.Name = "comboBoxMain";
            this.comboBoxMain.Size = new System.Drawing.Size(142, 40);
            this.comboBoxMain.TabIndex = 67;
            this.comboBoxMain.Text = "My Feed:";
            this.comboBoxMain.SelectedIndexChanged += new System.EventHandler(this.comboBoxMain_SelectedIndexChanged);
            // 
            // listBoxMain
            // 
            this.listBoxMain.FormattingEnabled = true;
            this.listBoxMain.ItemHeight = 26;
            this.listBoxMain.Location = new System.Drawing.Point(8, 412);
            this.listBoxMain.Name = "listBoxMain";
            this.listBoxMain.Size = new System.Drawing.Size(332, 186);
            this.listBoxMain.TabIndex = 55;
            // 
            // listBoxFriends
            // 
            this.listBoxFriends.FormattingEnabled = true;
            this.listBoxFriends.ItemHeight = 26;
            this.listBoxFriends.Location = new System.Drawing.Point(707, 412);
            this.listBoxFriends.Name = "listBoxFriends";
            this.listBoxFriends.Size = new System.Drawing.Size(331, 186);
            this.listBoxFriends.TabIndex = 66;
            // 
            // listBoxEvents
            // 
            this.listBoxEvents.FormattingEnabled = true;
            this.listBoxEvents.ItemHeight = 26;
            this.listBoxEvents.Location = new System.Drawing.Point(358, 412);
            this.listBoxEvents.Name = "listBoxEvents";
            this.listBoxEvents.Size = new System.Drawing.Size(331, 186);
            this.listBoxEvents.TabIndex = 65;
            // 
            // pictureBoxProfile
            // 
            this.pictureBoxProfile.Location = new System.Drawing.Point(885, 6);
            this.pictureBoxProfile.Name = "pictureBoxProfile";
            this.pictureBoxProfile.Size = new System.Drawing.Size(114, 110);
            this.pictureBoxProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxProfile.TabIndex = 64;
            this.pictureBoxProfile.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(704, 383);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 26);
            this.label2.TabIndex = 63;
            this.label2.Text = "Friends:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(355, 383);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 26);
            this.label1.TabIndex = 62;
            this.label1.Text = "Next events:";
            // 
            // textBoxPost
            // 
            this.textBoxPost.Location = new System.Drawing.Point(8, 168);
            this.textBoxPost.Name = "textBoxPost";
            this.textBoxPost.Size = new System.Drawing.Size(970, 159);
            this.textBoxPost.TabIndex = 57;
            this.textBoxPost.Text = "";
            this.textBoxPost.TextChanged += new System.EventHandler(this.textBoxPost_TextChanged);
            // 
            // labelAddPost
            // 
            this.labelAddPost.AutoSize = true;
            this.labelAddPost.Location = new System.Drawing.Point(8, 147);
            this.labelAddPost.Name = "labelAddPost";
            this.labelAddPost.Size = new System.Drawing.Size(122, 26);
            this.labelAddPost.TabIndex = 56;
            this.labelAddPost.Text = "Add a post:";
            // 
            // labelAppId
            // 
            this.labelAppId.AutoSize = true;
            this.labelAppId.Location = new System.Drawing.Point(8, 81);
            this.labelAppId.Name = "labelAppId";
            this.labelAppId.Size = new System.Drawing.Size(85, 26);
            this.labelAppId.TabIndex = 55;
            this.labelAppId.Text = "App ID:";
            // 
            // textBoxAppID
            // 
            this.textBoxAppID.Location = new System.Drawing.Point(69, 75);
            this.textBoxAppID.Name = "textBoxAppID";
            this.textBoxAppID.Size = new System.Drawing.Size(199, 32);
            this.textBoxAppID.TabIndex = 54;
            this.textBoxAppID.Text = "1450160541956417";
            this.textBoxAppID.TextChanged += new System.EventHandler(this.textBoxAppID_TextChanged);
            // 
            // buttonLogout
            // 
            this.buttonLogout.Enabled = false;
            this.buttonLogout.Location = new System.Drawing.Point(145, 19);
            this.buttonLogout.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(126, 34);
            this.buttonLogout.TabIndex = 52;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(11, 19);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(126, 34);
            this.buttonLogin.TabIndex = 36;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // pictureBoxCover
            // 
            this.pictureBoxCover.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxCover.Name = "pictureBoxCover";
            this.pictureBoxCover.Size = new System.Drawing.Size(1044, 136);
            this.pictureBoxCover.TabIndex = 69;
            this.pictureBoxCover.TabStop = false;
            // 
            // tabMainApp
            // 
            this.tabMainApp.Controls.Add(this.tabMainPage);
            this.tabMainApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMainApp.Location = new System.Drawing.Point(0, 0);
            this.tabMainApp.Name = "tabMainApp";
            this.tabMainApp.SelectedIndex = 0;
            this.tabMainApp.Size = new System.Drawing.Size(1052, 666);
            this.tabMainApp.TabIndex = 54;
            // 
            // openFileDialogPostPicture
            // 
            this.openFileDialogPostPicture.FileName = "openFileDialog1";
            this.openFileDialogPostPicture.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogPostPicture_FileOk);
            // 
            // checkBoxRememberMe
            // 
            this.checkBoxRememberMe.AutoSize = true;
            this.checkBoxRememberMe.Location = new System.Drawing.Point(287, 22);
            this.checkBoxRememberMe.Name = "checkBoxRememberMe";
            this.checkBoxRememberMe.Size = new System.Drawing.Size(183, 30);
            this.checkBoxRememberMe.TabIndex = 73;
            this.checkBoxRememberMe.Text = "Remember Me";
            this.checkBoxRememberMe.UseVisualStyleBackColor = true;
            this.checkBoxRememberMe.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 666);
            this.Controls.Add(this.tabMainApp);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabMainPage.ResumeLayout(false);
            this.tabMainPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).EndInit();
            this.tabMainApp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabMainPage;
        private System.Windows.Forms.TextBox textBoxAppID;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TabControl tabMainApp;
        private System.Windows.Forms.Label labelAppId;
        private System.Windows.Forms.Label labelAddPost;
        private System.Windows.Forms.RichTextBox textBoxPost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxProfile;
        private System.Windows.Forms.ListBox listBoxEvents;
        private System.Windows.Forms.ListBox listBoxFriends;
        private System.Windows.Forms.ListBox listBoxMain;
        private System.Windows.Forms.ComboBox comboBoxMain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxCover;
        private System.Windows.Forms.Button buttonCancelPost;
        private System.Windows.Forms.Button buttonAddPicture;
        private System.Windows.Forms.Button buttonPost;
        private System.Windows.Forms.OpenFileDialog openFileDialogPostPicture;
        private System.Windows.Forms.CheckBox checkBoxRememberMe;
    }
}

