using Process.Editor.Elements;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Process.Editor.Services
{
    public class ExportService
    {
        public static void Export<T>(T itemToExport, string filePath) where T : IExportableItem
        {
            if(itemToExport == null)
                throw new ArgumentNullException("itemToExport is null!");

            if(String.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("The Filename is null or empty!");

            using(TextWriter tw = new StreamWriter(filePath))
            {
                Type[] extras = { typeof(ProcessPointA), typeof(ProcessPointB), typeof(ProcessPointC), typeof(ProcessPointD) };
                XmlSerializer serializer = new XmlSerializer(typeof(T), extras);
                serializer.Serialize(tw, itemToExport);
            }
        }
    }
}
