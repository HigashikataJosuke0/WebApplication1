namespace WebApplication1.model.dto;

public class HabitsDto
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    public string Discriminator { get; set; }
    public Guid UserId { get; set; }
    public string? Reward { get; set; }
    public string? Penalty { get; set; }
}