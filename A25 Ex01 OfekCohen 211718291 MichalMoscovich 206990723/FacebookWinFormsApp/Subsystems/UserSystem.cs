using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System.Windows.Forms;

namespace BasicFacebookFeatures.Subsystems
{
    public class UserSystem
    {
        public LoginResult Login(string i_AppID)
        {
            return FacebookService.Login(i_AppID,
                "email",
                "public_profile",
                "user_hometown",
                "user_birthday",
                "user_link",
                "user_friends",
                "user_location",
                "user_likes",
                "user_photos",
                "user_videos",
                "user_posts");
        }

        public void Logout()
        {
            FacebookService.LogoutWithUI();
        }

        public void PostStatus(User i_User, string i_Text)
        {
            if (string.IsNullOrWhiteSpace(i_Text))
            {
                throw new System.Exception("Please enter your post first!");
            }

            Status postedStatus = i_User.PostStatus(i_Text);
            if (postedStatus == null)
            {
                throw new System.Exception("Post failed");
            }
        }

        public void LoadUserFeed(User i_User, ListBox i_ListBox)
        {
            i_ListBox.Items.Clear();
            foreach (Post post in i_User.NewsFeed)
            {
                if (!string.IsNullOrEmpty(post.Message))
                {
                    i_ListBox.Items.Add(post.Message);
                }
                else if (!string.IsNullOrEmpty(post.Caption))
                {
                    i_ListBox.Items.Add(post.Caption);
                }
                else
                {
                    i_ListBox.Items.Add($"[{post.CreatedTime}]");
                }
            }

            if (i_ListBox.Items.Count == 0)
            {
                i_ListBox.Items.Add("No feed to display.");
            }
        }

        public void LoadUserFriends(User i_User, ListBox i_ListBox)
        {
            i_ListBox.Items.Clear();
            i_ListBox.DisplayMember = "Name";

            foreach (User friend in i_User.Friends)
            {
                i_ListBox.Items.Add(friend);
            }

            if (i_ListBox.Items.Count == 0)
            {
                i_ListBox.Enabled = false;
                i_ListBox.Items.Add("No friends found.");
            }
        }
    }
} 