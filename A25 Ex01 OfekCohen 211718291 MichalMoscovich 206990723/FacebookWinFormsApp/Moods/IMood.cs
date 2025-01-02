namespace BasicFacebookFeatures.Moods
{
    public interface IMood
    {
        Image ApplyMood(Image i_OriginalImage);
        string GetMoodName();
        Color GetMoodColor();
        string GetMoodEmoji();
    }
} 