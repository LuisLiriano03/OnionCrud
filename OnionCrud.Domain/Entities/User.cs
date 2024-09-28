using System;
using System.Collections.Generic;

namespace OnionCrud.Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }
}
