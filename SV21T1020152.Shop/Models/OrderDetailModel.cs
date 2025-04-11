using SV21T1020152.DomainModels;

namespace SV21T1020152.Shop.Models
{
    /// <summary>
    /// Chi tiết đơn hàng
    /// </summary>
    public class OrderDetailModel
    {
        public required Order Order { get; set; }
        public required List<OrderDetail> Details { get; set; }
    }
}
