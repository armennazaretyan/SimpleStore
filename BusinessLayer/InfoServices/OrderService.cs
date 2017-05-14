using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace BusinessLayer.InfoServices
{
    public class OrderService : BaseService, IOrderService
    {
        public bool Add(OrderModel order, int userID)
        {
            bool retVal = false;

            try
            {
                var lShoppingCardModelDB = _UnitOfWork.ShoppingCard.GetByUserID(userID);

                // order only if we have selected products in card
                if (lShoppingCardModelDB.Count() > 0)
                {
                    decimal totalPrice = 0;
                    foreach (var item in lShoppingCardModelDB)
                    {
                        DataAccessLayer.EntityModel.Product product = _UnitOfWork.Products.GetDetails(item.ProductID);
                        totalPrice += product.Price * item.Quantity;
                    }


                    DataAccessLayer.EntityModel.Order dbOrder = new DataAccessLayer.EntityModel.Order()
                    {
                        FirstName = order.FirstName,
                        LastName = order.LastName,
                        Address = order.Address,
                        City = order.City,
                        State = order.State,
                        PostalCode = order.PostalCode,
                        Country = order.Country,
                        Phone = order.Phone,
                        Email = order.Email,
                        Price = totalPrice
                    };
                    _UnitOfWork.Orders.Insert(dbOrder);

                    _UnitOfWork.Complete();
                    int orderID = dbOrder.ID;

                    foreach (var item in lShoppingCardModelDB)
                    {
                        item.OrderID = orderID;
                    }

                    _UnitOfWork.Complete();
                }

                retVal = true;
            }
            catch (Exception ex)
            {
                retVal = false;
            }

            return retVal;
        }

        public bool ChangeCountInShoppingCard(ProductModel product, int userID, bool isIncrease)
        {
            bool retVal = false;
            try
            {
                if (isIncrease)
                {
                    _UnitOfWork.ShoppingCard.IncreaseProductCount(new DataAccessLayer.EntityModel.ShoppingCard()
                    {
                        ProductID = product.ID,
                        UserID = userID
                    });
                }
                else
                {
                    _UnitOfWork.ShoppingCard.DecreaseProductCount(new DataAccessLayer.EntityModel.ShoppingCard()
                    {
                        ProductID = product.ID,
                        UserID = userID
                    });
                }

                _UnitOfWork.Complete();

                retVal = true;
            }
            catch (Exception ex)
            {
                retVal = false;
            }

            return retVal;
        }

        public IEnumerable<OrderModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShoppingCardModel> GetUserShoppingCard(int userID)
        {
            var shoppingCardDB = _UnitOfWork.ShoppingCard.GetByUserID(userID);
            List<ShoppingCardModel> lShoppingCardModel = new List<ShoppingCardModel>();

            foreach (var item in shoppingCardDB)
            {
                ShoppingCardModel model = new ShoppingCardModel()
                {
                    ID = item.ID,
                    OrderID = (item.OrderID.HasValue) ? item.OrderID.Value : 0,
                    UserID = item.UserID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity
                };

                lShoppingCardModel.Add(model);
            }

            return lShoppingCardModel;
        }

        public bool RemoveProductFromShoppingCard(int userID, int productID)
        {
            bool retVal = _UnitOfWork.ShoppingCard.RemoveProductFromShoppingCard(userID, productID);
            _UnitOfWork.Complete();

            return retVal;
        }

    }
}
