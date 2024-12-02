using BasicFacebookFeatures.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    internal class ProfilePictureFilter
    {            
        public enum ProfileFilters
        {
            None = 0,
            PinkFilter,
            OrangeFilter,
            BlueFilter,
            GreenFilter
        }

        public Image ApplyFilter(Image originalImage, ProfileFilters filter)
        {
            Image filterImage = getFilterImage(filter);

            Bitmap filteredImage = new Bitmap(originalImage);
            using (Graphics g = Graphics.FromImage(filteredImage))
            {
                g.DrawImage(filterImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height));
            }

            return filteredImage;
        }

        private Image getFilterImage(ProfileFilters filter)
        {
            switch (filter)
            {
                case ProfileFilters.PinkFilter:
                    return Resources.pink_filter;
                case ProfileFilters.OrangeFilter:
                    return Resources.orange_filter;
                case ProfileFilters.BlueFilter:
                    return Resources.blue_filter;
                //case ProfileFilters.GreenFilter:
                //    return Resources.green_filter;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), "Unknown filter");
            }
        }

    }
}
