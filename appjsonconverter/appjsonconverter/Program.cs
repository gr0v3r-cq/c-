using System;
using DllConverter;

namespace appjsonconverter
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string s = "[\n\t{\n\t\t\"name\":\"name principal\",\n\t\t\"albun\":[\n\t\t\t{\n\t\t\t\t\"albun name\":\" tipo de canciones\",\n\t\t\t\t\"song\":[\n\t\t\t\t\t{\n\t\t\t\t\t\t\"song_name\":\"name_cancion\",\n\t\t\t\t\t\t\"time\":\"1212\"\n\t\t\t\t\t},\n\t\t\t\t\t{\n\t\t\t\t\t\t\"song_name\":\"name_cancion\",\n\t\t\t\t\t\t\"time\":\"1212\"\n\t\t\t\t\t}\n\t\t\t\t],\n\t\t\t\t\"descripcio\":\"descripcion ed los datos\"\n\t\t\t},\n\t\t\t{\n\t\t\t\t\"albun name\":\" tipo de canciones\",\n\t\t\t\t\"song\":[\n\t\t\t\t\t{\n\t\t\t\t\t\t\"song_name\":\"name_cancion\",\n\t\t\t\t\t\t\"time\":\"1212\"\n\t\t\t\t\t},\n\t\t\t\t\t{\n\t\t\t\t\t\t\"song_name\":\"name_cancion\",\n\t\t\t\t\t\t\"time\":\"1212\"\n\t\t\t\t\t}\n\t\t\t\t],\n\t\t\t\t\"descripcio\":\"descripcion ed los datos\"\n\t\t\t}\n\t\t]\n\t},\n\t{\n\t\t\"name\":\"name principal\",\n\t\t\"albun\":[\n\t\t\t{\n\t\t\t\t\"albun name\":\" tipo de canciones\",\n\t\t\t\t\"song\":[\n\t\t\t\t\t{\n\t\t\t\t\t\t\"song_name\":\"name_cancion\",\n\t\t\t\t\t\t\"time\":\"1212\"\n\t\t\t\t\t},\n\t\t\t\t\t{\n\t\t\t\t\t\t\"song_name\":\"name_cancion\",\n\t\t\t\t\t\t\"time\":\"1212\"\n\t\t\t\t\t}\n\t\t\t\t],\n\t\t\t\t\"descripcio\":\"descripcion ed los datos\"\n\t\t\t},\n\t\t\t{\n\t\t\t\t\"albun name\":\" tipo de canciones\",\n\t\t\t\t\"song\":[\n\t\t\t\t\t{\n\t\t\t\t\t\t\"song_name\":\"name_cancion\",\n\t\t\t\t\t\t\"time\":\"1212\"\n\t\t\t\t\t},\n\t\t\t\t\t{\n\t\t\t\t\t\t\"song_name\":\"name_cancion\",\n\t\t\t\t\t\t\"time\":\"1212\"\n\t\t\t\t\t}\n\t\t\t\t],\n\t\t\t\t\"descripcio\":\"descripcion ed los datos\"\n\t\t\t}\n\t\t]\n\t}\n]";
            //string data2 = Converterjson.Jsonver(s);
            string data2 = Converterjson.xmlformat(s);

            Console.WriteLine(data2);

            Console.ReadKey();
        }
    }
}
