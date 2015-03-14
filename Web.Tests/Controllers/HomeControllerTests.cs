using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;
using System.Web.Mvc;

namespace Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void IndexIsNotNull()
        {
           
            HomeController controller = new HomeController();

            
            ViewResult result = controller.Index() as ViewResult;

           
            Assert.IsNotNull(result);
        }
    }
}
