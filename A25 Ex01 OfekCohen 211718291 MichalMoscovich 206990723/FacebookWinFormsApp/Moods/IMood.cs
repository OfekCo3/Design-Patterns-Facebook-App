using System.Drawing;

namespace BasicFacebookFeatures.Moods
{
    public interface IMood
    {
        Image ApplyPicture(Image i_OriginalImage);
        string GetMoodName();
        Color GetMoodColor();
        string GetMoodEmoji();
    }
}