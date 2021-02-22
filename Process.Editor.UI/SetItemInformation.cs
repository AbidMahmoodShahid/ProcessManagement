using Process.Editor.Elements;
using Process.Editor.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Process.Editor.UI
{
    public class SetItemInformation : ISetItemInformation
    {
        public string SetNewID(List<string> usedIdList)
        {
            SetItemIdViewModel setItemIdViewModel = new SetItemIdViewModel(usedIdList, Visibility.Collapsed);

            SetItemInformationWindow setItemInformationWindow = new SetItemInformationWindow();
            setItemInformationWindow.DataContext = setItemIdViewModel;

            setItemInformationWindow.ShowDialog();

            if((bool)setItemInformationWindow.DialogResult)
            {
                if(string.IsNullOrWhiteSpace(setItemIdViewModel.ItemID))
                    throw new ArgumentException("ItemID is null or empty. SetNewID could not be implemented. ItemId= {0}", setItemIdViewModel.ItemID);

                return setItemIdViewModel.ItemID;
            }
            else
                return null;
        }

        public Dictionary<string, string> SetNewNameAndID(List<string> usedIdList)
        {
            SetItemIdViewModel setItemNameAndIdViewModel = new SetItemIdViewModel(usedIdList, Visibility.Visible);
            Dictionary<string, string> NameAndIdDictionary = new Dictionary<string, string>()
            {
                {"Name", "" },
                {"Id", "" }
            };

            SetItemInformationWindow setItemInformationWindow = new SetItemInformationWindow();
            setItemInformationWindow.DataContext = setItemNameAndIdViewModel;

            setItemInformationWindow.ShowDialog();

            if((bool)setItemInformationWindow.DialogResult)
            {
                if(string.IsNullOrWhiteSpace(setItemNameAndIdViewModel.ItemName))
                    throw new ArgumentException("ItemName is null or empty. SetNewNameAndID could not be implemented. ItemName= {0}", setItemNameAndIdViewModel.ItemName);

                if(string.IsNullOrWhiteSpace(setItemNameAndIdViewModel.ItemID))
                    throw new ArgumentException("ItemID is null or empty. SetNewNameAndID could not be implemented. ItemId= {0}", setItemNameAndIdViewModel.ItemID);

                NameAndIdDictionary["Name"] = setItemNameAndIdViewModel.ItemName;
                NameAndIdDictionary["Id"] = setItemNameAndIdViewModel.ItemID;

                return NameAndIdDictionary;
            }
            else
                return null;
        }

    }
}
