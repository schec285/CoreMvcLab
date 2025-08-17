using System.ComponentModel.DataAnnotations;

namespace CoreMvcLab.Entities;

public class Reply
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int BoardId { get; set; }
    
    [Required, StringLength(20)]
    public string ReplyName { get; set; } = "名無し";
    
    [Required, StringLength(1500)]
    public string? Body { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime? UpdatedAt { get; set; }
    
    public Board? Board { get; set; }
}
