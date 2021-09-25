using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalBandBakery.Infra.Repository.DB
{
    public class DBService
    {
        public static string stockFile = @"C:\Users\holacons\Desktop\VU WorkSpace\MetalBandBakery.V1\MetalBandBakery.Infra\Repository\DB\Stocks.txt";
        
        public static List<string> ReadTextFromFile(string filePath)
        {
            List<string> lines = new List<string>();
            using (StreamReader file = new StreamReader(@"C:\Users\holacons\Desktop\VU WorkSpace\MetalBandBakery.V1\MetalBandBakery.Infra\Repository\DB\Stocks.txt"))
            {
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    lines.Add(ln);
                }
                file.Close();
            }
            return lines;
        }

        public static void ReWriteFile(string filePath, List<string> lines)
        {
            File.WriteAllLines(filePath, lines);
        }
        
        public static int GetIndexOfText(char product, List<string> lines)
        {
            int cont = 0;
            foreach (var i in lines)
            {
                if (i.Split('=')[0] == product.ToString()) 
                    break;
                cont++;
            }
            return cont;
        }
        
        public static bool ExistsProductInFile(char product, string filePath)
        {
            bool exists = false;
            foreach(var i in DBService.ReadTextFromFile(filePath))
            {
                if (i.Split('=')[0] == product.ToString())
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }
    }
}
