using FacebookWrapper;

public sealed class FacebookServiceSingleton
{
    private static FacebookServiceSingleton s_Instance = null;
    private static readonly object s_LockObj = new object();

    private FacebookServiceSingleton()
    {
        FacebookService.s_UseForamttedToStrings = true;
    }

    public static FacebookServiceSingleton Instance
    {
        get
        {
            if (s_Instance == null)
            {
                lock (s_LockObj)
                {
                    if (s_Instance == null)
                    {
                        s_Instance = new FacebookServiceSingleton();
                    }
                }
            }

            return s_Instance;
        }
    }

    public void SetCollectionLimit(int i_CollectionLimit)
    {
        FacebookService.s_CollectionLimit = i_CollectionLimit;
    }

    public LoginResult Login(string i_AppId, params string[] i_Permissions)
    {
        return FacebookService.Login(i_AppId, i_Permissions);
    }

    public void Logout()
    {
        FacebookService.LogoutWithUI();
    }
}