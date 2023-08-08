using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nurbnb.Pagos.Domain.Exceptions
{
    public class PagoCreacionException:Exception
    {
        public PagoCreacionException(string motivo)
            : base("La transacción no puede ser creada por que " + motivo)
        {
        }
    }
}
