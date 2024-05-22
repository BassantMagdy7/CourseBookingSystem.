using System;
using System.Collections.Generic;

namespace CourseBookingSystem.DAL.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserPhone { get; set; } = null!;

    public string UserPassword { get; set; } = null!;
}
