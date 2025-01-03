namespace BasicFacebookFeatures.Moods
{
    public class NoneMoodCreator : MoodCreator
    {
        public override IMood CreateMood()
        {
            return new NoneMood();
        }
    }
} 