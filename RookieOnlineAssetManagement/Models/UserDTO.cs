using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Xml.Linq;

namespace RookieOnlineAssetManagement.Models
{
    public class UserDTO : IdentityUser
    {
        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        
        [Display(Name = "DateofBirth")]
        [Required(ErrorMessage = "DateofBirth is required")]
        public DateTime DateofBirth { get; set; }

        [Display(Name = "JoinedDay")]
        [Required(ErrorMessage = "JoinedDay is required")]
        public DateTime JoinedDay { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type is required")]
        public UserType Type { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }
        public string Location { get; set; } = null!;
        public string StaffCode { get; set; }

        public HttpStatusCode StatusCode { get; internal set; }
    }
}

