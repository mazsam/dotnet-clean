using System;
using System.Collections.Generic;

namespace VendorBoilerplate.Domain.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public byte Status { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
