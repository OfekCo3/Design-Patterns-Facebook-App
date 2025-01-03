namespace BasicFacebookFeatures.Moods
{
    public class HappyMood : BaseMood
    {
        protected override Image GetMoodOverlay()
        {
            return Properties.Resources.happy_mood;
        }

        public override string GetMoodName()
        {
            return "Happy";
        }

        public override Color GetMoodColor()
        {
            return Color.Yellow;
        }

        public override string GetMoodEmoji()
        {
            return "😊";
        }
    }
} 