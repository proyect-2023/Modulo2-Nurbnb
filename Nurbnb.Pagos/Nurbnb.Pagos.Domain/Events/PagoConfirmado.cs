using Restaurant.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nurbnb.Pagos.Domain.Events
{
    public record PagoConfirmado : DomainEvent
    {
        public Guid PagoId { get; init; }
        public ICollection<DetallePagoConfirmado> Detalle { get; init; }
        public PagoConfirmado(Guid pagoId,
            ICollection<DetallePagoConfirmado> detalle) : base(DateTime.Now)
        {
            PagoId = pagoId;
            Detalle = detalle;
        }

        public record DetallePagoConfirmado(Guid catalogoId, int porcentaje, decimal totalImporte);
    }
}
