using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;
using Newtonsoft.Json;

namespace RemoteData
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://localhost:100/api/data/xml")
            };
            request.Headers.Add("Accept", "text/xml");
            var response = client.SendAsync(request).Result;

            var data = response.Content.ReadAsStringAsync().Result;
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(data);
            if (string.IsNullOrWhiteSpace(xmlDocument.InnerText))
            {
                Console.WriteLine("Empty");
                Console.ReadKey();
            }
            var doc = XDocument.Parse(xmlDocument.InnerText);
            foreach (XElement node in doc.Elements())
            {
                Console.WriteLine("--");
                Console.WriteLine(node.XPathSelectElement("Name"));
                Console.WriteLine(node.XPathSelectElement("Age"));

            }
            Console.WriteLine(new string('*',50));
            request = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://localhost:100/api/data/json")
            };
            request.Headers.Add("Accept", "application/json");
            response = client.SendAsync(request).Result;
            var person = JsonConvert.DeserializeObject<Person>(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine(person.Data);

            Console.ReadKey();
        }
    }

    internal class Person

    {
    public string Data { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
