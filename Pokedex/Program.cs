using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Pokedex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite o número do pokemon: ");
            var pokeNumber = Console.ReadLine();
            // Create the web request  
            HttpWebRequest request = WebRequest.Create("http://pokeapi.co/api/v2/pokemon/" + pokeNumber) as HttpWebRequest;

            // Get response  
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Get the response stream  
                    var json = DeserializeFromStream(response.GetResponseStream());

                    Pokemon deserializedJson = JsonConvert.DeserializeObject<Pokemon>(json.ToString());

                    // Console application output  
                    Console.WriteLine("Nome: " + deserializedJson.name.ToUpperInvariant());

                    Console.ReadKey();
                }
            }
        }

        public static object DeserializeFromStream(Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize(jsonTextReader);
            }
        }
    }
}
