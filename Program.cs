using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace sw
{
    class Program
    {
        static void Main(string[] args)
        {
            //1
            //Console.WriteLine(isPalindrome("454"));

            //2
            //Console.WriteLine(minSplit(48));

            //3
            // int[] arr = new int[] {9,11,4};
            // Console.WriteLine(notContains(arr));

            //4
            // string expression = "(()() ) ";
            // bool stackResult = isProperly(expression);

            // if (stackResult)
            //     Console.WriteLine("Expression is Valid.");
            // else
            //     Console.WriteLine("\nExpression is not valid.");

            //5
            // int s = 9; 
            // Console.WriteLine("Number of ways = " + countVariants(s)); 
            
            //6
            // MyStructure ds = new MyStructure(); 
            // ds.add(10); 
            // ds.add(20); 
            // ds.add(30); 
            // ds.add(40);
            // ds.Remove(10);
            // ds.Remove(30);

            // foreach(var val in ds.GetAll())
            // {
            //     Console.WriteLine(val);
            // }
        

            //8
            // 
            //  Console.Write("Available Currencies : ");
            // foreach(var cur in GetCurrencies())
            // {
            //     Console.Write(cur.Name + " | ");
            // }
            //  Console.WriteLine();
            // Console.WriteLine("Enter from Value : ");
            // var from = Console.ReadLine();
            // Console.WriteLine("Enter to Value : ");
            // var to = Console.ReadLine();

            // exchangeRate(from, to);
        }

        public static bool isPalindrome(string text)
        {
            string reverse = "";

            for(int i = text.Length-1; i >= 0; i--)
            {
                reverse += text[i];
            }

            return reverse == text;
        }

        public static int minSplit(int amount)
        {
            int[] coins = new int[] {1, 5, 10, 20,50};
            List<int> coinList = new List<int>();

            for(int i = coins.Length-1; i >=0; i--)
            {
                while(amount >= coins[i]){
                    amount -= coins[i];
                    coinList.Add(coins[i]);
                }
            }

            return coinList.Count;
        }

        public static int notContains(int[] array)
        {
            Array.Sort(array);
            for(int i = 0; i < array.Length; i++)
            {
                var next = array[i] + 1;
                if(next > 0  && !Array.Exists(array, e => e.Equals(next)))
                {
                    return next;
                }
            }
            return 0;
        }
    
        public static bool isProperly(string expression)
        {
            Stack<char> openStack = new Stack<char>();
            foreach (char c in expression)
            {
                switch (c)
                {
                    case '(':
                    openStack.Push(c);
                    break;
                    case ')':
                    if (openStack.Count == 0 || openStack.Peek() != '(')
                    {
                        return false;
                    }
                    openStack.Pop();       
                    break;
                    default:
                    break;
                }
            }
            return openStack.Count == 0;
   }

    
        public static int countVariants(int stearsCount) 
        { 
            return fib(stearsCount + 1); 
        } 

        
        public static int fib(int n) 
        { 
            if (n <= 1) 
                return n; 
            return fib(n - 1) + fib(n - 2); 
        } 
    
     public static void exchangeRate(string from, string to)
        {
            var currencies = GetCurrencies();

            Currency fromClass = currencies.Where(x => x.Name == from).Single();
            Currency toClass = currencies.Where(x => x.Name == to).Single();

            Console.WriteLine( fromClass.Quantity + " - " + fromClass.Name + ": " + fromClass.Value);
            Console.WriteLine(toClass.Quantity + " - " + toClass.Name + ": " + toClass.Value);
            
            var fromVal = Convert.ToDouble(fromClass.Value);
            var toVal = Convert.ToDouble(toClass.Value);

            var result = (fromVal / Convert.ToDouble(fromClass.Quantity)) / (toVal / Convert.ToDouble(toClass.Quantity));
           Console.WriteLine("1 " + from + " is " +  result + " " + to);
        }
 

        //this class is used for pharse html from xml cdata and deserialize it to c# class
        public static List<Currency> GetCurrencies()
        {
            var currencies = new List<Currency>();

            XElement XTemp = XElement.Load("http://www.nbg.ge/rss.php");
            //get string from CDATA tag
                var queryCDATAXML = from element in XTemp.DescendantNodes()
                                    where element.NodeType == System.Xml.XmlNodeType.CDATA
                                    select element.Parent.Value.Trim();
                string bodyHtml = queryCDATAXML.ToList<string>()[0].ToString(); 

            //use HtmlAgilityPack to pharse html
            var doc = new HtmlDocument();
            doc.LoadHtml(bodyHtml);

            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table")) {
                //Console.WriteLine("Found: " + table.Id);
                foreach (HtmlNode row in table.SelectNodes("tr")) {
                   // Console.WriteLine("row");
                    var currencyToAdd = new Currency();
                    List<string> list = new List<string>();
                    foreach (HtmlNode cell in row.SelectNodes("th|td")) {
                        if(cell.InnerText != ""){
                            var cellSplited = cell.InnerText.Split(' ')[0];
                            list.Add(cellSplited);
                        }
                    }
                    currencyToAdd.Name = list[0];
                    currencyToAdd.Quantity = list[1];
                    currencyToAdd.Value = list[2];
                    currencyToAdd.decimalValue = list[3];
                    currencies.Add(currencyToAdd);
                }
            }

            return currencies;
        }

    }
}
