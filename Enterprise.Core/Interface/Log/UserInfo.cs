﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Core.Interface.Log
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Ip { get; set; }
        public string Hostname { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Loc { get; set; }
        public string Org { get; set; }
        public string Postal { get; set; }
        public string Phone { get; set; }
        public string UserEmail { get; set; }
    }
}
