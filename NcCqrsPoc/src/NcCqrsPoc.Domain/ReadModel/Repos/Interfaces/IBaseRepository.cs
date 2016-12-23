using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.ReadModel.Repos.Interfaces
{
    interface IBaseRepository<T>
    {
        T GetByID(int id);
        List<T> GetMultiple(List<int> ids);
        bool Exist(int id);
        void Save(T item);
    }
}
