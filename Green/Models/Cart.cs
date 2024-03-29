﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int Count { get; set; }
        public int quantity { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}