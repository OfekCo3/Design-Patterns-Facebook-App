namespace BasicFacebookFeatures.Moods
{
    public abstract class BaseMood : IMood
    {
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

            Bitmap resultImage = new Bitmap(i_OriginalImage.Width, i_OriginalImage.Height);
            using (Graphics g = Graphics.FromImage(resultImage))
            {
                ColorMatrix colorMatrix = new ColorMatrix();
                colorMatrix.Matrix33 = 0.8f; // Transparency
                
                ImageAttributes imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix);

                // Draw original image with mood color tint
                using (SolidBrush tintBrush = new SolidBrush(Color.FromArgb(30, GetMoodColor())))
                {
                    g.DrawImage(i_OriginalImage, 0, 0, i_OriginalImage.Width, i_OriginalImage.Height);
                    g.FillRectangle(tintBrush, 0, 0, resultImage.Width, resultImage.Height);
                }

                // Draw mood overlay
                g.DrawImage(moodOverlay, 
                    new Rectangle(0, 0, resultImage.Width, resultImage.Height),
                    0, 0, moodOverlay.Width, moodOverlay.Height,
                    GraphicsUnit.Pixel, imageAttributes);
            }

            return resultImage;
        }

        protected abstract Image GetMoodOverlay();
        public abstract string GetMoodName();
        public abstract Color GetMoodColor();
        public abstract string GetMoodEmoji();
    }
} 