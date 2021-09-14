//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DL_EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Venta
    {
        public Venta()
        {
            this.VentaProducto = new HashSet<VentaProducto>();
        }
    
        public int IdVenta { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> IdMetodoPago { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual MetodoPago MetodoPago { get; set; }
        public virtual ICollection<VentaProducto> VentaProducto { get; set; }
    }
}
