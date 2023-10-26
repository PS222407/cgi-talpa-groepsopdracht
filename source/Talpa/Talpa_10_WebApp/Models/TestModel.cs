using System.ComponentModel.DataAnnotations;

namespace Talpa_10_WebApp.Models;

public class TestModel
{
    [Required(ErrorMessage = "The Name field is required.")]
    public string Name { get; set; }
}