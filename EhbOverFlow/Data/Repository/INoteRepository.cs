using EhbOverFlow.Models;
using Microsoft.Extensions.Hosting;

namespace EhbOverFlow.Data.Repository
{
    public interface INoteRepository
    {
        Note GetNote(int id);
        List<Note> GetAllNotes();
        void AddNote(Note note);
        void UpdateNote(Note note);

        void RemoveNote(int id);

        Task<bool> SaveChangesAsync();

    }
}
