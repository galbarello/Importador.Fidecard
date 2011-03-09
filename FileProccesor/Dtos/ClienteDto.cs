using System;
using Castle.ActiveRecord;

namespace FileProccesor.Dtos
{
    [ActiveRecord("Clientes")]
    public class ClienteDto:ActiveRecordBase<ClienteDto>
    {
        [CompositeKey]
        public KeyCliente Key{ get; set;}
        
		[Property("CodParentesco")]
  	    public int CodParentesco { get; set;}
        
        [Property("CodExtension")]
        public int CodExtension{ get; set;}
	  	  	 
        [Property("fechaBaja")]
	  	  public DateTime FechaBaja	{ get; set;}
	  
        [Property("CodMotivoBaja")]
        public int MotivoBaja{ get; set;}
	  }
    [Serializable]
    public class KeyCliente
    {
        [KeyProperty(Column = "CodEmpresa")]
        public int CodEmpresa{ get; set;}

        [KeyProperty(Column = "NroCuenta")]
 	   public int NroCuenta	 { get; set;}	  	  

        [KeyProperty(Column = "NroDoc",Length = 9)]
        public string NumeroDocumento{ get; set;}

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (KeyCliente)) return false;
            return Equals((KeyCliente) obj);
        }

        public bool Equals(KeyCliente other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.CodEmpresa == CodEmpresa && other.NroCuenta == NroCuenta && Equals(other.NumeroDocumento, NumeroDocumento);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = CodEmpresa;
                result = (result*397) ^ NroCuenta;
                result = (result*397) ^ (NumeroDocumento != null ? NumeroDocumento.GetHashCode() : 0);
                return result;
            }
        }
    }
}