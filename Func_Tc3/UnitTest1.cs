using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Func_Tc3
{
    public class Tests
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            if (driver == null)
            {
                driver = new ChromeDriver();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                driver.Manage().Window.Maximize();
                driver.Url = "https://vaalikone.yle.fi/eduskuntavaali2019";
            }
        }

        [Test, Order(1)]
        public void TarkistaVaalikoneenEtusivunKuvake()
        {
            driver.FindElement(By.XPath("/html/body/div/main/div[1]/img"));
        }
        [Test, Order(2)]
        public void HaetaanHakuKenttaJaKlikataanSita()
        {
            driver.FindElement(By.XPath("/html/body/div/main/div[1]/section/div[2]/input")).Click();
        }
        [Test, Order(3)]
        public void LasketaanDropdownValikonKohteidenMaaraJaVerrataanKuntienMaaraan()
        {
            // Arrange
            int kuntienmaara = 311; // Kuntien m‰‰r‰ l‰hde: https://www.kuntaliitto.fi/tilastot-ja-julkaisut/kaupunkien-ja-kuntien-lukumaarat-ja-vaestotiedot 

            // Act

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            IList<IWebElement> kunnat;
            kunnat = driver.FindElement(By.CssSelector("#root > main > div.welcomepage__Container-sc-1rp6p64-1.dezjZV > section > div.searchinput__SearchInput-h3ib9i-1.iSGaFM > div")).FindElements(By.XPath("//a"));
           // kunnat = kunnat.Where(row => !row.Text.ToString().Contains("")).ToList();

            int vaalikonekuntienmaara = kunnat.Count;
            vaalikonekuntienmaara = vaalikonekuntienmaara - 5; // Sivulla on viisi linkki‰ "Tietosuojalauseke", "Etusivulle", "Eduskuntavaalit" -kuva, sek‰ "yle" -kuva jotka ovat a tagin alla ja lukeutuvat listaan.
            Console.WriteLine("Kuntien m‰‰r‰: " + kunnat.Count);

            // Assert
            Assert.AreEqual(kuntienmaara, vaalikonekuntienmaara);
        }
    }
}