using System.Windows.Forms;
using BasicFacebookFeatures.Facade;

namespace BasicFacebookFeatures.Strategy
{
    public static class ContentStrategyFactory
    {
        public enum eContentType
        {
            Feed,
            Likes,
            Albums,
            Groups
        }

        public static IContentLoadStrategy CreateStrategy(eContentType i_ContentType, ListBox i_ListBox, FacebookSystemFacade i_FacebookSystemFacade)
        {
            switch (i_ContentType)
            {
                case eContentType.Feed:
                    return new FeedLoadStrategy(i_ListBox, i_FacebookSystemFacade);
                case eContentType.Likes:
                    return new LikesLoadStrategy(i_ListBox, i_FacebookSystemFacade);
                case eContentType.Albums:
                    return new AlbumsLoadStrategy(i_ListBox, i_FacebookSystemFacade);
                case eContentType.Groups:
                    return new GroupsLoadStrategy(i_ListBox, i_FacebookSystemFacade);
                default:
                    return new FeedLoadStrategy(i_ListBox, i_FacebookSystemFacade); // Default to feed
            }
        }
    }
}