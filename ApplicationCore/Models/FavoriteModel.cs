namespace ApplicationCore.Models;

public class FavoriteModel
{
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public MovieCardModel Movie { get; set; }
}