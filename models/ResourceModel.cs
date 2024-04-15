using System.ComponentModel.DataAnnotations;

public class ResourceModel
{
    [Key]
    public int Id { get; set; }
    public string File { get; set; }
}
