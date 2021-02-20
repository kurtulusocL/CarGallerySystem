﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CarGallerySystem.Data.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public async Task<ClaimsIdentity> GenereteUserIdentityIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public ApplicationUser()
        {
            EmailConfirmed = true;
        }

        public string NameLastname { get; set; }
        public string Birthdate { get; set; }
        public string RespondTitle { get; set; }
    }
}