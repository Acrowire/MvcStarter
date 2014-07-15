using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReportSpace.WebApplication.Models
{
    #region [ Users ] 
    public class CreateUserViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "You must enter a valid email address.")]
        public string Email { get; set; }
    }

    public class EditUserViewModel
    {
        public Guid PublicId { get; set; } 

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "You must enter a valid email address.")]
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel
    {

        public Guid PublicId { get; set; } 

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    #endregion


    #region [ Roles ]
    public class NewRoleViewModel
    {
        public String Name { get; set; } 
    }

    public class EditRoleViewModel
    {
        public String Name { get; set; }
        public Guid PublicId { get; set; }
        public bool Active { get; set; } 
    }
    #endregion

    #region [ User Roles ]
    public class CreateUserRoleViewModel
    {
        public Guid RolePublicId { get; set; }

        public Guid UserPublicId { get; set; } 

    }

    public class EditUserRoleViewModel
    {
        public Int32 Id { get; set; }  
        public Guid RolePublicId { get; set; }

        public Guid UserPublicId { get; set; }

        public bool Active { get; set; }  
    }
    #endregion
}