using BBI.DataAccess.Data.Service.IRepository;
using BBI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBI.DataAccess.Data.Service.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void ChangeOrderStatus(int orderHeaderId, string status)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(o => o.Id == orderHeaderId);
            orderFromDb.Status = status;
            _db.SaveChanges();
        }
       
    }
}
