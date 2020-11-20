using ErtelenenIslerApp.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErtelenenIslerApp.Data.Repository
{
    public class RPersonnelEntity : Repository<PersonnelEntity>
    {
        public PersonnelEntity Get(string code)
        {
            try
            {
                using (var session = SessionFactory.GetFactory().OpenSession())
                {
                    
                    string query = String.Format("select * from TR_PERSONNEL Where CUSTOMER_NUMBER = '{0}'", code);
                    var list = session.CreateSQLQuery(query).AddEntity(typeof(PersonnelEntity));
                    var listObject = list.List<PersonnelEntity>();

                    PersonnelEntity selectedObjetc = null;
                    selectedObjetc = listObject.FirstOrDefault();
                    return selectedObjetc;

                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("PersonnelEntity.Get.Err : {0}", e.Message), e);
            }
        }
    }
}
