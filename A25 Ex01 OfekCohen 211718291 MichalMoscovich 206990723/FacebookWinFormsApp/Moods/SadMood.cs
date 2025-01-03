namespace BasicFacebookFeatures.Moods
{
    public class SadMood : BaseMood
    {
        protected override Image GetMoodOverlay()
        {
            return Properties.Resources.sad_mood;
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