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
        public readonly static string stocksFile = @"C:\Users\holacons\Desktop\VU WorkSpace\MetalBandBakery.V1\MetalBandBakery.Infra\Repository\DB\Stocks.txt";
        public readonly static string stocksMasterFile = @"C:\Users\holacons\Desktop\VU WorkSpace\MetalBandBakery.V1\MetalBandBakery.Infra\Repository\DB\StocksMaster.txt";
        public readonly static string warehousesFile = @"C:\Users\holacons\Desktop\VU WorkSpace\MetalBandBakery.V1\MetalBandBakery.Infra\Repository\DB\Warehouses.txt";
        public readonly static string pricesFile = @"C:\Users\holacons\Desktop\VU WorkSpace\MetalBandBakery.V1\MetalBandBakery.Infra\Repository\DB\Prices.txt";
        public readonly static string namesFile = @"C:\Users\holacons\Desktop\VU WorkSpace\MetalBandBakery.V1\MetalBandBakery.Infra\Repository\DB\Names.txt";
        public readonly static string cartFile = @"C:\Users\holacons\Desktop\VU WorkSpace\MetalBandBakery.V1\MetalBandBakery.Infra\Repository\DB\Cart.txt";
        public readonly static string cartValueFile = @"C:\Users\holacons\Desktop\VU WorkSpace\MetalBandBakery.V1\MetalBandBakery.Infra\Repository\DB\CartValue.txt";
        public readonly static string materialsFile = @"C:\Users\holacons\Desktop\VU WorkSpace\MetalBandBakery.V1\MetalBandBakery.Infra\Repository\DB\Materials.txt";
        public readonly static string productMatsFile = @"C:\Users\holacons\Desktop\VU WorkSpace\MetalBandBakery.V1\MetalBandBakery.Infra\Repository\DB\ProductMats.txt";

        public static List<string> ReadTextFromFile(string filePath)
        {
            List<string> lines = new List<string>();
            using (StreamReader file = new StreamReader(filePath))
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
            bool find = false;
            foreach (var i in lines)
            {
                if (i.Split('=')[0] == product.ToString())
                {
                    find = true;
                    break;
                }
                    
                cont++;
            }

            if (!find)
                return -1;

            return cont;
        }

        public static int GetIndexOfText(string product, List<string> lines)
        {
            int cont = 0;
            bool find = false;
            foreach (var i in lines)
            {
                if (i.Split('=')[0] == product)
                {
                    find = true;
                    break;
                }

                cont++;
            }

            if (!find)
                return -1;

            return cont;
        }

        public static bool ExistsIdInFile(char product, string filePath)
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

        public static bool ExistsValueInFile(string product, string filePath)
        {
            bool exists = false;
            foreach (var i in DBService.ReadTextFromFile(filePath))
            {
                if (i.Split('=')[1] == product)
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }
    }
}
