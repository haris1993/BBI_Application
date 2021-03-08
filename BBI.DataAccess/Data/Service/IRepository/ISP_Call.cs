using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBI.DataAccess.Data.Service.IRepository
{
    public interface ISP_Call : IDisposable
    {
        IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters parameters = null);
        void ExecuteWithoutReturn(string procedureName, DynamicParameters parameters = null);
        T ExecuteReturnScaler<T>(string procedureName, DynamicParameters parameters = null);
    }
}
