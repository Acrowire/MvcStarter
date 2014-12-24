using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acrowire.WebApplication.Models
{
    public class NewOrgViewModel
    {
        public String Name { get; set; } 
    }

    public class EditOrgViewModel
    {
        public Guid PublicId { get; set; }

        public String Name { get; set; }

        public bool Active { get; set; }  
    }

    public class CreateOrgUserViewModel
    {
        public Guid OrganizationPublicId { get; set; }

        public Guid UserPublicId { get; set; }  
    }

    public class EditOrgUserViewModel
    {
        public Int32 Id { get; set; }  
        public Guid OrganizationPublicId { get; set; }

        public Guid UserPublicId { get; set; }

        public bool Active { get; set; }  
    }
}