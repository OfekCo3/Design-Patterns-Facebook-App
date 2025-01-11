using System.Drawing;
using BasicFacebookFeatures.Moods.Interfaces;

namespace BasicFacebookFeatures.Moods.MoodCreators
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