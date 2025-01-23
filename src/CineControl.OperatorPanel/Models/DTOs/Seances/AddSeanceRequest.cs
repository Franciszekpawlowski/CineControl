using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CineControl.OperatorPanel.Models.DTOs.Seances;

public class AddSeanceRequest
{
    [Required]
    public int MovieId { get; set; }
    public List<SelectListItem> Movies { get; set; }
    [Required]
    public int TheaterId { get; set; }
    public List<SelectListItem> Theaters { get; set; }
    [Required]
    public int CinemaId { get; set; }
    [Required]
    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime StartTime { get; set; }

}