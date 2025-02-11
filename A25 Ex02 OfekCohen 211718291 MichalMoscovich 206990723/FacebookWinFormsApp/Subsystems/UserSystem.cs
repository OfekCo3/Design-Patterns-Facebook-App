using System;
using System.ComponentModel;
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

        public void LoadUserInformation(User i_User, PictureBox i_ProfilePicture)
        {
            if (!string.IsNullOrEmpty(i_User.PictureNormalURL))
            {
                i_ProfilePicture.ImageLocation = i_User.PictureNormalURL;
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

        public BindingList<Event> GetUserEvents(User i_User)
        {
            BindingList<Event> eventsList = new BindingList<Event>();

            foreach (Event userEvent in i_User.Events)
            {
                if (userEvent.Name != null)
                {
                    eventsList.Add(userEvent);
                }
            }

            return eventsList;
        }

        public BindingList<User> GetUserFriends(User i_User)
        {
            BindingList<User> friendsList = new BindingList<User>();

            foreach (User friend in i_User.Friends)
            {
                friendsList.Add(friend);
            }

            return friendsList;
        }

        public void LoadUserLikedPages(User i_User, ListBox io_ListBox)
        {
            foreach (Page page in i_User.LikedPages)
            {
                io_ListBox.Items.Add(page);
            }

            if (io_ListBox.Items.Count == 0)
            {
                io_ListBox.Items.Add("No liked pages to display");
            }
        }

        public void LoadUserGroups(User i_User, ListBox io_ListBox)
        {
            foreach (Group group in i_User.Groups)
            {
                io_ListBox.Items.Add(group);
            }

            if (io_ListBox.Items.Count == 0)
            {
                io_ListBox.Items.Add("No groups to display");
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