using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CipherCore;

namespace CipherCore
{
    public static class CipherCoreManager
    {
        public static string keyword;
        public static bool salted = false;
        public static CipherTable cipherTable;


        //Sets the keyword to the provided string
        public static void SetKeyword(string s)
        {
            keyword= s;
        }


        //Entry point
        [STAThread]
        static void Main(string[] args)
        {
            cipherTable = new CipherTable();

            Application.EnableVisualStyles();
            Application.Run(new CipherCoreMain());
        }


        //Checks if the provided string contains only legal characters.
        public static bool ContainsOnlyLegalCharacters(string s)
        {
            foreach (char c in s)
            {
                if (CipherCoreManager.cipherTable.CharacterIsLegal(c))
                {
                    //Character is legal; continue

                    continue;
                }
                else
                {
                    Console.WriteLine("Illegal Character: " + c);
                    return false;
                }
            }

            return true;
        }


        //Called when encryption is invoked
        public static string Encrypt(string message)
        {
            Console.WriteLine("Begin encryption.  Salting = " + salted.ToString() + ".");

            //Deal with salting
            string thisMessage = message;
            if (salted)
            {
                SaltShaker.PrepareSaltShaker(keyword);
                thisMessage = SaltShaker.Salt(message);
            }

            string newMessage = "";

            foreach (char c in thisMessage)
            {
                newMessage += CipherCoreManager.cipherTable.EncryptChar(c);
            }

            return newMessage;
        }

        //Called when decryption is invoked
        public static string Decrypt(string message)
        {
            Console.WriteLine("Begin decryption.  Salting = " + salted.ToString() + ".");

            string thisMessage = message;
            
            //Deal with salting
            if (salted)
            {
                SaltShaker.PrepareSaltShaker(keyword);
                thisMessage = SaltShaker.Desalt(message);
            }

            string newMessage = "";

            foreach (char c in thisMessage)
            {
                newMessage += CipherCoreManager.cipherTable.DecryptChar(c);
            }

            return newMessage;
        }


    }
}
