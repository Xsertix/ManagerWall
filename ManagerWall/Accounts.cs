using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace ManagerWall
{
    public class UserAccount
    {
        public string password { get; set; }
        public string login { get; set; }
    }

    public class Accounts
    {
        public static List<UserAccount> accounts = new List<UserAccount>();

        public static void SaveAccounts()
        {
            Console.WriteLine("\nEnter your password");
            string userPasword = Console.ReadLine();
            Console.WriteLine("Enter your login");
            string userLogin = Console.ReadLine();
            accounts.Add(new UserAccount { login = userLogin, password = userPasword });
            string serializedText = JsonSerializer.Serialize(accounts);
            byte[] encryptedBytes = Crypto.EncryptPlainText(serializedText, Program.HASH, Program.SALT);
            File.WriteAllBytes("error_report_2024.txt.json", encryptedBytes);
        }

        public static void LoadAccount()
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"    {UI.BOLD}{UI.BLUE}[{i + 1}]{UI.RESET} {UI.BOLD}{UI.CYAN}Login:{UI.RESET} {accounts[i].login} | {UI.BOLD}{UI.CYAN}Password:{UI.RESET} {accounts[i].password}");

            }
        }
    }
}
