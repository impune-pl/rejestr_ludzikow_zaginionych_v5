using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace rejestr_ludzikow_zaginionych_v5.Models
{
    public class User : IdentityUser<int>
    {
        [PersonalData]
        public ICollection<Person> Entries { get; set; }
    }
}
