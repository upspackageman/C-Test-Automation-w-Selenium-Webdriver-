using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using WebDriverWrapper;
using WebDriverWrapper.Extensions;

namespace AutomationProject
{
    /// <summary>
    /// Summary description for SeleniumImpetationTest
    /// </summary>
    [TestClass]
    [DeploymentItem("geckodriver.exe")]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("IEDriverServer.exe")]
    public class SeleniumImpetationTest:SeleniumHandler
    {
        public SeleniumImpetationTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
      // [DeploymentItem("geckodriver.exe")]
        public void WebDriverTest()
        {
            IWebDriver webDriver = new InternetExplorerDriver();
            Thread.Sleep(1000);
            webDriver.Dispose();

            webDriver = new ChromeDriver();
            Thread.Sleep(1000);
            webDriver.Dispose();

            webDriver = new FirefoxDriver();
            Thread.Sleep(1000);
            webDriver.Dispose();
        }

        [TestMethod]
        public void WebElementSamplesTest()
        {
            IWebDriver webdriver = new ChromeDriver();
            webdriver.Navigate().GoToUrl("https://travel.hotels.com/Packages-Flights?intlid=HOME+%253A%253A+header_main_section&");
            webdriver.Manage().Window.Maximize();

            var radioButton = webdriver.FindElement(By.XPath("//*[@id='qf - 0q - destination']"));
            radioButton.Click();

            webdriver.FindElement(By.XPath("//*[@id='citysqm - asi0 - s0']/td/div[2]")).SendKeys("New York, New York, United States of America");
           


            var textBox = webdriver.FindElement(By.XPath("//*[@id='widget - query - label - 1']"));
            textBox.Clear();
            textBox.SendKeys(DateTime.Now.ToString("mm/dd/yyyy"));

            textBox= webdriver.FindElement(By.XPath("//*[@id='widget - query - label - 3']"));
            textBox.Clear();
            textBox.SendKeys(DateTime.Now.AddDays(1).ToString("mm/dd/yyyy"));

            Assert.AreEqual(true, radioButton.Selected);   

            var comboBox = webdriver.FindElement(By.XPath("//*[@id='package - advanced - preferred -class']"));
            var selectElem = new SelectElement(comboBox);
            selectElem.SelectByIndex(4);
             
            
            comboBox= webdriver.FindElement(By.XPath("//*[@id='package - advanced - preferred -class']"));
            selectElem = new SelectElement(comboBox);
            selectElem.SelectByValue("");
           

            var checkBox = webdriver.FindElement(By.XPath("//*[@id='partialHotelBooking']"));
            checkBox.Click();
            Assert.AreEqual(true, checkBox.Selected);

            var image = webdriver.FindElement(By.XPath("//*[@id='search - button']"));
            image.Click();

            webdriver.Dispose();


        }

        [TestMethod]
        public void JsonObjectTest()
        {
            var jsonTest = new SeleniumWrapper();
            jsonTest.ParseJson("{\"FirstName\":\"Will\", \"LastName\":\"Ward\",\"Age\":33,\"LikeThisCourse\":true}");
        }

        [TestMethod]
        public void SeleniumHandlerSamples()
        {
            var drivers = new List<string>();
            drivers.Add("{\"Driver\":\"Chrome\"}");
            drivers.Add("{\"Driver\":\"IE\"}");
            drivers.Add("{\"Driver\":\"Firefox\"}");


            drivers.ForEach(_drivers =>
            {
                var handler = new SeleniumHandler();
                handler.webDriverParameter = _drivers;
                var driver = handler.WebDriver;
                try
                {
                    
                    driver.Navigate().GoToUrl("https://www.hotels.com/");
                    driver.Manage().Window.Maximize();
                    driver.FindElement(By.XPath("//*[@id='hdr-packages']")).Click();
                    driver.Close();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    driver.Dispose();
                }
                
            });
        }

        [TestMethod]
        public void SearchSitesWithWrapper()
        {
            webDriverParameter = "{\"Driver\":\"IE\"}";
            GoToUrl("https://www.hotels.com");

            FindElement(By.XPath("//*[@id='hdr-packages']"), 500,2000).Click();
           
            FindElement(By.XPath("//*[@id='qf-0q-compact-occupancy']")).ComboBox().SelectByIndex(1);

            FindElement(By.XPath("//*[@id='citysqm-asi0-s0']/td/div[2]")).SendKeys("New York");
          


            var textBox = FindElement(By.XPath("//*[@id='widget-query-label-1']"));
            textBox.Clear();
            textBox.SendKeys(DateTime.Now.ToString("mm/dd/yyyy"));

            textBox = FindElement(By.XPath("//*[@id='widget-query-label-3']"));
            textBox.Clear();
            textBox.SendKeys(DateTime.Now.AddDays(1).ToString("mm/dd/yyyy"));

            FindElement(By.XPath("//*[@id='package-advanced-preferred-class']")).ComboBox().SelectByValue("0:1");

            WebDriver.Dispose();
        }

        [TestMethod]
        public void FindElements()
        {
            webDriverParameter = "{\"Driver\":\"IE\"}";
            GoToUrl("https://www.expedia.com/");
            var elements = FindElements(By.XPath("//input[@type='checkbox']"));

            elements.ForEach(element => { element.Click(); });

        }

        [TestMethod]
        public void GetDisplayedElement()
        {
            webDriverParameter = "{\"Driver\":\"IE\"}";
            GoToUrl("https://www.hotels.com");

            var xpath = "//*[@id='citysqm-asi0-s0']";

            GetDisplayedElement(By.XPath(xpath)).SendKeys("Ne");
            GetDisplayedElement(By.XPath("//*[@id='citysqm-asi0-s0']/td/div[2]")).Click();
            
        }
        [TestMethod]
        public void GetDisplayedElements()
        {
            webDriverParameter = "{\"Driver\":\"IE\"}";
            GoToUrl("https://www.expedia.com/");
            var elements = FindElements(By.XPath("//input[@type='checkbox']"));

            elements.ForEach(element => { element.Click(); });
        }
        [TestMethod]
        public void WaitForDisplayedElement()
        {
            webDriverParameter = "{\"Driver\":\"IE\"}";
            GoToUrl("https://www.hotels.com");

            FindElement(By.XPath("//*[@id='hdr-packages']"), 500, 2000).Click();

            FindElement(By.XPath("//*[@id='qf-0q-compact-occupancy']")).ComboBox().SelectByIndex(1);

            FindElement(By.XPath("//*[@id='citysqm-asi0-s0']/td/div[2]")).SendKeys("New York");

            var textBox = FindElement(By.XPath("//*[@id='widget-query-label-1']"));
            textBox.Clear();
            textBox.SendKeys(DateTime.Now.ToString("mm/dd/yyyy"));

            textBox = FindElement(By.XPath("//*[@id='widget-query-label-3']"));
            textBox.Clear();
            textBox.SendKeys(DateTime.Now.AddDays(1).ToString("mm/dd/yyyy"));

            FindElement(By.XPath("//*[@id='package-advanced-preferred-class']")).ComboBox().SelectByValue("0:1");
            var temp = GetDisplayedElement(By.XPath("//*[@id='star5']"));
            WebDriver.Dispose();
        }

        [TestMethod]
        public void ScrollPages()
        {
            webDriverParameter = "{\"Driver\":\"IE\"}";
            GoToUrl("https://www.hotels.com");

            FindElement(By.XPath("//*[@id='hdr-packages']"), 500, 2000).Click();

            FindElement(By.XPath("//*[@id='qf-0q-compact-occupancy']")).ComboBox().SelectByIndex(1);

            FindElement(By.XPath("//*[@id='citysqm-asi0-s0']/td/div[2]")).SendKeys("New York");



            var textBox = FindElement(By.XPath("//*[@id='widget-query-label-1']"));
            textBox.Clear();
            textBox.SendKeys(DateTime.Now.ToString("mm/dd/yyyy"));

            textBox = FindElement(By.XPath("//*[@id='widget-query-label-3']"));
            textBox.Clear();
            textBox.SendKeys(DateTime.Now.AddDays(1).ToString("mm/dd/yyyy"));

            FindElement(By.XPath("//*[@id='package-advanced-preferred-class']")).ComboBox().SelectByValue("0:1");
            for (int i = 1; i < 5; i++) 
            {
                WebDriver.ScrollBrowserPage(2000*i);
                Thread.Sleep(500);

            }
            WebDriver.Dispose();
        } 

        [TestMethod]
        public void Actions()
        {
            webDriverParameter = "{\"Driver\":\"Chrome\"}";
            GoToUrl("https://www.hotels.com");

            //FindElement(By.XPath("//*[@id='main-content']/main/div/div/div[1]/div/div[1]/div[1]/div/div/form/fieldset[5]/button")).Actions().ContextClick().Perform();
            FindElement(By.XPath("//*[@id='main-content']/main/div/div/div[1]/div/div[1]/div[1]/div/div/form/fieldset[5]/button")).ContextClick();

            Thread.Sleep(5000);

        }

        [TestMethod]
        public void BannersListener()
        {
            webDriverParameter = "{\"Driver\":\"Chrome\"}";
            GoToUrl("http://www.bestbuy.com/");

            WebDriver.BannersListener(By.XPath("//*[@id='abt-email-modal']/div/div/div[1]/button/span[1]"));

            Thread.Sleep(5000);

            WebDriver.Dispose();
        }
    }
}
