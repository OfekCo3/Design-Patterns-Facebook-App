namespace BasicFacebookFeatures.Moods
{
    public class HungryMoodCreator : MoodCreator
    {
        public override IMood CreateMood()
        {
            return new HungryMood();
        }
    }
} 