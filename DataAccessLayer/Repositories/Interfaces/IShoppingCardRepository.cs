using DataAccessLayer.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IShoppingCardRepository : IGenericRepository<ShoppingCard>
    {
        IEnumerable<ShoppingCard> GetByUserID(int userID);
        bool IncreaseProductCount(ShoppingCard shoppingCard);
        bool DecreaseProductCount(ShoppingCard shoppingCard);
        bool RemoveProductFromShoppingCard(int userID, int productID);
    }
}
