using WebGoatCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading;

namespace WebGoatCore.Data
{
    public class OrderRepository
    {
        private readonly NorthwindContext _context;
        private readonly CustomerRepository _customerRepository;

        public OrderRepository(NorthwindContext context, CustomerRepository customerRepository)
        {
            _context = context;
            _customerRepository = customerRepository;
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Single(o => o.OrderId == orderId);
        }

        public int CreateOrder(Order ord)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            string shippedDate = order.ShippedDate.HasValue ? "'" + string.Format("yyyy-MM-dd", order.ShippedDate.Value) + "'" : "NULL";
            var sql = "INSERT INTO Orders (" +
                "CustomerId, EmpId, ConfirmationDate, RequiredDate, ShippedDate, ShippingMethod, Freight, ShipName, ShipAddress, " +
                "City, Region, ZipCode, Country" +
                ") VALUES (" +
                $"'{ord.CustomerId}','{ord.EmpId}','{ord.OrderDate:yyyy-MM-dd}','{ord.RequiredDate:yyyy-MM-dd}'," +
                $"{ord.shippedDate},'{ord.ShippingMethod}','{ord.Freight}','{ord.ShipName}','{ord.ShipAddress}'," +
                $"'{ord.City}','{ord.Region}','{ord.ZipCode}','{ord.Country}')";
            sql += ";\nSELECT OrderID FROM Orders ORDER BY OrderID DESC LIMIT 1;";

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                _context.Database.OpenConnection();

                using var dataReader = command.ExecuteReader();
                dataReader.Read();
                ord.OrderId = Convert.ToInt32(dataReader[0]);
            }

            return ord.OrderId;
        }

        public void CreateOrderPayment(int orderId, decimal amountPaid, string creditCardNumber, DateTime expirationDate, string approvalCode)
        {
            var orderPayment = new OrderPayment()
            {
                AmountPaid = Convert.ToDouble(amountPaid),
                CreditCardNumber = creditCardNumber,
                ApprovalCode = approvalCode,
                ExpirationDate = expirationDate,
                OrderId = orderId,
                PaymentDate = DateTime.Now
            };
            _context.OrderPayments.Add(orderPayment);
            _context.SaveChanges();
        }

        public ICollection<Order> GetAllOrdersByCustomerId(string customerId)
        {
            return _context.Orders
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.OrderDate)
                .ThenByDescending(o => o.OrderId)
                .ToList();
        }
    }
}