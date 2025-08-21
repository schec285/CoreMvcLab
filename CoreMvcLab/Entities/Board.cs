using System.ComponentModel.DataAnnotations;

namespace CoreMvcLab.Entities;

public class Board
{
    [Key]
    public int Id { get; set; }

    [StringLength(20)]
    public string? CreatedName { get; set; }
    
    [Required, StringLength(30)]
    public string? Title { get; set; }
    
    [Required, StringLength(1500)]
    public string? Body { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime? UpdatedAt { get; set; }

    public ICollection<Reply> Replies { get; set; } = [];
}