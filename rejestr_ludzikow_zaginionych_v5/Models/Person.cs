using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rejestr_ludzikow_zaginionych_v5.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public bool IsWoman { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LastSeenLocation { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
