using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NewsMVP.MOdels;

public partial class TblUser
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(64)]
    public string Name { get; set; } = null!;

    [StringLength(32)]
    public string Tell { get; set; } = null!;

    [StringLength(64)]
    public string UserName { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Column("RoleID")]
    public int RoleId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("TblUser")]
    public virtual TblRole Role { get; set; } = null!;
}
