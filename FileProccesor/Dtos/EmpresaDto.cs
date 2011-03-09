using System;
using Castle.ActiveRecord;
using FileProccesor.Dtos.ValueTypes;

namespace FileProccesor.Dtos
{
    [ActiveRecord("Empresas")]
    public class EmpresaDto : ActiveRecordBase<EmpresaDto>
    {
        [PrimaryKey(Column ="codempresa")]
        public int Id{ get; set;} 	  
    
        [Property("Razon_social",Length = 50)]
        public string RazonSocial{ get; set;}

        [Nested]
        public Direccion DireccionEmpresa { get; set; }
        
        [Property("Telefono",Length = 14)]
        public string Telefono { get; set;}
	    
        [Property("fax",Length = 14)]
        public string Fax{ get; set;}
		
        [BelongsTo("CodActividad")]
        public ActividadDto ActividadEmpresa{ get; set;}
        
        [Nested]
        public Contacto ContactoEmpresa{ get; set;}

	  	[Property("FechaBaja")]
        public DateTime? FechaBaja{ get; set;}
		
        [Property("CodMotivoBaja")]
        public int CodBaja{ get; set;}
		 
	  	[Property("Moroso")]
        public bool Moroso { get; set;}

        [Property("TopeImporte")]
        public int Tope { get; set; }
    }
}