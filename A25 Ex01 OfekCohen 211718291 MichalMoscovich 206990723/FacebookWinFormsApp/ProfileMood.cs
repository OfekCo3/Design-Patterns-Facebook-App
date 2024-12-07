using BasicFacebookFeatures.Properties;
using System.Drawing;

namespace BasicFacebookFeatures
{
    public class ProfileMood
    {
        public enum eProfileMoodType
        {
            None = 0,
            Happy,
            Loving,
            Sad,
            Hungry
        }

        public Image ApplyMood(Image i_OriginalImage, eProfileMoodType i_MoodType)
        {
            Image moodImage = getMoodImage(i_MoodType);
            Bitmap moodedImage = new Bitmap(i_OriginalImage);

            using (Graphics g = Graphics.FromImage(moodedImage))
            {
                g.DrawImage(moodImage, new Rectangle(0, 0, i_OriginalImage.Width, i_OriginalImage.Height));
            }

            return moodImage;
        }

        private Image getMoodImage(eProfileMoodType i_MoodType)
        {
            switch (i_MoodType)
            {
                case eProfileMoodType.Happy:
                    return Resources.happy_mood;
                case eProfileMoodType.Sad:
                    return Resources.sad_mood;
                case eProfileMoodType.Loving:
                    return Resources.inLove_mood;
                case eProfileMoodType.Hungry:
                    return Resources.hungry_mood;
                default:
                    return Resources.gray_background;
            }
        }
    }
}