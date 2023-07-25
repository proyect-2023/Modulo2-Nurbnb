using Nurbnb.Pagos.Domain.Events;
using Nurbnb.Pagos.Domain.Exceptions;
using Restaurant.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nurbnb.Pagos.Domain.Model.Pagos
{
    public class Pago: AggregateRoot
    {
        public DateTime Fecha { get; private set; }
        public DateTime? FechaCancelacion { get; private set; }
        public Guid ReservaId { get; private set; }
        public EstadoPago Estado { get; private set; }
        public string CuentaOrigen { get; private set; }
        public string CuentaDestino { get; private set; }
        public string BcoOrigen { get; private set; }
        public string BcoDestino { get; private set; }

        private readonly ICollection<DetallePago> _detalle;

        public IEnumerable<DetallePago> Detalle => _detalle;

        internal Pago(Guid reservaId, string cuentaOrigen, string cuentaDestino, string bcoOrigen,string bcoDestino)
        {
            Estado = EstadoPago.Registrado;
            ReservaId = reservaId;
            Fecha = DateTime.Now;
            CuentaDestino = cuentaDestino;
            CuentaOrigen= cuentaOrigen;
            BcoDestino = bcoDestino;
            BcoOrigen = bcoOrigen;
            _detalle = new List<DetallePago>();
        }
        public void AgregarDetallePago(Guid catalogoId, int porcentaje, decimal importe)
        {
            if (_detalle.Any(item => item.CatalogoId == catalogoId))
            {
                throw new CatalogoExitsException();
            }
            DetallePago detallePago = new DetallePago(catalogoId, porcentaje, importe);

            _detalle.Add(detallePago);
        }
        public void Confirmar()
        {
            if (Estado != EstadoPago.Registrado)
            {
                throw new EstadoNotFoundException();
            }
            Estado = EstadoPago.Procesado;

            List<PagoConfirmado.DetallePagoConfirmado> detallePago =
               _detalle.Select(item => new PagoConfirmado.DetallePagoConfirmado(item.CatalogoId, item.Porcentaje, item.Total))
               .ToList();

            PagoConfirmado evento = new PagoConfirmado(Id, detallePago);
            AddDomainEvent(evento);
        }
        public void Cancelar()
        {
            if (Estado != EstadoPago.Registrado)
            {
                throw new EstadoNotFoundException();
            }
            Estado = EstadoPago.Cancelado;
            FechaCancelacion = DateTime.Now;

            List<PagoCancelado.DetallePagoCancelado> detallePago =
               _detalle.Select(item => new PagoCancelado.DetallePagoCancelado(item.CatalogoId, item.Porcentaje, item.Total))
               .ToList();

            PagoCancelado evento = new PagoCancelado(Id, detallePago);
            AddDomainEvent(evento);
        }

    }
}
