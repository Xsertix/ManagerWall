using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Security.Cryptography;

namespace ManagerWall
{
    internal class Program
    {
        public static byte[] HASH;
        static void Main(string[] args)
        {
            Console.WriteLine("███╗░░░███╗░█████╗░███╗░░██╗░█████╗░░██████╗░███████╗██████╗░░██╗░░░░░░░██╗░█████╗░██╗░░░░░██╗░░░░░\r\n████╗░████║██╔══██╗████╗░██║██╔══██╗██╔════╝░██╔════╝██╔══██╗░██║░░██╗░░██║██╔══██╗██║░░░░░██║░░░░░\r\n██╔████╔██║███████║██╔██╗██║███████║██║░░██╗░█████╗░░██████╔╝░╚██╗████╗██╔╝███████║██║░░░░░██║░░░░░\r\n██║╚██╔╝██║██╔══██║██║╚████║██╔══██║██║░░╚██╗██╔══╝░░██╔══██╗░░████╔═████║░██╔══██║██║░░░░░██║░░░░░\r\n██║░╚═╝░██║██║░░██║██║░╚███║██║░░██║╚██████╔╝███████╗██║░░██║░░╚██╔╝░╚██╔╝░██║░░██║███████╗███████╗\r\n╚═╝░░░░░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝╚═╝░░╚═╝░╚═════╝░╚══════╝╚═╝░░╚═╝░░░╚═╝░░░╚═╝░░╚═╝░░╚═╝╚══════╝╚══════╝");
            Console.WriteLine("v0.1");
            Console.WriteLine("\n");
            if (!File.Exists("base.json"))
            {
                Console.WriteLine("Welcome! It looks like your first run.");
                Console.Write("Create your NEW MASTER-CODE: ");
                string masterCode = Console.ReadLine();

                using (SHA256 myHasher = SHA256.Create())
                {
                    HASH = myHasher.ComputeHash(Encoding.UTF8.GetBytes(masterCode));
                }
                Console.WriteLine("Master-code set successfully!");
                Console.WriteLine("Please do not forget your master code. Otherwise, you will lose all your data. \n");

            }
            else
            {
                Console.WriteLine("Enter your MASTER-CODE to unlock MasterWall.");
                string masterCode = Console.ReadLine();

                using (SHA256 myHasher = SHA256.Create())
                {
                    HASH = myHasher.ComputeHash(Encoding.UTF8.GetBytes(masterCode));
                }

                try
                {
                    byte[] encryptBytes = File.ReadAllBytes("base.json");
                    string decryptedText = Crypto.DecryptCipherText(encryptBytes, HASH);

                    Accounts.accounts = JsonSerializer.Deserialize<List<UserAccount>>(decryptedText);
                }
                catch (CryptographicException)
                {
                    Console.WriteLine("Invalid MASTERCODE!");
                    return;
                }
                catch (JsonException)
                {
                    Console.WriteLine("Invalid MASTERCODE!");
                    return;
                }
            }
            while (true)
            {
                Console.WriteLine("\n--------------------");
                Console.WriteLine("1. Add account.");
                Console.WriteLine("2. Show all accounts.");
                Console.WriteLine("3. Delete the file containing the accounts.");
                Console.WriteLine("--------------------\n");

                int cases;

                if (!int.TryParse(Console.ReadLine(), out cases))
                {
                    Console.WriteLine("Invalid command.");
                    continue;
                }
                switch (cases)
                {
                    case 1:
                        Accounts.SaveAccounts();
                        Console.WriteLine("Account added!");

                        break;
                    case 2:
                        Accounts.LoadAccount();

                        break;
                    case 3:
               
                        if (File.Exists("base.json"))
                        {
                            File.Delete("base.json");
                            Console.WriteLine("The password file has been deleted.");
                            Environment.Exit(0);
                            break ;
                        }
                        else
                        {
                            Console.WriteLine("Password file not found");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }
    }
}