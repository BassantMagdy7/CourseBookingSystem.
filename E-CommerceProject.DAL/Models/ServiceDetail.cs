using System;
using System.Collections.Generic;

namespace CourseBookingSystem.DAL.Models;

public partial class ServiceDetail
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public string? InstructorName { get; set; }

    public int? Duration { get; set; }

    public string? Schedule { get; set; }

    public int? Price { get; set; }

    public virtual Service Service { get; set; } = null!;
}
