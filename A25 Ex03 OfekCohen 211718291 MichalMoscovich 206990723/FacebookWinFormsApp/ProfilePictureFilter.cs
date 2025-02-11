using BasicFacebookFeatures.Properties;
using System.Drawing;
using BasicFacebookFeatures.Filters;

namespace BasicFacebookFeatures
{
    public class ProfilePictureFilter
    {            
        public enum eProfileFilter
        {
            None = 0,
            Pink,
            Orange,
            Blue,
            Green
        }

        public Image ApplyFilter(Image i_OriginalImage, eProfileFilter i_FilterType)
        {
            IFilterBuilder builder = new ProfileFilterBuilder(i_OriginalImage);
            FilterComposer composer = new FilterComposer(builder);

            switch (i_FilterType)
            {
                case eProfileFilter.Blue:
                    return composer.CreateBlueFilter();
                case eProfileFilter.Pink:
                    return composer.CreatePinkFilter();
                case eProfileFilter.Orange:
                    return composer.CreateOrangeFilter();
                case eProfileFilter.Green:
                    return composer.CreateGreenFilter();
                case eProfileFilter.None:
                default:
                    return i_OriginalImage;
            }
        }
    }
}