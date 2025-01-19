using BasicFacebookFeatures.Subsystems;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using static BasicFacebookFeatures.ProfileMood;
using static BasicFacebookFeatures.ProfilePictureFilter;

namespace BasicFacebookFeatures.Facade
{
    public class FacebookSystemFacade
    {
        private readonly ProfileSystem r_ProfileSystem;
        private readonly MoodSystem r_MoodSystem;
        private readonly UserSystem r_UserSystem;

        public FacebookSystemFacade()
        {
            r_ProfileSystem = new ProfileSystem();
            r_MoodSystem = new MoodSystem();
            r_UserSystem = new UserSystem();
        }

        public LoginResult Login(string i_AppID)
        {
            return r_UserSystem.Login(i_AppID);
        }

        public void Logout()
        {
            r_UserSystem.Logout();
        }

        public Image ApplyProfileFilter(Image i_Image, eProfileFilter i_FilterType)
        {
            return r_ProfileSystem.ApplyFilter(i_Image, i_FilterType);
        }

        public Image ApplyMood(Image i_Image, eProfileMoodType i_MoodType)
        {
            return r_MoodSystem.ApplyMood(i_Image, i_MoodType);
        }

        public void PostStatus(User i_User, string i_Text)
        {
            r_UserSystem.PostStatus(i_User, i_Text);
        }

        public void SaveProfilePicture(Image i_Image, string i_FilePath)
        {
            r_ProfileSystem.SavePicture(i_Image, i_FilePath);
        }

        public void UploadProfilePicture(User i_User, Image i_Image)
        {
            r_ProfileSystem.UploadPicture(i_User, i_Image);
        }

        public FormFriendsWithSameMood ShowFriendsWithSameMood(User i_User, eProfileMoodType i_MoodType)
        {
            return r_MoodSystem.ShowFriendsWithSameMood(i_User, i_MoodType);
        }

        public void LoadUserFeed(User i_User, ListBox i_ListBox)
        {
            r_UserSystem.LoadUserFeed(i_User, i_ListBox);
        }

        public void LoadUserFriends(User i_User, ListBox i_ListBox)
        {
            r_UserSystem.LoadUserFriends(i_User, i_ListBox);
        }

        public void LoadUserEvents(User i_User, ListBox i_ListBox)
        {
            r_UserSystem.LoadUserEvents(i_User, i_ListBox);
        }

        public void LoadUserLikedPages(User i_User, ListBox i_ListBox)
        {
            r_UserSystem.LoadUserLikedPages(i_User, i_ListBox);
        }

        public void LoadUserGroups(User i_User, ListBox i_ListBox)
        {
            r_UserSystem.LoadUserGroups(i_User, i_ListBox);
        }

        public void LoadUserAlbums(User i_User, ListBox i_ListBox)
        {
            r_UserSystem.LoadUserAlbums(i_User, i_ListBox);
        }

        public void LoadUserInformation(User i_User, PictureBox i_ProfilePicture, PictureBox i_CoverPicture)
        {
            r_UserSystem.LoadUserInformation(i_User, i_ProfilePicture, i_CoverPicture);
        }

        public void PostPicture(User i_User, Image i_Picture)
        {
            r_UserSystem.PostPicture(i_User, i_Picture);
        }

        public void PostStatusWithPicture(User i_User, string i_StatusText, string i_PicturePath)
        {
            r_UserSystem.PostStatusWithPicture(i_User, i_StatusText, i_PicturePath);
        }
    }
} 