using OnlineStore.DTO.OrderModels.Constracts;
using OnlineStore.DTO.ProductModels.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Providers.Contracts
{
    public interface IPDFExporter
    {
         void ExportProducts(IEnumerable<IProductModel> products);

         void ExportOrders(IEnumerable<IOrderModel> orders);
       
    }
}
