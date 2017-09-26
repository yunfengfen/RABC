﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RABC.ViewModel
{
    public class RoleModuleViewModel
    {
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int ModuleId { get; set; }
        public int UpdateModuleId { get; set; }
        public string RoleName { get; set; }
        public string ModuleName { get; set; }
    }
}