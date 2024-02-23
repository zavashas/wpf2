using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfApp7
{
    public class DeserializeSerialize
    {
        public static void Serialize<T>(T list)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter("Note.xml"))
            {
                serializer.Serialize(writer, list);
            }
        }

        public static T Deserialize<T>()
        {
            if (File.Exists("Note.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StreamReader reader = new StreamReader("Note.xml"))
                {
                    T notes = (T)serializer.Deserialize(reader);
                    return notes;
                }
            }
            else
            {
                return default(T);
            }
        }
    }
}
