using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
    public class OrderService : IOrderService
    {
        public void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach (var items in basketItems)
            {
                baseOrder.OrderItems.Add(new OrderItem()
                {
                    ProductId = items.Id,
                    Image = items.Image,
                    Price = items.Price,
                    ProductName = items.ProductName,
                    Quantity = items.Quantity
                }
                );
            }

            orderContext.Insert(baseOrder);
            orderContext.Commit();
        }

        IRepository<Order> orderContext;
        public OrderService(IRepository<Order> OrderContext)
        {
            this.orderContext = OrderContext;
        }

        public List<Order> GetOrderList()
        {
            return orderContext.Collection().ToList();
        }

        public Order GetOrder(string Id)
        {
            return orderContext.Find(Id);
        }

        public void UpdateOrder(Order order)
        {
            orderContext.Update(order);
            orderContext.Commit();
        }
    }
}
