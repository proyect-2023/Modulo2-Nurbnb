using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nurbnb.Pagos.Domain.Model.MedioPagos
{
    public interface IMedioPago
    {
        bool ProcesarPago(string cuentaOrigen, string cuentaDestino, string bcoOrigen, string bcoDestino, decimal importe);
    }
}
