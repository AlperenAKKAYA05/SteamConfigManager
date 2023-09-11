using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using UserFetchSteam.API;
using UserFetchSteam;
using System.Collections.Generic;

namespace SteamConfigManager
{
    public partial class SteamInfo : Form
    {
        private readonly Client _steamClient = new Client(); // Create a Steam client instance.
        private string[] errorArray = new string[] { }; // Array to store errors.
        private static string userid; // Static variable to store the user's Steam ID.

        public SteamInfo()
        {
            this.Hide(); // Hide the form initially.
            InitializeComponent(); // Initialize the main form components.
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string iconPath = "icon.ico";

            if (System.IO.File.Exists(iconPath))
            {
                this.Icon = new Icon(iconPath);
            }

            ConnectToSteamClient(); // Connect to the Steam client.

            steamid.Text = ""; // Clear the Steam ID text.
            userid = FetchAccountID(); // Fetch the user's Steam ID.
        }

        // Method to fetch the Steam ID.
        public string FetchAccountID()
        {
            try
            {
                if (_steamClient != null && _steamClient.SteamUser != null)
                {
                    ulong steamId64 = _steamClient.SteamUser.GetSteamID(); // Get the 64-bit Steam ID.
                    ulong accountID = steamId64 - 76561197960265728; // Calculate the account ID.
                    return accountID.ToString(); // Return the account ID as a string.
                }
                else
                {
                    lblStatus.Text = "Steam client or Steam user is null.";
                    return ""; // Return empty string in case of error.
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "An unknown error occurred: " + ex.Message;
                return ""; // Return empty string in case of error.
            }
        }

        // Method to check Steam client connection.
        private void ConnectToSteamClient()
        {
            // Update form title to indicate connection process
            Text = "Steam Community :: Getting User...";
            // Counter for tracking errors
            int err = 0;

            // Check various error conditions and handle them
            if (Application.StartupPath == Steam.GetInstallPath())
            {
                // Display error message for running from Steam directory
                lblStatus.Text = "Do not run this application from the Steam directory.";
                steamid.Text = "";
                err++;
            }
            else if (!_steamClient.Initialize(0L))
            {
                // Display error message if Steam is not running
                lblStatus.Text = "Steam is not running. Please start Steam and then run the application again.";
                steamid.Text = "";
                err++;
            }
            else if (!_steamClient.SteamUser.IsLoggedIn())
            {
                // Display error message for not being logged into Steam
                lblStatus.Text = "You are not logged into Steam.";
                steamid.Text = "";
                err++;
            }
            else if (errorArray.Contains("Err_profil_img"))
            {
                // Display error message for missing user selection
                lblStatus.Text = "You are logged into Steam, but no user selection has been made.";
                steamid.Text = "";
                err++;
            }
            else
            {
                // If no errors, show the main form and start downloading Steam profile data
                this.Show();
                userdata.Visible = true;
                DownloadSteamProfileData();
            }

            // Handle errors by displaying a pop-up and exiting the application
            if (err > 0)
            {
                this.Hide();
                string enteredText = lblStatus.Text;
                Application.Exit();
            }
        }

        // Method to download Steam profile data
        private void DownloadSteamProfileData()
        {
            try
            {
                using (var client = new WebClient())
                {
                    // Set up WebClient for UTF-8 encoding
                    client.Encoding = Encoding.UTF8;
                    // Set request headers
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    // Subscribe to event for handling completed download
                    client.DownloadStringCompleted += SteamProfileJsonDownloaded;

                    // Download Steam profile data asynchronously
                    client.DownloadStringAsync(new Uri($"https://steamcommunity.com/profiles/{_steamClient.SteamUser.GetSteamID()}"));
                }
            }
            catch
            {
                // If an exception occurs, set form title to "Unknown User"
                Text = "Unknown User";
            }
        }

        // Method called when Steam profile data is downloaded
        private void SteamProfileJsonDownloaded(object sender, DownloadStringCompletedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Result) && e.Result.Contains("Steam Community :: "))
            {
                // Extract profile data using regular expressions
                string _personaname = Regex.Match(e.Result, @"Steam Community :: (.+?)</title>").Groups[1].Value.Trim();
                string _realname_null = Regex.Match(e.Result, @"<bdi></bdi>").Groups[0].Value.Trim();
                string _realname_select = Regex.Match(e.Result, @"<bdi>(.+?)</bdi>").Groups[1].Value.Trim();
                string _avatarfull = Regex.Match(e.Result, @"<img src=""(.+?)_full.jpg"">").Groups[1].Value.Trim() + "_full.jpg";

                // Display real name or "Null" if not available
                realname.Text = _realname_null != "<bdi></bdi>" ? _realname_select : "Null";
                // Display Steam ID
                steamid.Text = $"{_steamClient.SteamUser.GetSteamID()}";
                // Display user ID if not "0"
                steamuserid.Text = userid == "0" ? "" : userid;
                // Display Steam installation path
                userdata.Text = $"{GetSteamInstallPath()}";

                // Download and display avatar image
                string imageUrl = $"{_avatarfull}";
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        // Download image data and display in PictureBox
                        byte[] imageData = client.DownloadData(imageUrl);
                        using (var stream = new System.IO.MemoryStream(imageData))
                        {
                            Image image = Image.FromStream(stream);
                            avatarfull.Image = image;
                        }
                    }
                    catch (WebException ex)
                    {
                        if (ex.Message.Contains("_full.jpg"))
                        {
                            // Handle specific error related to image
                            errorArray = new string[] { "Err_profil_img" };
                        }
                        else
                        {
                            // Show general error message in case of exception
                            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        // Reconnect to Steam client
                        ConnectToSteamClient();
                    }

                    // Update form title and user name if no error occurred
                    if (_personaname != "Error")
                    {
                        Text = $"Steam Community :: {_personaname}";
                        personaname.Text = _personaname;
                    }
                }
            }
        }

        // Method to get Steam installation path
        public static string GetSteamInstallPath()
        {
            // Get the Steam installation path
            string steamInstallPath = Steam.GetInstallPath();
            if (!string.IsNullOrEmpty(steamInstallPath))
            {
                // Return the full path to the userdata folder
                return $"{steamInstallPath}\\userdata\\{userid}";
            }
            // Return null if Steam installation path is not available
            return null;
        }
    }
}
