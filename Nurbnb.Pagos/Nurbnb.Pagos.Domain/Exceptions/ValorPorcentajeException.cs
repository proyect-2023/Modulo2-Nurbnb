using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nurbnb.Pagos.Domain.Exceptions
{
    public class ValorPorcentajeException:Exception
    {
        public ValorPorcentajeException()
            : base("Valor de Porcentaje no puede ser mayor a 100")
        {
        }
    }
}
