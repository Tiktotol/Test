using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("hello, this is my test task");
            Console.WriteLine(@"would you like me to download the data? Y\N");
            do
            {
                ConsoleKeyInfo cki = Console.ReadKey();
                if (cki.Key.ToString() == "Y" || cki.Key.ToString() == "y")
                {
                    Console.WriteLine();
                    WebRequest request = WebRequest.Create("https://tester.consimple.pro/");
                    WebResponse response = await request.GetResponseAsync();
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string result = reader.ReadToEnd();

                            CreateTable(JsonSerializer.Deserialize<ModelRoot>(result));
                            Console.WriteLine(@"Maybe you want to try again? Y\N");
                        }
                    }
                }
                else if (cki.Key.ToString() == "N" || cki.Key.ToString() == "n")
                {
                    Environment.Exit(0);
                    break;
                }
            } while (true);


        }
        private static void CreateTable(ModelRoot root)
        {
            var table = new Tables();

            table.SetHeaders("Product name", "Category name");

            for (int i = 0; i <= root.Products.Count - 1; i++)
            {
                foreach (var item in root.Categories)
                {
                    if (item.Id == root.Products[i].CategoryId)
                    {
                        table.AddRow(root.Products[i].Name, item.Name);
                    }
                }
            }
            Console.WriteLine(table.ToString());
        }
    }
}
