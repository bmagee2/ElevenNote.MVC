using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    // The entity model represents how we will set up data in our database. The properties we add to this class will be added as columns in our SQL table. This class will also be used by the service layer to persist data and query the database.s
    public class Note
    {
        [Key]   // attribute specifies the property that uniquely identifies an entity; the primary key will always be a unique number.
        public int NoteId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Your Note")]
        public string Content { get; set; }

        // ADDED & HAD TO MIGRATE
        [DefaultValue(false)]
        public bool IsStarred { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }    // ? allows the value type to be null; referred to as the null-conditional operator
    }
}
