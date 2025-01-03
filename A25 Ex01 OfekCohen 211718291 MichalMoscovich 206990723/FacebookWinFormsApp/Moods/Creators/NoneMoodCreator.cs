using BasicFacebookFeatures.Moods.Interfaces;
using BasicFacebookFeatures.Moods.MoodObjects;

namespace BasicFacebookFeatures.Moods.MoodCreators
{
    public class NoneMoodCreator : MoodCreator
    {
        public override IMood CreateMood()
        {
            return new NoneMood();
        }
    }
} 