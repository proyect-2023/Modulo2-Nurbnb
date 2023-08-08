using MediatR;
using Nurbnb.Pagos.Domain.Exceptions;
using Nurbnb.Pagos.Domain.Factories;
using Nurbnb.Pagos.Domain.Model.Catalogos;
using Nurbnb.Pagos.Domain.Model.Pagos;
using Nurbnb.Pagos.Domain.Repositories;
using Restaurant.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Nurbnb.Pagos.Application.UseCases.Pagos.Command.CrearPago
{
    internal class CrearPagoHandler : IRequestHandler<CrearPagoCommand, Guid>
    {
        private readonly IPagoFactory _pagoFactory;
        private readonly IPagoRepository _pagoRepository;
        private readonly ICatalogoRepository _catalogoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CrearPagoHandler(IPagoFactory pagoFactory, IPagoRepository pagoRepository, ICatalogoRepository catalogoRepository, IUnitOfWork unitOfWork)
        {
            _pagoFactory = pagoFactory;
            _pagoRepository = pagoRepository;
            _catalogoRepository = catalogoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CrearPagoCommand request, CancellationToken cancellationToken)
        {
            Pago pago = _pagoFactory.CrearPago((int)request.TipoPago, request.OperacionId, request.CostoTotalRenta, request.CuentaOrigen, request.BcoOrigen);
            

            foreach (var item in request.Detalle)
            {
                Catalogo? storedCatalogo = await _catalogoRepository.FindByIdAsync(item.CatalogoId);
                
                if (storedCatalogo == null)
                {
                    throw new PagoCreacionException(" El catálogo con ID: " + item.CatalogoId + " no existe");
                }

                pago.AgregarDetallePago(item.CatalogoId, storedCatalogo.Porcentaje, request.CostoTotalRenta);
            }


            pago.Confirmar();
           

            await _pagoRepository.CreateAsync(pago);


            await _unitOfWork.Commit();

            return pago.Id;
        }
    }
}
