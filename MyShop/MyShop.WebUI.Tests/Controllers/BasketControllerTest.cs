using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.Services;
using MyShop.WebUI.Controllers;
using MyShop.WebUI.Tests.Mocks;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTests
    {
        [TestMethod]
        public void CanAddBasketItem()
        {

            //SetUp for Test

            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();

            var HttpContext = new MockHttpContext();


            IBasketService basketService = new BasketService(products, baskets);
            var controller = new BasketController(basketService);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(HttpContext, new System.Web.Routing.RouteData(), controller);

            //Act for Tests

            //basketService.AddToBasket(HttpContext, "1");

            controller.AddToBasket("1");

            Basket basket = baskets.Collection().FirstOrDefault();

            //Assert

            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count);
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ProductId);

        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();

            products.Insert(new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price = 5.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 1 });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(products, baskets);

            var controller = new BasketController(basketService);
            var HttpContext = new MockHttpContext();
            HttpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommercebasket") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(HttpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSumary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSumary.BasketCount);
            Assert.AreEqual(25.00m, basketSumary.BasketTotal);
        }
    }
}
