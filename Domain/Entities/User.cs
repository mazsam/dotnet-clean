using VendorBoilerplate.Domain.Infrastructures;
using System;
using System.Collections.Generic;

namespace VendorBoilerplate.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { set; get; } = string.Empty;
        public string Notes { set; get; }  = string.Empty;
        public string Phone { set; get; }  = string.Empty;
        public string UserName { set; get; } = string.Empty;
    }
}