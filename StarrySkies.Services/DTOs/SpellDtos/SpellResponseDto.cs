namespace StarrySkies.Services.DTOs.SpellDtos
{
    public class SpellResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MPCost { get; set; }
        public string SpellTarget { get; set; }
    }
    
}