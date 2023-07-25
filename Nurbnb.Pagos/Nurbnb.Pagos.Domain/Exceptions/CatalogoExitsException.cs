using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nurbnb.Pagos.Domain.Exceptions
{
    public class CatalogoExitsException:
        Exception
    {
        public CatalogoExitsException()
            : base("Ya existe el catálogo en el detalle del pago")
        {
        }
    }
}
