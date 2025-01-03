namespace BasicFacebookFeatures.Moods
{
    public class InLoveMoodCreator : MoodCreator
    {
        public override IMood CreateMood()
        {
            return new InLoveMood();
        }
    }
} 