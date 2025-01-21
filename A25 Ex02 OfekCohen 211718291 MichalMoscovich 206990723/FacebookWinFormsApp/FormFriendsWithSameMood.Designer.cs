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
            this.flowLayoutPanelFriends = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanelFriends
            // 
            this.flowLayoutPanelFriends.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelFriends.AutoScroll = true;
            this.flowLayoutPanelFriends.Location = new System.Drawing.Point(12, 41);
            this.flowLayoutPanelFriends.Name = "flowLayoutPanelFriends";
            this.flowLayoutPanelFriends.Size = new System.Drawing.Size(560, 408);
            this.flowLayoutPanelFriends.TabIndex = 0;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(12, 12);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // FormFriendsWithSameMood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.flowLayoutPanelFriends);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "FormFriendsWithSameMood";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Friends With Same Mood";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelFriends;
        private System.Windows.Forms.Button buttonRefresh;
    }
}