using BasicFacebookFeatures.Properties;
using System.Drawing;
using BasicFacebookFeatures.Filters;

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

        public Image ApplyFilter(Image i_OriginalImage, eProfileFilter i_FilterType)
        {
            IFilterBuilder builder = new ProfileFilterBuilder(i_OriginalImage);

            switch (i_FilterType)
            {
                case eProfileFilter.Blue:
                    return builder
                        .ApplyColorTint(Color.Blue)
                        .ApplyOverlay(Properties.Resources.blue_filter)
                        .ApplyTransparency(0.8f)
                        .Build();
                case eProfileFilter.Pink:
                    return builder
                        .ApplyColorTint(Color.Pink)
                        .ApplyOverlay(Properties.Resources.pink_filter)
                        .ApplyTransparency(0.7f)
                        .ApplySepia()
                        .Build();
                case eProfileFilter.Orange:
                    return builder
                        .ApplyColorTint(Color.Orange)
                        .ApplyOverlay(Properties.Resources.orange_filter)
                        .ApplyTransparency(0.75f)
                        .ApplyBlur(2)
                        .Build();
                case eProfileFilter.Green:
                    return builder
                        .ApplyColorTint(Color.Green)
                        .ApplyOverlay(Properties.Resources.green_filter)
                        .ApplyTransparency(0.85f)
                        .ApplyGrayscale()
                        .Build();
                case eProfileFilter.None:
                default:
                    return i_OriginalImage;
            }
        }
    }
}