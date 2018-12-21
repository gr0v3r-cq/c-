namespace ConsoleApp1
{
    using DllConverter;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            //StreamReader sr = new StreamReader(@"C:\Users\Developer\Documents\json0.txt");
            //StreamReader sr = new StreamReader(@"C:\Users\Developer\Documents\mediumemployee_1875069837.json");
            //StreamReader sr = new StreamReader(@"C:\Users\Developer\Documents\easycolors_1875069837.json");
            //StreamReader sr = new StreamReader(@"C:\Users\Developer\Documents\hardplaylist_1875069837.json");
            //StreamReader sr = new StreamReader(@"C:\Users\Developer\Documents\prueba.json");
            StreamReader sr = new StreamReader(@"C:\Users\Developer\Documents\2.json");
            string s = sr.ReadToEnd();
            //string data = converterjson.groupobj(s);
            //string data1 = jsonConverter.verificjsonini(data);

            //string data2 = Converterjson.Jsonver(s);
            string data2 = Converterjson.ArrayJson(s);
            //string data2 = converterjson.JsontoXml(s);
            Console.WriteLine(data2);
           
            Console.ReadKey();
        }
    }
}
