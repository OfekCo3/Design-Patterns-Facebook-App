using System.Drawing;
using BasicFacebookFeatures.Moods.Factory;
using BasicFacebookFeatures.Moods.Interfaces;

namespace BasicFacebookFeatures
{
    public class ProfileMood
    {
        public enum eProfileMoodType
        {
            None = 0,
            Happy,
            Sad,
            InLove,
            Hungry
        }

        public Image ApplyMood(Image i_OriginalImage, eProfileMoodType i_MoodType)
        {
            IMood mood = MoodFactory.CreateMood(i_MoodType);
            return mood.ApplyMood(i_OriginalImage);
        }
    }
}