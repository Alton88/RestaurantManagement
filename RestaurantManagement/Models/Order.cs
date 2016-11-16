using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Models
{
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey("Starter")]
        public int StarterId { get; set; }
        public Starter Starter { get; set; }
        [ForeignKey("Entree")]
        public int EntreeId { get; set; }
        public Entree Entree { get; set; }
        public decimal Total { get; set; }
        public string Type { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey("WaitStaff")]
        public int WaitStaffId { get; set; }
        public WaitStaff WaitStaff { get; set; }
        [ForeignKey("Driver")]
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}