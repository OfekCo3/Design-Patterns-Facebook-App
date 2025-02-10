using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace BasicFacebookFeatures.Strategy
{
    public class AlbumsLoadStrategy : IContentLoadStrategy
    {
        public IEnumerable<object> LoadContent(User user)
        {
            return user.Albums?.Cast<object>() ?? Enumerable.Empty<object>();
        }

        public string GetContentDescription()
        {
            return "Photo Albums";
        }
    }
}