using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CipherCore
{
    public class CipherTable
    {
        public List<char> usableCharacters;
        public List<int> usableCharacterValues;
        public Dictionary<char, char> translationTable;

        public CipherTable()
        {
            Initialize();
        }
        
        public void Initialize()
        {
            //Builds the initial list of selectable characters
            usableCharacters = new List<char>(){
                'A','B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 
                'X', 'Y', 'Z', '1', '!', '2', '@', '3', '#', '4', '$', '5', '%', '6', '^', '7', '&', '8', '*', '9', '(', '0', ')',
                '`', '~', '-', '+', '_', '=', '{', '}', '[', ']', ':', ';', '\'', '\"', ',', '.', '<', '>', '?', '/', '\\', '|', ' '
            };

            //Builds a list of arbitrary integers corresponding with the list of usable characters; for use in salting
            usableCharacterValues = new List<int>(){
                2, 3, 5, 3, 4, 1, 2, 1, 3, 2, 3, 3, 6, 4, 3, 1, 7, 5, 4, 3, 1, 1, 3, 4, 4, 5, 2, 1, 5, 6, 5, 4, 7, 2, 4, 3, 5, 4, 
                3, 2, 3, 4, 1, 2, 1, 1, 2, 5, 6, 2, 1, 1, 3, 3, 5, 4, 2, 1, 3, 3, 2, 1, 3, 5, 2, 6, 2, 1, 3
            };
            
            //Create the dictionary for translation
            translationTable = new Dictionary<char, char>();

            Console.WriteLine("Usable Characters: ");
            foreach (char c in usableCharacters)
            {
                Console.Write(c + " ");
            }

            Console.WriteLine("\nEntries in usable characters: " + usableCharacters.Count);
        }


        public bool CharacterIsLegal(char c)
        {
            return (usableCharacters.Contains(c) || usableCharacters.Contains(Char.ToUpper(c)));
        }


        public int GetUsableCharIndex(char c)
        {
            for(int i = 0; i < usableCharacters.Count; i++)
            {
                if(usableCharacters[i] == c)
                {
                    return i;
                }
            }

            return 0;
        }


        //This method builds the second half of the translationTable dictionary based on the provided keyword
        public void BuildTranslationTable(string keyword)
        {
            keyword = keyword.ToUpper();
            Console.WriteLine("\nBuilding translation table.");

            //Copy the list of usable characters; this list will be the source for the dictionary keys
            List<char> temporaryCharList = new List<char>(usableCharacters);

            //Convert the provided keyword into a char array for processing

            List<char> newCharList = new List<char>();
            foreach (char c in keyword)
            {
                //If the temporary character list contains the character...
                if (temporaryCharList.Contains(c))
                {
                    //Remove the character from the temporary list and add it to the next slot in the new character list
                    temporaryCharList.Remove(c);
                    newCharList.Add(c);
                }
                else
                {
                    //Character has been used already; replace it with the rear-most character remaining in the list
                    char newChar = temporaryCharList[temporaryCharList.Count - 1];
                    temporaryCharList.Remove(newChar);
                    newCharList.Add(newChar);
                }
            }
            //Now that all characters in the keyword have been added to the new list and removed from the temporary list, add the remaining 
            //characters to the new list
            foreach (char c in temporaryCharList)
            {
                newCharList.Add(c);
            }

            //Now that the list is completed, shift the table an arbitrary number of spaces forward to create a more difficult cipher.
            //The table is shifted by the total value of the letters that make up the keyword
            int keywordValue = 0;
            int firstCharValue = usableCharacterValues[GetUsableCharIndex(keyword[0])];
            int lastCharValue = usableCharacterValues[GetUsableCharIndex(keyword[keyword.Length - 1])];
            foreach (char c in keyword)
            {
                keywordValue += usableCharacterValues[GetUsableCharIndex(c)];
            }
            int numberOfShifts = (keywordValue % firstCharValue) + ((int)((float)(lastCharValue + keyword.Length) / (float)3)) + 1;
            ShiftTable(ref newCharList, numberOfShifts);

            //Dispose of the temporary list
            temporaryCharList.Clear();

            //Clear the dictionary to prevent contamination from previous sessions
            translationTable.Clear();

            //Now that the new list is complete, populate the translationTable dictionary
            for (int i = 0; i < usableCharacters.Count; i++)
            {
                translationTable.Add(usableCharacters[i], newCharList[i]);
            }

            Console.WriteLine("\n----------------\nTranslation Table\n----------------\n");
            foreach (KeyValuePair<char, char> pair in translationTable)
            {
                Console.WriteLine(pair.Key + " : " + pair.Value + "\n");
            }
        }



        //Shifts the translation side of the table the indicated spaces forward.
        public void ShiftTable(ref List<char> list, int i)
        {
            Console.WriteLine("Shift the translation table forward by " + i + " elements.");
            Console.WriteLine("First element before shift: " + list[0]);

            if (i > 0)
            {
                for (int cnt = 0; cnt < i; cnt++)
                {
                    list = list.WrappingPush();
                    //list = list.WrappingPop();
                }
            }
            else
            {
                return;
            }

            Console.WriteLine("First element after shift: " + list[0]);
        }


        //Returns the encrypted value of char c
        public char EncryptChar(char c)
        {
            if (c == '\n' || c == '\0')
            {
                return c;
            }

            if (translationTable.ContainsKey(c))
            {
                return translationTable[c];
            }
            else if(translationTable.ContainsKey(Char.ToUpper(c)))
            {
                    //Return it as an uppercase value
                    return translationTable[Char.ToUpper(c)];
            }
            else
            {
                Console.WriteLine("Not found in translation lookup: " + c);
                return new Char();
            }
        }


        //Returns the decrypted key of encrypted char c
        public char DecryptChar(char c)
        {
            if(c == '\n' || c == '\0')
            {
                return c;
            }

            if (translationTable.ContainsValue(c))
            {
                //Passed an upper case character; return it as an upper case character
                return translationTable.GetKeyByValue(c);
            }
            else if (translationTable.ContainsKey(Char.ToUpper(c)))
            {
                    //Received lowercase; return as uppercase
                    return translationTable.GetKeyByValue(Char.ToUpper(c));
            }
            else
            {
                throw new Exception("Character not found.");
            }
        }
    }
}
