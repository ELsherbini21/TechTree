﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTree.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //public string Address1 { get; set; }

        public bool IsAgree { get; set; }




        public virtual ICollection<UserCategory> UserCategories { get; set; } = new List<UserCategory>();



    }
}
