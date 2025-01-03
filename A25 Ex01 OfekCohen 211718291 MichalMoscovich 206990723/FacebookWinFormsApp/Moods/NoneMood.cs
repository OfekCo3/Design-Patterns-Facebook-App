namespace BasicFacebookFeatures.Moods
{
    public class NoneMood : BaseMood
    {
        protected override Image GetMoodOverlay()
        {
            return Properties.Resources.gray_background;
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
            return "";
        }
    }
} 