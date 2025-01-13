using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace BasicFacebookFeatures.Filters
{
    public class ProfileFilterBuilder : IFilterBuilder
    {
        private readonly Image r_OriginalImage;
        private Color m_TintColor;
        private Image m_Overlay;
        private float m_TransparencyFactor;
        private bool m_IsGrayscale;
        private bool m_IsSepia;
        private int m_BlurRadius;
        private List<(Image Sticker, Point Position, float Scale)> m_Stickers;

        public ProfileFilterBuilder(Image i_OriginalImage)
        {
            r_OriginalImage = i_OriginalImage;
            m_TransparencyFactor = 1.0f;
            m_TintColor = Color.Transparent;
            m_Stickers = new List<(Image, Point, float)>();
        }

        public IFilterBuilder ApplyColorTint(Color i_Color)
        {
            m_TintColor = i_Color;
            return this;
        }

        public IFilterBuilder ApplyOverlay(Image i_Overlay)
        {
            m_Overlay = i_Overlay;
            return this;
        }

        public IFilterBuilder ApplyTransparency(float i_Factor)
        {
            m_TransparencyFactor = i_Factor;
            return this;
        }

        public IFilterBuilder ApplyGrayscale()
        {
            m_IsGrayscale = true;
            return this;
        }

        public IFilterBuilder ApplySepia()
        {
            m_IsSepia = true;
            return this;
        }

        public IFilterBuilder ApplyBlur(int i_Radius)
        {
            m_BlurRadius = i_Radius;
            return this;
        }

        public IFilterBuilder AddSticker(Image i_Sticker, Point i_Position, float i_Scale)
        {
            m_Stickers.Add((i_Sticker, i_Position, i_Scale));
            return this;
        }

        public Image Build()
        {
            if (r_OriginalImage == null)
            {
                return null;
            }

            Bitmap resultImage = new Bitmap(r_OriginalImage.Width, r_OriginalImage.Height);
            using (Graphics g = Graphics.FromImage(resultImage))
            {
                // Draw original image
                g.DrawImage(r_OriginalImage, 0, 0);

                // Apply effects in order
                if (m_IsGrayscale)
                {
                    applyGrayscaleEffect(resultImage);
                }
                if (m_IsSepia)
                {
                    applySepiaEffect(resultImage);
                }
                if (m_BlurRadius > 0)
                {
                    applyBlurEffect(resultImage, m_BlurRadius);
                }
                if (m_TintColor != Color.Transparent)
                {
                    using (SolidBrush tintBrush = new SolidBrush(Color.FromArgb(30, m_TintColor)))
                    {
                        g.FillRectangle(tintBrush, 0, 0, resultImage.Width, resultImage.Height);
                    }
                }
                if (m_Overlay != null)
                {
                    applyOverlayImage(g, resultImage);
                }

                // Apply stickers
                foreach (var (sticker, position, scale) in m_Stickers)
                {
                    int width = (int)(sticker.Width * scale);
                    int height = (int)(sticker.Height * scale);
                    g.DrawImage(sticker, position.X, position.Y, width, height);
                }
            }

            return resultImage;
        }

        private void applyGrayscaleEffect(Bitmap i_Image)
        {
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][]
                {
                    new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                    new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                    new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });

            applyColorMatrix(i_Image, colorMatrix);
        }

        private void applySepiaEffect(Bitmap i_Image)
        {
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][]
                {
                    new float[] {0.393f, 0.349f, 0.272f, 0, 0},
                    new float[] {0.769f, 0.686f, 0.534f, 0, 0},
                    new float[] {0.189f, 0.168f, 0.131f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });

            applyColorMatrix(i_Image, colorMatrix);
        }

        private void applyColorMatrix(Bitmap i_Image, ColorMatrix i_ColorMatrix)
        {
            using (Graphics g = Graphics.FromImage(i_Image))
            {
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(i_ColorMatrix);
                g.DrawImage(i_Image, new Rectangle(0, 0, i_Image.Width, i_Image.Height),
                    0, 0, i_Image.Width, i_Image.Height, GraphicsUnit.Pixel, attributes);
            }
        }

        private void applyBlurEffect(Bitmap i_Image, int i_Radius)
        {
            // Simple box blur implementation
            Bitmap blurred = new Bitmap(i_Image);
            for (int x = i_Radius; x < i_Image.Width - i_Radius; x++)
            {
                for (int y = i_Radius; y < i_Image.Height - i_Radius; y++)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int count = 0;

                    for (int i = -i_Radius; i <= i_Radius; i++)
                    {
                        for (int j = -i_Radius; j <= i_Radius; j++)
                        {
                            Color pixel = i_Image.GetPixel(x + i, y + j);
                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;
                            count++;
                        }
                    }

                    blurred.SetPixel(x, y, Color.FromArgb(avgR / count, avgG / count, avgB / count));
                }
            }

            using (Graphics g = Graphics.FromImage(i_Image))
            {
                g.DrawImage(blurred, 0, 0);
            }
        }

        private void applyOverlayImage(Graphics i_Graphics, Bitmap i_ResultImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix();
            colorMatrix.Matrix33 = m_TransparencyFactor;
            
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);

            i_Graphics.DrawImage(
                m_Overlay,
                new Rectangle(0, 0, i_ResultImage.Width, i_ResultImage.Height),
                0, 0, m_Overlay.Width, m_Overlay.Height,
                GraphicsUnit.Pixel,
                imageAttributes);
        }
    }
} 