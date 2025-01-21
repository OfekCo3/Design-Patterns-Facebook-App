using BasicFacebookFeatures.Moods.Interfaces;
using BasicFacebookFeatures.Moods.MoodObjects;
using static BasicFacebookFeatures.ProfileMood;

namespace BasicFacebookFeatures.Moods.Factory
{
    public static class MoodFactory
    {
        public static IMood CreateMood(eProfileMoodType i_MoodType)
        {
            switch (i_MoodType)
            {
                case eProfileMoodType.Happy:
                    return new HappyMood();
                case eProfileMoodType.Sad:
                    return new SadMood();
                case eProfileMoodType.InLove:
                    return new InLoveMood();
                case eProfileMoodType.Hungry:
                    return new HungryMood();
                case eProfileMoodType.None:
                default:
                    return new NoneMood();
            }
        }
    }
}