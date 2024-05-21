using Domain.Entities.Base;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Users : BaseEntityAudit<string>
    {
        public Users()
        {
            
        }
        public Users(string name,string address,string email, string password)
        {
            this.Name = name;
            this.Address = address;
            this.Email = email;
            this.Password = password;
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public static Users MapFrom<T>(T obj) => obj.Adapt<Users>();

    }
}
