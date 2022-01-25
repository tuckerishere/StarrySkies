using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StarrySkies.Data.Models
{
    public class VocationSpell
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        
        [Required]
        public int VocationId { get; set; }
        public Vocation Vocation { get; set; }
        [Required]
        public int SpellId { get; set; }
        public Spell Spell { get; set; }
        [Range(0,99)]
        public int LevelLearned { get; set; }  
    }
}