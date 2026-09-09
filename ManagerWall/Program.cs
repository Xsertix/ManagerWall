using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ManagerWall
{
    internal class Program
    {
        public static byte[] HASH;
        public static byte[] SALT; 
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            if (!File.Exists("error_report_2024.txt.json"))
            {
                Console.WriteLine("Welcome! It looks like your first run.");
                Console.WriteLine("\nUse at least 12 characters with a mix of uppercase, lowercase, numbers, and symbols (like !, @, #). ");
                Console.Write("Create your NEW MASTER-CODE: ");
                string masterCode = Console.ReadLine();

                SALT = Crypto.GenerateSalt();
                HASH = Crypto.CreateKey(masterCode, SALT);

                Console.WriteLine("\nMaster-code set successfully!");
                Console.WriteLine("Please do not forget your master code. Otherwise, you will lose all your data. \n");
            }
            else
            {
                Console.Write("Enter your MASTER-CODE to unlock MasterWall: ");
                string masterCode = Console.ReadLine();

                using (SHA256 myHasher = SHA256.Create())
                {
                    HASH = myHasher.ComputeHash(Encoding.UTF8.GetBytes(masterCode));
                }
                try
                {
                    byte[] encryptBytes = File.ReadAllBytes("error_report_2024.txt.json");

                    SALT = new byte[32];
                    Array.Copy(encryptBytes, 0 , SALT, 0, 32);
                    HASH = Crypto.CreateKey(masterCode, SALT);

                    string decryptedText = Crypto.DecryptCipherText(encryptBytes, HASH);

                    Accounts.accounts = JsonSerializer.Deserialize<List<UserAccount>>(decryptedText);
                    Console.WriteLine("\nWelcome back!\n");
                }
                catch (CryptographicException)
                {
                    Console.WriteLine("\nInvalid MASTERCODE!");
                    return;
                }
                catch (JsonException)
                {
                    Console.WriteLine("\nInvalid MASTERCODE!");
                    return;
                }
                
            }
            while (true)
            {
                UI.DrawLogo();
                Console.WriteLine(" ┌────────────────────────────────────────┐");
                Console.WriteLine(" │            AVAILABLE ACTIONS           │");
                Console.WriteLine(" ├────────────────────────────────────────┤");
                Console.WriteLine(" │  1. Add new account                    │");
                Console.WriteLine(" │  2. Show all accounts                  │");
                Console.WriteLine(" │  3. Delete the vault file              │");
                Console.WriteLine(" │  4. About the project                  │");
                Console.WriteLine(" │  0. Exit program                       │");
                Console.WriteLine(" │                                        │");
                Console.WriteLine(" │  v0.2                                  │");
                Console.WriteLine(" └────────────────────────────────────────┘");

                Console.Write("\nSelect an option: ");
                int cases;

                if (!int.TryParse(Console.ReadLine(), out cases))
                {
                    Console.Clear();
                    Console.WriteLine("\nInvalid command.\n");
                    continue;
                }
                switch (cases)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("\n=== [ ADD NEW ACCOUNT ] ===");
                        Accounts.SaveAccounts();
                        Console.Clear();

                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("\n=== [ YOUR SAVED ACCOUNTS ] ===");
                        Accounts.LoadAccount();
                        Console.WriteLine("\nPress Enter to return to the main menu.");

                        if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                            break;
                        }
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("\nPlease note: all your saved data will be lost after deletion.");
                        Console.WriteLine("yes - delete my passwords | no - return to the main menu\n");
                        string choise = Console.ReadLine();
                        if (choise == "yes")
                        {
                            if (File.Exists("error_report_2024.txt.json"))
                            {
                                File.Delete("error_report_2024.txt.json");
                                Console.WriteLine("The password file has been deleted.");
                                Environment.Exit(0);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Password file not found");
                            }
                        }
                        if (choise == "no")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid command");
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            Console.WriteLine("ManagerWall is an encrypted password manager.");

                            Console.WriteLine("\nIf you like the project, you can give it a * star on GitHub.");
                            Console.WriteLine("That’s the best way to support the author!");

                            Console.WriteLine("\nPress Enter to open the repository. | Press any other key to cancel. ");
                            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                            {
                                string url = "https://github.com/Xsertix/ManagerWall";
                                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                            }
                            Console.Clear();
                            break; 
                        }
                    case 0:
                        Console.WriteLine("Good Bye!");
                        return;
                    default:
                        Console.WriteLine("\nInvalid command.");
                        break;
                }
            }
        }
    }
}