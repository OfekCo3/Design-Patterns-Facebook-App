using System.Drawing;

namespace BasicFacebookFeatures.Filters
{
    public class FilterComposer
    {
        private readonly IFilterBuilder r_Builder;

        public FilterComposer(IFilterBuilder i_Builder)
        {
            r_Builder = i_Builder;
        }

        public Image CreateBlueFilter()
        {
            return r_Builder
                .ApplyColorTint(Color.Blue)
                .ApplyOverlay(Properties.Resources.blue_filter)
                .ApplyTransparency(0.8f)
                .Build();
        }

        public Image CreatePinkFilter()
        {
            return r_Builder
                .ApplyColorTint(Color.Pink)
                .ApplyOverlay(Properties.Resources.pink_filter)
                .ApplyTransparency(0.7f)
                .ApplySepia()
                .Build();
        }

        public Image CreateOrangeFilter()
        {
            return r_Builder
                .ApplyColorTint(Color.Orange)
                .ApplyOverlay(Properties.Resources.orange_filter)
                .ApplyTransparency(0.75f)
                .ApplyBlur(2)
                .Build();
        }

        public Image CreateGreenFilter()
        {
            return r_Builder
                .ApplyColorTint(Color.Green)
                .ApplyOverlay(Properties.Resources.green_filter)
                .ApplyTransparency(0.85f)
                .ApplyGrayscale()
                .Build();
        }
    }
} 