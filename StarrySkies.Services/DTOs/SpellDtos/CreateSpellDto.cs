namespace StarrySkies.Services.DTOs.SpellDtos
{
    public class CreateSpellDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MPCost { get; set; }
        public string SpellTarget { get; set; }
    }
}