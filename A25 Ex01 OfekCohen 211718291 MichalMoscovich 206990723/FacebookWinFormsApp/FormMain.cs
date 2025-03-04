﻿using System;
using System.Drawing;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using static BasicFacebookFeatures.ProfilePictureFilter;
using static BasicFacebookFeatures.ProfileMood;
using BasicFacebookFeatures.Moods.Factory;
using BasicFacebookFeatures.Moods.Interfaces;
using BasicFacebookFeatures.Facade;
using System.Threading;
using System.ComponentModel;

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
        private readonly FacebookSystemFacade r_FacebookSystemFacade;

        private enum eComboboxMainOption
        {
            Feed,
            Likes,
            Albums,
            Groups
        }

        public FormMain()
        {
            InitializeComponent();
            tabMainApp.TabPages[0].Text = "Welcome";
            this.StartPosition = FormStartPosition.Manual;
            FacebookService.s_CollectionLimit = k_CollectionLimit;
            r_AppSettings = AppSettings.LoadFromFile();
            r_FacebookSystemFacade = new FacebookSystemFacade();
            initializeFilterFeature();
            initializeMoodFeature();
        }

        private void initializeFilterFeature()
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

        private void initializeMoodFeature()
        {
            // Update mood label position relative to pictureBoxCover
            labelMoodName.BringToFront(); // Make sure label is visible above other controls
            labelMoodName.Location = new Point(10, pictureBoxCover.Bottom - 40);
            labelMoodName.Parent = pictureBoxCover;
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
            base.OnShown(e);
            
            checkBoxRememberMe.Checked = r_AppSettings.RememberLoggedInUser;
            this.Size = r_AppSettings.LastWindowSize;
            this.Location = r_AppSettings.LastWindowLocation;

            if (r_AppSettings.RememberLoggedInUser && !string.IsNullOrEmpty(r_AppSettings.AccessToken))
            {
                try
                {
                    new Thread(loginAndUpdateUI).Start();
                }
                catch (Exception)
                {
                    this.Invoke(new Action(() => MessageBox.Show(m_LoginResult.ErrorMessage, "Login Failed")));
                    m_LoginResult = null;
                }
            }
        }

        private void loginAndUpdateUI()
        {
            m_LoginResult = FacebookService.Connect(r_AppSettings.AccessToken);
            m_ActiveUser = m_LoginResult.LoggedInUser;
            this.Invoke(new Action(() => loadUserDataToUI()));
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
            buttonLogin.Invoke(new Action(() => buttonLogin.Enabled = !i_IsLoggedIn));
            buttonLogout.Invoke(new Action(() => buttonLogout.Enabled = i_IsLoggedIn));
            comboBoxMain.Invoke(new Action(() => comboBoxMain.Enabled = i_IsLoggedIn));
            textBoxPost.Invoke(new Action(() => textBoxPost.Enabled = i_IsLoggedIn));
            buttonProfilePictureFilter.Invoke(new Action(() => buttonProfilePictureFilter.Enabled = i_IsLoggedIn));
            buttonApplyMood.Invoke(new Action(() => buttonApplyMood.Enabled = i_IsLoggedIn));
            comboBoxFilters.Invoke(new Action(() => comboBoxFilters.Enabled = i_IsLoggedIn));
            comboBoxMood.Invoke(new Action(() => comboBoxMood.Enabled = i_IsLoggedIn));
        }

        private void loadUserInformation()
        {
            try
            {
                if (!string.IsNullOrEmpty(m_ActiveUser.PictureNormalURL))
                {
                    r_FacebookSystemFacade.LoadUserInformation(m_ActiveUser, pictureBoxProfile);
                    pictureBoxCover.Invoke(new Action(() =>
                    {
                        pictureBoxCover.LoadCompleted += (sender, e) =>
                        {
                            m_OriginalCoverImage = (Image)pictureBoxCover.Image.Clone();
                        };
                    }));
                }
                else
                {
                    pictureBoxCover.Invoke(new Action(() =>
                    {
                        m_OriginalCoverImage = Properties.Resources.gray_background;
                        pictureBoxCover.Image = Properties.Resources.gray_background;
                    }));
                }
            }
            catch (Exception)
            {
                pictureBoxCover.Invoke(new Action(() => pictureBoxCover.Image = Properties.Resources.gray_background));
            }

            new Thread(loadUserFeed).Start();
            new Thread(loadUserEvents).Start();
            new Thread(loadUserFriends).Start();
        }

        private void loadUserEvents()
        {
            try
            {
                BindingList<Event> eventsList = r_FacebookSystemFacade.GetUserEvents(m_ActiveUser);
                if (eventsList != null && eventsList.Count > 0)
                {
                    this.Invoke(new Action(() =>
                    {
                        listBoxEvents = null;
                        eventBindingSource.DataSource = eventsList;
                        listBoxEvents.DataSource = eventBindingSource.DataSource;
                    }));
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        listBoxEvents.DataSource = null;
                        listBoxEvents.Items.Clear();
                        listBoxEvents.Items.Add("No Events");
                    }));
                }
            }
            catch
            {
                this.Invoke(new Action(() => MessageBox.Show("Error retrieving events.")));
            }
        }

        private void loadUserFeed()
        {
            this.Invoke(new Action(() => listBoxMain.Items.Clear()));
            try
            {
                this.Invoke(new Action(() => r_FacebookSystemFacade.LoadUserFeed(m_ActiveUser, listBoxMain)));
            }
            catch
            {
                this.Invoke(new Action(() => MessageBox.Show("Error retrieving feed.")));
            }
        }

        private void loadUserFriends()
        {
            try
            {
                if (m_ActiveUser.Friends != null && m_ActiveUser.Friends.Count > 0)
                {
                    BindingList<User> friendsList = r_FacebookSystemFacade.GetUserFriends(m_ActiveUser);

                    this.Invoke(new Action(() =>
                    {
                        listBoxFriends.DataSource = null;
                        friendListBindingSource.DataSource = friendsList;
                        listBoxFriends.DataSource = friendListBindingSource;
                        listBoxFriends.DisplayMember = "Name";
                    }));
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        listBoxFriends.DataSource = null;
                        listBoxFriends.Items.Clear();
                        listBoxFriends.Items.Add("No friends");
                    }));
                }
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() => MessageBox.Show($"Error retrieving friends: {ex.Message}")));
            }
        }

        private void loadUserLikedPages()
        {
            listBoxMain.Invoke(new Action(() =>
            {
                listBoxMain.Items.Clear();
                listBoxMain.DisplayMember = "Name";
            }));

            try
            {
                r_FacebookSystemFacade.LoadUserLikedPages(m_ActiveUser, listBoxMain);
            }
            catch
            {
                this.Invoke(new Action(() => MessageBox.Show("Error retrieving liked pages.")));
            }
        }

        private void loadUserGroups()
        {
            listBoxMain.Invoke(new Action(() =>
            {
                listBoxMain.Items.Clear();
                listBoxMain.DisplayMember = "Name";
            }));

            try
            {
                r_FacebookSystemFacade.LoadUserGroups(m_ActiveUser, listBoxMain);
            }
            catch
            {
                this.Invoke(new Action(() => MessageBox.Show("Error retrieving groups.")));
            }
        }

        private void loadUserAlbums()
        {
            listBoxMain.Invoke(new Action(() =>
            {
                listBoxMain.Items.Clear();
                listBoxMain.DisplayMember = "Name";
            }));

            try
            {
                r_FacebookSystemFacade.LoadUserAlbums(m_ActiveUser, listBoxMain);
            }
            catch
            {
                this.Invoke(new Action(() => MessageBox.Show("Error retrieving albums.")));
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            const bool v_LoginEnable = true;
            FacebookService.LogoutWithUI();
            buttonLogin.Invoke(new Action(() =>
            {
                buttonLogin.Text = "Login";
                buttonLogin.BackColor = buttonLogout.BackColor;
                buttonLogin.Enabled = v_LoginEnable;
            }));

            buttonLogout.Invoke(new Action(() => buttonLogout.Enabled = !v_LoginEnable));

        }

        private void comboBoxMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            eComboboxMainOption selectedFeature = (eComboboxMainOption)comboBoxMain.SelectedIndex;

            switch (selectedFeature)
            {
                case eComboboxMainOption.Feed:
                    new Thread(loadUserFeed).Start();
                    break;
                case eComboboxMainOption.Likes:
                    new Thread(loadUserLikedPages).Start();
                    break;
                case eComboboxMainOption.Albums:
                    new Thread(loadUserAlbums).Start();
                    break;
                case eComboboxMainOption.Groups:
                    new Thread(loadUserGroups).Start();
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
            buttonPost.Invoke(new Action(() => buttonPost.Enabled = i_ButtonsState));
            buttonAddPicture.Invoke(new Action(() => buttonAddPicture.Enabled = i_ButtonsState));
            buttonCancelPost.Invoke(new Action(() => buttonCancelPost.Enabled = i_ButtonsState));
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
                            r_FacebookSystemFacade.PostStatus(m_ActiveUser, textBoxPost.Text);
                            changePostButtonsState(!v_PostButtonsEnabled);
                            MessageBox.Show("Posted!");
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
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                textBoxPost.Invoke(new Action(() => Text = string.Empty));
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
                    MessageBox.Show("Please choose a picture first!");
                }
                else
                {
                    try
                    {
                        r_FacebookSystemFacade.PostStatusWithPicture(m_ActiveUser, textBoxPost.Text, picturePath);
                        MessageBox.Show("Posted successfully!");
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


            textBoxPost.Invoke(new Action(() => Text = string.Empty));
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
                        Image filteredImage = r_FacebookSystemFacade.ApplyProfileFilter(m_OriginalProfilePicture, i_Filter);
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
                    pictureBoxCover.Invoke(new Action(() =>
                    {
                        pictureBoxCover.Image = (Image)m_OriginalCoverImage.Clone();
                    }));
                }
                else
                {
                    pictureBoxCover.Invoke(new Action(() => pictureBoxCover.Image = Properties.Resources.gray_background));
                }

                IMood mood = MoodFactory.CreateMood(i_Mood);
                pictureBoxCover.Invoke(new Action(() => pictureBoxCover.Image = r_FacebookSystemFacade.ApplyMood(pictureBoxCover.Image, i_Mood)));
                pictureBoxCover.Invoke(new Action(() => pictureBoxCover.SizeMode = PictureBoxSizeMode.StretchImage));

                if (i_Mood != eProfileMoodType.None)
                {
                    labelMoodName.Invoke(new Action(() =>
                    {
                        labelMoodName.Text = $"Current Mood: {mood.GetMoodName()} {mood.GetMoodEmoji()}";
                        labelMoodName.ForeColor = Color.White;
                        labelMoodName.BackColor = Color.Transparent;
                    }));

                    System.Windows.Forms.Timer fadeTimer = new System.Windows.Forms.Timer();
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
                r_FacebookSystemFacade.PostPicture(m_ActiveUser, pictureBoxProfile.Image);
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
                    r_FacebookSystemFacade.SaveProfilePicture(pictureBoxProfile.Image, saveFileDialogSaveProfilePicture.FileName);
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
            buttonUploadPicture.Enabled = true;
            buttonSaveToFile.Enabled = true;
        }

        private void buttonApplyMood_Click(object sender, EventArgs e)
        {
            buttonWhoInTheMood.Enabled = true;
        }

        private void buttonWhoInTheMood_Click(object sender, EventArgs e)
        {
            m_FormFriendsWithSameMood = new FormFriendsWithSameMood(m_ActiveUser, (eProfileMoodType)comboBoxMood.SelectedIndex);
            m_FormFriendsWithSameMood.ShowDialog();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("design.patterns");

            if (!string.IsNullOrEmpty(textBoxAppID.Text))
            {
                if (m_LoginResult == null)
                {
                    new Thread(login).Start();
                }
            }
            else
            {
                MessageBox.Show("Please put an App ID", "");
            }
        }

        private void login()
        {
            m_LoginResult = r_FacebookSystemFacade.Login(textBoxAppID.Text);

            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                loadUserDataToUI();
            }
            else
            {
                m_LoginResult = null;
            }
        }
    }
}