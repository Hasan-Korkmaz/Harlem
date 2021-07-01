using System;

namespace Harlem.Entity.DTO.Users
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string Role { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        //For First 40 Character
        public string FullName { get; set; }


    }
}
