using Nurbnb.Pagos.Domain.Events;
using Nurbnb.Pagos.Domain.Model.Pagos;
using Restaurant.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nurbnb.Pagos.Domain.Model.Devoluciones
{
    public class Devolucion : AggregateRoot
    {
        public DateTime Fecha { get; private set; }
        public Guid PagoId { get; private set; }
        public Guid CatalogoID { get; private set; }
        public DevolucionMotivo Motivo { get; private set; }
        public int Porcentaje { get; private set; }
        public decimal Importe { get; private set; }
        public decimal TotalDevolucion { get; private set; }

        internal Devolucion( Guid pagoId, Guid catalogoID, string motivo, int porcentaje,decimal importe)
        {
            Fecha = DateTime.Now;
            PagoId = pagoId;
            CatalogoID = catalogoID;
            Motivo = new DevolucionMotivo(motivo);
            Porcentaje = porcentaje;
            Importe = importe;
            TotalDevolucion = new DevolucionImporte(importe,porcentaje).CalculoDevolucion();
        }
        public void Confirmar()
        {
            if (TotalDevolucion == 0) return;

            DevolucionConfirmada.DetalleDevolucionConfirmada detalleDevolucion =
                new DevolucionConfirmada.DetalleDevolucionConfirmada(PagoId, CatalogoID, TotalDevolucion);


            DevolucionConfirmada evento = new DevolucionConfirmada(Id, detalleDevolucion);
            AddDomainEvent(evento);
        }
    }
}
