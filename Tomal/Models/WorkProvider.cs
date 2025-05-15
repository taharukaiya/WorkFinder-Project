using System;
using System.Collections.Generic;

namespace WorkFinder.Models;

public partial class WorkProvider
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string WorkerNeed { get; set; } = null!;

    public string? ContactNumber { get; set; }
}
