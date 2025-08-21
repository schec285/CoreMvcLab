using System.ComponentModel.DataAnnotations;

namespace CoreMvcLab.Entities;

public class Reply
{
    [Key]
    public int Id { get; set; }

    // Foreign key to the Board entity
    public int BoardId { get; set; }
    
    [Required, StringLength(20)]
    public string? RepliedName { get; set; }
    
    [Required, StringLength(1500)]
    public string? Body { get; set; }
    
    public DateTime RepliedAt { get; set; } = DateTime.Now;
    
    public DateTime? UpdatedAt { get; set; }
    
    public Board? Board { get; set; }
}
