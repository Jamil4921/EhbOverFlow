using EhbOverFlow.Models;

namespace EhbOverFlow.Data.Repository
{
    public interface INoteRepository
    {
        void AddNote(Note note);
        List<Note> GetAllNotes();
        Note GetNote(int id);
        void RemoveNote(int id);
        void UpdateNote(Note note);

        Task<bool> SaveChangesAsync();
    }
}
