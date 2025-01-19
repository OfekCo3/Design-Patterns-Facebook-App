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
        private readonly ProfileSystem m_ProfileSystem;
        private readonly MoodSystem m_MoodSystem;
        private readonly UserSystem m_UserSystem;

        public FacebookSystemFacade()
        {
            m_ProfileSystem = new ProfileSystem();
            m_MoodSystem = new MoodSystem();
            m_UserSystem = new UserSystem();
        }

        public LoginResult Login(string i_AppID)
        {
            return m_UserSystem.Login(i_AppID);
        }

        public void Logout()
        {
            m_UserSystem.Logout();
        }

        public Image ApplyProfileFilter(Image i_Image, eProfileFilter i_FilterType)
        {
            return m_ProfileSystem.ApplyFilter(i_Image, i_FilterType);
        }

        public Image ApplyMood(Image i_Image, eProfileMoodType i_MoodType)
        {
            return m_MoodSystem.ApplyMood(i_Image, i_MoodType);
        }

        public void PostStatus(User i_User, string i_Text)
        {
            m_UserSystem.PostStatus(i_User, i_Text);
        }

        public void SaveProfilePicture(Image i_Image, string i_FilePath)
        {
            m_ProfileSystem.SavePicture(i_Image, i_FilePath);
        }

        public void UploadProfilePicture(User i_User, Image i_Image)
        {
            m_ProfileSystem.UploadPicture(i_User, i_Image);
        }

        public FormFriendsWithSameMood ShowFriendsWithSameMood(User i_User, eProfileMoodType i_MoodType)
        {
            return m_MoodSystem.ShowFriendsWithSameMood(i_User, i_MoodType);
        }

        public void LoadUserFeed(User i_User, ListBox i_ListBox)
        {
            m_UserSystem.LoadUserFeed(i_User, i_ListBox);
        }

        public void LoadUserFriends(User i_User, ListBox i_ListBox)
        {
            m_UserSystem.LoadUserFriends(i_User, i_ListBox);
        }
    }
} 