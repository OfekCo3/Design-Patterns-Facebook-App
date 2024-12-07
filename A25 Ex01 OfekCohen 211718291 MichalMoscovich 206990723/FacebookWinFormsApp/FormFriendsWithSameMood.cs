using FacebookWrapper.ObjectModel;
using System;
using System.Windows.Forms;
using static BasicFacebookFeatures.ProfileMood;

namespace BasicFacebookFeatures
{
    public partial class FormFriendsWithSameMood : Form
    {
        private readonly User r_ActiveUser;
        private readonly eProfileMoodType r_UserMood;

        public FormFriendsWithSameMood(User i_ActiveUser, eProfileMoodType i_Mood)
        {
            r_ActiveUser = i_ActiveUser;
            r_UserMood = i_Mood;
            InitializeComponent();
            labelFriendInMood.Text = "Friends in the mood: " + i_Mood;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            showFriendsInTheSameMood();
        }

        private void showFriendsInTheSameMood()
        {
            listBoxFriendsWithMood.Items.Clear();

            try
            {
                foreach (User friend in r_ActiveUser.Friends)
                {
                    string displayText = $"{friend.Name} is {r_UserMood}";
                    listBoxFriendsWithMood.Items.Add(displayText);
                }

                if (listBoxFriendsWithMood.Items.Count == 0)
                {
                    listBoxFriendsWithMood.Items.Add("No one is " + r_UserMood.ToString());
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed to fetch friends");
            }
        }
    }
}