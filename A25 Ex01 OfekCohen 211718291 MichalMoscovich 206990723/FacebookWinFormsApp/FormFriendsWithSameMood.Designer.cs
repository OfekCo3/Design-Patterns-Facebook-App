namespace BasicFacebookFeatures
{
    partial class FormFriendsWithSameMood
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
            this.labelFriendInMood = new System.Windows.Forms.Label();
            this.listBoxFriendsWithMood = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // labelFriendInMood
            // 
            this.labelFriendInMood.AutoSize = true;
            this.labelFriendInMood.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFriendInMood.Location = new System.Drawing.Point(165, 35);
            this.labelFriendInMood.Name = "labelFriendInMood";
            this.labelFriendInMood.Size = new System.Drawing.Size(432, 42);
            this.labelFriendInMood.TabIndex = 0;
            this.labelFriendInMood.Text = "Friend with mood: Happy";
            // 
            // listBoxFriendsWithMood
            // 
            this.listBoxFriendsWithMood.FormattingEnabled = true;
            this.listBoxFriendsWithMood.Location = new System.Drawing.Point(172, 112);
            this.listBoxFriendsWithMood.Name = "listBoxFriendsWithMood";
            this.listBoxFriendsWithMood.Size = new System.Drawing.Size(413, 290);
            this.listBoxFriendsWithMood.TabIndex = 17;
            // 
            // FormFriendsWithSameMood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 450);
            this.Controls.Add(this.listBoxFriendsWithMood);
            this.Controls.Add(this.labelFriendInMood);
            this.Name = "FormFriendsWithSameMood";
            this.Text = "Friends With The Same Mood";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFriendInMood;
        private System.Windows.Forms.ListBox listBoxFriendsWithMood;
    }
}