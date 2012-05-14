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
            DateTime dt = new DateTime();
            int i = 1;
            LogEvent actual;
            LogEvent expected;
            actual = LogParser.parseEvent(dt, eventLine);
            Assert.IsNull(actual, "Empty event line returned non-null item");

            eventLine = "00:00:05 - *DEAD* [Tequila_Rising_CCC] yeah lost pony speed advantage on the hill";
            expected = new ChatEvent(eventLine, ++i, "Tequila_Rising_CCC");
            actual = LogParser.parseEvent(dt, eventLine);
            Assert.AreEqual(expected, actual, "ChatEvent parsed incorrectly");

            eventLine = "23:59:45 - TLB_Pleep started a poll to kick player Diesel.";
            expected = new KickEvent(eventLine, ++i, "TLB_Pleep");
            actual = LogParser.parseEvent(dt, eventLine);
            Assert.AreEqual(expected, actual, "KickEvent parsed incorrectly");

            eventLine = "00:00:00 - Bodyguard_of_Prophetess <img=ico_spear> lockermoker";
            expected = new KillEvent(eventLine, ++i, "Bodyguard_of_Prophetess", "lockermoker");
            actual = LogParser.parseEvent(dt, eventLine);
            Assert.AreEqual(expected, actual, "KillEvent parsed incorrectly");

            eventLine = "23:58:45 - sirzosh77 has joined the game with ID: 417914";
            expected = new LoginEvent(eventLine, ++i, "sirzosh77", "417914");
            actual = LogParser.parseEvent(dt, eventLine);
            Assert.AreEqual(expected, actual, "LoginEvent parsed incorrectly");

            eventLine = "23:44:07 - zarcov started a poll to change map to Nord Town and factions to Sarranid Sultanate and Kingdom of Rhodoks.";
            expected = new MapPollEvent(eventLine, ++i, "zarcov");
            actual = LogParser.parseEvent(dt, eventLine);
            Assert.AreEqual(expected, actual, "MapPollEvent parsed incorrectly");

            eventLine = "19:13:26 - lucas123456789 is banned permanently by Wappaw_Redknight.";
            expected = new PermaBanEvent(eventLine, ++i, "lucas123456789");
            actual = LogParser.parseEvent(dt, eventLine);
            Assert.AreEqual(expected, actual, "PermaBanEvent parsed incorrectly");

            eventLine = "17:12:50 - El~Fisto is banned temporarily.";
            expected = new TmpBanEvent(eventLine, ++i, "El~Fisto");
            actual = LogParser.parseEvent(dt, eventLine);
            Assert.AreEqual(expected, actual, "TmpBanEvent parsed incorrectly");
        }
    }
}
