using System;
using System.Collections.Generic;

namespace Lab6.Models;

public partial class Program
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Length { get; set; }

    public double? Rating { get; set; }

    public string? Description { get; set; }

    public int GenreId { get; set; }

    public virtual Genre Genre { get; set; } = null!;
}
