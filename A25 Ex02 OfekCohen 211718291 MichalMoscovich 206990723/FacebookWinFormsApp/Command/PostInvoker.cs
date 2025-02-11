using System;

namespace BasicFacebookFeatures.Command
{
    public class PostInvoker
    {
        private Action m_Command;

        public void SetCommand(Action i_Command)
        {
            m_Command = i_Command;
        }

        public void ExecuteCommand()
        {
            m_Command?.Invoke();
        }
    }
}