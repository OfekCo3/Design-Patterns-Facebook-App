using BasicFacebookFeatures.Moods.Interfaces;
using BasicFacebookFeatures.Moods.MoodObjects;

namespace BasicFacebookFeatures.Moods.MoodCreators
{
    public class HappyMoodCreator : MoodCreator
    {
        public override IMood CreateMood()
        {
            return new HappyMood();
        }
    }
}