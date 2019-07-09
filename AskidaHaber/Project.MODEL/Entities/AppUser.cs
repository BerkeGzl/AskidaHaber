using Project.MODEL.Enums;
using System;
using System.Collections.Generic;

namespace Project.MODEL.Entities
{
    public class AppUser : AccessClass
    {   
        public Role Role { get; set; }

        public Guid? ActivationCode { get; set; }

        public bool  IsActive { get; set; }
               
        public string UserIP { get; set; }

        public AppUser()
        {
            Role = Role.Member;
            IsActive = false;
            ActivationCode = Guid.NewGuid();
            ResetPassword = Guid.NewGuid();
        }

        //Relational Properties
        public virtual List<Comment> Comments { get; set; }
    }
}
