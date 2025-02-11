using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using static BasicFacebookFeatures.ProfileMood;

namespace BasicFacebookFeatures
{
    public partial class FormFriendsWithSameMood : Form
    {
        private readonly User r_LoggedInUser;
        private readonly eProfileMoodType r_CurrentMood;
        private readonly Dictionary<string, PictureBox> r_FriendPictureBoxes;
        private const int k_PanelWidth = 150;
        private const int k_PanelHeight = 200;
        private const int k_PictureWidth = 130;
        private const int k_PictureHeight = 130;
        private const int k_Padding = 10;
        private const int k_RandomFactor = 5;

        public FormFriendsWithSameMood(User i_LoggedInUser, eProfileMoodType i_CurrentMood)
        {
            InitializeComponent();
            r_LoggedInUser = i_LoggedInUser;
            r_CurrentMood = i_CurrentMood;
            r_FriendPictureBoxes = new Dictionary<string, PictureBox>();
            Text = $"Friends in {i_CurrentMood} Mood";
            initializeFriendsPanel();
        }

        private void initializeFriendsPanel()
        {
            try
            {
                flowLayoutPanelFriends.Controls.Clear();
                r_FriendPictureBoxes.Clear();

                foreach (User friend in r_LoggedInUser.Friends)
                {
                    if (isFriendInSameMood(friend))
                    {
                        addFriendToPanel(friend);
                    }
                }

                if (flowLayoutPanelFriends.Controls.Count == 0)
                {
                    addNoFriendsLabel();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading friends: {ex.Message}");
            }
        }

        private void addNoFriendsLabel()
        {
            Label noFriendsLabel = new Label
            {
                Text = $"No friends are in {r_CurrentMood} mood right now",
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            };

            flowLayoutPanelFriends.Controls.Add(noFriendsLabel);
        }

        private bool isFriendInSameMood(User i_Friend)
        {
            Random random = new Random();
            return random.Next(0, k_RandomFactor) == 0;
        }

        private void addFriendToPanel(User i_Friend)
        {
            Panel friendPanel = createFriendPanel();
            PictureBox pictureBox = createPictureBox();
            Label nameLabel = createNameLabel(i_Friend.Name);

            if (!string.IsNullOrEmpty(i_Friend.PictureNormalURL))
            {
                pictureBox.LoadAsync(i_Friend.PictureNormalURL);
                r_FriendPictureBoxes.Add(i_Friend.Id, pictureBox);
            }
            else
            {
                pictureBox.Image = Properties.Resources.gray_background;
            }

            friendPanel.Controls.Add(pictureBox);
            friendPanel.Controls.Add(nameLabel);
            flowLayoutPanelFriends.Controls.Add(friendPanel);
        }

        private Panel createFriendPanel()
        {
            return new Panel
            {
                Width = k_PanelWidth,
                Height = k_PanelHeight,
                Margin = new Padding(k_Padding),
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private PictureBox createPictureBox()
        {
            return new PictureBox
            {
                Width = k_PictureWidth,
                Height = k_PictureHeight,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(k_Padding, k_Padding)
            };
        }

        private Label createNameLabel(string i_FriendName)
        {
            return new Label
            {
                Text = i_FriendName,
                AutoSize = true,
                Location = new Point(k_Padding, k_PanelHeight - 50)
            };
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            initializeFriendsPanel();
        }
    }
}