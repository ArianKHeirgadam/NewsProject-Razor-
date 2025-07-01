using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NewsMVP.MOdels;

public partial class TblRole
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(32)]
    public string Name { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<TblUser> TblUser { get; set; } = new List<TblUser>();
}
