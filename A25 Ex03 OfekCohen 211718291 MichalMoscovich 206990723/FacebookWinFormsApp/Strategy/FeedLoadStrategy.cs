using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BasicFacebookFeatures.Facade;

namespace BasicFacebookFeatures.Strategy
{
    public class FeedLoadStrategy : IContentLoadStrategy
    {
        private readonly ListBox r_ListBox;
        private readonly FacebookSystemFacade r_FacebookSystemFacade;

        public FeedLoadStrategy(ListBox i_ListBox, FacebookSystemFacade i_FacebookSystemFacade)
        {
            r_ListBox = i_ListBox;
            r_FacebookSystemFacade = i_FacebookSystemFacade;
        }

        public IEnumerable<object> LoadContent(User i_User)
        {
            var tempListBox = new ListBox();
            r_FacebookSystemFacade.LoadUserFeed(i_User, tempListBox);
            return tempListBox.Items.Cast<object>().ToList();
        }

        public string GetContentDescription()
        {
            return "News Feed Posts";
        }
    }
}