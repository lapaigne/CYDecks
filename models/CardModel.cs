using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class CardModel
{
    [Key]
    public int Id { get; set; }
    public int Resource_Id { get; set; }
}
