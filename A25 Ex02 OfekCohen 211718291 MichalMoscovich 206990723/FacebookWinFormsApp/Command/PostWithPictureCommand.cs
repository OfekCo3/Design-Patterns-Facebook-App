namespace BasicFacebookFeatures.Command
{
    public class PostWithPictureCommand : ICommand
    {
        private readonly PostReceiver r_Receiver;
        private readonly string r_PostText;
        private readonly string r_PicturePath;

        public PostWithPictureCommand(PostReceiver i_Receiver, string i_PostText, string i_PicturePath)
        {
            r_Receiver = i_Receiver;
            r_PostText = i_PostText;
            r_PicturePath = i_PicturePath;
        }

        public void Execute()
        {
            r_Receiver.SetPostData(r_PostText, r_PicturePath);
            r_Receiver.PostStatusWithPicture();
        }
    }
}