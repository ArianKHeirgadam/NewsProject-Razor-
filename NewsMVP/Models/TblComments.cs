using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NewsMVP.MOdels;

public partial class TblComments
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    public string Author { get; set; } = null!;

    [StringLength(400)]
    public string Body { get; set; } = null!;

    public DateOnly Date { get; set; }

    [Column("NewsID")]
    public int NewsId { get; set; }

    public bool IsValid { get; set; }

    [StringLength(255)]
    public string Profile { get; set; } = null!;

    [ForeignKey("NewsId")]
    [InverseProperty("TblComments")]
    public virtual TblNews News { get; set; } = null!;
}
