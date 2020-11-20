using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErtelenenIslerApp.Base
{
    public interface IRepository<T>
    {
        object Query(string s);
        T Save(T obj);
        T Update(T obj);
        T SaveOrUpdate(T obj);
        void Delete(T obj);
        T Get(object id);
        IList<T> GetCustomQuery(string hql);
        IList<object> GetCustomQuery2(string hql);
        IList<T> GetAll();
    }
}
