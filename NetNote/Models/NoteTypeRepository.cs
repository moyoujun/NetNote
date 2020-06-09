using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote.Models
{
    public interface INoteTypeRepository
    {
        Task<List<NoteType>> GetListAsync();

        Task AddAsync(NoteType noteType);
    }


    public class NoteTypeRepository :INoteTypeRepository
    {
        private NoteContext noteContext;

        public NoteTypeRepository(NoteContext context)
        {
            noteContext = context;
        }

        public Task<List<NoteType>> GetListAsync()
        {
            return noteContext.NoteTypes.ToListAsync();
        }

        public Task AddAsync(NoteType noteType)
        {
            noteContext.NoteTypes.Add(noteType);

            return noteContext.SaveChangesAsync();
        }
    }
}
