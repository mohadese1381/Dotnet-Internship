using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Domain;

public class Post
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
}