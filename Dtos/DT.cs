using System.ComponentModel.DataAnnotations;
namespace JobAPI.Dtos;

public record class DT(
    int Id, 
    [Required] string Name, 
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);