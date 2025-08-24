namespace CoreMvcLab.Models;
public class BoardDetailsViewModel
{
    public BoardViewModel? Boards { get; set; }
    public List<ReplyViewModel>? Replies { get; set; } = [];
    public ReplyViewModel? Reply { get; set; }
}
