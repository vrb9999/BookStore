using BusinessLayer.Interface;
using DatabaseLayer.Order;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class OrderBL : IOrderBL
    {
        IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public bool AddOrder(OrderModel orderModel)
        {
            try
            {
                return orderRL.AddOrder(orderModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetOrderModel> GetAllOrders(int UserId)
        {
            try
            {
                return orderRL.GetAllOrders(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteOrder(int UserId, int OrderId)
        {
            try
            {
                return orderRL.DeleteOrder(UserId, OrderId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
