namespace PontoAll.WebAPI.Objects.Dtos.Entities;

public class BiometricDataDTO
{
    public int Id { get; set; }
    public float[] FacialEmbedding { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
}
