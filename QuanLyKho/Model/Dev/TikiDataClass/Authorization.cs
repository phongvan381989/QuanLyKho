﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiDataClass
{
    /// <summary>
    /// Chứa thông tin ủy quyền connect tới shop Tiki
    /// </summary>
    public class Authorization
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string scope { get; set; }
        public string token_type { get; set; }
    }
}