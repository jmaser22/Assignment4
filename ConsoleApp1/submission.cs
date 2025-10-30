using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "https://jmaser22.github.io/Assignment4/ConsoleApp1/Hotels.xml";
        public static string xmlErrorURL = "https://jmaser22.github.io/Assignment4/ConsoleApp1/HotelsErrors.xml";
        public static string xsdURL = "https://jmaser22.github.io/Assignment4/ConsoleApp1/Hotels.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            Boolean isValid = false;
            String errorMessage = "";

            XmlSchemaSet sc = new XmlSchemaSet();
            sc.Add(null, xsdUrl);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;

            settings.ValidationEventHandler += (sender, e) => {
                isValid = false;
                errorMessage = e.Message;
                Console.WriteLine($"{e.Severity}: {e.Message}");
            };

            XmlReader reader = XmlReader.Create(xmlUrl, settings);
            try
            {
                while (reader.Read())
                {
                    Console.WriteLine("The XML file validation has completed");
                }


            }
            catch (XmlException ex)
            {
                Console.WriteLine($"File is not well-formed: {ex.Message}");
            }
            if (isValid)
            {
               Console.WriteLine("No Error");
            }
            else
            {
            Console.WriteLine("error");
            }
            //return "No Error" if XML is valid. Otherwise, return the desired exception message.
            return isValid ? "No Error" : errorMessage;
            
        }

        public static string Xml2Json(string xmlUrl)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl); // Load the XML file or URL

            string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);

            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            return jsonText;

        }
    }

}
