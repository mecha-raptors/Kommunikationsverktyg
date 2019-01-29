using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models.ViewModels
{
    public class ListUsersByRoleViewModel
    {
        public List<ApplicationUser> Users { get; set; }
    }
}