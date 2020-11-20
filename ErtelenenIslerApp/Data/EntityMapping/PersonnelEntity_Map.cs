using FluentNHibernate.Mapping;

namespace ErtelenenIslerApp.Data.EntityMapping
{
    public class PersonnelEntity_Map : ClassMap<PersonnelEntity>
    {
        public PersonnelEntity_Map()
        {
            Table("TR_PERSONNEL");
            Id(x => x.PersonnelCode).Column("PER_CODE");
            Id(x => x.PersonnelDesc).Column("PER_DESC");
            Map(x => x.CustomerNumber).Column("CUSTOMER_NUMBER");
            Map(x => x.AccountCode).Column("ACCOUNT_CODE");
            Map(x => x.AccountDesc).Column("ACCOUNT_DESC");
            Map(x => x.Salary).Column("SALARY");
            Map(x => x.Currency).Column("CURRENCY");
            Map(x => x.IsPaid).Column("ISPAID");
        }
    }
}
