﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWEB.Models
{
    public class UserModel
    {
        public int Id { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
    }
}