using System;
using System.Drawing;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using static BasicFacebookFeatures.ProfilePictureFilter;
using static BasicFacebookFeatures.ProfileMood;
using BasicFacebookFeatures.Moods.Factory;
using BasicFacebookFeatures.Moods.Interfaces;
using BasicFacebookFeatures.Facade;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        private User m_ActiveUser;
        private LoginResult m_LoginResult;
        private readonly AppSettings r_AppSettings;
        private FormFriendsWithSameMood m_FormFriendsWithSameMood;
        private Image m_OriginalProfilePicture;
        private Image m_OriginalCoverImage;
        private const int k_CollectionLimit = 25;
        private enum eComboboxMainOption
        {
            Feed,
            Likes,
            Albums,
            Groups
        }
        private readonly FacebookSystemFacade r_FacebookSystem;

        public FormMain()
        {
            InitializeComponent();
            tabMainApp.TabPages[0].Text = "Welcome";
            FacebookService.s_CollectionLimit = k_CollectionLimit;
            this.StartPosition = FormStartPosition.Manual;
            r_AppSettings = AppSettings.LoadFromFile();
            initializeFilterComboBox();
            initializeMoodComboBox();
            r_FacebookSystem = new FacebookSystemFacade();

            // Update position relative to pictureBoxCover
            labelMoodName.BringToFront(); // Make sure label is visible above other controls
            labelMoodName.Location = new Point(
                10,  // X position relative to tabMainPage
                pictureBoxCover.Bottom - 40); // Y position relative to pictureBoxCover
        }

        private void initializeFilterComboBox()
        {
            comboBoxFilters.DataSource = Enum.GetValues(typeof(eProfileFilter));
            comboBoxFilters.DropDownStyle = ComboBoxStyle.DropDownList;

            if (r_AppSettings.AccessToken == null)
            {
                comboBoxFilters.SelectedIndex = 0;
                comboBoxFilters.SelectedItem = eProfileFilter.None;
            }
            else if (Enum.TryParse(r_AppSettings.LastSelectedFilter, out eProfileFilter o_SavedFilter))
            {
                comboBoxFilters.SelectedItem = o_SavedFilter;
            }

            buttonProfilePictureFilter.Click += (sender, e) => applySelectedFilter((eProfileFilter)comboBoxFilters.SelectedIndex);
        }

        private void initializeMoodComboBox()
        {
            comboBoxMood.DataSource = Enum.GetValues(typeof(eProfileMoodType));
            comboBoxMood.DropDownStyle = ComboBoxStyle.DropDownList;

            if (r_AppSettings.AccessToken == null)
            {
                comboBoxMood.SelectedIndex = 0;
                pictureBoxCover.Image = Properties.Resources.gray_background;
            }
            else if (Enum.TryParse(r_AppSettings.LastSelectedMood, out eProfileMoodType o_SavedMood))
            {
                comboBoxMood.SelectedItem = o_SavedMood;
            }

            buttonApplyMood.Click += (sender, e) => applySelectedMood((eProfileMoodType)comboBoxMood.SelectedIndex);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            r_AppSettings.LastWindowLocation = this.Location;
            r_AppSettings.LastWindowSize = this.Size;
            r_AppSettings.LastSelectedFilter = comboBoxFilters.SelectedItem.ToString();
            r_AppSettings.LastSelectedMood = comboBoxMood.SelectedItem.ToString();

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
                    m_ActiveUser = m_LoginResult.LoggedInUser;
                    loadUserDataToUI();
                }
                catch (Exception)
                {
                    MessageBox.Show(m_LoginResult.ErrorMessage, "Login Failed");
                    m_LoginResult = null;
                }
            }

            eProfileMoodType savedMood = (eProfileMoodType)comboBoxMood.SelectedItem;

            if (savedMood != eProfileMoodType.None)
            {
                applySelectedMood(savedMood);
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("design.patterns");

            if (!string.IsNullOrEmpty(textBoxAppID.Text))
            {

                if (m_LoginResult == null)
                {
                    m_LoginResult = r_FacebookSystem.Login(textBoxAppID.Text);
                }

                if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
                {
                    loadUserDataToUI();
                }
                else
                {
                    m_LoginResult = null;
                }
            }
            else
            {
                MessageBox.Show("Please put an App ID", "");
            }
        }

        private void loadUserDataToUI()
        {
            const bool v_IsLoggingIn = true;
            m_ActiveUser = m_LoginResult.LoggedInUser;
            buttonLogin.Text = $"Logged in as {m_ActiveUser.Name}";
            buttonLogin.BackColor = Color.LightGreen;
            tabMainApp.Text = $"{m_ActiveUser.Name}";
            updateUIBasedOnLoginStatus(v_IsLoggingIn);
            loadUserInformation();
        }

        private void updateUIBasedOnLoginStatus(bool i_IsLoggedIn)
        {
            buttonLogin.Enabled = !i_IsLoggedIn;
            buttonLogout.Enabled = i_IsLoggedIn;
            comboBoxMain.Enabled = i_IsLoggedIn;
            textBoxPost.Enabled = i_IsLoggedIn;
            buttonProfilePictureFilter.Enabled = i_IsLoggedIn;
            buttonApplyMood.Enabled = i_IsLoggedIn;
            comboBoxFilters.Enabled = i_IsLoggedIn;
            comboBoxMood.Enabled = i_IsLoggedIn;
            textBoxPost.Enabled = i_IsLoggedIn;
        }

        private void loadUserInformation()
        {
            try
            {
                if (!string.IsNullOrEmpty(m_ActiveUser.PictureNormalURL))
                {
                    pictureBoxProfile.ImageLocation = m_ActiveUser.PictureNormalURL;
                }

                if (m_ActiveUser.Cover != null && !string.IsNullOrEmpty(m_ActiveUser.Cover.SourceURL))
                {
                    // Store the initial cover image
                    m_OriginalCoverImage = Properties.Resources.gray_background;
                    pictureBoxCover.ImageLocation = m_ActiveUser.Cover.SourceURL;
                    pictureBoxCover.LoadCompleted += (sender, e) =>
                    {
                        m_OriginalCoverImage = (Image)pictureBoxCover.Image.Clone();
                    };
                }
                else
                {
                    m_OriginalCoverImage = Properties.Resources.gray_background;
                    pictureBoxCover.Image = Properties.Resources.gray_background;
                }
            }
            catch (Exception)
            {
                pictureBoxCover.Image = Properties.Resources.gray_background;
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
            {
                MessageBox.Show("falied to retrive albums");
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            const bool v_LoginEnable = true;
            FacebookService.LogoutWithUI();
            buttonLogin.Text = "Login";
            buttonLogin.BackColor = buttonLogout.BackColor;
            m_LoginResult = null;
            buttonLogin.Enabled = v_LoginEnable;
            buttonLogout.Enabled = !v_LoginEnable;
        }

        private void comboBoxMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            eComboboxMainOption selectedFeature = (eComboboxMainOption)comboBoxMain.SelectedIndex;

            switch (selectedFeature)
            {
                case eComboboxMainOption.Feed:
                    loadUserFeed();
                    break;
                case eComboboxMainOption.Likes:
                    loadUserLikedPages();
                    break;
                case eComboboxMainOption.Albums:
                    loadUserAlbums();
                    break;
                case eComboboxMainOption.Groups:
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
                        catch (Exception)
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

        private void postPicture(Image i_PostPicture)
        {
            try
            {
                byte[] imageData = new ImageConverter().ConvertTo(i_PostPicture, typeof(byte[])) as byte[];

                if (imageData != null)
                {
                    Post postedPicture = m_ActiveUser.PostPhoto(imageData);

                    if (postedPicture == null)
                    {
                        throw new Exception("Failed to post the picture");
                    }
                }
                else
                {
                    throw new Exception("Error converting the picture");
                }
            }
            catch (Exception)
            {
                throw new Exception("This action is not supported by Facebook");
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

                    catch (Exception)
                    {
                        MessageBox.Show("Picture post failed");
                    }
                }
            }
        }

        private void buttonCancelPost_Click(object sender, EventArgs e)
        {
            const bool v_PostButtonsEnabled = true;

            textBoxPost.Text = string.Empty;
            changePostButtonsState(!v_PostButtonsEnabled);
        }

        private void applySelectedFilter(eProfileFilter i_Filter)
        {
            if (pictureBoxProfile.Image != null)
            {
                try
                {
                    if (m_OriginalProfilePicture == null)
                    {
                        m_OriginalProfilePicture = (Image)pictureBoxProfile.Image.Clone();
                    }

                    if (i_Filter == eProfileFilter.None)
                    {
                        pictureBoxProfile.Image = (Image)m_OriginalProfilePicture.Clone();
                    }
                    else
                    {
                        Image filteredImage = r_FacebookSystem.ApplyProfileFilter(m_OriginalProfilePicture, i_Filter);
                        pictureBoxProfile.Image = filteredImage;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to apply filter: {ex.Message}");
                }
            }
        }

        private void applySelectedMood(eProfileMoodType i_Mood)
        {
            try
            {
                if (m_OriginalCoverImage == null)
                {
                    m_OriginalCoverImage = pictureBoxCover.Image;
                }

                if (m_OriginalCoverImage != null)
                {
                    pictureBoxCover.Image = (Image)m_OriginalCoverImage.Clone();
                }
                else
                {
                    pictureBoxCover.Image = Properties.Resources.gray_background;
                }

                IMood mood = MoodFactory.CreateMood(i_Mood);
                pictureBoxCover.Image = r_FacebookSystem.ApplyMood(pictureBoxCover.Image, i_Mood);
                pictureBoxCover.SizeMode = PictureBoxSizeMode.StretchImage;

                if (i_Mood != eProfileMoodType.None)
                {
                    labelMoodName.Text = $"Current Mood: {mood.GetMoodName()} {mood.GetMoodEmoji()}";
                    labelMoodName.ForeColor = Color.White;
                    labelMoodName.Visible = true;
                    labelMoodName.BackColor = Color.Transparent;

                    Timer fadeTimer = new Timer();
                    fadeTimer.Interval = 50;
                    int opacity = 0;
                    fadeTimer.Tick += (s, e) =>
                    {
                        opacity += 5;
                        if (opacity >= 255)
                        {
                            fadeTimer.Stop();
                            fadeTimer.Dispose();
                        }
                        labelMoodName.BackColor = Color.FromArgb(opacity, mood.GetMoodColor());
                    };
                    fadeTimer.Start();
                }
                else
                {
                    labelMoodName.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to apply mood: {ex.Message}");
            }
        }

        private void buttonUploadPicture_Click(object sender, EventArgs e)
        {
            try
            {
                postPicture(pictureBoxProfile.Image);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void buttonSaveToFile_Click(object sender, EventArgs e)
        {
            if (pictureBoxProfile.Image != null)
            {
                saveFileDialogSaveProfilePicture.Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg|Bitmap Image (*.bmp)|*.bmp";
                saveFileDialogSaveProfilePicture.Title = "Save Image";

                if (saveFileDialogSaveProfilePicture.ShowDialog() == DialogResult.OK)
                {
                    pictureBoxProfile.Image.Save(saveFileDialogSaveProfilePicture.FileName);
                    MessageBox.Show("Profile picture was saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("You have no profile picture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonProfilePictureFilter_Click(object sender, EventArgs e)
        {
            buttonUploadPicture.Visible = true;
            buttonSaveToFile.Visible = true;
        }

        private void buttonApplyMood_Click(object sender, EventArgs e)
        {
            buttonWhoInTheMood.Visible = true;
        }

        private void buttonWhoInTheMood_Click(object sender, EventArgs e)
        {
            m_FormFriendsWithSameMood = new FormFriendsWithSameMood(m_ActiveUser, (eProfileMoodType)comboBoxMood.SelectedIndex);
            m_FormFriendsWithSameMood.ShowDialog();
        }
    }
}