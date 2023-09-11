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
        private static int err = 0;
        private static string OpenUser = "LoginUser.inf";

        static void Main(string[] args)
        {
            Console.WriteLine("Steam Profile Information Loading");

            ConnectToSteamClient();
            userid = FetchAccountID();
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
                    // Hata Mesajı
                    ColorizeMessage("Note: Steam client may not be open.", "Note:", ConsoleColor.Red);
                    err++;
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
            //string err = "Steam Profile Information Loading Err";
            string steamInstallPath = GetSteamInstallPath();

            if (AppDomain.CurrentDomain.BaseDirectory == Steam.GetInstallPath())
            {
                // Hata Mesajı
                ColorizeMessage("Do not run this application from the Steam directory. | [Fail]", "[Fail]", ConsoleColor.Red);
                err++;
            }
            else if (!_steamClient.Initialize(0L))
            {
                // Hata Mesajı
                ColorizeMessage("Steam is not running. Please start Steam and then run the application again. | [Fail]", "[Fail]", ConsoleColor.Red);
                err++;
            }
            else if (!_steamClient.SteamUser.IsLoggedIn())
            {
                // Hata Mesajı
                ColorizeMessage("You are not logged into Steam. | [Fail]", "[Fail]", ConsoleColor.Red);
                err++;
            }
            else if (string.IsNullOrEmpty(steamInstallPath))
            {
                // Hata Mesajı
                ColorizeMessage("Steam installation path not found. | [Fail]", "[Fail]", ConsoleColor.Red);
                err++;
            }
            else
            {
                DownloadSteamProfileData();
            }

                    if (err > 0)
                    {
                CreateOrOpenFile(OpenUser, "0");
                    }
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

            if (avatarfull == "_full.jpg")
            {
                // Hata Mesajı
                ColorizeMessage("Downloading Steam profile data | [Fail]", "[Fail]", ConsoleColor.Red);
                CreateOrOpenFile(OpenUser, "0");
                ColorizeMessage("Note: Steam user may not have been selected.", "Note:", ConsoleColor.Red);
            }
            else
            {
                ColorizeMessage("Connected to Steam client. | [Success]", "[Success]", ConsoleColor.Green);
                Console.WriteLine("Downloading Steam profile data...");

                Console.WriteLine("Profile Name: " + personaname);
                Console.WriteLine("Real Name: " + (realname_null != "<bdi></bdi>" ? realname_select : "Null"));
                Console.WriteLine("Steam ID: " + _steamClient.SteamUser.GetSteamID());
                Console.WriteLine("User ID: " + (userid == "0" ? "" : userid));
                Console.WriteLine("Steam Install Path: " + GetSteamInstallPath());
                Console.WriteLine("Avatar Image URL: " + avatarfull);
                ColorizeMessage("Steam profile data downloaded. | [Success]", "[Success]", ConsoleColor.Green);

                SavePersonToJson(personaname, realname_select, _steamClient.SteamUser.GetSteamID(), int.Parse(userid), GetSteamInstallPath(), avatarfull);

                Console.ReadLine();
                //Environment.Exit(0);
            }
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
            ColorizeMessage("Steam Profile Info in the JSON data saved. | [Success]", "[Success]", ConsoleColor.Green);

                    CreateOrOpenFile(OpenUser, $"{userid}");
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



        // confiçin
        static void CreateOrOpenFile(string fileName, string content)
        {
            // Dosyayı oluşturun veya varsa açın (varolan verileri silmez)
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(content);
            }
        }
        //Err Mesage
        static void ColorizeMessage(string errorMessage, string target, ConsoleColor textColor)
        {
            int baslangicIndex = errorMessage.IndexOf(target); // Renkli alanın başlangıç indeksini bul

            if (baslangicIndex >= 0)
            {
                Console.Write(errorMessage.Substring(0, baslangicIndex)); // Renkli alanın başından önceki kısmı yazdır
                Console.ForegroundColor = textColor; // Renkli alanın rengini ayarla
                Console.Write(target); // Renkli alanı yazdır
                Console.ResetColor(); // Yazı rengini geri çevir
                Console.WriteLine(errorMessage.Substring(baslangicIndex + target.Length)); // Renkli alanın sonrasındaki kısmı yazdır
            }
            else
            {
                Console.WriteLine(errorMessage); // Renkli alan bulunamazsa, metni tamamen yazdır
            }
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
