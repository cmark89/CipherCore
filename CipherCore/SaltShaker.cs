using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CipherCore
{
    public static class SaltShaker
    {
        public static int saltLength;           //Length of salt to use
        public static List<int> saltingIntervals = new List<int>(); //Determines where salting will occur
        public static Random random = new Random();

        public static void PrepareSaltShaker(string keyword)
        {
            keyword = keyword.ToUpper();
            //Create the interval list
            saltingIntervals = new List<int>();
            int keywordTotalValue = 0;

            Console.WriteLine("\nPreparing to salt with keyword: " + keyword);

            //Get the salting interval for each character in the keyword
            foreach (char c in keyword)
            {
                saltingIntervals.Add(CipherCoreManager.cipherTable.usableCharacterValues[CipherCoreManager.cipherTable.GetUsableCharIndex(c)]);
            }

            //Get the length of salt using the total value of the keyword
            foreach (int i in saltingIntervals)
            {
                keywordTotalValue += i;
            }

            saltLength = (keywordTotalValue % keyword.Length) + 3;
        }


        //Pushes the front interval to the end, allowing it to loop
        public static void NextSaltingInterval()
        {
            int i = saltingIntervals[0];
            saltingIntervals.RemoveAt(0);
            saltingIntervals.Add(i);
        }


        //Generates 'length' characters of salt
        public static string GenerateSalt(int length)
        {
            string salt = "";
            int max = CipherCoreManager.cipherTable.usableCharacters.Count;
            for (int i = 0; i < length; i++)
            {
                char newSaltChar = CipherCoreManager.cipherTable.usableCharacters[random.Next(max)];
                salt += newSaltChar;
            }

            Console.WriteLine("Generated salt sequence: " + salt);
            return salt;
        }



        public static string Salt(string s)
        {
            string saltedString = "";
            int currentIndex = 0;

            //Calculate the next index to salt
            int nextSalt = currentIndex + saltingIntervals[0];

            foreach(char c in s)
            {
                saltedString += c;
                if (currentIndex < nextSalt)
                {
                    //Skip the character because the salt has not yet started
                }
                else
                {
                    //Insert salt at this point in the string
                    saltedString += GenerateSalt(saltLength);
                    //Pop the next interval into the stack
                    NextSaltingInterval();
                    //Set the next salting point
                    nextSalt = currentIndex + saltingIntervals[0];
                }

                currentIndex++;
            }

            return saltedString;
        }

        public static string Desalt(string s)
        {
            string originalString = s;

            //Index starts at 1 in order to accomodate an offset resulting from the salting process
            int currentIndex = 1;
           
            Console.WriteLine("Desalting");

            while (currentIndex <= s.Length)
            {
                //Find the index of the next salt start as long as it does not go over the string length
                if (currentIndex + saltingIntervals[0] <= s.Length)
                {
                    Console.WriteLine("Interval " + saltingIntervals[0]);

                    //Update the index to match the current point of reference
                    currentIndex += saltingIntervals[0];
                    //Remove characters from here up until salt length
                    if (currentIndex + saltLength <= s.Length)
                    {
                        string tempString = s.Remove(currentIndex, saltLength);
                        s = tempString;
                    }
                    else
                    {
                        //Attempts to desalt out of bounds of the 
                        System.Windows.Forms.MessageBox.Show("Attempting to desalt out of bounds of the string.  It is possible that the source text was not salted.  Proceeding with standard decryption.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        
                        return originalString;
                    }                 

                    Console.WriteLine("New string: " + s);
                    
                    NextSaltingInterval();
                }
                else
                {
                    break;
                }
            }
            
           return s;
        }
    }
}
