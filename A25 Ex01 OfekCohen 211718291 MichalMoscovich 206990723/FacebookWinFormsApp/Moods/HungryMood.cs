namespace BasicFacebookFeatures.Moods
{
    public class HungryMood : BaseMood
    {
        protected override Image GetMoodOverlay()
        {
            return Properties.Resources.hungry_mood;
        }

        public override string GetMoodName()
        {
            return "Hungry";
        }

        public override Color GetMoodColor()
        {
            return Color.Orange;
        }

        public override string GetMoodEmoji()
        {
            return "😋";
        }
    }
} 