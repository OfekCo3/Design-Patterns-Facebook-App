using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static BasicFacebookFeatures.ProfilePictureFilter;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        private User m_ActiveUser;
        private LoginResult m_LoginResult;
        private readonly AppSettings r_AppSettings;
        private ProfilePictureFilter m_ProfilePictureFilter;

        private enum eComboboxMainOptions
        {
            Feed,
            Likes,
            Albums,
            Groups
        }

        public FormMain()
        {
            InitializeComponent();
            FacebookService.s_CollectionLimit = 25;
            this.StartPosition = FormStartPosition.Manual;
            r_AppSettings = AppSettings.LoadFromFile();
            initializeFilterComponents();
            m_ProfilePictureFilter = new ProfilePictureFilter();

        }

        private void initializeFilterComponents()
        {
            comboBoxFilters.DataSource = Enum.GetValues(typeof(ProfileFilters));
            comboBoxFilters.SelectedIndex = 0;
            comboBoxFilters.DropDownStyle = ComboBoxStyle.DropDownList;
            
            if (Enum.TryParse(r_AppSettings.LastSelectedFilter, out ProfileFilters savedFilter))
            {
                comboBoxFilters.SelectedItem = savedFilter;
            }
            else
            {
                comboBoxFilters.SelectedIndex = 0; // Default to "None"
            }

            buttonProfilePictureFilter.Click += (sender, e) => applySelectedFilter((ProfileFilters)comboBoxFilters.SelectedIndex);
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            r_AppSettings.LastWindowLocation = this.Location;
            r_AppSettings.LastWindowSize = this.Size;
            r_AppSettings.LastSelectedFilter = comboBoxFilters.SelectedItem.ToString();

            if (checkBoxRememberMe.Checked && m_LoginResult != null)
            {
                r_AppSettings.RememberLoggedInUser = true;
                r_AppSettings.AccessToken = m_LoginResult.AccessToken;
            }
            else
            {
                r_AppSettings.RememberLoggedInUser = false;
                r_AppSettings.AccessToken = null;
            }

            r_AppSettings.SaveToFile();
        }

        protected override void OnShown(EventArgs e)
        {
            checkBoxRememberMe.Checked = r_AppSettings.RememberLoggedInUser;
            this.Size = r_AppSettings.LastWindowSize;
            this.Location = r_AppSettings.LastWindowLocation;

            base.OnShown(e);

            if (r_AppSettings.RememberLoggedInUser && !string.IsNullOrEmpty(r_AppSettings.AccessToken))
            {
                try
                {
                    m_LoginResult = FacebookService.Connect(r_AppSettings.AccessToken);
                    loadUserDataToUI();
                }
                catch (Exception)
                {
                    MessageBox.Show(m_LoginResult.ErrorMessage, "Login Failed");
                    m_LoginResult = null;
                }
            }

            ProfileFilters savedFilter = (ProfileFilters)comboBoxFilters.SelectedItem;
            if (savedFilter != ProfileFilters.None)
            {
                applySelectedFilter(savedFilter);
            }

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("design.patterns");

            if (m_LoginResult == null)
            {
                login();
            }
        }

        private void login()
        {
            m_LoginResult = FacebookService.Login(
                textBoxAppID.Text,
                "email",
                "public_profile",
                "user_hometown",
                "user_birthday",
                "user_link",
                "user_friends",
                "user_location",
                "user_likes",
                "user_photos",
                "user_videos",
                "user_posts"
            );

            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                loadUserDataToUI();
            }
            else
            {
                MessageBox.Show(m_LoginResult.ErrorMessage, "Login Failed");
                m_LoginResult = null;
            }
        }

        private void loadUserDataToUI()
        {
            const bool isLoggingIn = true;

            m_ActiveUser = m_LoginResult.LoggedInUser;
            buttonLogin.Text = $"Logged in as {m_ActiveUser.Name}";
            buttonLogin.BackColor = Color.LightGreen;
            tabMainApp.Text = $"{m_ActiveUser.Name}";
            updateUIBasedOnLoginStatus(isLoggingIn);
            loadUserInformation();
        }

        private void updateUIBasedOnLoginStatus(bool i_IsLoggedIn)
        {
            buttonLogin.Enabled = !i_IsLoggedIn;
            buttonLogout.Enabled = i_IsLoggedIn;
            comboBoxMain.Enabled = i_IsLoggedIn;
            textBoxPost.Enabled = i_IsLoggedIn;
            buttonProfilePictureFilter.Enabled = i_IsLoggedIn;
        }

        private void loadUserInformation()
        {
            try
            {
                pictureBoxProfile.ImageLocation = !string.IsNullOrEmpty(m_ActiveUser.PictureNormalURL)
                    ? m_ActiveUser.PictureNormalURL
                    : null;

                pictureBoxCover.ImageLocation = m_ActiveUser.Cover?.SourceURL;
            }
            catch (Exception)
            {
                //pictureBoxCover.Image = Properties.Resources.DefaultCoverImage;
            }

            loadUserFeed();
            loadUserEvents();
            loadUserFriends();
        }

        private void loadUserEvents()
        {
            listBoxEvents.Items.Clear();

            try
            {
                foreach (Event userEvent in m_ActiveUser.Events)
                {
                    if (userEvent.Name != null)
                    {
                        listBoxEvents.Items.Add($"{userEvent.Name} [{userEvent.TimeString}]");
                    }
                }

                if (listBoxEvents.Items.Count == 0)
                {
                    listBoxEvents.Items.Add("No upcoming events.");
                }
            }
            catch
            {
                MessageBox.Show("Error retrieving events.");
            }
        }

        private void loadUserFeed()
        {
            listBoxMain.Items.Clear();

            try
            {
                foreach (Post post in m_ActiveUser.NewsFeed)
                {
                    if (!string.IsNullOrEmpty(post.Message))
                    {
                        listBoxMain.Items.Add(post.Message);
                    }
                    else if (!string.IsNullOrEmpty(post.Caption))
                    {
                        listBoxMain.Items.Add(post.Caption);
                    }
                    else
                    {
                        listBoxMain.Items.Add($"[{post.CreatedTime}]");
                    }
                }

                if (listBoxMain.Items.Count == 0)
                {
                    listBoxMain.Items.Add("No feed to display.");
                }
            }
            catch
            {
                MessageBox.Show("Error retrieving feed.");
            }
        }

        private void loadUserFriends()
        {
            listBoxFriends.Items.Clear();
            listBoxFriends.DisplayMember = "Name";

            try
            {
                foreach (User friend in m_ActiveUser.Friends)
                {
                    listBoxFriends.Items.Add(friend);
                }

                if (listBoxFriends.Items.Count == 0)
                {
                    listBoxFriends.Enabled = false;
                    listBoxFriends.Items.Add("No friends found.");
                }
            }
            catch
            {
                MessageBox.Show("Error retrieving friends.");
            }
        }

        private void loadUserLikedPages()
        {
            listBoxMain.Items.Clear();
            listBoxMain.DisplayMember = "Name";
            try
            {
                foreach (Page page in m_ActiveUser.LikedPages)
                {
                    listBoxMain.Items.Add(page);
                }

                if (listBoxMain.Items.Count == 0)
                {
                    listBoxMain.Items.Add("No liked pages to display");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("failed to retrive liked pages");
            }
        }

        private void loadUserGroups()
        {
            listBoxMain.Items.Clear();
            listBoxMain.DisplayMember = "Name";
            try
            {
                foreach (Group group in m_ActiveUser.Groups)
                {
                    listBoxMain.Items.Add(group);
                }

                if (listBoxMain.Items.Count == 0)
                {
                    listBoxMain.Items.Add("No groups to display");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("failed to retrive groups");
            }
        }

        private void loadUserAlbums()
        {
            listBoxMain.Items.Clear();
            listBoxMain.DisplayMember = "Name";
            try
            {
                foreach (Album album in m_ActiveUser.Albums)
                {
                    listBoxMain.Items.Add(album);
                }

                if (listBoxMain.Items.Count == 0)
                {
                    listBoxMain.Items.Add("No Albums to display");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("falied to retrive albums");
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            FacebookService.LogoutWithUI();
            buttonLogin.Text = "Login";
            buttonLogin.BackColor = buttonLogout.BackColor;
            m_LoginResult = null;
            buttonLogin.Enabled = true;
            buttonLogout.Enabled = false;
        }

        private void textBoxAppID_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxProfile_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            eComboboxMainOptions selectedFeature = (eComboboxMainOptions)comboBoxMain.SelectedIndex;

            switch (selectedFeature)
            {
                case eComboboxMainOptions.Feed:
                    loadUserFeed();
                    break;
                case eComboboxMainOptions.Likes:
                    loadUserLikedPages();
                    break;
                case eComboboxMainOptions.Albums:
                    loadUserAlbums();
                    break;
                case eComboboxMainOptions.Groups:
                    loadUserGroups();
                    break;
            }

        }

        private void textBoxPost_TextChanged(object sender, EventArgs e)
        {
            const bool v_PostButtonsEnabled = true;

            changePostButtonsState(v_PostButtonsEnabled);

        }
        private void changePostButtonsState(bool i_ButtonsState)
        {
            buttonPost.Enabled = i_ButtonsState;
            buttonAddPicture.Enabled = i_ButtonsState;
            buttonCancelPost.Enabled = i_ButtonsState;
        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            const bool v_PostButtonsEnabled = true;

            try
            {
                if (m_ActiveUser != null)
                {
                    if (string.IsNullOrWhiteSpace(textBoxPost.Text))
                    {
                        throw new Exception("Please enter your post first!");
                    }
                    else
                    {
                        try
                        {
                            Status postedStatus = m_ActiveUser.PostStatus(textBoxPost.Text);

                            if (postedStatus == null)
                            {
                                throw new Exception("Post failed");
                            }
                        }
                        catch (Exception exception)
                        {
                            throw new Exception("Post failed");
                        }
                    }
                }
                else
                {
                    throw new Exception("Please Login first!");
                }
                changePostButtonsState(!v_PostButtonsEnabled);
                MessageBox.Show("Posted!");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                textBoxPost.Text = string.Empty;
            }

        }

        private void buttonAddPicture_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialogPostPicture.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string picturePath = openFileDialogPostPicture.FileName;

                if (string.IsNullOrWhiteSpace(picturePath))
                {
                    throw new Exception("Please choose a picture first!");
                }
                else
                {
                    try
                    {
                        Status postedStatus = m_ActiveUser.PostStatus(i_StatusText: textBoxPost.Text, i_PictureURL: picturePath);
                        
                        if (postedStatus == null)
                        {
                            throw new Exception("Picture post failed");
                        }
                    }
                    
                    catch (Exception exception)
                    {
                        MessageBox.Show("Picture post failed");
                    }

                }
            }

        }

        private void openFileDialogPostPicture_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void buttonCancelPost_Click(object sender, EventArgs e)
        {
            const bool v_PostButtonsEnabled = true;

            textBoxPost.Text = string.Empty;
            changePostButtonsState(!v_PostButtonsEnabled);
        }

        private void buttonProfilePictureFilter_Click(object sender, EventArgs e)
        {
        }

        private void applySelectedFilter(ProfileFilters filter)
        {
            if (pictureBoxProfile.Image == null)
            {
                MessageBox.Show("No profile picture loaded.");
                return;
            }

            try
            {
                if (filter == ProfileFilters.None)
                {
                    pictureBoxProfile.ImageLocation = m_ActiveUser?.PictureNormalURL;
                }

                Image filteredImage = m_ProfilePictureFilter.ApplyFilter(pictureBoxProfile.Image, filter);
                pictureBoxProfile.Image = filteredImage;
                MessageBox.Show($"Filter '{filter}' applied successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to apply filter: {ex.Message}");
            }
        }

        private void comboBoxFiltersSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
