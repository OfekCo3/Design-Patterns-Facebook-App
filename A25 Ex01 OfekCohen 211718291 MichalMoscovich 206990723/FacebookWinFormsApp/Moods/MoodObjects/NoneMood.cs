using System.Drawing;
using BasicFacebookFeatures.Properties;

namespace BasicFacebookFeatures.Moods.MoodObjects
{
    public class NoneMood : BaseMood
    {
        protected override Image GetMoodOverlay()
        {
            return Resources.gray_background;
        }

        public override string GetMoodName()
        {
            return "None";
        }

        public override Color GetMoodColor()
        {
            return Color.Gray;
        }

        public override string GetMoodEmoji()
        {
            return string.Empty;
        }
    }
} 