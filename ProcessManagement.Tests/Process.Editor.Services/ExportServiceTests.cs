using Microsoft.VisualStudio.TestTools.UnitTesting;
using Process.Editor.Elements;
using Process.Editor.Services;
using System;
using System.IO;
using System.Linq;

namespace ProcessManagement.Tests.Process.Editor.Services
{
    [TestClass]
    public class ExportServiceTests
    {
        #region Exceptions

        [TestMethod]
        public void ExportServiceTests_ItemToExport_ArgumentNullException()
        {
            ProcessModel itemToExport = null;
            Assert.ThrowsException<ArgumentNullException>(() => ExportService.Export(itemToExport, "FileName"), "ArgumentNullException for null itemToExport not thrown");
        }

        [TestMethod]
        public void ExportServiceTests_FileName_ArgumentException()
        {
            string filename = null;
            Assert.ThrowsException<ArgumentException>(() => ExportService.Export(new ProcessModel(), filename), "ArgumentException for null or empty FileName not thrown");
        }

        #endregion

        #region ExportServiceTests Basic Tests

        [TestMethod]
        public void ExportServiceTests_ProcessWithoutGroups()
        {
            //Arrange
            string folderPath = String.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
            string expectedExportFilePath = folderPath.Replace("bin\\Debug", "Process.Editor.Services\\ExportedFiles\\ExpectedExport.xml");
            string[] expectedExport = File.ReadAllLines(expectedExportFilePath);
            expectedExport = expectedExport.Skip(1).ToArray();

            //Act
            ProcessModel actualProcessModel = new ProcessModel("ExportedProcess", "EP_01", 4);
            string actualExportFilePath = Path.GetTempFileName() + ".xml";
            ExportService.Export(actualProcessModel, actualExportFilePath);
            string[] actualExport = File.ReadAllLines(actualExportFilePath);
            actualExport = actualExport.Skip(1).ToArray();

            //Assert
            int count = 0;
            foreach(string exportLine in expectedExport)
            {
                Assert.AreEqual(exportLine, actualExport[count], "The expected exported Process does not match the actual exported file.\nExpected string: {0}\nActual string: {1}", exportLine, actualExport[count]);
                count++;
            }
        }

        [TestMethod]
        public void ExportServiceTests_ProcessWithGroupsAndPoints()
        {
            //Arrange
            string folderPath = String.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
            string expectedExportFilePath = folderPath.Replace("bin\\Debug", "Process.Editor.Services\\ExportedFiles\\ExpectedExportedProcessWithGroupsAndPoints.xml");
            string[] expectedExport = File.ReadAllLines(expectedExportFilePath);
            expectedExport = expectedExport.Skip(1).ToArray();

            //Act
            ProcessModel actualProcessModel = new ProcessModel("Process 1", "P_1", 1);

            for(int i = 1; i < 4; i++)
            {
                string processGroupName = actualProcessModel.Id + " Process Group " + i;
                string processGroupId = actualProcessModel.Id + "_PG_" + i;
                actualProcessModel.ItemCollection.Add(new ProcessGroupModel(processGroupName, processGroupId, i));
            }

            foreach(ProcessGroupModel processGroupModel in actualProcessModel.ItemCollection)
            {
                for(int i = 1; i < 6; i++)
                {
                    string processPointName = processGroupModel.Id + " Process Point " + i;
                    string processPointId = processGroupModel.Id + "_PP_" + i;
                    processGroupModel.ItemCollection.Add(new ProcessPointA(processPointName, processPointId, i));
                }
                for(int i = 6; i < 11; i++)
                {
                    string processPointName = processGroupModel.Id + " Process Point " + i;
                    string processPointId = processGroupModel.Id + "_PP_" + i;
                    processGroupModel.ItemCollection.Add(new ProcessPointB(processPointName, processPointId, i));
                }
                for(int i = 11; i < 16; i++)
                {
                    string processPointName = processGroupModel.Id + " Process Point " + i;
                    string processPointId = processGroupModel.Id + "_PP_" + i;
                    processGroupModel.ItemCollection.Add(new ProcessPointC(processPointName, processPointId, i));
                }
                for(int i = 16; i < 21; i++)
                {
                    string processPointName = processGroupModel.Id + " Process Point " + i;
                    string processPointId = processGroupModel.Id + "_PP_" + i;
                    processGroupModel.ItemCollection.Add(new ProcessPointD(processPointName, processPointId, i));
                }
            }

            string actualExportFilePath = Path.GetTempFileName() + ".xml";
            ExportService.Export(actualProcessModel, actualExportFilePath);
            string[] actualExport = File.ReadAllLines(actualExportFilePath);
            actualExport = actualExport.Skip(1).ToArray();

            //Assert
            int count = 0;
            foreach(string exportLine in expectedExport)
            {
                Assert.AreEqual(exportLine, actualExport[count], "The expected exported Process does not match the actual exported file.\nExpected string: {0}\nActual string: {1}", exportLine, actualExport[count]);
                count++;
            }
        }

        #endregion
    }
}
