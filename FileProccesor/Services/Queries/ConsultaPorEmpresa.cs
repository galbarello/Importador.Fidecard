using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using FileProccesor.Dtos;

namespace FileProccesor.Services.Queries
{
    public class ConsultaPorEmpresa 
    {
        private readonly string _dni;
        private readonly int _empresa;

        public ConsultaPorEmpresa(string cuit, int empresa)
        {
            _empresa = empresa;
            _dni = cuit;
        }

        public IEnumerable<ClienteDto> Execute()
        {
            return ActiveRecordBase<ClienteDto>.FindAll();
        }
    }
}