namespace BasicFacebookFeatures.Moods
{
    public abstract class MoodCreator
    {
        public abstract IMood CreateMood();
        
        public Image ApplyMood(Image i_OriginalImage)
        {
            IMood mood = CreateMood();
            return mood.ApplyMood(i_OriginalImage);
        }
    }
} 