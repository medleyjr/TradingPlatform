using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medley.Common.Defs.Interfaces
{
    public interface IRepository 
    {
        void Open(string config);
        void Close();
        void AddEntity<T>(T model) where T : class;
        void UpdateEntity<T>(T model) where T : class;
        T GetEntity<T>(long Id) where T : class, new();
        List<T> FindEnities<T>(ISearchParam searchParam) where T : class, new();
        List<T> FindAllEnities<T>() where T : class, new();
        List<T> FindEnitiesByField<T>(string fieldName, object fieldValue) where T : class, new();
    }
}
