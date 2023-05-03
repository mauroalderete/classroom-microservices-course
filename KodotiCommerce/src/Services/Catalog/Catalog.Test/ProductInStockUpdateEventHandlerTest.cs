using Castle.Core.Logging;
using Catalog.Service.EventHandlers;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.EventHandlers.Exceptions;
using Catalog.Test.Config;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Catalog.Test
{
    [TestClass]
    public class ProductInStockUpdateEventHandlerTest
    {

        private ILogger<ProductInStockUpdateStockEventHandler> GetLogger
        {
            get { return new Mock<ILogger<ProductInStockUpdateStockEventHandler>>().Object; }
        }

        [TestMethod]
        public void TryTosubstracStockWhenProductHasStock()
        {
            var context = ApplicationDbContextInMemory.Get();

            const int productStockId = 1;
            const int productId = 1;

            context.Stocks.Add(new Domain.ProductInStock()
            {
                ProductId = productId,
                ProductInStockId = productStockId,
                Stock = 1,
            });

            context.SaveChanges();

            var handler = new ProductInStockUpdateStockEventHandler(context, GetLogger);

            handler.Handle(new ProductInStockUpdateCommand()
            {
                Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem() {
                        ProductId = productId,
                        Action = common.Enums.ProductInStockAction.Substract,
                        Stock = 1
                    }
                }
            }, new CancellationToken()).Wait();
        }

        [TestMethod]
        [ExpectedException(typeof(ProductInStockUpdateCommandException))]
        public void TryTosubstracStockWhenProductHasNotStock()
        {
            var context = ApplicationDbContextInMemory.Get();

            const int productStockId = 2;
            const int productId = 2;

            context.Stocks.Add(new Domain.ProductInStock()
            {
                ProductId = productId,
                ProductInStockId = productStockId,
                Stock = 1,
            });

            context.SaveChanges();

            var handler = new ProductInStockUpdateStockEventHandler(context, GetLogger);

            try
            {
                handler.Handle(new ProductInStockUpdateCommand()
                {
                    Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem() {
                        ProductId = productId,
                        Action = common.Enums.ProductInStockAction.Substract,
                        Stock = 2
                    }
                }
                }, new CancellationToken()).Wait();
            } catch (AggregateException ae)
            {
                var exception = ae.GetBaseException();
                Assert.IsTrue(exception is ProductInStockUpdateCommandException);
                throw exception;
            }
        }
    }
}
