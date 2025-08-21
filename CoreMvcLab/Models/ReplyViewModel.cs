using System.ComponentModel.DataAnnotations;

namespace CoreMvcLab.Models;

public class ReplyViewModel
{

    public int Id { get; set; }
    public int BoardId { get; set; }

    [Display(Name = "返信者")]
    [RegularExpression(@"^[^\s].*", ErrorMessage = "先頭は空白以外にしてください")]
    [StringLength(20, ErrorMessage = "20文字以内で入力してください")]
    public string? RepliedName { get; set; }
    
    [Display(Name = "返信内容")]
    [Required(ErrorMessage = "必須入力です")]
    [StringLength(1500, ErrorMessage = "1500字以内で入力してください")]
    public string? Body { get; set; }

    [Display(Name = "返信日時")]
    public DateTime RepliedAt { get; set; }
}
