using Catalog.Listener.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Listener.Proxies
{
    public interface ICatalogProxy
    {
        Task UpdateStockAsync(ProductInStockUpdateCommand command);
    }
}
