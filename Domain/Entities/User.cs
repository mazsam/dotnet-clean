using VendorBoilerplate.Domain.Infrastructures;
using System;
using System.Collections.Generic;

namespace VendorBoilerplate.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { set; get; }
        public string Notes { set; get; }
        public string Phone { set; get; }
        public string UserName { set; get; }
    }
}