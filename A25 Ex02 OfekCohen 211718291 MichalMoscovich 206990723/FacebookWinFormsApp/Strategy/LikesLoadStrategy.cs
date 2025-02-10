using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace BasicFacebookFeatures.Strategy
{
    public class LikesLoadStrategy : IContentLoadStrategy
    {
        public IEnumerable<object> LoadContent(User user)
        {
            return user.LikedPages?.Cast<object>() ?? Enumerable.Empty<object>();
        }

        public string GetContentDescription()
        {
            return "Liked Pages";
        }
    }
}