using System.Drawing;

namespace BasicFacebookFeatures.Filters
{
    public interface IFilterBuilder
    {
        IFilterBuilder ApplyColorTint(Color i_Color);
        IFilterBuilder ApplyOverlay(Image i_Overlay);
        IFilterBuilder ApplyTransparency(float i_Factor);
        IFilterBuilder ApplyGrayscale();
        IFilterBuilder ApplySepia();
        IFilterBuilder ApplyBlur(int i_Radius);
        IFilterBuilder AddSticker(Image i_Sticker, Point i_Position, float i_Scale);
        Image Build();
    }
} 