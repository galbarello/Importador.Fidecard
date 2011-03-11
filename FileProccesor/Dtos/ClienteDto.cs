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

        public ClienteDto(KeyCliente cliente,Parentesco parentesco,int maxExtension)
        {
            Key = cliente;
            CodParentesco = parentesco;
            CodExtension = maxExtension;
        }

        [CompositeKey]
        public KeyCliente Key{ get; set;}
        
		[Property("CodParentesco")]
  	    public Parentesco CodParentesco { get; set;}
        
        [Property("CodExtension")]
        public int CodExtension{ get; set;}
	  	  	 
        [Property("fechaBaja")]
        public DateTime? FechaBaja	{ get; set;}
	  
        [Property("CodMotivoBaja")]
        public int? MotivoBaja{ get; set;}
	  }
}