using System.Drawing;
using BasicFacebookFeatures.Moods.MoodCreators;

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

        public MoodCreator GetMoodCreator(eProfileMoodType i_MoodType)
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
            MoodCreator creator = GetMoodCreator(i_MoodType);
            return creator.ApplyMood(i_OriginalImage);
        }
    }
}