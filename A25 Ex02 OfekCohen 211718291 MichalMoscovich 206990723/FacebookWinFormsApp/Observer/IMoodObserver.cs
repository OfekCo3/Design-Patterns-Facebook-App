using FacebookWrapper.ObjectModel;
using static BasicFacebookFeatures.ProfileMood;

namespace BasicFacebookFeatures.Observer
{
    public interface IMoodObserver
    {
        void Update(eProfileMoodType i_NewMood);
        void OnFriendMoodChanged(User i_Friend, eProfileMoodType i_NewMood);
    }
}