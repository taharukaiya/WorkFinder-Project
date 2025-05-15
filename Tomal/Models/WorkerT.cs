using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkFinder.Models;

public partial class WorkerT
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Age { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string Salary { get; set; } = null!;
    [Display(Name = "Occupation")]
    public string Experienced { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? ContactNumber { get; set; }
}
