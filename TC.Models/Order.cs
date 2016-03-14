using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Models
{
    public enum OrderStatus
    {
        WaitingPayment,
        Processing,
        Sent,
        Delivered
    }

    public class Order
    {
        public int Protocol { get; set; } 
        public Customer Customer { get; set; }
        public DateTime DateTime { get; set; }
        public List<OrderItem> Items { get; set; }
        public OrderStatus Status { get; set; }
    }
}
