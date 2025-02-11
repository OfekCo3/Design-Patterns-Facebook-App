using FacebookWrapper.ObjectModel;
using static BasicFacebookFeatures.ProfileMood;

namespace BasicFacebookFeatures.Observer
{
    public interface IMoodObserver
    {
        void Update(eProfileMoodType i_NewMood);
    }
}