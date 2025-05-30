﻿using Microsoft.AspNetCore.Mvc.Rendering;
using SV21T1020152.Shop.Models;
using SV21T1020152.DomainModels;

namespace SV21T1020152.Shop.Models
{
    /// <summary>
    /// Đầu ra tìm kiếm đơn hàng
    /// </summary>
    public class OrderSearchOutput : PaginationSearchOutput<Order>
    {
        public int Status { get; set; } = 0;
        public string DateRange { get; set; } = "";
    }
}