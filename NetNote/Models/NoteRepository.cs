using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote.Models
{
    public interface INoteRepository
    {
        Task<Note> GetByIdAsync(int id);


        Task<List<Note>> GetListAsync();

        Task AddAsync(Note note);

        Task UpdateAsync(Note note);

        Task RemoveAsync(Note note);

        Task<NotePagination> PageListAsync(int pageIndex, int pageSize);
    }

    public class NotePagination
    {
        public List<Note> Notes { get; set; }

        public int TotalCount { get; set; }

        public int PageIndex { get; set;}

        public int PageSize { get; set; }
    }



    public class NoteRepository : INoteRepository
    {
        private NoteContext context;

        public NoteRepository(NoteContext _context)
        {
            context = _context;
        }

        public Task AddAsync(Note note)
        {
            context.Notes.Add(note);

            return context.SaveChangesAsync();
        }


        public Task<Note> GetByIdAsync(int id)
        {
            return context.Notes.FindAsync(id).AsTask();
        }

        public Task<List<Note>> GetListAsync()
        {
            return context.Notes.Include(t => t.Type).ToListAsync<Note>();
        }

        public async Task<NotePagination> PageListAsync(int pageIndex, int pageSize)
        {
            var query = context.Notes.Include(t => t.Type).AsQueryable();
            var count = query.Count();
            var pageCount = count / pageSize;

            if (count % pageSize != 0)
            {
                pageCount++;
            }

            var notes = await query.OrderByDescending(r => r.Create).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new NotePagination()
            {
                Notes = notes,
                TotalCount = pageCount,
                PageSize = pageSize,
                PageIndex = pageIndex
            };
        }

        public Task RemoveAsync(Note note)
        {
            context.Notes.Remove(note);
            return context.SaveChangesAsync();
        }

        public Task UpdateAsync(Note note)
        {
            context.Entry<Note>(note).State = EntityState.Modified;

            return context.SaveChangesAsync();
        }
    }
}
