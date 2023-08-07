using System;
using System.Collections.Generic;

namespace VendorBoilerplate.Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string IdpSub { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? VendorId { get; set; }

    public int RoleId { get; set; }

    public string? Password { get; set; }

    public string Name { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Picture { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public virtual Role Role { get; set; } = null!;
}
