using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OrderMetadata
    {
        [DisplayName("價格")]
        [Required(ErrorMessage = "價格不可以空白 !")]
        [Range(0, 100000, ErrorMessage = "價格必須大於等於零 !")]
        public Nullable<int> Price { get; set; }

        [Required(ErrorMessage = "請輸入日期 !")]
        public Nullable<System.DateTime> OrderDate { get; set; }
    }

    [MetadataType(typeof(OrderMetadata))]
    public partial class Order
    {
    }

}