using System;
using System.Collections.Generic;

namespace LogInAPI.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string OtherName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string OtherLastName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
