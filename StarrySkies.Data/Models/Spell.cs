using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarrySkies.Data.Models
{
    public class Spell
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int MpCost { get; set; }
        public string SpellTarget { get; set; }  
        public List<VocationSpell> VocationsSpells { get; set; }
        
        
    }
}