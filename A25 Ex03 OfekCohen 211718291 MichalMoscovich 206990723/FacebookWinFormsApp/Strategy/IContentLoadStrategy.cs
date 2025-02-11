using FacebookWrapper.ObjectModel;
using System.Collections.Generic;

namespace BasicFacebookFeatures.Strategy
{
    public interface IContentLoadStrategy
    {
        IEnumerable<object> LoadContent(User i_User);
        string GetContentDescription();
    }
}