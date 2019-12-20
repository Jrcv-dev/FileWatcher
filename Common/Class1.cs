using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Common
{
    public class Common
    {
        public const string PATH = @"C:\Users\Curso\Downloads\";
        public static void SerializeList<T>(T obj, string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            //Create a FileStream object connected to the target file
            FileStream fileStream = new FileStream(PATH + fileName, FileMode.Create);
            ser.Serialize(fileStream, obj);
            fileStream.Close();
        }

        public static T DeserializeList<T>(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            System.Threading.Thread.Sleep(500);
            doc.Load(PATH + fileName);
            T result;
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (TextReader tr = new StringReader(doc.OuterXml))
            {
                result = (T)ser.Deserialize(tr);
            }
            return result;
        }
        public static void Delete(string fileName)
        {
            File.Delete(PATH + fileName);
        }
    }
}
