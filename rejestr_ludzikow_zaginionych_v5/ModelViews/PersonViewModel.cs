using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rejestr_ludzikow_zaginionych_v5.ModelViews
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public bool IsWoman { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LastSeenLocation { get; set; }
        public int OwnerId { get; set; }
        public IFormFile Image { get; set; }
    }
}
