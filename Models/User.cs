using System.Collections.Generic;

namespace customeIdentity.Models
{
    public class User : EntityBase
    {
        public string UserName{get;set;}
        public string PasswordHash{get;set;}
        public string Email{get;set;}

        public ICollection<UserRole> UserRoles {get;set;} = new List<UserRole>();
    }
}