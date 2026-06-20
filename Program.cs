using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CommonScripts;

namespace password_generator
{
    class Program
    {
        static readonly string alphabet = "abcdefghijklmnopqrstuvwxyz";
        static readonly string numbers = "0123456789";
        static bool isStr, prevIsStr;
        static byte forceSymbol;

        public static void GetLength(out int length)
        {
            CustomParsing.ParseString("Specify the password length: ", out length);
        }

        public static char CreateChar(in string choice)
        {
            int index;
            char passChar;
            switch (choice)
            {
                case "number":
                    index = RandomNumberGenerator.GetInt32(0, numbers.Length);
                    passChar = numbers[index];
                    break;
                case "letter":
                    index = RandomNumberGenerator.GetInt32(0, alphabet.Length);
                    passChar = alphabet[index];
                    break;
                default:
                    passChar = (char)0;
                    break;
            }
            return passChar;
        }

        /// <summary>Forcefully creates a string character or a string number depending on the conditions (forceSymbol == 1 - number, 2 - letter).</summary>
        /// <returns>A string value that becomes a part of the password.</returns>
        public static char ForceChar(in int forceSymbol)
        {
            char passChar = (char)0;
            if (forceSymbol == 1)
            {
                passChar = CreateChar("number");
            }
            else
            {
                passChar = CreateChar("letter");
                if (RandomNumberGenerator.GetInt32(0, 2) == 1)
                {
                    return Char.ToUpper(passChar);
                }
            }
            return passChar;
        }

        /// <summary>Generates a random character that can either be a string letter or a string number.</summary>
        /// <returns>A string value that becomes a part of the password.</returns>
        public static char ConfigureChar(in int forceSymbol, out bool isStr)
        {
            char passChar = (char)0;
            if (forceSymbol != 0)
            {
                isStr = forceSymbol == 2 ? true : false;
                return ForceChar(forceSymbol);
            }
            if (RandomNumberGenerator.GetInt32(0, 2) == 0)
            {
                passChar = CreateChar("number");
                isStr = false;
            }
            else
            {
                passChar = CreateChar("letter");
                isStr = true;
                if (RandomNumberGenerator.GetInt32(0, 2) == 1)
                {
                    return Char.ToUpper(passChar);
                }
            }
            return passChar;
        }

        /// <summary>Creates a string value containing the password and displays it.</summary>
        public static void CreatePassword(in int passLength)
        {
            prevIsStr = true;
            byte patience = (byte)0;
            StringBuilder generatedPassword = new StringBuilder(passLength);
            Console.Clear();
            for (int i = 0; i < passLength; i++)
            {
                generatedPassword.Append(ConfigureChar(forceSymbol, out isStr));
                forceSymbol = 0;
                if (isStr == prevIsStr)
                {
                    patience++;
                }
                if (patience >= 1)
                {
                    forceSymbol = isStr ? (byte)1 : (byte)2;
                    patience = 0;
                }
                prevIsStr = isStr;
            }
            Console.Write($"Your unique password: {generatedPassword}");
        }

        static void Main(string[] args)
        {
            while (true)
            {
                GetLength(out int passLength);
                CreatePassword(passLength);
                CommonTools.Wait();
            }
        }
    }
}