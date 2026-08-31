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
            Console.WriteLine("\n");
            Console.WriteLine("Enter your password");
            string userPasword = Console.ReadLine();
            Console.WriteLine("Enter your login");
            string userLogin = Console.ReadLine();
            accounts.Add(new UserAccount { login = userLogin, password = userPasword });
            string serializedText = JsonSerializer.Serialize(accounts);
            byte[] encryptedBytes = Crypto.EncryptPlainText(serializedText, Program.HASH);
            File.WriteAllBytes("base.json", encryptedBytes);
        }

        public static void LoadAccount()
        {
            Console.WriteLine("\n--------------------");
            Console.WriteLine("Your accounts:");
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"    {i + 1} Login: {accounts[i].login} | Password: {accounts[i].password}");

            }
            Console.WriteLine("--------------------");
        }
    }
}
