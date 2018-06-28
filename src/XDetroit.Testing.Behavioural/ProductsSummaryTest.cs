﻿using System;
using System.Linq;
using NUnit.Framework;
using XDetroit.WebFrontend.Dal;

namespace XDetroit.Testing.Behavioural
{
    [TestFixture]
    public class ProductsSummaryTest
    {
        private IDataProvider dataProvider;
        private DataLayer dataLayer;

        [SetUp]
        public void Setup()
        {
            dataProvider = new InMemoryDataProvider();
            dataLayer = new DataLayer(dataProvider);
        }

        [Test]
        public void TheRequestedPageOfProductsShouldBeReturned()
        {
            var category = dataProvider.CreateEntity(new ItemCategory { Name = "sample category" });
            var product1 = dataProvider.CreateEntity(new ProductItem { Name = "sample product 1", ExtCategoryId = category.Id});
            dataProvider.CreateEntity(new ProductItem { Name = "sample product 2", ExtCategoryId = category.Id});
            dataProvider.SaveChanges();

            var behaviourResult = dataLayer.GetProducts(10, 0);

            Assert.IsTrue(behaviourResult.Value.Any(
                    x => x.Name.Equals(product1.Name, StringComparison.InvariantCultureIgnoreCase)), "Product not found.");

            Assert.AreEqual(2, behaviourResult.Value.Count, "Count is incorrect.");
        }
    }
}