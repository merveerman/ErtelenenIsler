using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErtelenenIslerApp.Data
{
    public class PersonnelEntity
    {
        public virtual string PersonnelCode { get; set; }
        public virtual string PersonnelDesc { get; set; }
        public virtual int CustomerNumber { get; set; }
        public virtual string AccountCode { get; set; }
        public virtual string AccountDesc { get; set; }
        public virtual int Salary { get; set; }
        public virtual string Currency { get; set; }
        public virtual bool IsPaid { get; set; }
    }
}
