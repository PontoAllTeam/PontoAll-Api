using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Models;

[Table("biometricdata")]
public class BiometricData
{
    [Column("id")]
    public int Id { get; set; }

    [Column("facialembedding")]
    public float[] FacialEmbedding { get; set; } = [];

    [Column("createdat")]
    public DateTime CreatedAt { get; set; }

    [Column("userid")]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public BiometricData() { }

    public BiometricData(int id, float[] facialEmbedding, DateTime createdAt, int userId)
    {
        Id = id;
        FacialEmbedding = facialEmbedding;
        CreatedAt = createdAt;
        UserId = userId;
    }
}
