using System;
using Castle.ActiveRecord;

namespace FileProccesor.Keys
{
    [Serializable]
    public class KeyCuenta
    {
        [KeyProperty(Column = "CodEmpresa")]
        public int CodEmpresa { get; set; }

        [KeyProperty(Column = "nrocomprobante", Length = 50)]
        public string NumeroComprobante { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (KeyCuenta) && Equals((KeyCuenta) obj);
        }

        public bool Equals(KeyCuenta other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.CodEmpresa == CodEmpresa && Equals(other.NumeroComprobante, NumeroComprobante);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (CodEmpresa*397) ^ (NumeroComprobante != null ? NumeroComprobante.GetHashCode() : 0);
            }
        }
    }
}