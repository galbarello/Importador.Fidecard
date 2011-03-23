using System;
using Castle.ActiveRecord;

namespace FileProccesor.Keys
{
    [Serializable]
    public class KeyCliente
    {
        /// <summary>
        /// solo para usar castle activerecord
        /// </summary>
        public KeyCliente(){}

        public KeyCliente(int empresa, string documento,int cuenta)
        {
            CodEmpresa = empresa;
            NumeroDocumento = documento;
            NroCuenta = cuenta;
        }

        [KeyProperty(Column = "CodEmpresa")]
        public int CodEmpresa{ get; set;}

        [KeyProperty(Column = "NroCuenta")]
        public int NroCuenta { get; set;}	  	  

        [KeyProperty(Column = "NroDoc",Length = 9)]
        public string NumeroDocumento{ get; set;}

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (KeyCliente) && Equals((KeyCliente) obj);
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