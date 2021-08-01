using MTTApi.Controllers;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestApiUnitTest
{
    public class Tests
    {
        ProductController productsController = new ProductController();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestFeaturedProducts()
        {
            var products = productsController.Get("1, 2");
                
            Assert.IsTrue(products != null && products.Count == 4);
        }

        [Test]
        public void TestProductByCategory()
        {
            var products = productsController.Get(5);

            Assert.IsTrue(products.Count == 2);
        }

        [Test]
        public void TestAvailableCategories()
        {
            var categories = productsController.GetCategories();

            Assert.IsTrue(categories.Count == 5);
        }

    }
}