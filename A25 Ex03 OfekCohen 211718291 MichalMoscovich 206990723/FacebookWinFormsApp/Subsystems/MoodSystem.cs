using FacebookWrapper.ObjectModel;
using System.Drawing;
using static BasicFacebookFeatures.ProfileMood;

namespace BasicFacebookFeatures.Subsystems
{
    public class MoodSystem
    {
        private readonly ProfileMood r_ProfileMood;

        public MoodSystem()
        {
            r_ProfileMood = new ProfileMood();
        }

        public Image ApplyMood(Image i_Image, eProfileMoodType i_MoodType)
        {
            return r_ProfileMood.ApplyMood(i_Image, i_MoodType);
        }
    }
}