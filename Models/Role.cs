using System.Collections.Generic;

namespace customeIdentity.Models
{
    public class Role : EntityBase
    {
        public string Text{get;set;}
        public string Describtion{get;set;}

        public ICollection<UserRole> UserRoles {get;set;} = new List<UserRole>();
    }
}