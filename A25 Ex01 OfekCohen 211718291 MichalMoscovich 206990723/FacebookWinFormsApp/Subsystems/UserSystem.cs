using System;
using System.Drawing;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures.Subsystems
{
    public class UserSystem
    {
        public LoginResult Login(string i_AppID)
        {
            LoginResult loginResult = FacebookService.Login(
                i_AppID,
                "email",
                "public_profile",
                "user_age_range",
                "user_birthday",
                "user_events",
                "user_friends",
                "user_gender",
                "user_hometown",
                "user_likes",
                "user_link",
                "user_location",
                "user_photos",
                "user_posts",
                "user_videos"
            );

            return loginResult;
        }

        public void Logout()
        {
            FacebookService.LogoutWithUI();
        }

        public void LoadUserInformation(User i_User, PictureBox i_ProfilePicture, PictureBox i_CoverPicture)
        {
            if (!string.IsNullOrEmpty(i_User.PictureNormalURL))
            {
                i_ProfilePicture.ImageLocation = i_User.PictureNormalURL;
            }

            if (i_User.Cover != null && !string.IsNullOrEmpty(i_User.Cover.SourceURL))
            {
                i_CoverPicture.ImageLocation = i_User.Cover.SourceURL;
            }
        }

        public void LoadUserFeed(User i_User, ListBox i_ListBox)
        {
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

        public void LoadUserEvents(User i_User, ListBox i_ListBox)
        {
            foreach (Event userEvent in i_User.Events)
            {
                if (userEvent.Name != null)
                {
                    i_ListBox.Items.Add($"{userEvent.Name} [{userEvent.TimeString}]");
                }
            }
            if (i_ListBox.Items.Count == 0)
            {
                i_ListBox.Items.Add("No upcoming events.");
            }
        }

        public void LoadUserFriends(User i_User, ListBox i_ListBox)
        {
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

        public void LoadUserLikedPages(User i_User, ListBox i_ListBox)
        {
            foreach (Page page in i_User.LikedPages)
            {
                i_ListBox.Items.Add(page);
            }

            if (i_ListBox.Items.Count == 0)
            {
                i_ListBox.Items.Add("No liked pages to display");
            }
        }

        public void LoadUserGroups(User i_User, ListBox i_ListBox)
        {
            foreach (Group group in i_User.Groups)
            {
                i_ListBox.Items.Add(group);
            }

            if (i_ListBox.Items.Count == 0)
            {
                i_ListBox.Items.Add("No groups to display");
            }
        }

        public void LoadUserAlbums(User i_User, ListBox i_ListBox)
        {
            foreach (Album album in i_User.Albums)
            {
                i_ListBox.Items.Add(album);
            }

            if (i_ListBox.Items.Count == 0)
            {
                i_ListBox.Items.Add("No Albums to display");
            }
        }

        public void PostStatus(User i_User, string i_Text)
        {
            Status postedStatus = i_User.PostStatus(i_Text);
            if (postedStatus == null)
            {
                throw new Exception("Post failed");
            }
        }

        public void PostPicture(User i_User, Image i_Picture)
        {
            try
            {
                byte[] imageData = new ImageConverter().ConvertTo(i_Picture, typeof(byte[])) as byte[];

                if (imageData != null)
                {
                    Post postedPicture = i_User.PostPhoto(imageData);
                    if (postedPicture == null)
                    {
                        throw new Exception("Failed to post the picture");
                    }
                }
                else
                {
                    throw new Exception("Error converting the picture");
                }
            }
            catch (Exception)
            {
                throw new Exception("This action is not supported by Facebook");
            }
        }

        public void PostStatusWithPicture(User i_User, string i_StatusText, string i_PicturePath)
        {
            Status postedStatus = i_User.PostStatus(i_StatusText, i_PicturePath);
            if (postedStatus == null)
            {
                throw new Exception("Picture post failed");
            }
        }
    }
} 