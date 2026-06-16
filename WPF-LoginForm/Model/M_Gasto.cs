using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Model
{
    public class M_Gasto
    {
        
            public int IdGastos { get; set; }

            public string Descripcion { get; set; }

            public decimal Monto { get; set; }

            public int IdUsuario { get; set; }

            public int IdCategoria { get; set; }

            public DateTime Fecha { get; set; }
        
    }
}
