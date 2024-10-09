using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OT.Assessment.App.Models.Casino
{
    [Table("Players")]
    public class Players
    {
        [Key]
        [Column("PlayerId")]
        public Guid PlayerId { get; set; }

        [Column("Username")]
        public string Username { get; set; }
    }
}
