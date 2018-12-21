namespace DllConverter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Converterjson
    {
        public static string Jsonver(string jsonobj)
        {
            string cadena = "<?xml version=\"1.0\" encoding=\"UTF - 8\" ?>\n";
            string xml = " ";
            var jsontipo = TipoJson(jsonobj);
            if (jsontipo == "array")
            {
                var arryJSON = JSONArray(jsonobj);
                cadena = arryJSON;
            }

            if (jsontipo == "obj")
            {
                var objJSON = JSONObject(jsonobj);
                xml = Formatxml(objJSON);
                cadena = xml;
            }

            if (jsontipo == "error")
            {
                cadena = "no es archivo json";
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

        public static string Goupllavearray(int a, string jsonobj)
        {
            var eliminacorchetes = DeletCharArray(jsonobj);
            string[] auxArray = eliminacorchetes.Split('\n');
            int puntero_llavas = 0;
            int inicio = 0;
            int fin = 0;
            for (int i = a; i < auxArray.Length; i++)
            {
                if (auxArray[i].Trim() == "}" || auxArray[i].Trim() == "},")
                {
                    puntero_llavas--;
                }
                else
                {
                    if (auxArray[i].Trim() == "{")
                    {
                        puntero_llavas++;
                    }
                }

                if (puntero_llavas == 0)
                {
                    inicio = a;
                    fin = i;
                    i = auxArray.Length + 1;
                }
            }

            return inicio + ":" + fin;
        }

        public static string ArrayJson(string jsonobj)
        {
            var eliminacorchetes = DeletCharArray(jsonobj);
            string[] arrTmp = eliminacorchetes.Split('\n');
            string inifin = " ";
            string inifinbra = " ";
            int punterarray = 0;
            string cadena = " ";
            int countElements = 0;

            for (int i = 0; i < arrTmp.Length; i++)
            {
                if (arrTmp[i].Trim() == "{")
                {
                    inifin = Goupllave(i, jsonobj);
                    var splitinifin = inifin.Split(':');
                    arrTmp[Convert.ToInt32(splitinifin[0])] = punterarray.ToString();
                    arrTmp[Convert.ToInt32(splitinifin[1])] = punterarray.ToString();
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
                                arrTmp[Convert.ToInt32(splitinifin[0])] = auxArr[0] + " :array ";
                                arrTmp[Convert.ToInt32(splitinifin[1])] = "/" + auxArr[0] + ":array ";
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
                                countElements = 0;
                                for (int j = i; j < arrTmp.Length; j++)
                                {
                                    string varfinarray = "/" + auxArr[j].Trim();
                                    if (arrTmp[j].Trim() == varfinarray)
                                    {
                                        string ramas = Goupllavearray(i + 1, jsonobj);
                                        arrTmp[Convert.ToInt32(ramas[0])] = countElements.ToString();
                                        arrTmp[Convert.ToInt32(ramas[1])] = countElements.ToString();
                                        countElements++;
                                        i = Convert.ToInt32(ramas[1]);
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

        public static string JSONArray(string jsonobj)
        {
            var eliminacorchetes = DeletCharArray(jsonobj);
            string[] arrTmp = eliminacorchetes.Split('\n');
            string[] auxArr = new string[arrTmp.Length];
            string cadena = " ";
            int count = 0;
            int puntero = 0;
            int ptllave = 0;
            int ptcorch = 0;
            int punteroArry = 0;
            int inicio = 0;
            
            for (int i = 0; i < arrTmp.Length; i++)
            {
                if (arrTmp[i].Trim().Length == 1)
                {
                    var caracter = TipoCaracter(arrTmp[i].Trim());
                    if (caracter == "{")
                    {
                        auxArr[count] = puntero.ToString();
                        ptllave++;
                        count++;
                    }
                }
                else
                {
                    string[] datoArr = arrTmp[i].Trim().Split(':');
                    string[] auxArrTmp = arrTmp;
                    var verifica = TipoCaracter(datoArr[1]);
                    if (verifica == "[")
                    {
                        ptcorch++;
                        for (int j = i + 1; j < auxArrTmp.Length; j++)
                        {
                            if (auxArrTmp[j].Trim() == "],")
                            {
                                ptcorch--;
                            }
                            else
                            {
                                if (auxArrTmp[j].Trim().Length == 1)
                                {
                                    string tipchar = auxArrTmp[j].Trim();
                                    if (tipchar == "[")
                                    {
                                        punteroArry = 999;
                                    }

                                    if (tipchar == "]")
                                    {
                                        ptcorch--;
                                    }
                                }
                                else
                                {
                                    if (auxArrTmp[j].Trim().Length > 1)
                                    {
                                        if (auxArrTmp[j].Trim() != "},")
                                        {
                                            string[] value2 = auxArrTmp[j].Trim().Split(':');
                                            if (value2[1].Trim() == "[")
                                            {
                                                ptcorch++;
                                            }
                                        }
                                    }
                                }
                            }

                            if (ptcorch == 0)
                            {
                                inicio = i;
                                punteroArry = j;
                                j = auxArrTmp.Length + 1;
                            }
                            else
                            {
                                punteroArry = 999;
                            }
                        }
                    }
                    else
                    {
                        auxArr[count] = arrTmp[i];
                        count++;
                    }

                    for (int k = inicio; k < punteroArry + 1; k++)
                    {
                        auxArr[count] = datoArr[0] + arrTmp[k];
                        count++;
                    }
                }
            }

            for (int i = 0; i < auxArr.Length; i++)
            {
                cadena = cadena + auxArr[i];
            }

            return cadena;
        }

        public static string TipoCaracter(string cadena)
        {
            string cadnew = " ";
            if (cadena.Length > 0)
            {
                cadnew = cadena.Substring(0, 1);
            }
            else
            {
                cadnew = "error";
            }

            return cadnew;
        }

        public static string JSONObject(string jsonobj)
        {
            return jsonobj;
        }

        private static string Formatxml(string objxml)
        {
            string[] arrTemp = objxml.Split('\n');
            bool sw = true;
            string cadnew = " ";

            for (int i = 0; i < arrTemp.Length; i++)
            {
                if (arrTemp[i].Trim().Length == 1)
                {
                    if (sw)
                    {
                        cadnew = cadnew + "<" + arrTemp[i].Trim() + ">\n";
                        sw = false;
                    }
                    else
                    {
                        cadnew = cadnew + "</" + arrTemp[i].Trim() + ">\n";
                        sw = true;
                    }
                }
                else
                {
                    if (arrTemp[i].Trim().Length > 1)
                    {
                        string[] auxArr = arrTemp[i].Trim().Split(':');
                        string name = " ";
                        string valu = " ";
                        name = auxArr[0].Trim().Replace("\"", " ");
                        valu = auxArr[1].Trim().Replace("\"", " ").Replace(",", " ");
                        cadnew = cadnew + "<" + name.Trim() + ">" + valu.Trim() + "</" + name.Trim() + ">\n";
                    }
                }
            }

            return cadnew;
        }

        private static string Deletnulol(string datos)
        {
            string[] arrTemp = datos.Split('\n');
            string cadnew = " ";

            for (int i = 0; i < arrTemp.Length; i++)
            {
                if (arrTemp[i].Trim() != " ")
                {
                    cadnew = cadnew + arrTemp[i] + "\n";
                }
            }

            return cadnew;
        }

        private static string XmlArray(string dato)
        {
            string[] arrTemp = dato.Split('\n');
            string[] auxArr = new string[arrTemp.Length];
            int count = 0;
            int puntero = 0;
            string deletspac = " ";
            string cadnew = " ";
            string caracter = " ";
            string caracter2 = " ";

            for (int i = 0; i < arrTemp.Length; i++)
            {
                deletspac = arrTemp[i].Trim();
                if (deletspac.Length > 0)
                {
                    caracter = deletspac.Substring(0, 1);
                }
                else
                {
                    caracter = "}";
                }
                
                if (caracter == "{")
                {
                    auxArr[count] = puntero.ToString();
                    
                    count++;
                }
                else
                {
                    if (deletspac.Length > 2)
                    {
                        caracter2 = deletspac.Substring(0, 2);
                    }
                    else
                    {
                        caracter2 = " ";
                    }

                    if (caracter == "}" || caracter2 == "},")
                    {
                        auxArr[count] = puntero.ToString();
                        puntero++;
                        count++;
                    }
                    else
                    {
                        auxArr[count] = deletspac;
                        count++;
                    }
                }
            }

            for (int i = 0; i < auxArr.Length - 1; i++)
            {
                cadnew = cadnew + auxArr[i] + "\n";
            }

            return cadnew;
        }

        private static string Verificachar(string objsjsonchar)
        {
            string[] arrTemp = objsjsonchar.Split('\n');
            string cadena = arrTemp[0].Trim();
            string caracter = cadena.Substring(0, 1);
            return caracter;
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
    }
}
