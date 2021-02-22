using Microsoft.VisualStudio.TestTools.UnitTesting;
using Process.Editor.Elements;
using Process.Editor.Services;
using System;
using System.Collections.ObjectModel;

namespace ProcessManagement.Tests.Process.Editor.Services
{
    [TestClass]
    public class SortUpDownServiceTests
    {
        #region Exceptions

        [TestMethod]
        public void GetterServiceTests_ItemUp_ArgumentNullException_SortableCollectionContainer()
        {
            ProcessModel processModel = null;

            Assert.ThrowsException<ArgumentNullException>(() => SortUpDownService.ItemUp(processModel), "ArgumentNullException for null ItemCollection not thrown");
        }

        [TestMethod]
        public void GetterServiceTests_ItemUp_ArgumentNullException_SelectedItem()
        {
            ProcessModel processModel = new ProcessModel("P01", "P_01", 1);
            processModel.SelectedItem = null;

            Assert.ThrowsException<ArgumentNullException>(() => SortUpDownService.ItemUp(processModel), "ArgumentNullException for null SelectedItem not thrown");
        }

        [TestMethod]
        public void GetterServiceTests_ItemUp_ArgumentNullException_ItemCollection()
        {
            ProcessModel processModel = new ProcessModel("P01", "P_01", 1);
            processModel.ItemCollection = null;
            processModel.SelectedItem = new ProcessGroupModel();

            Assert.ThrowsException<ArgumentNullException>(() => SortUpDownService.ItemUp(processModel), "ArgumentNullException for null ItemCollection not thrown");
        }

        [TestMethod]
        public void GetterServiceTests_ItemUp_ArgumentException_ItemCollectionEmpty()
        {
            ProcessModel processModel = new ProcessModel("P01", "P_01", 1);
            processModel.SelectedItem = new ProcessGroupModel();

            Assert.ThrowsException<ArgumentException>(() => SortUpDownService.ItemUp(processModel), "ArgumentException for empty ItemCollection not thrown");
        }

        [TestMethod]
        public void GetterServiceTests_ItemUp_ArgumentException_FirstItem()
        {
            ProcessModel processModel = new ProcessModel("P01", "P_01", 1);
            processModel.ItemCollection = new ObservableCollection<ProcessGroupModel>()
            {
                new ProcessGroupModel("G1", "G_1", 1),
                new ProcessGroupModel("G2", "G_2", 2),
                new ProcessGroupModel("G3", "G_3", 3)
            };
            processModel.SelectedItem = processModel.ItemCollection[0];

            Assert.ThrowsException<ArgumentException>(() => SortUpDownService.ItemUp(processModel), "ArgumentException for first Item in ItemCollection not thrown");
        }

        [TestMethod]
        public void GetterServiceTests_ItemDown_ArgumentNullException_SortableCollectionContainer()
        {
            ProcessModel processModel = null;

            Assert.ThrowsException<ArgumentNullException>(() => SortUpDownService.ItemDown(processModel), "ArgumentNullException for null ItemCollection not thrown");
        }

        [TestMethod]
        public void GetterServiceTests_ItemDown_ArgumentNullException_SelectedItem()
        {
            ProcessModel processModel = new ProcessModel("P01", "P_01", 1);
            processModel.SelectedItem = null;

            Assert.ThrowsException<ArgumentNullException>(() => SortUpDownService.ItemDown(processModel), "ArgumentNullException for null SelectedItem not thrown");
        }

        [TestMethod]
        public void GetterServiceTests_ItemDown_ArgumentNullException_ItemCollection()
        {
            ProcessModel processModel = new ProcessModel("P01", "P_01", 1);
            processModel.ItemCollection = null;
            processModel.SelectedItem = new ProcessGroupModel();

            Assert.ThrowsException<ArgumentNullException>(() => SortUpDownService.ItemDown(processModel), "ArgumentNullException for null ItemCollection not thrown");
        }

        [TestMethod]
        public void GetterServiceTests_ItemDown_ArgumentException_ItemCollectionEmpty()
        {
            ProcessModel processModel = new ProcessModel("P01", "P_01", 1);
            processModel.SelectedItem = new ProcessGroupModel();

            Assert.ThrowsException<ArgumentException>(() => SortUpDownService.ItemDown(processModel), "ArgumentException for empty ItemCollection not thrown");
        }

        [TestMethod]
        public void GetterServiceTests_ItemDown_ArgumentException_LastItem()
        {
            ProcessModel processModel = new ProcessModel("P01", "P_01", 1);
            processModel.ItemCollection = new ObservableCollection<ProcessGroupModel>()
            {
                new ProcessGroupModel("G1", "G_1", 1),
                new ProcessGroupModel("G2", "G_2", 2),
                new ProcessGroupModel("G3", "G_3", 3)
            };
            processModel.SelectedItem = processModel.ItemCollection[processModel.ItemCollection.Count - 1];

            Assert.ThrowsException<ArgumentException>(() => SortUpDownService.ItemDown(processModel), "ArgumentException for last Item in ItemCollection not thrown");
        }


        #endregion

        #region SortUpDownServiceTests Basic Tests

        [TestMethod]
        public void SortUpDownServiceTests_ItemUp()
        {
            //Arrange
            ObservableCollection<ProcessGroupModel> expectedItemCollection = new ObservableCollection<ProcessGroupModel>()
            {
                new ProcessGroupModel("PG1","PG_1",1),
                new ProcessGroupModel("PG3","PG_3",2),
                new ProcessGroupModel("PG2","PG_2",3),
                new ProcessGroupModel("PG4","PG_4",4),
                new ProcessGroupModel("PG5","PG_5",5),
            };

            //Act
            ProcessModel actualProcessModel = new ProcessModel("P1", "P_1", 1);
            actualProcessModel.ItemCollection = new ObservableCollection<ProcessGroupModel>()
            {
                new ProcessGroupModel("PG1","PG_1",1),
                new ProcessGroupModel("PG2","PG_2",2),
                new ProcessGroupModel("PG3","PG_3",3),
                new ProcessGroupModel("PG4","PG_4",4),
                new ProcessGroupModel("PG5","PG_5",5),
            };
            actualProcessModel.SelectedItem = actualProcessModel.ItemCollection[2];
            SortUpDownService.ItemUp(actualProcessModel);
            ObservableCollection<ProcessGroupModel> actualItemCollection = actualProcessModel.ItemCollection;

            //Assert
            int count = 0;
            foreach(ProcessGroupModel expectedprocessGroup in expectedItemCollection)
            {
                Assert.AreEqual(expectedprocessGroup.Name, actualItemCollection[count].Name, "The ItemUp Service did not function.");
                Assert.AreEqual(expectedprocessGroup.Id, actualItemCollection[count].Id, "The ItemUp Service did not function.");
                Assert.AreEqual(expectedprocessGroup.SortingNumber, actualItemCollection[count].SortingNumber, "The ItemUp Service did not function.");
                count++;
            }
        }

        [TestMethod]
        public void SortUpDownServiceTests_ItemDown()
        {
            //Arrange
            ObservableCollection<ProcessGroupModel> expectedItemCollection = new ObservableCollection<ProcessGroupModel>()
            {
                new ProcessGroupModel("PG1","PG_1",1),
                new ProcessGroupModel("PG3","PG_3",2),
                new ProcessGroupModel("PG2","PG_2",3),
                new ProcessGroupModel("PG4","PG_4",4),
                new ProcessGroupModel("PG5","PG_5",5),
            };

            //Act
            ProcessModel actualProcessModel = new ProcessModel("P1", "P_1", 1);
            actualProcessModel.ItemCollection = new ObservableCollection<ProcessGroupModel>()
            {
                new ProcessGroupModel("PG1","PG_1",1),
                new ProcessGroupModel("PG2","PG_2",2),
                new ProcessGroupModel("PG3","PG_3",3),
                new ProcessGroupModel("PG4","PG_4",4),
                new ProcessGroupModel("PG5","PG_5",5),
            };
            actualProcessModel.SelectedItem = actualProcessModel.ItemCollection[1];
            SortUpDownService.ItemDown(actualProcessModel);
            ObservableCollection<ProcessGroupModel> actualItemCollection = actualProcessModel.ItemCollection;

            //Assert
            int count = 0;
            foreach(ProcessGroupModel expectedprocessGroup in expectedItemCollection)
            {
                Assert.AreEqual(expectedprocessGroup.Name, actualItemCollection[count].Name, "The ItemDown Service did not function.");
                Assert.AreEqual(expectedprocessGroup.Id, actualItemCollection[count].Id, "The ItemDown Service did not function.");
                Assert.AreEqual(expectedprocessGroup.SortingNumber, actualItemCollection[count].SortingNumber, "The ItemDown Service did not function.");
                count++;
            }
        }

        #endregion
    }
}
