using System;
namespace DllConverter
{
    public class Converterjson
    {
        public static string xmlformat(string objjson)
        {
            var dato = Jsonver(objjson);
            var dato1 = Elimina_espacios(dato);
            var formato = Etiquetaxml(dato1);
            return formato;
        }

        public static string Etiquetaxml(string jsonobj)
        {
            string cadena = "<?xml version=\"1.0\" encoding=\"UTF - 8\" ?>\n";
            string[] arrTmp = jsonobj.Split('\n');
            string cad = " ";
            for (int i = 0; i < arrTmp.Length; i++)
            {
                cad = arrTmp[i].Trim();
                if (cad.Length == 1)
                {
                    cadena = cadena + "<" + arrTmp[i] + ">\n";
                }
                if (cad.Length > 1)
                {
                    if (cad.Length == 2)
                    {
                        cadena = cadena + "<" + arrTmp[i] + ">\n";
                    }
                    else
                    {
                        string[] auxArr = arrTmp[i].Trim().Split(':');
                        string var = auxArr[0].Trim().Substring(0, 1);
                        if (auxArr[1].Trim() == "array" && var != "/")
                        {
                            cadena = cadena + "<" + auxArr[0] + "name = \"languages_array\">\n";
                        }
                        else
                        {
                            if (auxArr[1].Trim() == "array" && var == "/")
                            {
                                cadena = cadena + "<" + auxArr[0] + ">\n";
                            }
                            else
                            {
                                string name = auxArr[0].Trim().Replace("\n", "");
                                string value = auxArr[1].Trim().Replace("\n", "").Replace(",", "");
                                cadena = cadena + "<" + name + ">" + value + "</" + name + ">\n";
                            }
                        }
                    }
                }
            }
            return cadena;
        }

        public static string Elimina_espacios(string jsonobj)
        {
            string[] arrTmp = jsonobj.Split('\n');
            string cadena = " ";
            for (int i = 0; i < arrTmp.Length; i++)
            {
                cadena = cadena + arrTmp[i].Trim() + "\n";
            }
            return cadena;
        }

        public static string Jsonver(string jsonobj)
        {
            string cadena = " ";
            var jsontipo = TipoJson(jsonobj);
            if (jsontipo == "array")
            {
                var arryJSON = JSONArray(jsonobj);
                cadena = arryJSON;
            }

            if (jsontipo == "obj")
            {
                var objJSON = JSONObject(jsonobj);
                cadena = objJSON;
            }

            if (jsontipo == "error")
            {
                cadena = "no es archivo json";
            }

            return cadena;
        }

        public static string JSONObject(string jsonobj)
        {
            return "";
        }

        public static string JSONArray(string jsonobj)
        {
            var eliminacorchetes = DeletCharArray(jsonobj);
            string[] arrTmp = eliminacorchetes.Split('\n');
            string inifin = " ";
            string inifinbra = " ";
            int punterarray = 0;
            string cadena = " ";
            int countElements = 0;
            string inisiocadena = " ";

            for (int i = 0; i < arrTmp.Length; i++)
            {
                if (arrTmp[i].Trim() == "{")
                {
                    inifin = Goupllave(i, jsonobj);
                    var splitinifin = inifin.Split(':');
                    arrTmp[Convert.ToInt32(splitinifin[0])] = punterarray.ToString();
                    arrTmp[Convert.ToInt32(splitinifin[1])] = "/"+punterarray.ToString().Trim();
                    punterarray++;
                    i = Convert.ToInt32(splitinifin[1]);
                }
            }

            for (int i = 0; i < arrTmp.Length; i++)
            {
                if (arrTmp[i].Trim().Length > 1)
                {
                    if (arrTmp[i].Trim().Length != 2)
                    {
                        if (arrTmp[i].Trim().Length != 1)
                        {
                            string[] auxArr = arrTmp[i].Trim().Split(':');
                            if (auxArr[1] == "[")
                            {
                                inifinbra = Grupobracket(i + 1, jsonobj);
                                var splitinifin = inifinbra.Split(':');
                                arrTmp[Convert.ToInt32(splitinifin[0])] = auxArr[0].Trim() + ":array";
                                arrTmp[Convert.ToInt32(splitinifin[1])] = "/" + auxArr[0].Trim() + ":array";
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < arrTmp.Length; i++)
            {
                if (arrTmp[i].Trim().Length > 1)
                {
                    if (arrTmp[i].Trim().Length != 2)
                    {
                        if (arrTmp[i].Trim().Length != 1)
                        {
                            string[] auxArr = arrTmp[i].Trim().Split(':');
                            if (auxArr[1] == "array")
                            {
                                inisiocadena = "/"+arrTmp[i];
                                inisiocadena.Trim();
                                for (int j = i; j < arrTmp.Length; j++)
                                {
                                    if (arrTmp[j].Trim() != inisiocadena.Trim())
                                    {
                                        if (arrTmp[j].Trim().Length > 1)
                                        {
                                            inifin = Goupllave(j + 1, jsonobj);
                                        }
                                        else
                                        {
                                            if (arrTmp[j].Trim().Length == 1)
                                            {
                                                inifin = Goupllave(j, jsonobj);
                                            }
                                        }

                                        string[] dd = inifin.Split(':');
                                        arrTmp[Convert.ToInt32(dd[0])] = countElements.ToString();
                                        arrTmp[Convert.ToInt32(dd[1])] = "/"+countElements.ToString().Trim();
                                        countElements++;
                                        j = Convert.ToInt32(dd[1]);
                                    }
                                    else
                                    {
                                        countElements = 0;
                                        i = j;
                                        j = arrTmp.Length + 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < arrTmp.Length; i++)
            {
                if (arrTmp[i].Trim().Length > 1)
                {
                    if (arrTmp[i].Trim().Length != 2)
                    {
                        if (arrTmp[i].Trim().Length != 1)
                        {
                            string[] arrult = arrTmp[i].Trim().Split(':');
                            if (arrult[1].Trim() == "array")
                            {
                                if (arrTmp[i+1].Trim() == "{")
                                {
                                    inisiocadena = "/" + arrTmp[i];
                                    for (int j = i; j < arrTmp.Length; j++)
                                    {
                                        if (arrTmp[j].Trim() != inisiocadena.Trim())
                                        {
                                            if (arrTmp[j].Trim().Length > 1)
                                            {
                                                inifin = Goupllave(j + 1, jsonobj);
                                            }
                                            else
                                            {
                                                if (arrTmp[j].Trim().Length == 1)
                                                {
                                                    inifin = Goupllave(j, jsonobj);
                                                }
                                            }

                                            string[] dd = inifin.Split(':');
                                            arrTmp[Convert.ToInt32(dd[0])] = countElements.ToString();
                                            arrTmp[Convert.ToInt32(dd[1])] = "/"+countElements.ToString().Trim();
                                            countElements++;
                                            j = Convert.ToInt32(dd[1]);
                                        }
                                        else
                                        {
                                            countElements = 0;
                                            i = j;
                                            j = arrTmp.Length + 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < arrTmp.Length; i++)
            {
                cadena = cadena + arrTmp[i] + "\n";
            }

            return cadena;
        }

        public static string Goupllave(int a, string jsonobj)
        {
            var eliminacorchetes = DeletCharArray(jsonobj);
            string[] arrTmp = eliminacorchetes.Split('\n');
            int puntero_llavas = 0;
            int inicio = 0;
            int fin = 0;

            string[] auxArray = arrTmp;
            for (int j = a; j < auxArray.Length; j++)
            {
                if (auxArray[j].Trim() == "}" || auxArray[j].Trim() == "},")
                {
                    puntero_llavas--;
                }
                else
                {
                    if (auxArray[j].Trim() == "{")
                    {
                        puntero_llavas++;
                    }
                }

                if (puntero_llavas == 0)
                {
                    inicio = a;
                    fin = j;
                    j = auxArray.Length + 1;
                }
            }

            return inicio.ToString() + ":" + fin.ToString();
        }

        public static string Grupobracket(int a, string jsonobj)
        {
            var eliminacorchetes = DeletCharArray(jsonobj);
            string[] arrTmp = eliminacorchetes.Split('\n');
            int puntero_bracket = 1;
            int inicio = 0;
            int fin = 0;

            string[] auxArray = arrTmp;
            for (int j = a; j < auxArray.Length; j++)
            {
                if (auxArray[j].Trim() == "]" || auxArray[j].Trim() == "],")
                {
                    puntero_bracket--;
                }
                else
                {
                    string cade = auxArray[j].Trim();
                    if (cade.Length != 2)
                    {
                        if (cade.Length != 1)
                        {
                            string[] verifica = auxArray[j].Trim().Split(':');
                            if (verifica[1] == "[")
                            {
                                puntero_bracket++;
                            }
                        }
                    }
                }

                if (puntero_bracket == 0)
                {
                    inicio = a;
                    fin = j;
                    j = auxArray.Length + 1;
                }
            }

            return inicio - 1 + ":" + fin;
        }

        private static string DeletCharArray(string jsonobj)
        {
            string cadena = " ";
            string[] arrTemp = jsonobj.Split('\n');
            string[] auxArr = new string[arrTemp.Length - 1];

            for (int i = 1; i < arrTemp.Length - 1; i++)
            {
                auxArr[i - 1] = arrTemp[i];
            }

            for (int i = 0; i < auxArr.Length; i++)
            {
                cadena = cadena + auxArr[i] + "\n";
            }

            return cadena;
        }
        public static string TipoJson(string dato)
        {
            string[] arrTemp = dato.Split('\n');
            string cade = " ";
            bool sw = false;
            if (arrTemp[0].Trim() == "[" && arrTemp[arrTemp.Length - 1].Trim() == "]")
            {
                cade = "array";
                sw = true;
            }

            if (arrTemp[0].Trim() == "{" && arrTemp[arrTemp.Length - 1].Trim() == "}")
            {
                cade = "obj";
                sw = true;
            }

            if (!sw)
            {
                cade = "error";
            }

            return cade;
        }
    }
}
