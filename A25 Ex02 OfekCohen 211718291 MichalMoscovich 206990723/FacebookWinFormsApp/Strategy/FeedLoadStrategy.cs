using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace BasicFacebookFeatures.Strategy
{
    public class FeedLoadStrategy : IContentLoadStrategy
    {
        public IEnumerable<object> LoadContent(User user)
        {
            return user.Posts?.Cast<object>() ?? Enumerable.Empty<object>();
        }

        public string GetContentDescription()
        {
            return "News Feed Posts";
        }
    }
}