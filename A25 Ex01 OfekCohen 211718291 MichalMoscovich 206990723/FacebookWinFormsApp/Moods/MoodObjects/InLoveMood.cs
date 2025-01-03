using System.Drawing;
using BasicFacebookFeatures.Properties;

namespace BasicFacebookFeatures.Moods.MoodObjects
{
    public class InLoveMood : BaseMood
    {
        protected override Image GetMoodOverlay()
        {
            return Resources.inLove_mood;
        }

        public override string GetMoodName()
        {
            return "In Love";
        }

        public override Color GetMoodColor()
        {
            return Color.Pink;
        }

        public override string GetMoodEmoji()
        {
            return "😍";
        }
    }
} 