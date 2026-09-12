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

            if (!File.Exists("error_report_2024.txt.json"))
            {
                Console.WriteLine($"{UI.BOLD}{UI.CYAN}Welcome! It looks like your first run.{UI.RESET}");
                Console.WriteLine($"\nUse at least 12 characters with a mix of uppercase, lowercase, numbers, and symbols {UI.GREEN}(like !, @, #).{UI.RESET} ");
                Console.Write($"{UI.GREEN}Create your NEW MASTER-CODE:{UI.RESET} ");
                string masterCode = Console.ReadLine();

                SALT = Crypto.GenerateSalt();
                HASH = Crypto.CreateKey(masterCode, SALT);

                Console.WriteLine($"\n{UI.GREEN}Master-code set successfully!{UI.RESET}");
                Console.WriteLine($"Please {UI.RED}do not forget{UI.RESET} your master code. Otherwise, {UI.RED}you will lose all your data.{UI.RESET} \n");
            }
            else
            {
                Console.Write($"Enter your {UI.RED}MASTER-CODE{UI.RESET} to {UI.GREEN}unlock{UI.RESET} MasterWall: ");
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
                    Console.Clear();
                }
                catch (CryptographicException)
                {
                    Console.WriteLine($"\n{UI.RED}Invalid MASTERCODE!{UI.RESET}");
                    return;
                }
                catch (JsonException)
                {
                    Console.WriteLine($"\n{UI.RED}Invalid MASTERCODE!{UI.RESET}");
                    return;
                }
                
            }
            while (true)
            {
                UI.DrawLogo();
                Console.WriteLine(" ┌────────────────────────────────────────┐");
                Console.WriteLine($" │            {UI.BOLD}{UI.CYAN}AVAILABLE ACTIONS{UI.RESET}           │");
                Console.WriteLine(" ├────────────────────────────────────────┤");
                Console.WriteLine($" │  {UI.CYAN}1.{UI.RESET} Add new account                    │");
                Console.WriteLine($" │  {UI.CYAN}2.{UI.RESET} Show all accounts                  │");
                Console.WriteLine($" │  {UI.CYAN}3.{UI.RESET} Delete the vault file              │");
                Console.WriteLine($" │  {UI.CYAN}4.{UI.RESET} About the project                  │");
                Console.WriteLine($" │  {UI.CYAN}0.{UI.RESET} Exit program                       │");
                Console.WriteLine(" │                                        │");
                Console.WriteLine($" │  {UI.CYAN}managerwall v0.2.1{UI.RESET}                    │");
                Console.WriteLine(" └────────────────────────────────────────┘");

                Console.Write($"\n{UI.BOLD}{UI.CYAN}Select an option:{UI.RESET} ");
                int cases;

                if (!int.TryParse(Console.ReadLine(), out cases))
                {
                    Console.Clear();
                    Console.WriteLine($"\n{UI.RED}Invalid command.{UI.RESET}\n");
                    continue;
                }
                switch (cases)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine($"\n{UI.BOLD}{UI.CYAN}=== [ ADD NEW ACCOUNT ] ==={UI.RESET}");
                        Accounts.SaveAccounts();
                        Console.Clear();

                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine($"\n{UI.BOLD}{UI.CYAN}=== [ YOUR SAVED ACCOUNTS ] ==={UI.RESET}\n");
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
                        Console.WriteLine($"\nPlease note: {UI.RED}all your saved data will be lost after deletion.{UI.RESET}");
                        Console.WriteLine($"yes - {UI.RED}delete my passwords{UI.RESET} | no - {UI.GREEN}return to the main menu{UI.RESET}\n");
                        string choise = Console.ReadLine();
                        if (choise == "yes")
                        {
                            if (File.Exists("error_report_2024.txt.json"))
                            {
                                File.Delete("error_report_2024.txt.json");
                                Console.WriteLine($"{UI.RED}The passwords file has been deleted.{UI.RESET}");
                                Environment.Exit(0);
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"{UI.RED}Password file not found{UI.RESET}");
                            }
                        }
                        if (choise == "no")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"\n{UI.RED}Invalid command{UI.RESET}");
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            Console.WriteLine($"{UI.BOLD}{UI.CYAN}ManagerWall{UI.RESET} is an {UI.BOLD}{UI.CYAN}encrypted password manager.{UI.RESET}");

                            Console.WriteLine($"\nIf you like the project, {UI.BOLD}{UI.CYAN}you can give it a star on GitHub.{UI.RESET}");
                            Console.WriteLine("That’s the best way to support the author!");

                            Console.WriteLine($"\nPress Enter to {UI.GREEN}open the repository.{UI.RESET} | Press any other key {UI.RED}to cancel.{UI.RESET} ");
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
                        Console.WriteLine($"\n{UI.RED}Invalid command.{UI.RED}");
                        break;
                }
            }
        }
    }
}