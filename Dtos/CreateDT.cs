using System.ComponentModel.DataAnnotations;
namespace JobAPI.Dtos;

public record class CreateDT(
    [Required]string Name, 
    int GenreID,
    decimal Price,
    DateOnly ReleaseDate);
