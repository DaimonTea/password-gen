using System;
using System.Linq;
using System.Security.Cryptography;

namespace random
{
    class Program
    {
        public static void GetLength(out int length)
        {
            do
            {
                Console.Write($"Specify the password length: ");
                if (int.TryParse(Console.ReadLine(), out length))
                {
                    return;
                }
                Console.Clear();
                Console.Write($"Incorrect value.\n");
            } while (true);
        }

        public static string ForceSymbolGen(in int forceSymbol)
        {
            char passChar = (char)0;
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "0123456789";
            if (forceSymbol == 1)
            {
                int index = RandomNumberGenerator.GetInt32(0, numbers.Length);
                passChar = numbers[index];
                return passChar.ToString();
            }
            else
            {
                int index = RandomNumberGenerator.GetInt32(0, alphabet.Length);
                passChar = alphabet[index];
                if (RandomNumberGenerator.GetInt32(0, 2) == 1)
                {
                    return passChar.ToString().ToUpper();
                }
                return passChar.ToString();
            }
        }

        public static string GenerateChar(in int forceSymbol, out bool isStr)
        {
            char passChar = (char)0;
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "0123456789";
            if (forceSymbol != 0)
            {
                isStr = forceSymbol == 2 ? true : false;
                return ForceSymbolGen(forceSymbol);
            }
            if (RandomNumberGenerator.GetInt32(0, 2) == 0)
            {
                int index = RandomNumberGenerator.GetInt32(0, numbers.Length);
                passChar = numbers[index];
                isStr = false;
                return passChar.ToString();
            }
            else
            {
                int index = RandomNumberGenerator.GetInt32(0, alphabet.Length);
                passChar = alphabet[index];
                isStr = true;
                if (RandomNumberGenerator.GetInt32(0, 2) == 1)
                {
                    return passChar.ToString().ToUpper();
                }
                return passChar.ToString();
            }
        }

        public static void CreatePassword(in int passLength)
        {
            bool isStr, prevIsStr = true;
            int patience = 0, forceSymbol = 0;
            Console.Clear();
            Console.Write($"Your unique password: ");
            forceSymbol = 0;
            for (int i = 0; i < passLength; i++)
            {
                Console.Write($"{GenerateChar(forceSymbol, out isStr)}");
                forceSymbol = 0;
                if (isStr == prevIsStr)
                {
                    patience++;
                }
                if (patience >= 1)
                {
                    forceSymbol = isStr ? 1 : 2;
                    patience = 0;
                }
                prevIsStr = isStr;
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                GetLength(out int passLength);
                CreatePassword(passLength);
                Console.Write($"\nTo restart the program, press Enter...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}