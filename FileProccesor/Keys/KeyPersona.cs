using System;
using Castle.ActiveRecord;

namespace FileProccesor.Keys
{
    [Serializable]
    public class KeyPersona : IEquatable<KeyPersona>
    {
        /// <summary>
        /// solo para usar castle activerecord
        /// </summary>
        public KeyPersona(){}

        public KeyPersona(int empresa, string documento, string tipo)
        {
            EmpresaPersona = empresa;
            NroDocumento = documento;
            TipoDocumento = tipo;
        }

        [KeyProperty(Column = "NroDocumento", Length =9)]
        public string NroDocumento { get; set; }

        [KeyProperty(Column = "CodEmpresa")]
        public int EmpresaPersona { get; set; }

        [KeyProperty(Column = "TipoDocumento",Length = 4)]
        public string TipoDocumento{get; set;}

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (KeyPersona) && Equals((KeyPersona) obj);
        }

        /// <summary>
        /// Indica si el objeto actual es igual a otro objeto del mismo tipo.
        /// </summary>
        /// <returns>
        /// true si el objeto actual es igual al parámetro other; en caso contrario, false.
        /// </returns>
        /// <param name="other">Objeto que se va a comparar con este objeto.</param>
        public bool Equals(KeyPersona other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.NroDocumento, NroDocumento) && other.EmpresaPersona == EmpresaPersona && Equals(other.TipoDocumento, TipoDocumento);
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
                var result = (NroDocumento != null ? NroDocumento.GetHashCode() : 0);
                result = (result*397) ^ EmpresaPersona;
                result = (result*397) ^ (TipoDocumento != null ? TipoDocumento.GetHashCode() : 0);
                return result;
            }
        }

        public static bool operator ==(KeyPersona left, KeyPersona right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(KeyPersona left, KeyPersona right)
        {
            return !Equals(left, right);
        }
    }
}