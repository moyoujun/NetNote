﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote.Models
{
    public class NoteType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)] 
        public string Name { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}
