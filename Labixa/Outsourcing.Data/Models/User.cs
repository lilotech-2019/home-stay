﻿using System.ComponentModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Outsourcing.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            DateCreated = DateTime.Now;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public bool Activated { get; set; }

        public Gender Gender { get; set; }

        public SystemRoles RoleId { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool Deleted { get; set; }
        public bool IsSubcribe{get;set;}
        public int promotionID { get; set; }

        public string DisplayName
        {
            get { return LastName + " " + FirstName; }
        }
    }
    public enum SystemRoles
    {
        [Description("Admin")]
        Role01 = 0,
        [Description("Sup")]
        Role02 = 1,
        [Description("Shipment")]
        Role03 = 2,
        [Description("User")]
        Role04 = 3
    }
    public enum Gender
    {
        [Description("Nam")]
        Male = 0,
        [Description("Nữ")]
        Female = 1
    }
}
