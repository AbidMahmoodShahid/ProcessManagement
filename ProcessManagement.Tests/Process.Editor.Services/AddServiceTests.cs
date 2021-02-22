using Microsoft.VisualStudio.TestTools.UnitTesting;
using Process.Editor.Elements;
using Process.Editor.Services;
using System;
using System.Collections.ObjectModel;

namespace ProcessManagement.Tests.Process.Editor.Services
{
    [TestClass]
    public class AddServiceTests
    {
        #region Exceptions

        [TestMethod]
        public void AddServiceTests_ItemCollection_ArgumentNullException()
        {
            ObservableCollection<ProcessModel> itemCollection = null;
            ProcessModel newProcessModel = new ProcessModel();
            AddItemService addItemService = new AddItemService();
            Assert.ThrowsException<ArgumentNullException>(() => addItemService.AddService(itemCollection, newProcessModel), "ArgumentNullException for null ItemCollection not thrown");
        }

        [TestMethod]
        public void AddServiceTests_newItem_ArgumentNullException()
        {
            ObservableCollection<ProcessModel> itemCollection = new ObservableCollection<ProcessModel>();
            ProcessModel newProcessModel = null;
            AddItemService addItemService = new AddItemService();
            Assert.ThrowsException<ArgumentNullException>(() => addItemService.AddService(itemCollection, newProcessModel), "ArgumentNullException for null newProcessModel not thrown");
        }

        #endregion

        #region AddItemService Basic Tests

        [TestMethod]
        public void AddProcessServiceTest()
        {
            //Arrange
            ObservableCollection<ProcessModel> expectedCollection = new ObservableCollection<ProcessModel>
            {
                new ProcessModel(),
                new ProcessModel(),
                new ProcessModel()
            };
            int expectedCollectionCount = expectedCollection.Count;

            //Act
            ObservableCollection<ProcessModel> actualCollection = new ObservableCollection<ProcessModel>
            {
                new ProcessModel(),
                new ProcessModel()
            };
            ProcessModel newProcessModel = new ProcessModel();

            AddItemService addItemService = new AddItemService();
            addItemService.AddService(actualCollection, newProcessModel);
            int actualCollectionCount = actualCollection.Count;

            //Assert
            Assert.AreEqual(expectedCollectionCount, actualCollectionCount, "AddItemService could not add a new ProcessModel");
        }

        [TestMethod]
        public void AddProcessGroupServiceTest()
        {
            //Arrange
            ObservableCollection<ProcessGroupModel> expectedCollection = new ObservableCollection<ProcessGroupModel>
            {
                new ProcessGroupModel(),
                new ProcessGroupModel(),
                new ProcessGroupModel()
            };
            int expectedCollectionCount = expectedCollection.Count;

            //Act
            ObservableCollection<ProcessGroupModel> actualCollection = new ObservableCollection<ProcessGroupModel>
            {
                new ProcessGroupModel(),
                new ProcessGroupModel()
            };
            ProcessGroupModel newProcessGroupModel = new ProcessGroupModel();

            AddItemService addItemService = new AddItemService();
            addItemService.AddService(actualCollection, newProcessGroupModel);
            int actualCollectionCount = actualCollection.Count;

            //Assert
            Assert.AreEqual(expectedCollectionCount, actualCollectionCount, "AddItemService could not add a new ProcessGroup");
        }

        [TestMethod]
        public void AddProcessPointServiceTest()
        {
            //Arrange
            ObservableCollection<ProcessPoint> expectedCollection = new ObservableCollection<ProcessPoint>
            {
                new ProcessPointA(),
                new ProcessPointB(),
                new ProcessPointC(),
                new ProcessPointD()
            };
            int expectedCollectionCount = expectedCollection.Count;

            //Act
            ObservableCollection<ProcessPoint> actualCollection = new ObservableCollection<ProcessPoint>
            {
                new ProcessPointA(),
                new ProcessPointB(),
                new ProcessPointC()
            };
            ProcessPointD newProcessPoint = new ProcessPointD();

            AddItemService addItemService = new AddItemService();
            addItemService.AddService(actualCollection, newProcessPoint);
            int actualCollectionCount = actualCollection.Count;

            //Assert
            Assert.AreEqual(expectedCollectionCount, actualCollectionCount, "AddItemService could not add a new ProcessPoint");
        }


        [TestMethod]
        public void AddProcessGroupSortingTest_01()
        {
            //Arrange
            ObservableCollection<ProcessGroupModel> expectedCollection = new ObservableCollection<ProcessGroupModel>
            {
                new ProcessGroupModel("G1", "G_1", 1),
                new ProcessGroupModel("G2", "G_2", 2),
                new ProcessGroupModel("G3", "G_3", 3)
            };

            //Act
            ObservableCollection<ProcessGroupModel> actualCollection = new ObservableCollection<ProcessGroupModel>
            {
                new ProcessGroupModel("G1", "G_2", 2),
                new ProcessGroupModel("G3", "G_3", 1)
            };
            ProcessGroupModel newProcessGroupModel = new ProcessGroupModel("G2", "G_2", 2);

            AddItemService addItemService = new AddItemService();
            addItemService.AddService(actualCollection, newProcessGroupModel);

            for(int listCount = 0; listCount < expectedCollection.Count; listCount++)
            {
                int expectedSortingnumber = expectedCollection[listCount].SortingNumber;
                int actualSortingNumber = actualCollection[listCount].SortingNumber;
                //Assert
                Assert.AreEqual(expectedSortingnumber, actualSortingNumber, "AddItemService could not sort a new ProcessGroup");
            }
        }

        [TestMethod]
        public void AddProcessGroupSortingTest_02()
        {
            //Arrange
            ObservableCollection<ProcessGroupModel> expectedCollection = new ObservableCollection<ProcessGroupModel>
            {
                new ProcessGroupModel("G1", "G_1", 1),
                new ProcessGroupModel("G2", "G_2", 2),
                new ProcessGroupModel("G3", "G_3", 3),
                new ProcessGroupModel("G4", "G_4", 4),
                new ProcessGroupModel("G5", "G_5", 5),
                new ProcessGroupModel("G6", "G_6", 6),
                new ProcessGroupModel("G7", "G_7", 7)
            };

            //Act
            ObservableCollection<ProcessGroupModel> actualCollection = new ObservableCollection<ProcessGroupModel>
            {
                new ProcessGroupModel("G1", "G_1", 1),
                new ProcessGroupModel("G2", "G_2", 2),
                new ProcessGroupModel("G3", "G_3", 3),
                new ProcessGroupModel("G2", "G_2", 4),
                new ProcessGroupModel("G3", "G_3", 5),
                new ProcessGroupModel("G2", "G_2", 6)
            };
            ProcessGroupModel newProcessGroupModel = new ProcessGroupModel("Gnew", "G_new", 3);

            AddItemService addItemService = new AddItemService();
            addItemService.AddService(actualCollection, newProcessGroupModel);

            for(int listCount = 0; listCount < expectedCollection.Count; listCount++)
            {
                int expectedSortingnumber = expectedCollection[listCount].SortingNumber;
                int actualSortingNumber = actualCollection[listCount].SortingNumber;
                //Assert
                Assert.AreEqual(expectedSortingnumber, actualSortingNumber, "AddItemService could not sort a new ProcessGroup");
            }
        }

        [TestMethod]
        public void AddProcessGroupSortingTest_03()
        {
            //Arrange
            ObservableCollection<ProcessGroupModel> expectedCollection = new ObservableCollection<ProcessGroupModel>
            {
                new ProcessGroupModel("G1", "G_1", 1),
                new ProcessGroupModel("G2", "G_2", 2),
                new ProcessGroupModel("G3", "G_3", 3),
                new ProcessGroupModel("G4", "G_4", 4),
                new ProcessGroupModel("G5", "G_5", 5),
                new ProcessGroupModel("G6", "G_6", 6),
                new ProcessGroupModel("G7", "G_7", 7)
            };

            //Act
            ObservableCollection<ProcessGroupModel> actualCollection = new ObservableCollection<ProcessGroupModel>
            {
                new ProcessGroupModel("G1", "G_1", 1),
                new ProcessGroupModel("G2", "G_2", 2),
                new ProcessGroupModel("G3", "G_3", 2),
                new ProcessGroupModel("G2", "G_2", 2),
                new ProcessGroupModel("G3", "G_3", 5),
                new ProcessGroupModel("G2", "G_2", 6)
            };
            ProcessGroupModel newProcessGroupModel = new ProcessGroupModel("Gnew", "G_new", 0);

            AddItemService addItemService = new AddItemService();
            addItemService.AddService(actualCollection, newProcessGroupModel);

            for(int listCount = 0; listCount < expectedCollection.Count; listCount++)
            {
                int expectedSortingnumber = expectedCollection[listCount].SortingNumber;
                int actualSortingNumber = actualCollection[listCount].SortingNumber;
                //Assert
                Assert.AreEqual(expectedSortingnumber, actualSortingNumber, "AddItemService could not sort a new ProcessGroup");
            }
        }

        [TestMethod]
        public void AddProcessGroupSortingTest_04()
        {
            //Arrange
            ObservableCollection<ProcessGroupModel> expectedCollection = new ObservableCollection<ProcessGroupModel>
            {
                new ProcessGroupModel("G1", "G_1", 1),
                new ProcessGroupModel("G2", "G_2", 2),
                new ProcessGroupModel("G3", "G_3", 3),
                new ProcessGroupModel("G4", "G_4", 4),
                new ProcessGroupModel("G5", "G_5", 5),
                new ProcessGroupModel("G6", "G_6", 6),
                new ProcessGroupModel("G7", "G_7", 7)
            };

            //Act
            ObservableCollection<ProcessGroupModel> actualCollection = new ObservableCollection<ProcessGroupModel>
            {
                new ProcessGroupModel("G1", "G_1", 1),
                new ProcessGroupModel("G2", "G_2", 2),
                new ProcessGroupModel("G3", "G_3", 4),
                new ProcessGroupModel("G2", "G_2", 6),
                new ProcessGroupModel("G3", "G_3", 5),
                new ProcessGroupModel("G2", "G_2", 6)
            };
            ProcessGroupModel newProcessGroupModel = new ProcessGroupModel("Gnew", "G_new", 10);

            AddItemService addItemService = new AddItemService();
            addItemService.AddService(actualCollection, newProcessGroupModel);

            for(int listCount = 0; listCount < expectedCollection.Count; listCount++)
            {
                int expectedSortingnumber = expectedCollection[listCount].SortingNumber;
                int actualSortingNumber = actualCollection[listCount].SortingNumber;
                //Assert
                Assert.AreEqual(expectedSortingnumber, actualSortingNumber, "AddItemService could not sort a new ProcessGroup");
            }
        }


        [TestMethod]
        public void AddProcessPointSortingTest_01()
        {
            //Arrange
            ObservableCollection<ProcessPoint> expectedCollection = new ObservableCollection<ProcessPoint>
            {
                new ProcessPointC("G1", "G_1", 1),
                new ProcessPointC("G2", "G_2", 2),
                new ProcessPointC("G3", "G_3", 3)
            };

            //Act
            ObservableCollection<ProcessPoint> actualCollection = new ObservableCollection<ProcessPoint>
            {
                new ProcessPointC("G1", "G_2", 2),
                new ProcessPointC("G3", "G_3", 1)
            };
            ProcessPointC newProcessPointC = new ProcessPointC("G2", "G_2", 2);

            AddItemService addItemService = new AddItemService();
            addItemService.AddService(actualCollection, newProcessPointC);

            for(int listCount = 0; listCount < expectedCollection.Count; listCount++)
            {
                int expectedSortingnumber = expectedCollection[listCount].SortingNumber;
                int actualSortingNumber = actualCollection[listCount].SortingNumber;
                //Assert
                Assert.AreEqual(expectedSortingnumber, actualSortingNumber, "AddItemService could not sort a new ProcessGroup");
            }
        }

        [TestMethod]
        public void AddProcessPointSortingTest_02()
        {
            //Arrange
            ObservableCollection<ProcessPoint> expectedCollection = new ObservableCollection<ProcessPoint>
            {
                new ProcessPointC("G1", "G_1", 1),
                new ProcessPointC("G2", "G_2", 2),
                new ProcessPointC("G3", "G_3", 3),
                new ProcessPointC("G4", "G_4", 4),
                new ProcessPointC("G5", "G_5", 5),
                new ProcessPointC("G6", "G_6", 6),
                new ProcessPointC("G7", "G_7", 7)
            };

            //Act
            ObservableCollection<ProcessPoint> actualCollection = new ObservableCollection<ProcessPoint>
            {
                new ProcessPointC("G1", "G_1", 1),
                new ProcessPointC("G2", "G_2", 2),
                new ProcessPointC("G3", "G_3", 3),
                new ProcessPointC("G2", "G_2", 4),
                new ProcessPointC("G3", "G_3", 5),
                new ProcessPointC("G2", "G_2", 6)
            };
            ProcessPointC newProcessPointC = new ProcessPointC("Gnew", "G_new", 3);

            AddItemService addItemService = new AddItemService();
            addItemService.AddService(actualCollection, newProcessPointC);

            for(int listCount = 0; listCount < expectedCollection.Count; listCount++)
            {
                int expectedSortingnumber = expectedCollection[listCount].SortingNumber;
                int actualSortingNumber = actualCollection[listCount].SortingNumber;
                //Assert
                Assert.AreEqual(expectedSortingnumber, actualSortingNumber, "AddItemService could not sort a new ProcessGroup");
            }
        }

        [TestMethod]
        public void AddProcessPointSortingTest_03()
        {
            //Arrange
            ObservableCollection<ProcessPoint> expectedCollection = new ObservableCollection<ProcessPoint>
            {
                new ProcessPointC("G1", "G_1", 1),
                new ProcessPointC("G2", "G_2", 2),
                new ProcessPointC("G3", "G_3", 3),
                new ProcessPointC("G4", "G_4", 4),
                new ProcessPointC("G5", "G_5", 5),
                new ProcessPointC("G6", "G_6", 6),
                new ProcessPointC("G7", "G_7", 7)
            };

            //Act
            ObservableCollection<ProcessPoint> actualCollection = new ObservableCollection<ProcessPoint>
            {
                new ProcessPointC("G1", "G_1", 1),
                new ProcessPointC("G2", "G_2", 2),
                new ProcessPointC("G3", "G_3", 2),
                new ProcessPointC("G2", "G_2", 2),
                new ProcessPointC("G3", "G_3", 5),
                new ProcessPointC("G2", "G_2", 6)
            };
            ProcessPointC newProcessPointC = new ProcessPointC("Gnew", "G_new", 0);

            AddItemService addItemService = new AddItemService();
            addItemService.AddService(actualCollection, newProcessPointC);

            for(int listCount = 0; listCount < expectedCollection.Count; listCount++)
            {
                int expectedSortingnumber = expectedCollection[listCount].SortingNumber;
                int actualSortingNumber = actualCollection[listCount].SortingNumber;
                //Assert
                Assert.AreEqual(expectedSortingnumber, actualSortingNumber, "AddItemService could not sort a new ProcessGroup");
            }
        }

        [TestMethod]
        public void AddProcessPointSortingTest_04()
        {
            //Arrange
            ObservableCollection<ProcessPoint> expectedCollection = new ObservableCollection<ProcessPoint>
            {
                new ProcessPointC("G1", "G_1", 1),
                new ProcessPointC("G2", "G_2", 2),
                new ProcessPointC("G3", "G_3", 3),
                new ProcessPointC("G4", "G_4", 4),
                new ProcessPointC("G5", "G_5", 5),
                new ProcessPointC("G6", "G_6", 6),
                new ProcessPointC("G7", "G_7", 7)
            };

            //Act
            ObservableCollection<ProcessPoint> actualCollection = new ObservableCollection<ProcessPoint>
            {
                new ProcessPointC("G1", "G_1", 1),
                new ProcessPointC("G2", "G_2", 2),
                new ProcessPointC("G3", "G_3", 4),
                new ProcessPointC("G2", "G_2", 6),
                new ProcessPointC("G3", "G_3", 5),
                new ProcessPointC("G2", "G_2", 6)
            };
            ProcessPointC newProcessPointC = new ProcessPointC("Gnew", "G_new", 10);

            AddItemService addItemService = new AddItemService();
            addItemService.AddService(actualCollection, newProcessPointC);

            for(int listCount = 0; listCount < expectedCollection.Count; listCount++)
            {
                int expectedSortingnumber = expectedCollection[listCount].SortingNumber;
                int actualSortingNumber = actualCollection[listCount].SortingNumber;
                //Assert
                Assert.AreEqual(expectedSortingnumber, actualSortingNumber, "AddItemService could not sort a new ProcessGroup");
            }
        }

        #endregion
    }
}
