using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }

        public string Nombre { get; set; }

        public decimal PrecioUnitario { get; set; }

        public byte Stock { get; set; }

        public ML.Proveedor proveedor { get; set; }

        public ML.Departamento departamento { get; set; }

        public string Descripcion { get; set; }

        public List<object> Productos { get; set; }

        public byte[] Imagen { get; set; }       

    }
}
