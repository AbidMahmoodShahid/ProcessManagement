using Microsoft.VisualStudio.TestTools.UnitTesting;
using Process.Editor.Elements;
using Process.Editor.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProcessManagement.Tests.Process.Editor.Services
{
    [TestClass]
    public class GetterServiceTests
    {
        #region Exceptions

        [TestMethod]
        public void GetterServiceTests_GetIdList_ArgumentNullException()
        {
            ProcessModel processModel = new ProcessModel();
            processModel.ItemCollection = null;

            GetterService getterService = new GetterService();
            Assert.ThrowsException<ArgumentNullException>(() => getterService.GetIDList(processModel.ItemCollection), "ArgumentNullException for null ItemCollection not thrown");
        }


        #endregion

        #region GetterServiceTests Basic Tests

        [TestMethod]
        public void GetterServiceTests_GetIdList()
        {
            //Arrange
            List<string> expectedIdList = new List<string>()
            {
                "PP_01",
                "PP_02",
                "PP_03",
                "PP_04",
            };

            //Act
            ProcessGroupModel processGroupModel = new ProcessGroupModel("G1", "G_1", 1);
            processGroupModel.ItemCollection = new ObservableCollection<ProcessPoint>()
            {
                new ProcessPointA("PP1", "PP_01", 1),
                new ProcessPointA("PP2", "PP_02", 2),
                new ProcessPointA("PP3", "PP_03", 3),
                new ProcessPointA("PP4", "PP_04", 4),
            };
            GetterService getterService = new GetterService();
            List<string> actualIdList = getterService.GetIDList(processGroupModel.ItemCollection);

            //Assert
            int count = 0;
            foreach(string expectedId in expectedIdList)
            {
                Assert.AreEqual(expectedId, actualIdList[count], "The expected Id does not match the actual Id.\nExpected Id: {0}\nActual Id: {1}", expectedId, actualIdList[count]);
                count++;
            }
        }

        [TestMethod]
        public void GetterServiceTests_GetIdList_Empty()
        {
            //Arrange
            List<string> expectedIdList = new List<string>();

            //Act
            ProcessGroupModel processGroupModel = new ProcessGroupModel("G1", "G_1", 1);
            processGroupModel.ItemCollection = new ObservableCollection<ProcessPoint>();
            GetterService getterService = new GetterService();
            List<string> actualIdList = getterService.GetIDList(processGroupModel.ItemCollection);

            //Assert
            Assert.AreEqual(expectedIdList.Count, actualIdList.Count, "The expected Id List count does not match the actual Id List count.\nExpected Id List: {0}\nActual Id List: {1}", expectedIdList.Count, actualIdList.Count);
        }

        [TestMethod]
        public void GetterServiceTests_GetInheritedClassesArray_Missing()
        {
            //Arrange
            Type[] expectedProcessPointTypeList = new Type[]
            {
                typeof(ProcessPointA),
                typeof(ProcessPointB),
                typeof(ProcessPointC),
                typeof(ProcessPointD)
            };

            //Act
            GetterService getterService = new GetterService();
            Type[] actualProcessPointTypeList = getterService.GetInheritedClassesArray(typeof(ProcessPoint));

            //Assert
            foreach(Type actualType in actualProcessPointTypeList)
            {
                if(!(expectedProcessPointTypeList.Contains(actualType)))
                {
                    Assert.Fail("{0} does not exist in the List of expected Point Types.", actualType);
                }
            }
        }

        [TestMethod]
        public void GetterServiceTests_GetInheritedClassesArray_Redundant()
        {
            //Arrange
            Type[] expectedProcessPointTypeList = new Type[]
            {
                typeof(ProcessPointA),
                typeof(ProcessPointB),
                typeof(ProcessPointC),
                typeof(ProcessPointD)
            };

            //Act
            GetterService getterService = new GetterService();
            Type[] actualProcessPointTypeList = getterService.GetInheritedClassesArray(typeof(ProcessPoint));

            //Assert
            foreach(Type expectedType in expectedProcessPointTypeList)
            {
                if(!(actualProcessPointTypeList.Contains(expectedType)))
                {
                    Assert.Fail("{0} does not exist in the List of actual Point Types.", expectedType);
                }
            }
        }

        [TestMethod]
        public void GetterServiceTests_GetProcessPointTypeNameList_Missing()
        {
            //Arrange
            List<string> expectedTypeNameList = new List<string>()
            {
                "ProcessPointA",
                "ProcessPointB",
                "ProcessPointC",
                "ProcessPointD"
            };

            //Act
            GetterService getterService = new GetterService();
            Type[] actualProcessPointTypeList = getterService.GetInheritedClassesArray(typeof(ProcessPoint));
            List<string> actualTypeNameList = getterService.GetProcessPointTypeNameList(actualProcessPointTypeList);

            //Assert
            foreach(string actualTypeName in actualTypeNameList)
            {
                if(!(expectedTypeNameList.Contains(actualTypeName)))
                {
                    Assert.Fail("{0} does not exist in the List of expected TypeNames.", actualTypeName);
                }
            }
        }

        [TestMethod]
        public void GetterServiceTests_GetProcessPointTypeNameList_Redundant()
        {
            //Arrange
            List<string> expectedTypeNameList = new List<string>()
            {
                "ProcessPointA",
                "ProcessPointB",
                "ProcessPointC",
                "ProcessPointD"
            };

            //Act
            GetterService getterService = new GetterService();
            Type[] actualProcessPointTypeList = getterService.GetInheritedClassesArray(typeof(ProcessPoint));
            List<string> actualTypeNameList = getterService.GetProcessPointTypeNameList(actualProcessPointTypeList);

            //Assert
            foreach(string expectedTypeName in expectedTypeNameList)
            {
                if(!(actualTypeNameList.Contains(expectedTypeName)))
                {
                    Assert.Fail("{0} does not exist in the List of actual TypeNames.", expectedTypeName);
                }
            }
        }

        #endregion
    }
}
