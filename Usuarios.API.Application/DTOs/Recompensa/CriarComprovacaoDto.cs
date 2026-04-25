namespace Usuarios.API.Application.DTOs.Recompensa;
using System.ComponentModel.DataAnnotations;

public class CriarComprovacaoDto
{
    [Required]
    public int TarefaId { get; set; }
    [Required]
    public string UrlFoto { get; set; } = string.Empty;
}
