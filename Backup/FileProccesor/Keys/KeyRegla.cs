using System;
using Castle.ActiveRecord;

namespace FileProccesor.Keys
{
    [Serializable]
    public class KeyRegla : IEquatable<KeyRegla>
    {
         /// <summary>
        /// solo para usar castle activerecord
        /// </summary>
        public KeyRegla(){}

        public KeyRegla(int empresa, int sucursal,DateTime inicio)
        {
            CodEmpresa = empresa;
            Sucursal = sucursal;
            FechaInicio = inicio;
        }

        [KeyProperty(Column = "CodEmpresa")]
        public int CodEmpresa{ get; set;}

        [KeyProperty(Column = "CodSucursal")]
        public int Sucursal { get; set;}

        [KeyProperty(Column = "FechaInicio")]
        public DateTime FechaInicio { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (KeyRegla) && Equals((KeyRegla) obj);
        }

        /// <summary>
        /// Indica si el objeto actual es igual a otro objeto del mismo tipo.
        /// </summary>
        /// <returns>
        /// true si el objeto actual es igual al parámetro other; en caso contrario, false.
        /// </returns>
        /// <param name="other">Objeto que se va a comparar con este objeto.</param>
        public bool Equals(KeyRegla other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.CodEmpresa == CodEmpresa && other.Sucursal == Sucursal && other.FechaInicio.Equals(FechaInicio);
        }

        /// <summary>
        /// Sirve como función hash para un tipo concreto. <see cref="M:System.Object.GetHashCode"/> es apropiado para su utilización en algoritmos de hash y en estructuras de datos como las tablas hash.
        /// </summary>
        /// <returns>
        /// Código hash para la clase <see cref="T:System.Object"/> actual.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                var result = CodEmpresa;
                result = (result*397) ^ Sucursal;
                result = (result*397) ^ FechaInicio.GetHashCode();
                return result;
            }
        }

        public static bool operator ==(KeyRegla left, KeyRegla right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(KeyRegla left, KeyRegla right)
        {
            return !Equals(left, right);
        }
    }
}