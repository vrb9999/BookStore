using DatabaseLayer.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrderBL
    {
        public bool AddOrder(OrderModel orderModel);
        public List<GetOrderModel> GetAllOrders(int UserId);
        public bool DeleteOrder(int UserId, int OrderId);
    }
}
