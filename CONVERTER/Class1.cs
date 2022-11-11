using Newtonsoft.Json;
using System;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace CONVERTER
{
    internal class Class1
    {
        int count = -1;
        public int window = 0;
        string innput = "";
        Saving_MESTO place = new Saving_MESTO();
        List<Saving_MESTO> list = new List<Saving_MESTO>();
        Saving_MESTO mESTO;
        string piece1 = "";
        public void Reading(string input)
        {
            innput = input;
            Console.Clear();
            int index = input.IndexOf(".") + 1;
            string piece = input.Substring(index);
            Console.WriteLine("Содерживмое важего файла " + piece);
            if (piece == "txt")
            {
                string myText = File.ReadAllText(input);
                string[] pieces = myText.Split('\n');
                foreach (var word in pieces)
                {
                    count++;
                    if (count == 0)
                    {
                        place.Name = word;
                    }
                    else if (count == 1)
                    {
                        place.Width = Convert.ToInt32(word);
                    }
                    else if (count == 2)
                    {
                        place.Height = Convert.ToInt32(word);
                        count = 0;
                    }
                }
                //List<Saving_MESTO> list = new List<Saving_MESTO>();
                list.Add(place);
                Console.WriteLine(place.Name + "\n" + place.Width + "\n" + place.Height);
                piece1 = piece;
                Console.WriteLine("Нажмите F1, чтобы продолжить.");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.F1)
                {
                    window = 1;
                }
            }
            else if (piece == "json")
            {
                string text = File.ReadAllText(input);
                List<Saving_MESTO> result = JsonConvert.DeserializeObject<List<Saving_MESTO>>(text);
                place.Name = result[0].Name;
                place.Width = result[0].Width;
                place.Height = result[0].Height;
                Console.WriteLine(place.Name + "\n" + place.Width + "\n" + place.Height);
                list.Add(place);
                piece1 = piece;
                Console.WriteLine("Нажмите F1, чтобы продолжить.");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.F1)
                {
                    window = 1;
                }
            }
            else if (piece == "xml")
            {
                XmlSerializer xml = new XmlSerializer(typeof(Saving_MESTO));
                using (FileStream fs = new FileStream(input, FileMode.Open))
                {
                    mESTO = (Saving_MESTO)xml.Deserialize(fs);
                }
                Console.WriteLine(mESTO.Name + "\n" + mESTO.Width + "\n" + mESTO.Height);
                piece1 = piece;
                Console.WriteLine("Нажмите F1, чтобы продолжить.");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.F1)
                {
                    window = 1;
                }
            }
            
        }

        public void Saving(string output)
        {
            Console.Clear();
            Console.WriteLine("Вы хотите записать файл " + innput + " в файл " + output + " ? Если да то нажмите F1, чтобы подтвердить или нжмите ESC для выхода.");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.F1)
            {
                int indexo = output.IndexOf(".") + 1;
                string pieceo = output.Substring(indexo);
                if (pieceo == "json")
                {
                    if (piece1 == "txt")
                    {
                        string json = JsonConvert.SerializeObject(place);
                        File.WriteAllText(output, ("[" + json + "]"));
                    }
                    else if (piece1 == "xml")
                    {
                        string json = JsonConvert.SerializeObject(mESTO);
                        File.WriteAllText(output, ("[" + json + "]"));
                    }
                }
                else if (pieceo == "xml")
                {
                    XmlSerializer xml = new XmlSerializer(typeof(Saving_MESTO));
                    using (FileStream fs = new FileStream(output, FileMode.OpenOrCreate))
                    {
                        xml.Serialize(fs, place);
                    }
                }
                else if (pieceo == "txt")
                {
                    if (piece1 == "xml")
                    {
                        place.Name = mESTO.Name;
                        place.Width = mESTO.Width;
                        place.Height = mESTO.Height;
                        File.WriteAllText(output, (place.Name + "\n" + place.Width + "\n" + place.Height));
                    }
                    else if (piece1 == "json")
                    {
                        File.WriteAllText(output, (place.Name + "\n" + place.Width + "\n" + place.Height));
                    }
                }
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }
    }
}
