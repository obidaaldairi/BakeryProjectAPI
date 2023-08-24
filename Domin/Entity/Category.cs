﻿using System.ComponentModel.DataAnnotations;

namespace Domin.Entity
{
    public class Category : BaseEntity
    {
        [Required]
        public string ArabicTitle { get; set; }
        [Required]
        public string EnglishTitle { get; set; }
    }
}
