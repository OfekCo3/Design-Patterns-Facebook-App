using BasicFacebookFeatures.Properties;
using System.Drawing;

namespace BasicFacebookFeatures
{
    internal class ProfilePictureFilter
    {            
        public enum eProfileFilter
        {
            None = 0,
            Pink,
            Orange,
            Blue,
            Green
        }

        public Image ApplyFilter(Image i_OriginalImage, eProfileFilter i_Filter)
        {
            Image filterImage = getFilterImage(i_Filter);
            Bitmap filteredImage = new Bitmap(i_OriginalImage);

            using (Graphics g = Graphics.FromImage(filteredImage))
            {
                if (filterImage != null)
                {
                    g.DrawImage(filterImage, new Rectangle(0, 0, i_OriginalImage.Width, i_OriginalImage.Height));
                }
                else
                {
                    g.Clear(Color.Transparent);
                }
            }

            return filteredImage;
        }

        private Image getFilterImage(eProfileFilter i_Filter)
        {
            switch (i_Filter)
            {
                case eProfileFilter.Pink:
                    return Resources.pink_filter;
                case eProfileFilter.Orange:
                    return Resources.orange_filter;
                case eProfileFilter.Blue:
                    return Resources.blue_filter;
                case eProfileFilter.Green:
                    return Resources.green_filter;
                default:
                    return null;
            }
        }
    }
}