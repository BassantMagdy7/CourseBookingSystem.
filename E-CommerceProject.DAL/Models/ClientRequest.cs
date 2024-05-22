using System;
using System.Collections.Generic;

namespace CourseBookingSystem.DAL.Models;

public partial class ClientRequest
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int ServiceId { get; set; }

    public string? SomeServiceDetails { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
