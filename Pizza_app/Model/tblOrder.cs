//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pizza_app.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblOrder()
        {
            this.tblMealOrders = new HashSet<tblMealOrder>();
        }
    
        public int OrderID { get; set; }
        public System.DateTime DateOfOrder { get; set; }
        public string OrdererPersonalNo { get; set; }
        public string IsApproved { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblMealOrder> tblMealOrders { get; set; }
    }
}