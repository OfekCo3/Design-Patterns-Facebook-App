using BasicFacebookFeatures.Properties;
using System.Drawing;
using BasicFacebookFeatures.Moods;

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

        private MoodCreator getMoodCreator(eProfileMoodType i_MoodType)
        {
            switch (i_MoodType)
            {
                case eProfileMoodType.Happy:
                    return new HappyMoodCreator();
                case eProfileMoodType.Sad:
                    return new SadMoodCreator();
                case eProfileMoodType.InLove:
                    return new InLoveMoodCreator();
                case eProfileMoodType.Hungry:
                    return new HungryMoodCreator();
                default:
                    return new NoneMoodCreator();
            }
        }

        public Image ApplyMood(Image i_OriginalImage, eProfileMoodType i_MoodType)
        {
            MoodCreator creator = getMoodCreator(i_MoodType);
            return creator.ApplyMood(i_OriginalImage);
        }
    }
}