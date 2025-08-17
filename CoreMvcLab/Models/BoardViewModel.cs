using System.ComponentModel.DataAnnotations;

namespace CoreMvcLab.Models;
public class BoardViewModel
{
    public int Id { get; set; }
    [Display(Name = "投稿者"), StringLength(20)]
    public string? CreatedName { get; set; }
    [Display(Name = "タイトル"), Required, StringLength(30, MinimumLength = 1)]
    public string? Title { get; set; }
    [Display(Name = "内容"), Required, StringLength(1500, MinimumLength = 1)]
    public string? Body { get; set; }
    [Display(Name = "投稿日時")]
    public DateTime CreatedAt { get; set; }
}