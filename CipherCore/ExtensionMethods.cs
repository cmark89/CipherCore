using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CipherCore
{
    public static class ExtensionMethods
    {

        public static TKey GetKeyByValue<TKey, TValue> (this Dictionary<TKey, TValue> dictionary, TValue value)
        {
            if (dictionary != null)
            {
                foreach (KeyValuePair<TKey, TValue> pair in dictionary)
                {
                    if (value.Equals(pair.Value))
                    {
                        return pair.Key;
                    }
                }

                System.Windows.Forms.MessageBox.Show("Value not found during lookup: " + value, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                throw new Exception("Not found during lookup.");
            }
            else
            {
                throw new Exception("Dictionary is null.");
            }
        }

        //Push each element in the list one space forward, and bring the last element to the front.
        //Figure out how to make this type safe
        public static List<char> WrappingPush (this List<char> list)
        {
            Console.WriteLine("Pushing table.");
            List<char> pushedList = new List<char>();

            //Bring the final element to the front and remove from the previous list.
            pushedList.Add(list[list.Count - 1]);
            list.RemoveAt(list.Count - 1);

            foreach (char c in list)
            {
                pushedList.Add(c);
            }

            return pushedList;
        }


        public static List<char> WrappingPop(this List<char> list)
        {
            Console.WriteLine("Pulling table.");
            List<char> pulledList = new List<char>(list);

            //Save the first element off and then remove it, adding it to the end
            char firstChar = pulledList[0];
            pulledList.RemoveAt(0);
            pulledList.Add(firstChar);

            return pulledList;
        }
    }



}
