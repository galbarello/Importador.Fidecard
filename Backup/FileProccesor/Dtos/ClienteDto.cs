using System;
using Castle.ActiveRecord;
using FileProccesor.Keys;

namespace FileProccesor.Dtos
{
    [ActiveRecord("Clientes")]
    public class ClienteDto:ActiveRecordBase<ClienteDto>
    {
        /// <summary>
        /// solo para usar castle activerecord
        /// </summary>
        public ClienteDto(){}

        public ClienteDto(KeyCliente cliente,ParentescoDto parentesco,int maxExtension)
        {
            Key = cliente;
            Parentesco = parentesco;
            CodExtension = maxExtension;
        }

        [CompositeKey]
        public KeyCliente Key{ get; set;}
        
		[BelongsTo("CodParentesco")]
  	    public ParentescoDto Parentesco { get; set;}
        
        [Property("CodExtension")]
        public int CodExtension{ get; set;}
	  	  	 
        [Property("fechaBaja")]
        public DateTime? FechaBaja	{ get; set;}
	  
        [Property("CodMotivoBaja")]
        public int? MotivoBaja{ get; set;}
	  }
}