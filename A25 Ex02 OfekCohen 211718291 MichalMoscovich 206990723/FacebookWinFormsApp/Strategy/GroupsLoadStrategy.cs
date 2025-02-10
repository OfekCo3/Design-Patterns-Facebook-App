using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace BasicFacebookFeatures.Strategy
{
    public class GroupsLoadStrategy : IContentLoadStrategy
    {
        public IEnumerable<object> LoadContent(User user)
        {
            return user.Groups?.Cast<object>() ?? Enumerable.Empty<object>();
        }

        public string GetContentDescription()
        {
            return "Groups";
        }
    }
}