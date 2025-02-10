using FacebookWrapper.ObjectModel;
using System.Collections.Generic;

namespace BasicFacebookFeatures.Strategy
{
    public interface IContentLoadStrategy
    {
        IEnumerable<object> LoadContent(User user);
        string GetContentDescription();
    }
}