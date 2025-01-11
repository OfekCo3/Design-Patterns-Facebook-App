using System.Drawing;
using System.Drawing.Imaging;
using BasicFacebookFeatures.Moods.Interfaces;

namespace BasicFacebookFeatures.Moods.MoodObjects
{
    public abstract class BaseMood : IMood
    {
        private const float k_TransparencyFactor = 0.8f;
        private const int k_ColorAlpha = 30;

        public virtual Image ApplyMood(Image i_OriginalImage)
        {
            if (i_OriginalImage == null)
            {
                return null;
            }

            Image moodOverlay = GetMoodOverlay();
            if (moodOverlay == null)
            {
                return i_OriginalImage;
            }

            return createMoodedImage(i_OriginalImage, moodOverlay);
        }

        private Image createMoodedImage(Image i_OriginalImage, Image i_MoodOverlay)
        {
            Bitmap resultImage = new Bitmap(i_OriginalImage.Width, i_OriginalImage.Height);
            using (Graphics g = Graphics.FromImage(resultImage))
            {
                ColorMatrix colorMatrix = new ColorMatrix();
                colorMatrix.Matrix33 = k_TransparencyFactor;
                
                ImageAttributes imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix);

                applyImageWithTint(g, i_OriginalImage, resultImage);
                applyMoodOverlay(g, i_MoodOverlay, resultImage, imageAttributes);
            }

            return resultImage;
        }

        private void applyImageWithTint(Graphics i_Graphics, Image i_OriginalImage, Bitmap i_ResultImage)
        {
            using (SolidBrush tintBrush = new SolidBrush(Color.FromArgb(k_ColorAlpha, GetMoodColor())))
            {
                i_Graphics.DrawImage(i_OriginalImage, 0, 0, i_OriginalImage.Width, i_OriginalImage.Height);
                i_Graphics.FillRectangle(tintBrush, 0, 0, i_ResultImage.Width, i_ResultImage.Height);
            }
        }

        private void applyMoodOverlay(
            Graphics i_Graphics, 
            Image i_MoodOverlay, 
            Bitmap i_ResultImage, 
            ImageAttributes i_ImageAttributes)
        {
            i_Graphics.DrawImage(
                i_MoodOverlay,
                new Rectangle(0, 0, i_ResultImage.Width, i_ResultImage.Height),
                0,
                0,
                i_MoodOverlay.Width,
                i_MoodOverlay.Height,
                GraphicsUnit.Pixel,
                i_ImageAttributes);
        }

        protected abstract Image GetMoodOverlay();

        public abstract string GetMoodName();

        public abstract Color GetMoodColor();

        public abstract string GetMoodEmoji();
    }
} 