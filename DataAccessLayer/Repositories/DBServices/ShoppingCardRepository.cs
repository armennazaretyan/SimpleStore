using DataAccessLayer.EntityModel;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.DBServices
{
    public class ShoppingCardRepository : GenericRepository<ShoppingCard>, IShoppingCardRepository
    {
        public SimpleStoreEntities SimpleStoreContext
        {
            get { return context as SimpleStoreEntities; }
        }

        public ShoppingCardRepository(SimpleStoreEntities context)
            : base(context)
        {

        }

        public IEnumerable<ShoppingCard> GetByUserID(int userID)
        {
            return SimpleStoreContext.ShoppingCards.Where(w => w.UserID == userID && w.OrderID == null).ToList();
        }

        public bool IncreaseProductCount(ShoppingCard shoppingCard)
        {
            ShoppingCard _shoppingCard = SimpleStoreContext.ShoppingCards.Where(w => w.UserID == shoppingCard.UserID && w.ProductID == shoppingCard.ProductID && w.OrderID == null).SingleOrDefault();
            if (_shoppingCard == null)
            {
                // Add new
                _shoppingCard = new ShoppingCard();
                _shoppingCard.UserID = shoppingCard.UserID;
                _shoppingCard.ProductID = shoppingCard.ProductID;
                _shoppingCard.Quantity = 1;

                SimpleStoreContext.ShoppingCards.Add(_shoppingCard);
            }
            else
            {
                // Increase count
                _shoppingCard.Quantity += 1;
            }

            return true;
        }

        public bool DecreaseProductCount(ShoppingCard shoppingCard)
        {
            ShoppingCard _shoppingCard = SimpleStoreContext.ShoppingCards.Where(w => w.UserID == shoppingCard.UserID && w.ProductID == shoppingCard.ProductID && w.OrderID == null).SingleOrDefault();
            if (_shoppingCard == null)
            {
                throw new Exception("There is no such product in card");
            }
            else
            {
                // Increase count
                if (_shoppingCard.Quantity == 1)
                {
                    // Remove
                    this.Delete(_shoppingCard);
                }
                else
                {
                    _shoppingCard.Quantity -= 1;
                }
            }

            return true;
        }

        public bool RemoveProductFromShoppingCard(int userID, int productID)
        {
            ShoppingCard _shoppingCard = SimpleStoreContext.ShoppingCards.Where(w => w.UserID == userID && w.ProductID == productID && w.OrderID == null).SingleOrDefault();
            if (_shoppingCard != null)
            {
                this.Delete(_shoppingCard);
            }

            return true;
        }

    }
}
