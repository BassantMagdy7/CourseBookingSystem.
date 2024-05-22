using System;
using System.Collections.Generic;

namespace CourseBookingSystem.DAL.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public virtual ICollection<ClientRequest> ClientRequests { get; set; } = new List<ClientRequest>();
}
