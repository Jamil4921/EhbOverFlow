using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Models;
using System.Linq;

namespace EhbOverFlow.Data.Repository
{
    public class NoteRepository : INoteRepository
    {
        private ApplicationDbContext _ehbOverFlowDbContext;

        public NoteRepository(ApplicationDbContext ehbOverFlowDbContext)
        {
            _ehbOverFlowDbContext = ehbOverFlowDbContext;
        }
        public void AddNote(Note note)
        {
            _ehbOverFlowDbContext.notes.Add(note);
            
        }

        public List<Note> GetAllNotes()
        {
            return _ehbOverFlowDbContext.notes.ToList();
        }

        public Note GetNote(int id)
        {
            return _ehbOverFlowDbContext.notes.FirstOrDefault(n => n.Id == id);
        }

        public void RemoveNote(int id)
        {
            _ehbOverFlowDbContext.Remove(GetNote(id));
        }

        public void UpdateNote(Note note)
        {
            _ehbOverFlowDbContext.Update(note);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if(await _ehbOverFlowDbContext.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
