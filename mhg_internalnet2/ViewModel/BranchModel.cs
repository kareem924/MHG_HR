using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mhg_internalnet2.ViewModel
{
    public class BranchModel
    {
        [ScaffoldColumn(false)]
        public int StoreId { get; set; }
        [DisplayName("Store Name")]
        public string StoreName { get; set; }
        [DisplayName("Openning Time")]
        [DataType(DataType.Time)]
        public DateTime OpenningTime { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("Closing Time")]
        public DateTime ClosingTime { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Mobile")]
        public string MobilePhone { get; set; }
        [DisplayName("Land line")]
        public string Telephone { get; set; }
        public string Color { get; set; }
        [UIHint("GridForeignKey")]
        [DisplayName("Brand")]
        public int? BrandId { get; set; }
        [UIHint("GridForeignKey")]
        [DisplayName("Brand")]
        public BrandViewModel brand { get; set; }


    }
}