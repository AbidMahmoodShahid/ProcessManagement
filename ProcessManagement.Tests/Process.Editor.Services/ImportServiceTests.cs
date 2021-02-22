using Microsoft.VisualStudio.TestTools.UnitTesting;
using Process.Editor.Elements;
using Process.Editor.Services;
using Process.Editor.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.Tests.Process.Editor.Services
{
    [TestClass]
    public class ImportServiceTests
    {
        #region Exceptions

        [TestMethod]
        public void ImportService_clonableCollectionContainer_ArgumentNullException()
        {
            ProcessEditorViewModel clonableCollectionContainer = null;
            string importedItemFilePath = Path.GetTempFileName() + ".xml";
            File.Create(importedItemFilePath);

            Assert.ThrowsException<ArgumentNullException>(() => ImportService.Import(clonableCollectionContainer, importedItemFilePath, new Dictionary<string, string>()), "ArgumentNullException for null clonableCollectionContainer not thrown");
        }

        [TestMethod]
        public void ImportService_newNameAndIdDictionary_ArgumentNullException()
        {
            ProcessEditorViewModel clonableCollectionContainer = new ProcessEditorViewModel(false);
            string importedItemFilePath = Path.GetTempFileName() + ".xml";
            File.Create(importedItemFilePath);

            Assert.ThrowsException<ArgumentNullException>(() => ImportService.Import(clonableCollectionContainer, importedItemFilePath, null), "ArgumentNullException for null NewNameAndIdDictionary not thrown");
        }

        [TestMethod]
        public void ImportService_EmptyImportedFilePath_ArgumentException()
        {
            ProcessEditorViewModel clonableCollectionContainer = new ProcessEditorViewModel(false);

            Assert.ThrowsException<ArgumentException>(() => ImportService.Import(clonableCollectionContainer, "", new Dictionary<string, string>()), "ArgumentException for null or empty ImportedFilePath not thrown");
        }

        [TestMethod]
        public void ImportService_EmptyNewName_ArgumentException()
        {
            ProcessEditorViewModel clonableCollectionContainer = new ProcessEditorViewModel(false);
            Dictionary<string, string> newNameAndIdDictionary = new Dictionary<string, string>()
            {
                {"Name","" },
                {"Id","testId" }
            };
            string importedItemFilePath = Path.GetTempFileName() + ".xml";
            File.Create(importedItemFilePath);

            Assert.ThrowsException<ArgumentException>(() => ImportService.Import(clonableCollectionContainer, importedItemFilePath, newNameAndIdDictionary), "ArgumentException for null or empty new Name not thrown");
        }

        [TestMethod]
        public void ImportService_EmptyNewId_ArgumentException()
        {
            ProcessEditorViewModel clonableCollectionContainer = new ProcessEditorViewModel(false);
            Dictionary<string, string> newNameAndIdDictionary = new Dictionary<string, string>()
            {
                {"Name","testname" },
                {"Id","" }
            };
            string importedItemFilePath = Path.GetTempFileName() + ".xml";
            File.Create(importedItemFilePath);

            Assert.ThrowsException<ArgumentException>(() => ImportService.Import(clonableCollectionContainer, importedItemFilePath, newNameAndIdDictionary), "ArgumentException for null or empty new Id not thrown");
        }

        [TestMethod]
        public void ImportService_UnknownImportFilePath_ArgumentException()
        {
            ProcessEditorViewModel clonableCollectionContainer = new ProcessEditorViewModel(false);
            Dictionary<string, string> newNameAndIdDictionary = new Dictionary<string, string>()
            {
                {"Name","testname" },
                {"Id","id" }
            };

            Assert.ThrowsException<ArgumentException>(() => ImportService.Import(clonableCollectionContainer, "unknown filepath", newNameAndIdDictionary), "ArgumentException for unknown imported item filepath not thrown");
        }

        #endregion

        #region ImportService Basic Tests

        [TestMethod]
        public void ImportService_NewNameAndIdTest()
        {
            //Arrange
            string expectedName = "Imported Process 1";
            string expectedId = "Imported P_1";

            //Act
            ProcessEditorViewModel actualProcessEditorViewModel = new ProcessEditorViewModel(false);
            Dictionary<string, string> expectedProcessNameAndIdDictionary = new Dictionary<string, string>()
            {
                { "Name", "Imported Process 1" },
                { "Id", "Imported P_1" }
            };
            string folderPath = String.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
            string actualImportFilePath = folderPath.Replace("bin\\Debug", "Process.Editor.Services\\ExportedFiles\\ImportProcessWithGroupsAndPoints.xml");

            ImportService.Import(actualProcessEditorViewModel, actualImportFilePath, expectedProcessNameAndIdDictionary);

            string actualName = actualProcessEditorViewModel.ItemCollection[actualProcessEditorViewModel.ItemCollection.Count - 1].Name;
            string actualId = actualProcessEditorViewModel.ItemCollection[actualProcessEditorViewModel.ItemCollection.Count - 1].Id;

            //Assert
            Assert.AreEqual(expectedName, actualName, "Expected process name not the same as actual process name.\nExpected ProcessName:{0}\nActual ProcessName{1}", expectedName, actualName);
            Assert.AreEqual(expectedId, actualId, "Expected process Id not the same as actual process Id.\nExpected ProcessId:{0}\nActual ProcessId{1}", expectedId, actualId);
        }

        [TestMethod]
        public void ImportService_ItemCollectionCountTest()
        {
            //Arrange
            int expectedCount = 1;

            //Act
            ProcessEditorViewModel actualProcessEditorViewModel = new ProcessEditorViewModel(false);
            Dictionary<string, string> expectedProcessNameAndIdDictionary = new Dictionary<string, string>()
            {
                { "Name", "Imported Process 1" },
                { "Id", "Imported P_1" }
            };
            string folderPath = String.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
            string actualImportFilePath = folderPath.Replace("bin\\Debug", "Process.Editor.Services\\ExportedFiles\\ImportProcessWithGroupsAndPoints.xml");
            ImportService.Import(actualProcessEditorViewModel, actualImportFilePath, expectedProcessNameAndIdDictionary);

            int actualCount = actualProcessEditorViewModel.ItemCollection.Count;

            //Assert
            Assert.AreEqual(expectedCount, actualCount, "Expected ItemCollection count not the same as actual ItemCollection count.\nExpected ItemCollection count:{0}\nActual ItemCollection count{1}", expectedCount, actualCount);
        }

        [TestMethod]
        public void ImportService_ImportedProcessItemCollection()
        {
            //Arrange
            int expectedItemCollectionCount = 3;

            //Act
            ProcessEditorViewModel actualProcessEditorViewModel = new ProcessEditorViewModel(false);
            Dictionary<string, string> expectedProcessNameAndIdDictionary = new Dictionary<string, string>()
            {
                { "Name", "Imported Process 1" },
                { "Id", "Imported P_1" }
            };
            string folderPath = String.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
            string actualImportFilePath = folderPath.Replace("bin\\Debug", "Process.Editor.Services\\ExportedFiles\\ImportProcessWithGroupsAndPoints.xml");

            ImportService.Import(actualProcessEditorViewModel, actualImportFilePath, expectedProcessNameAndIdDictionary);

            int actualItemCollectionCount = actualProcessEditorViewModel.ItemCollection[actualProcessEditorViewModel.ItemCollection.Count - 1].ItemCollection.Count;

            //Assert
            Assert.AreEqual(expectedItemCollectionCount, actualItemCollectionCount, "Expected Process ItemCollection count not the same as actual Process ItemCollection count.\nExpected ItemCollection count:{0}\nActual ItemCollection count{1}", expectedItemCollectionCount, actualItemCollectionCount);

        }

        [TestMethod]
        public void ImportService_ImportedProcessGroupItemCollection()
        {
            //Arrange
            int expectedGroupItemCollectionCount = 20;

            //Act
            ProcessEditorViewModel actualProcessEditorViewModel = new ProcessEditorViewModel(false);
            Dictionary<string, string> expectedProcessNameAndIdDictionary = new Dictionary<string, string>()
            {
                { "Name", "Imported Process 1" },
                { "Id", "Imported P_1" }
            };
            string folderPath = String.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
            string actualImportFilePath = folderPath.Replace("bin\\Debug", "Process.Editor.Services\\ExportedFiles\\ImportProcessWithGroupsAndPoints.xml");

            ImportService.Import(actualProcessEditorViewModel, actualImportFilePath, expectedProcessNameAndIdDictionary);

            int actualGroupItemCollectionCount = actualProcessEditorViewModel.ItemCollection[actualProcessEditorViewModel.ItemCollection.Count - 1].ItemCollection[0].ItemCollection.Count;

            //Assert
            Assert.AreEqual(expectedGroupItemCollectionCount, actualGroupItemCollectionCount, "Expected ProcessGroup ItemCollection count not the same as actual ProcessGroup ItemCollection count.\nExpected ItemCollection count:{0}\nActual ItemCollection count{1}", expectedGroupItemCollectionCount, actualGroupItemCollectionCount);

        }

        #endregion
    }
}
