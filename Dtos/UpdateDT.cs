using System.ComponentModel.DataAnnotations;
namespace JobAPI.Dtos;

public record class UpdateDT(
    [Required] string Name, 
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);