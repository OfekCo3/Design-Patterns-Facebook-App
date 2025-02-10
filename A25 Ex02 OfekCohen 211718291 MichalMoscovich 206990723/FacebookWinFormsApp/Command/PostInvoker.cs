namespace BasicFacebookFeatures.Command
{
    public class PostInvoker
    {
        private ICommand m_Command;

        public void SetCommand(ICommand i_Command)
        {
            m_Command = i_Command;
        }

        public void ExecuteCommand()
        {
            m_Command?.Execute();
        }
    }
}