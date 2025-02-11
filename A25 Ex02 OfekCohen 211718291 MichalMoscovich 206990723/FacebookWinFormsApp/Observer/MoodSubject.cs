using System.Collections.Generic;
using FacebookWrapper.ObjectModel;
using static BasicFacebookFeatures.ProfileMood;

namespace BasicFacebookFeatures.Observer
{
    public class MoodSubject
    {
        private readonly List<IMoodObserver> r_Observers = new List<IMoodObserver>();
        private eProfileMoodType m_CurrentMood;
        private readonly Dictionary<User, eProfileMoodType> r_FriendsMoods = new Dictionary<User, eProfileMoodType>();

        public eProfileMoodType CurrentMood
        {
            get => m_CurrentMood;
            set
            {
                if (m_CurrentMood != value)
                {
                    m_CurrentMood = value;
                    Notify();
                }
            }
        }

        public void UpdateFriendMood(User i_Friend, eProfileMoodType i_Mood)
        {
            r_FriendsMoods[i_Friend] = i_Mood;
            NotifyFriendMoodChanged(i_Friend, i_Mood);
        }

        public void Attach(IMoodObserver i_Observer)
        {
            r_Observers.Add(i_Observer);
        }

        public void Detach(IMoodObserver i_Observer)
        {
            r_Observers.Remove(i_Observer);
        }

        private void Notify()
        {
            foreach (IMoodObserver observer in r_Observers)
            {
                observer.Update(m_CurrentMood);
            }
        }

        private void NotifyFriendMoodChanged(User i_Friend, eProfileMoodType i_Mood)
        {
            foreach (IMoodObserver observer in r_Observers)
            {
                observer.OnFriendMoodChanged(i_Friend, i_Mood);
            }
        }

        public Dictionary<User, eProfileMoodType> GetFriendsWithSameMood(eProfileMoodType i_Mood)
        {
            Dictionary<User, eProfileMoodType> sameMoodFriends = new Dictionary<User, eProfileMoodType>();
            
            foreach (var friendMood in r_FriendsMoods)
            {
                if (friendMood.Value == i_Mood)
                {
                    sameMoodFriends.Add(friendMood.Key, friendMood.Value);
                }
            }

            return sameMoodFriends;
        }
    }
}