using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    // will handle the task of collecting property data for a list of notes
    // IF THESE PROPERTIES CHANGE, I'LL HAVE TO REDO THE INDEX VIEW IN THE NOTE FOLDER
    public class NoteListItem
    {
        public int NoteId { get; set; }
        public string Title { get; set; }

        [UIHint("Starred")]
        [Display(Name = "Important")]
        public bool IsStarred { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
