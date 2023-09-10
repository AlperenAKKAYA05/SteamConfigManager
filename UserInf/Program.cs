using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using UserFetchSteam;
using Newtonsoft.Json;
using System.IO;

namespace SteamConfigManager
{
    class Program
    {
        private static readonly Client _steamClient = new Client();
        private static string userid;

        static void Main(string[] args)
        {
            Console.WriteLine("Steam Profile Information Loading");

            ConnectToSteamClient();
            userid = FetchAccountID();
            DownloadSteamProfileData();
            
            Console.WriteLine("Steam Profile Info in the JSON data saved. Success");

            Console.ReadLine();
        }

        public static string FetchAccountID()
        {
            try
            {
                if (_steamClient != null && _steamClient.SteamUser != null)
                {
                    ulong steamId64 = _steamClient.SteamUser.GetSteamID();
                    ulong accountID = steamId64 - 76561197960265728;
                    return accountID.ToString();
                }
                else
                {
                    Console.WriteLine("Steam client or Steam user is null.");
                    return "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unknown error occurred: " + ex.Message);
                return "";
            }
        }

        private static void ConnectToSteamClient()
        {
            string steamInstallPath = GetSteamInstallPath();

            if (string.IsNullOrEmpty(steamInstallPath))
            {
                Console.WriteLine("Steam installation path not found.");
                Environment.Exit(0);
            }

            if (AppDomain.CurrentDomain.BaseDirectory.StartsWith(steamInstallPath, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Do not run this application from the Steam directory.");
                Environment.Exit(0);
            }

            if (!_steamClient.Initialize(0L))
            {
                Console.WriteLine("Steam is not running. Please start Steam and then run the application again.");
                Environment.Exit(0);
            }

            if (!_steamClient.SteamUser.IsLoggedIn())
            {
                Console.WriteLine("You are not logged into Steam.");
                Environment.Exit(0);
            }

            Console.WriteLine("Connected to Steam client.");
            Console.WriteLine("Downloading Steam profile data...");
        }

        private static void DownloadSteamProfileData()
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    client.DownloadStringCompleted += SteamProfileJsonDownloaded;
                    client.DownloadStringAsync(new Uri($"https://steamcommunity.com/profiles/{_steamClient.SteamUser.GetSteamID()}"));
                }
            }
            catch
            {
                Console.WriteLine("Unknown User");
            }
        }

        private static void SteamProfileJsonDownloaded(object sender, DownloadStringCompletedEventArgs e)
        {
            string personaname = Regex.Match(e.Result, @"Steam Community :: (.+?)</title>").Groups[1].Value.Trim();
            string realname_null = Regex.Match(e.Result, @"<bdi></bdi>").Groups[0].Value.Trim();
            string realname_select = Regex.Match(e.Result, @"<bdi>(.+?)</bdi>").Groups[1].Value.Trim();
            string avatarfull = Regex.Match(e.Result, @"<img src=""(.+?)_full.jpg"">").Groups[1].Value.Trim() + "_full.jpg";

            Console.WriteLine("Profile Name: " + personaname);
            Console.WriteLine("Real Name: " + (realname_null != "<bdi></bdi>" ? realname_select : "Null"));
            Console.WriteLine("Steam ID: " + _steamClient.SteamUser.GetSteamID());
            Console.WriteLine("User ID: " + (userid == "0" ? "" : userid));
            Console.WriteLine("Steam Install Path: " + GetSteamInstallPath());
            Console.WriteLine("Avatar Image URL: " + avatarfull);

            SavePersonToJson(personaname, realname_select, _steamClient.SteamUser.GetSteamID(), int.Parse(userid), GetSteamInstallPath(), avatarfull);


            Environment.Exit(0);
        }

        static void SavePersonToJson(string profileName, string realName, ulong steamId, int userId, string installPath, string avatarUrl)
        {
            Person person = new Person
            {
                Profile_Name = profileName,
                Real_Name = realName != "<bdi></bdi>" ? realName : "Null",
                Steam_ID = steamId,
                User_ID = userId,
                Steam_Install_Loc = installPath,
                Avatar_Img = avatarUrl
            };

            string json = JsonConvert.SerializeObject(person);

            string folderPath = $"userdata/{userId}";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, $"{userId}.json");
            string filePath2 = Path.Combine(folderPath, $"{userId}.json");
            File.WriteAllText(filePath, json);
        }

        public static string GetSteamInstallPath()
        {
            string steamInstallPath = Steam.GetInstallPath();
            if (!string.IsNullOrEmpty(steamInstallPath))
            {
                return $"{steamInstallPath}\\userdata\\{userid}";
            }
            return null;
        }
    }
}

class Person
{
    public string Profile_Name { get; set; }
    public string Real_Name { get; set; }
    public ulong Steam_ID { get; set; }
    public int User_ID { get; set; }
    public string Steam_Install_Loc { get; set; }
    public string Avatar_Img { get; set; }
}
