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

        public static IContentLoadStrategy CreateStrategy(eContentType i_ContentType)
        {
            switch (i_ContentType)
            {
                case eContentType.Feed:
                    return new FeedLoadStrategy();
                case eContentType.Likes:
                    return new LikesLoadStrategy();
                case eContentType.Albums:
                    return new AlbumsLoadStrategy();
                case eContentType.Groups:
                    return new GroupsLoadStrategy();
                default:
                    return new FeedLoadStrategy(); // Default to feed
            }
        }
    }
}