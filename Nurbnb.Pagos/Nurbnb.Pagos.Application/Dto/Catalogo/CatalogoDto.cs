using Nurbnb.Pagos.Domain.Model.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nurbnb.Pagos.Application.Dto.Catalogo
{
    public class CatalogoDto
    {
        public Guid CatalogoId { get; set; }
        public string Descripcion { get;  set; }
        public int Porcentaje { get;  set; }
        public int EsReserva { get;  set; }
    }
}
