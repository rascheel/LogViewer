using LogViewer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogViewerTest
{
    
    
    /// <summary>
    ///This is a test class for LogParserTest and is intended
    ///to contain all LogParserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LogParserTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for parseEvent
        ///</summary>
        [TestMethod()]
        public void parseEventTest()
        {
            string eventLine = string.Empty;
            LogEvent actual;
            LogEvent expected;
            actual = LogParser.parseEvent(eventLine);
            Assert.IsNull(actual, "Empty event line returned non-null item");

            eventLine = "00:00:00 - Bodyguard_of_Prophetess <img=ico_spear> lockermoker";
            expected = new KillEvent(eventLine, 2, "Bodyguard_of_Prophetess", "lockermoker");
            actual = LogParser.parseEvent(eventLine);
            Assert.AreEqual(expected, actual, "Kill event parsed incorrectly");
        }
    }
}
