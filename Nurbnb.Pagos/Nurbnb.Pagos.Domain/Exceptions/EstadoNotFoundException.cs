using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nurbnb.Pagos.Domain.Exceptions
{
    public class EstadoNotFoundException:Exception
    {
        public EstadoNotFoundException()
            : base("El pago no tiene el estado Registrado")
        {
        }
    }
}
