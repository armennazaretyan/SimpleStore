using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IOrderService
    {
        bool Add(OrderModel order, int userID);
        bool ChangeCountInShoppingCard(ProductModel product, int userID, bool isIncrease);
        IEnumerable<OrderModel> GetAll();
        IEnumerable<ShoppingCardModel> GetUserShoppingCard(int userID);
        bool RemoveProductFromShoppingCard(int userID, int productID);
    }
}
