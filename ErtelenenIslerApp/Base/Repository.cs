using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErtelenenIslerApp.Base
{
    public abstract class Repository<T> : IRepository<T>, IDisposable
    {
        public object Query(string s)
        {
            object tmpr;
            try
            {
                using (var sess = SessionFactory.GetFactory().OpenSession())
                {
                    tmpr = sess.CreateSQLQuery(s).UniqueResult();
                    sess.Flush();
                    if (tmpr == null)
                        tmpr = "";
                }
            }
            catch (Exception e)
            {
                string innerException = "";
                if (e.InnerException != null)
                    innerException = e.InnerException.Message;
                throw new Exception(string.Format("{1}.Query.Err : {0} Exception Detail : {2}", e.Message, typeof(T).FullName, innerException), e);

                //   throw new Exception(string.Format("{1}.Query.Err : {0}", e.Message, typeof(T).FullName), e);
            }

            return tmpr;
        }

        public virtual T Save(T obj)
        {
            try
            {
                using (var sess = SessionFactory.GetFactory().OpenSession())
                {
                    sess.Save(obj);
                    sess.Flush();
                    return obj;
                }
            }
            catch (Exception e)
            {
                string triggerMessage = "";
                if (e.InnerException != null && e.InnerException.Message != null)
                {
                    if (e.InnerException.Message.Split('\n').Length > 0 && e.InnerException.Message.Split('\n')[0].Contains("ORA-20003"))
                    {
                        triggerMessage = e.InnerException.Message.Split('\n')[0].Replace("ORA-20003:  ", "");
                    }
                }

                if (string.IsNullOrEmpty(triggerMessage))
                {
                    throw new Exception(string.Format("{2}.Save.Err : {0} Data : {1}", e.Message, ExeptionDataBuilder<T>.DataBuilder(obj), typeof(T).FullName), e);
                }
                else
                {
                    throw new Exception(triggerMessage);
                }
            }
        }

        public T Update(T obj)
        {
            try
            {
                using (var sess = SessionFactory.GetFactory().OpenSession())
                {
                    sess.Update(obj);
                    sess.Flush();
                    return obj;
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("{2}.Update.Err : {0} Data : {1}", e.Message, ExeptionDataBuilder<T>.DataBuilder(obj), typeof(T).FullName), e);
            }
        }

        public T SaveOrUpdate(T obj)
        {
            try
            {
                using (var sess = SessionFactory.GetFactory().OpenSession())
                {
                    sess.SaveOrUpdate(obj);
                    sess.Flush();
                    return obj;
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("{2}.SaveOrUpdate.Err : {0} Data : {1}", e.Message, ExeptionDataBuilder<T>.DataBuilder(obj), typeof(T).FullName), e);
            }
        }

        public void Delete(T obj)
        {
            try
            {
                using (var sess = SessionFactory.GetFactory().OpenSession())
                {
                    sess.Delete(obj);
                    sess.Flush();
                }
            }
            catch (Exception e)
            {
                //throw new Exception(string.Format("{2}.Delete.Err : {0} Data : {1}", e.Message, ExeptionDataBuilder<T>.DataBuilder(obj), typeof(T).FullName), e);
            }
        }

        public T Get(object id)
        {
            try
            {
                using (var sess = SessionFactory.GetFactory().OpenSession())
                {
                    return sess.Get<T>(id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("{1}.Get.Err : {0}", e.Message, typeof(T).FullName), e);
            }
        }

        public IList<T> GetCustomQuery(string hql)
        {
            try
            {
                using (var sess = SessionFactory.GetFactory().OpenSession())
                {
                    var obj = sess.CreateSQLQuery(hql);
                    obj.AddEntity(typeof(T).Name, typeof(T));
                    return obj.List<T>();
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("{1}.GetCustomQuery.Err : {0}", e.Message, typeof(T).FullName), e);
            }
        }

        public IList<object> GetCustomQuery2(string hql)
        {
            try
            {
                using (var sess = SessionFactory.GetFactory().OpenSession())
                {
                    var obj = sess.CreateSQLQuery(hql).List<object>();
                    return obj;
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Repository.GetCustomQuery2.Err : {0}", e.Message, typeof(T).FullName), e);
            }
        }

        public T GetCustomQuery3(string hql)
        {
            try
            {
                using (var sess = SessionFactory.GetFactory().OpenSession())
                {
                    var obj = sess.CreateSQLQuery(hql);
                    obj.AddEntity(typeof(T).Name, typeof(T));
                    return obj.List<T>().FirstOrDefault(); ;
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("{1}.GetCustomQuery.Err : {0}", e.Message, typeof(T).FullName), e);
            }
        }

        public IList<T> GetAll()
        {
            try
            {
                using (var session = SessionFactory.GetFactory().OpenSession())
                {
                    var cr = session.CreateCriteria(typeof(T));
                    return cr.List<T>();
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("{1}.GetAll.Err : {0}", e.Message, typeof(T).FullName), e);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
