using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NewsMVP.MOdels;

public partial class TblCategories
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    public bool Active { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<TblNews> TblNews { get; set; } = new List<TblNews>();
}
