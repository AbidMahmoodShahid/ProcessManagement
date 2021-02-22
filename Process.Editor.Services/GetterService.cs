using Process.Editor.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Process.Editor.Services
{
    public class GetterService
    {
        public List<string> GetIDList<T>(ObservableCollection<T> itemCollection) where T : IClonableItem
        {
            if(itemCollection == null)
                throw new ArgumentNullException("itemCollection is null!");

            List<string> idList = new List<string>();

            if(itemCollection.Count < 1)
            {
                return idList;
            }
            else
            {
                foreach(T item in itemCollection)
                {
                    idList.Add(item.Id);
                }

                return idList;
            }
        }

        public Type[] GetInheritedClassesArray(Type baseType)
        {
            return Assembly.GetAssembly(baseType).GetTypes().Where(theType => theType.IsClass && !theType.IsAbstract && theType.IsSubclassOf(baseType)).ToArray();
        }

        public List<string> GetProcessPointTypeNameList(Type[] typeArray)
        {
            List<string> typeNameList = new List<string>();
            foreach(Type type in typeArray)
            {
                typeNameList.Add(type.Name);
            }
            return typeNameList;
        }
    }
}
