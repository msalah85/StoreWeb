using System;
using System.Collections.Generic;
using System.Text;

namespace Hasseeb.Application.ViewModels
{
    public class AccountViewModel
    {
        public int ID { get; set; }
        public Nullable<int> AccountNatureID { get; set; }
        public Nullable<int> ParentAccountID { get; set; }
        public string AccountSerial { get; set; }
        public string AccountNatureName { get; set; }
        public string ParentAccountName { get; set; }
        public string AccountName { get; set; }
        public string AccountDesc { get; set; }
        public int GroupOrder { get; set; }
        public bool Active { get; set; }
        public bool IsMain { get; set; }

    }
}
