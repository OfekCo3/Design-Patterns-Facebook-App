using BasicFacebookFeatures.Moods.Interfaces;
using BasicFacebookFeatures.Moods.MoodObjects;

namespace BasicFacebookFeatures.Moods.MoodCreators
{
    public class HungryMoodCreator : MoodCreator
    {
        public override IMood CreateMood()
        {
            return new HungryMood();
        }
    }
} 