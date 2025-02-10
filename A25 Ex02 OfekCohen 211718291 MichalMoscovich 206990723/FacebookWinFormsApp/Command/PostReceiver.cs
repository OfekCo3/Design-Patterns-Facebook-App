using FacebookWrapper.ObjectModel;
using BasicFacebookFeatures.Facade;

namespace BasicFacebookFeatures.Command
{
    public class PostReceiver
    {
        private readonly User r_User;
        private readonly FacebookSystemFacade r_FacebookSystemFacade;
        private string m_PostText;
        private string m_PicturePath;

        public PostReceiver(User i_User, FacebookSystemFacade i_FacebookSystemFacade)
        {
            r_User = i_User;
            r_FacebookSystemFacade = i_FacebookSystemFacade;
        }

        public void SetPostData(string i_PostText)
        {
            m_PostText = i_PostText;
        }

        public void SetPostData(string i_PostText, string i_PicturePath)
        {
            m_PostText = i_PostText;
            m_PicturePath = i_PicturePath;
        }

        public void PostStatus()
        {
            r_FacebookSystemFacade.PostStatus(r_User, m_PostText);
        }

        public void PostStatusWithPicture()
        {
            r_FacebookSystemFacade.PostStatusWithPicture(r_User, m_PostText, m_PicturePath);
        }
    }
}