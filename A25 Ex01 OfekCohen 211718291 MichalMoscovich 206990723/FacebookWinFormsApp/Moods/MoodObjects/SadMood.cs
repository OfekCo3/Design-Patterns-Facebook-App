using System.Drawing;
using BasicFacebookFeatures.Properties;

namespace BasicFacebookFeatures.Moods.MoodObjects
{
    public class SadMood : BaseMood
    {
        protected override Image GetMoodOverlay()
        {
            return Resources.sad_mood;
        }

        public override string GetMoodName()
        {
            return "Sad";
        }

        public override Color GetMoodColor()
        {
            return Color.Blue;
        }

        public override string GetMoodEmoji()
        {
            return "😢";
        }
    }
} 