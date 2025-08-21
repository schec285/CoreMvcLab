using System.ComponentModel.DataAnnotations;

namespace CoreMvcLab.Models;
public class BoardViewModel
{
    const string RequiredErrMsg = "必須入力です";

    public int Id { get; set; }

    [Display(Name = "投稿者")]
    [RegularExpression(@"^[^\s].*", ErrorMessage = "先頭は空白以外にしてください")]
    [StringLength(20, ErrorMessage = "20文字以内で入力してください")]
    public string? CreatedName { get; set; }

    [Display(Name = "タイトル")]
    [RegularExpression(@"^[^\s].*", ErrorMessage = "先頭は空白以外にしてください")]
    [Required(ErrorMessage = RequiredErrMsg)]
    [StringLength(30)]
    public string? Title { get; set; }

    [Display(Name = "内容")]
    [Required(ErrorMessage = RequiredErrMsg)]
    [StringLength(1500, ErrorMessage = "1500字以内で入力してください")]
    public string? Body { get; set; }

    [Display(Name = "投稿日時")]
    public DateTime CreatedAt { get; set; }
}