using Restaurant.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nurbnb.Pagos.Domain.Model.MedioPagos
{
    public class MedioPago:AggregateRoot
    {
        private readonly IMedioPago _medioPago;
        public string CuentaOrigen { get; private set; }
        public string CuentaDestino { get; private set; }
        public string BcoOrigen { get; private set; }
        public string BcoDestino { get; private set; }
        public decimal Importe { get; private set; }

        public MedioPago(string cuentaOrigen, string cuentaDestino, string bcoOrigen, string bcoDestino, decimal importe)
        {
            CuentaOrigen = cuentaOrigen;
            CuentaDestino= cuentaDestino;
            BcoDestino = bcoDestino;
            BcoOrigen= bcoOrigen;
            Importe = importe;
            _medioPago = CrearMedioPago();
        }
        protected virtual IMedioPago CrearMedioPago() { return new MedioPagoPaypal(); }
        public void Pagar(string cuentaOrigen, string cuentaDestino, string bcoOrigen, string bcoDestino, decimal importe)
        {
            bool procesar= _medioPago.ProcesarPago(cuentaOrigen,cuentaDestino,bcoOrigen,bcoDestino,importe);
            
        }

    }
}
