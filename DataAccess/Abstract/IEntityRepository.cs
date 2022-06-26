using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    // class: Referans tip
    // IEntity: IEntity ya da onu implement eden bir class olmalı.
    // new(): o tip üzerinden belirli bir obje yaratılabilmeli.
    public interface IEntityRepository<T> where T : class, IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>>? filter = null);
        T Get(Expression<Func<T,bool>> filter);
        void Add(T type);
        void Update(T type);
        void Delete(T type);
    }
}
