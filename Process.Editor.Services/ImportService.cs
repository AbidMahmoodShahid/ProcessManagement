using Microsoft.Win32;
using Process.Dialog.UI;
using Process.Dialog.ViewModels;
using Process.Editor.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Process.Editor.Services
{
    public class ImportService
    {
        public static void Import<T>(IImportableCollectionContainer<T> collectionContainer, string importedItemFilePath, Dictionary<string, string> newNameAndIdDictionary) where T : IImportableItem
        {
            if(collectionContainer == null)
                throw new ArgumentNullException("collectionContainer is null. Import can not be implemented!");

            if(string.IsNullOrWhiteSpace(importedItemFilePath))
                throw new ArgumentException("importedItemFilePath is null or empty. Import can not be implemented! ImportedItemFilePath: {0}", importedItemFilePath);

            if(newNameAndIdDictionary == null)
                throw new ArgumentNullException("nameAndIdDictionary is null. Import can not be implemented!");

            if(string.IsNullOrWhiteSpace(newNameAndIdDictionary["Name"]))
                throw new ArgumentException("New Name is null or empty. Import can not be implemented! Name: {0}", newNameAndIdDictionary["Name"]);

            if(string.IsNullOrWhiteSpace(newNameAndIdDictionary["Id"]))
                throw new ArgumentException("New Id is null or empty. Import can not be implemented! Id: {0}", newNameAndIdDictionary["Id"]);

            if(!(File.Exists(importedItemFilePath)))
                throw new ArgumentException("Imported item filepath does not exits. Import can not be implemented! ImportedItemFilePath: {0}", importedItemFilePath);

            Type[] extras = { typeof(ProcessPointA), typeof(ProcessPointB), typeof(ProcessPointC), typeof(ProcessPointD) };
            XmlSerializer deserializer = new XmlSerializer(typeof(T), extras);
            TextReader textReader = new StreamReader(importedItemFilePath);
            object importedObject = deserializer.Deserialize(textReader);
            textReader.Close();

            collectionContainer.ImportedItem = (T)importedObject;
            collectionContainer.ImportedItem.Name = newNameAndIdDictionary["Name"];
            collectionContainer.ImportedItem.Id = newNameAndIdDictionary["Id"];
            collectionContainer.ItemCollection.Add(collectionContainer.ImportedItem);
        }
    }
}
