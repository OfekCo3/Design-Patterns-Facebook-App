namespace BasicFacebookFeatures.Moods
{
    public class HappyMoodCreator : MoodCreator
    {
        public override IMood CreateMood()
        {
            return new HappyMood();
        }
    }
} 