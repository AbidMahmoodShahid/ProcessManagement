using Microsoft.VisualStudio.TestTools.UnitTesting;
using Process.Editor.Elements;
using Process.Editor.Services;
using Process.Editor.ViewModels;
using System;
using System.Collections.ObjectModel;

namespace ProcessManagement.Tests.Process.Editor.Services
{
    [TestClass]
    public class DeepCloneServiceTests
    {
        #region Exceptions

        [TestMethod]
        public void DeepCloneService_clonableCollectionContainer_ArgumentNullException()
        {
            ProcessGroupModel clonableCollectionContainer = null;
            Assert.ThrowsException<ArgumentNullException>(() => DeepCloneService.DeepClone(clonableCollectionContainer, "id"), "ArgumentNullException for null clonableCollectionContainer not thrown");
        }

        [TestMethod]
        public void DeepCloneService_ItemCollection_ArgumentNullException()
        {
            ProcessGroupModel clonableCollectionContainer = new ProcessGroupModel();
            clonableCollectionContainer.ItemCollection = null;
            Assert.ThrowsException<ArgumentNullException>(() => DeepCloneService.DeepClone(clonableCollectionContainer, "id"), "ArgumentNullException for null ItemCollection not thrown");
        }

        [TestMethod]
        public void DeepCloneService_EmptyItemCollection_ArgumentException()
        {
            ProcessGroupModel clonableCollectionContainer = new ProcessGroupModel();
            clonableCollectionContainer.ClonedItem = new ProcessPointA("PP2", "PP_2", 2);
            Assert.ThrowsException<ArgumentException>(() => DeepCloneService.DeepClone(clonableCollectionContainer, "id"), "ArgumentException for empty ItemCollection not thrown");
        }

        [TestMethod]
        public void DeepCloneService_ClonedItem_ArgumentNullException()
        {
            ProcessGroupModel clonableCollectionContainer = new ProcessGroupModel();
            clonableCollectionContainer.ItemCollection.Add(new ProcessPointA("PP1", "PP_1", 1));
            clonableCollectionContainer.ClonedItem = null;
            Assert.ThrowsException<ArgumentNullException>(() => DeepCloneService.DeepClone(clonableCollectionContainer, "id"), "ArgumentNullException for null ClonedItem not thrown");
        }

        [TestMethod]
        public void DeepCloneService_newId_ArgumentNullException()
        {
            ProcessGroupModel clonableCollectionContainer = new ProcessGroupModel();
            clonableCollectionContainer.ItemCollection.Add(new ProcessPointA("PP1", "PP_1", 1));
            clonableCollectionContainer.ClonedItem = new ProcessPointA("PP2", "PP_2", 2);
            Assert.ThrowsException<ArgumentNullException>(() => DeepCloneService.DeepClone(clonableCollectionContainer, null), "ArgumentNullException for null newId not thrown");
        }

        #endregion

        #region DeepCloneService Basic Tests

        [TestMethod]
        public void DeepCloneService_ProcessPointTest()
        {
            //Arrange
            ProcessGroupModel expectedclonableCollectionContainer = new ProcessGroupModel();
            expectedclonableCollectionContainer.ItemCollection = new ObservableCollection<ProcessPoint>()
            {
                new ProcessPointA("Name_1", "Id_1", 1),
                new ProcessPointB("Name_2", "Id_2", 2),
                new ProcessPointC("Name_3", "Id_3", 3),
                new ProcessPointD("Name_4", "Id_4_Cloned", 4)
            };
            int expectedItemCollectionCount = expectedclonableCollectionContainer.ItemCollection.Count;
            string expectedId = expectedclonableCollectionContainer.ItemCollection[expectedItemCollectionCount - 1].Id;

            //Act
            ProcessGroupModel actualclonableCollectionContainer = new ProcessGroupModel();
            actualclonableCollectionContainer.ItemCollection = new ObservableCollection<ProcessPoint>()
            {
                new ProcessPointA("Name_1", "Id_1", 1),
                new ProcessPointB("Name_2", "Id_2", 2),
                new ProcessPointC("Name_3", "Id_3", 3)
            };
            actualclonableCollectionContainer.ClonedItem = new ProcessPointD("Name_4", "Id_4", 4);
            DeepCloneService.DeepClone(actualclonableCollectionContainer, "Id_4_Cloned");

            int actualItemCollectionCount = actualclonableCollectionContainer.ItemCollection.Count;
            string actualId = actualclonableCollectionContainer.ItemCollection[actualItemCollectionCount - 1].Id;

            //Assert
            Assert.AreEqual(expectedItemCollectionCount, actualItemCollectionCount, "DeepCloneService could not add the cloned ProcessPoint");
            Assert.AreEqual(expectedId, actualId, "DeepCloneService could not edit the Id of the cloned ProcessPoint");
        }

        [TestMethod]
        public void DeepCloneService_ProcessGroupTest()
        {
            //Arrange
            ProcessGroupModel expecteditem1 = new ProcessGroupModel("Name_1", "Id_1", 1);
            expecteditem1.ItemCollection = new ObservableCollection<ProcessPoint>()
            {
                new ProcessPointA("Name_1", "Id_1", 1),
                new ProcessPointB("Name_2", "Id_2", 2),
                new ProcessPointC("Name_3", "Id_3", 3)
            };
            ProcessGroupModel expectedClonedItem = new ProcessGroupModel("Name_2", "Id_2_Cloned", 2);
            expectedClonedItem.ItemCollection = new ObservableCollection<ProcessPoint>()
            {
                new ProcessPointA("Name_1", "Id_1", 1),
                new ProcessPointB("Name_2", "Id_2", 2)
            };

            ProcessModel expectedclonableCollectionContainer = new ProcessModel();
            expectedclonableCollectionContainer.ItemCollection = new ObservableCollection<ProcessGroupModel>()
            {
                expecteditem1,
                expectedClonedItem
            };
            int expectedGroupCollectionCount = expectedclonableCollectionContainer.ItemCollection.Count;
            string expectedId = expectedclonableCollectionContainer.ItemCollection[expectedGroupCollectionCount - 1].Id;
            int expectedPointCollectionCount = expectedclonableCollectionContainer.ItemCollection[expectedGroupCollectionCount - 1].ItemCollection.Count;

            //Act
            ProcessGroupModel actualitem1 = new ProcessGroupModel("Name_1", "Id_1", 1);
            actualitem1.ItemCollection = new ObservableCollection<ProcessPoint>()
            {
                new ProcessPointA("Name_1", "Id_1", 1),
                new ProcessPointB("Name_2", "Id_2", 2),
                new ProcessPointC("Name_3", "Id_3", 3)
            };
            ProcessGroupModel itemToClone = new ProcessGroupModel("Name_2", "Id_2", 2);
            itemToClone.ItemCollection = new ObservableCollection<ProcessPoint>()
            {
                new ProcessPointA("Name_1", "Id_1", 1),
                new ProcessPointB("Name_2", "Id_2", 2)
            };

            ProcessModel actualclonableCollectionContainer = new ProcessModel();
            actualclonableCollectionContainer.ItemCollection = new ObservableCollection<ProcessGroupModel>()
            {
                actualitem1
            };
            actualclonableCollectionContainer.ClonedItem = itemToClone;
            DeepCloneService.DeepClone(actualclonableCollectionContainer, "Id_2_Cloned");

            int actualItemCollectionCount = actualclonableCollectionContainer.ItemCollection.Count;
            string actualId = actualclonableCollectionContainer.ItemCollection[actualItemCollectionCount - 1].Id;
            int actualPointCollectionCount = actualclonableCollectionContainer.ItemCollection[actualItemCollectionCount - 1].ItemCollection.Count;

            //Assert
            Assert.AreEqual(expectedGroupCollectionCount, actualItemCollectionCount, "DeepCloneService could not add the cloned ProcessGroup");
            Assert.AreEqual(expectedId, actualId, "DeepCloneService could not edit the Id of the cloned ProcessGroup");
            Assert.AreEqual(expectedPointCollectionCount, actualPointCollectionCount, "DeepCloneService could not clone the ProcessPoint Collection");
        }

        [TestMethod]
        public void DeepCloneService_ProcessTest()
        {
            //Arrange
            ProcessGroupModel GroupModel_1 = new ProcessGroupModel("Name_1", "Id_1", 1);
            GroupModel_1.ItemCollection = new ObservableCollection<ProcessPoint>()
            {
                new ProcessPointA("Name_1", "Id_1", 1),
                new ProcessPointB("Name_2", "Id_2", 2),
                new ProcessPointC("Name_3", "Id_3", 3)
            };
            ProcessGroupModel GroupModel_2 = new ProcessGroupModel("Name_1", "Id_1", 1);
            GroupModel_2.ItemCollection = new ObservableCollection<ProcessPoint>()
            {
                new ProcessPointA("Name_1", "Id_1", 1),
                new ProcessPointB("Name_2", "Id_2", 2),
                new ProcessPointC("Name_3", "Id_3", 3)
            };

            ProcessModel ProcessModel_1 = new ProcessModel("P1", "P1_Id", 0);
            ProcessModel_1.ItemCollection = new ObservableCollection<ProcessGroupModel>()
            {
                GroupModel_1
            };
            ProcessModel expectedClonedProcessModel = new ProcessModel("P2", "P2_Id_Cloned", 0);
            expectedClonedProcessModel.ItemCollection = new ObservableCollection<ProcessGroupModel>()
            {
                GroupModel_1,
                GroupModel_2
            };

            ProcessEditorViewModel expectedProcessEditorViewModel = new ProcessEditorViewModel(false);
            expectedProcessEditorViewModel.ItemCollection = new ObservableCollection<ProcessModel>()
            {
                ProcessModel_1,
                expectedClonedProcessModel
            };

            int expectedProcessCollectionCount = expectedProcessEditorViewModel.ItemCollection.Count;
            string expectedId = expectedProcessEditorViewModel.ItemCollection[expectedProcessCollectionCount - 1].Id;
            int expectedGroupCollectionCount = expectedProcessEditorViewModel.ItemCollection[expectedProcessCollectionCount - 1].ItemCollection.Count;
            int expectedPointCollectionCount = expectedProcessEditorViewModel.ItemCollection[expectedProcessCollectionCount - 1].ItemCollection[expectedGroupCollectionCount - 1].ItemCollection.Count;


            //Act
            ProcessGroupModel GM_1 = new ProcessGroupModel("Name_1", "Id_1", 1);
            GM_1.ItemCollection = new ObservableCollection<ProcessPoint>()
            {
                new ProcessPointA("Name_1", "Id_1", 1),
                new ProcessPointB("Name_2", "Id_2", 2),
                new ProcessPointC("Name_3", "Id_3", 3)
            };
            ProcessGroupModel GM_2 = new ProcessGroupModel("Name_1", "Id_1", 1);
            GM_2.ItemCollection = new ObservableCollection<ProcessPoint>()
            {
                new ProcessPointA("Name_1", "Id_1", 1),
                new ProcessPointB("Name_2", "Id_2", 2),
                new ProcessPointC("Name_3", "Id_3", 3)
            };

            ProcessModel PM_1 = new ProcessModel("P1", "P1_Id", 0);
            PM_1.ItemCollection = new ObservableCollection<ProcessGroupModel>()
            {
                GroupModel_1
            };
            ProcessModel actualProcessModelToClone = new ProcessModel("P2", "P2_Id", 0);
            actualProcessModelToClone.ItemCollection = new ObservableCollection<ProcessGroupModel>()
            {
                GM_1,
                GM_2
            };

            ProcessEditorViewModel actualProcessEditorViewModel = new ProcessEditorViewModel(false);
            actualProcessEditorViewModel.ItemCollection = new ObservableCollection<ProcessModel>()
            {
                PM_1
            };
            actualProcessEditorViewModel.ClonedItem = actualProcessModelToClone;
            DeepCloneService.DeepClone(actualProcessEditorViewModel, "P2_Id_Cloned");

            int actualProcessCollectionCount = actualProcessEditorViewModel.ItemCollection.Count;
            string actualId = actualProcessEditorViewModel.ItemCollection[actualProcessCollectionCount - 1].Id;
            int actualGroupCollectionCount = actualProcessEditorViewModel.ItemCollection[actualProcessCollectionCount - 1].ItemCollection.Count;
            int actualPointCollectionCount = actualProcessEditorViewModel.ItemCollection[actualProcessCollectionCount - 1].ItemCollection[actualGroupCollectionCount - 1].ItemCollection.Count;


            //Assert
            Assert.AreEqual(expectedProcessCollectionCount, actualProcessCollectionCount, "DeepCloneService could not add the cloned Process.");
            Assert.AreEqual(expectedId, actualId, "DeepCloneService could not edit the Id of the cloned Process");
            Assert.AreEqual(expectedGroupCollectionCount, actualGroupCollectionCount, "DeepCloneService could not clone the ProcessGroup Collection.");
            Assert.AreEqual(expectedPointCollectionCount, actualPointCollectionCount, "DeepCloneService could not clone the ProcessPoint Collection.");
        }

        #endregion
    }
}
