namespace BasicFacebookFeatures.Moods
{
    public class SadMoodCreator : MoodCreator
    {
        public override IMood CreateMood()
        {
            return new SadMood();
        }
    }
} 