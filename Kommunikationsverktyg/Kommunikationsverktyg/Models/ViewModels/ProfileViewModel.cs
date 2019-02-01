using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models.ViewModels
{
    public class ProfileViewModel
    {
        public ApplicationUser ApplicationUser {get; set;}

        public RegisterViewModel RegisterViewModel { get; set; }
    }
}