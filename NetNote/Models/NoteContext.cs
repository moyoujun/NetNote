using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NetNote.Models
{
    public class NoteContext: IdentityDbContext<NoteUser>
    {
        public NoteContext(DbContextOptions<NoteContext> options) :base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }

        public DbSet<NoteType> NoteTypes { get; set; }
    }
}
