using System.ComponentModel.DataAnnotations;

public class PlayerModel
{
    [Key]
    public int Id { get; set; }
    public int Deck_Id { get; set; }
}
