using FacebookWrapper.ObjectModel;
using System.Drawing;
using static BasicFacebookFeatures.ProfilePictureFilter;

namespace BasicFacebookFeatures.Subsystems
{
    public class ProfileSystem
    {
        private readonly ProfilePictureFilter r_ProfileFilter;

        public ProfileSystem()
        {
            r_ProfileFilter = new ProfilePictureFilter();
        }

        public Image ApplyFilter(Image i_Image, eProfileFilter i_FilterType)
        {
            return r_ProfileFilter.ApplyFilter(i_Image, i_FilterType);
        }

        public void SavePicture(Image i_Image, string i_FilePath)
        {
            i_Image.Save(i_FilePath);
        }

        public void UploadPicture(User i_User, Image i_Image)
        {
            byte[] imageData = new ImageConverter().ConvertTo(i_Image, typeof(byte[])) as byte[];
            i_User.PostPhoto(imageData);
        }
    }
} 