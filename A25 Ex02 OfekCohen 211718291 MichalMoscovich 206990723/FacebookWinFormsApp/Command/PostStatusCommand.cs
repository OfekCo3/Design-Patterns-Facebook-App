namespace BasicFacebookFeatures.Command
{
    public class PostStatusCommand : ICommand
    {
        private readonly PostReceiver r_Receiver;
        private readonly string r_PostText;

        public PostStatusCommand(PostReceiver i_Receiver, string i_PostText)
        {
            r_Receiver = i_Receiver;
            r_PostText = i_PostText;
        }

        public void Execute()
        {
            r_Receiver.SetPostData(r_PostText);
            r_Receiver.PostStatus();
        }
    }
}