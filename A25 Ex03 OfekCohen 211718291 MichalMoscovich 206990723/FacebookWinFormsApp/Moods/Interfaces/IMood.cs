using System.Drawing;

namespace BasicFacebookFeatures.Moods.Interfaces
{
    public interface IMood
    {
        Image ApplyMood(Image i_OriginalImage);
        string GetMoodName();
        Color GetMoodColor();
        string GetMoodEmoji();
    }
}