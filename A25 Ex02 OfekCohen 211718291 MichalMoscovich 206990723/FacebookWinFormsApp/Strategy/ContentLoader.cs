using FacebookWrapper.ObjectModel;
using System.Collections.Generic;

namespace BasicFacebookFeatures.Strategy
{
    public class ContentLoader
    {
        private IContentLoadStrategy m_LoadStrategy;

        public ContentLoader(IContentLoadStrategy i_InitialStrategy)
        {
            m_LoadStrategy = i_InitialStrategy;
        }

        public void SetStrategy(IContentLoadStrategy i_ContentLoadStrategy)
        {
            m_LoadStrategy = i_ContentLoadStrategy;
        }

        public IEnumerable<object> LoadContent(User i_User)
        {
            return m_LoadStrategy.LoadContent(i_User);
        }

        public string GetContentDescription()
        {
            return m_LoadStrategy.GetContentDescription();
        }
    }
}