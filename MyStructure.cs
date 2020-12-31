using System;
using System.Collections.Generic;

namespace sw
{
    public class MyStructure
    {
        List<int> arr;
        Dictionary<int,int> hash;

        public MyStructure()
        {
            arr = new List<int>();
            hash = new Dictionary<int, int>();
        }

        // A function to add an element to MyStructure 
        // data structure 
        public void add(int x) 
        { 
            // If ekement is already present, then noting to do 
            if (hash.ContainsKey(x)) 
                return; 
        
            // Else put element at the end of arr 
            int s = arr.Count; 
            arr.Add(x); 
        
            // And put in hash also 
            hash.Add(x, s); 
        }

        public bool Remove(int item)
        {
            // Replace index of value to remove with last item in values list
            int keyIndex = this.hash[item];
            var lastItem = this.arr[this.arr.Count - 1];
            this.arr[keyIndex] = lastItem;

            // Update index in dictionary for last item that was just moved
            this.hash[lastItem] = keyIndex;

            // Remove old value
            this.hash.Remove(item);
            this.arr.RemoveAt(this.arr.Count - 1);

            return true;
        }


        public List<int> GetAll() 
        { 
            return arr; 
        }  
    }
}