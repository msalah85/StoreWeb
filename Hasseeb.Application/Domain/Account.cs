using System;
using System.Collections.Generic;
using System.Text;

namespace Hasseeb.Application.Domain
{
    public class Account:BaseObject
    {
        public Nullable<int> AccountNature { get; set; }
        public Nullable<int> ParentAccount { get; set; }
        public string AccountSerial { get; set; }
        public string AccountName { get; set; }
        public string AccountDesc { get; set; }
        public int GroupOrder { get; set; }
        public bool Active { get; set; }
        public System.DateTime AddDate { get; set; }
        public bool IsMain { get; set; }

        //public virtual AccountNature AccountNature { get; set; }
    }
}
