using System;
using System.ComponentModel.DataAnnotations;
using NetNote.Models;

namespace NetNote.ViewModels
{
    public class NoteModel
    {
        public int Id { get; set; }


        [Required]
        [Display(Name="Title")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int TypeId { get; set; }

        public Note CreateEntity()
        {
            return new Note()
            {
                Title = Title,
                Content = Content,
                Create = DateTime.Now,
                TypeId = TypeId
            };
        }
    }

    public class NoteTypeModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        public NoteType CreateEntity()
        {
            return new NoteType()
            {
                Name = Name
            };
        }
    }
}
