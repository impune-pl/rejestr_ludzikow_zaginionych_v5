﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rejestr_ludzikow_zaginionych_v5.ModelViews
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Płeć")]
        public bool IsWoman { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Ostania lokalizacja")]
        public string LastSeenLocation { get; set; }
        public int OwnerId { get; set; }
        [Display(Name = "Zdjęcie")]
        public IFormFile Image { get; set; }
    }
}
