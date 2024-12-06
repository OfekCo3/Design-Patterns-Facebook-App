using BasicFacebookFeatures.Properties;
using System.Drawing;

namespace BasicFacebookFeatures
{
    internal class ProfilePictureFilter
    {            
        public enum eProfileFilters
        {
            None = 0,
            PinkFilter,
            OrangeFilter,
            BlueFilter
        }

        public Image ApplyFilter(Image i_OriginalImage, eProfileFilters i_Filter)
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

        private Image getFilterImage(eProfileFilters i_Filter)
        {
            switch (i_Filter)
            {
                case eProfileFilters.PinkFilter:
                    return Resources.pink_filter;
                case eProfileFilters.OrangeFilter:
                    return Resources.orange_filter;
                case eProfileFilters.BlueFilter:
                    return Resources.blue_filter;
                default:
                    return null;
            }
        }
    }
}