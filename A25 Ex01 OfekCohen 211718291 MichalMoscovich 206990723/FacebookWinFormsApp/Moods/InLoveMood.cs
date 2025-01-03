namespace BasicFacebookFeatures.Moods
{
    public class InLoveMood : BaseMood
    {
        protected override Image GetMoodOverlay()
        {
            return Properties.Resources.inLove_mood;
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