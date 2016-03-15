using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TC.DataAccess
{
    public class Serializer
    {
        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public static void SerializeObject<T>(T serializableObject, string fileName) where T : new()
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlSerializer writer = new XmlSerializer(serializableObject.GetType());
                StreamWriter file = new StreamWriter(fileName);
                writer.Serialize(file, serializableObject);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at serialize: {0}", ex.Message), ex);
            }
        }


        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeSerializeObject<T>(string fileName) where T : new()
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = new T();

            if (!File.Exists(fileName))
                Serializer.SerializeObject<T>(objectOut, fileName);

            try
            {
                string attributeXml = string.Empty;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at deserialize the object: {0}", ex.Message), ex);
            }

            return objectOut;
        }
    }
}
