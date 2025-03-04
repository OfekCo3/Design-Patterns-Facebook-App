using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using static BasicFacebookFeatures.ProfileMood;

namespace BasicFacebookFeatures.Observer
{
    public class FriendsMoodObserver : IMoodObserver
    {
        private readonly User r_User;
        private FormFriendsWithSameMood m_FormFriendsWithSameMood;
        private readonly MoodSubject r_Subject;

        public FriendsMoodObserver(User i_User, MoodSubject i_Subject)
        {
            r_User = i_User;
            r_Subject = i_Subject;
            r_Subject.Attach(this);
            initializeFriendsMoods();
        }

        private void initializeFriendsMoods()
        {
            if (r_User.Friends != null)
            {
                foreach (User friend in r_User.Friends)
                {
                    // We will assume None as default for friends' mood
                    r_Subject.UpdateFriendMood(friend, eProfileMoodType.None);
                }
            }
        }

        private void createForm()
        {
            m_FormFriendsWithSameMood = new FormFriendsWithSameMood(r_User, r_Subject.CurrentMood);
        }

        public void Update(eProfileMoodType i_NewMood)
        {
            if (i_NewMood != eProfileMoodType.None)
            {
                createForm();
                MessageBox.Show($"Your mood has been updated! Click 'Who's in the mood?' to see friends with the same mood.");
            }
        }

        public void ShowFriendsWithSameMood()
        {
            if (m_FormFriendsWithSameMood != null)
            {
                m_FormFriendsWithSameMood.ShowDialog();
            }
        }
    }
}