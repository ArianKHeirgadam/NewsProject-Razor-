using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NewsMVP.MOdels;

public partial class TblNews
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("TITLE")]
    [StringLength(250)]
    public string Title { get; set; } = null!;

    [StringLength(500)]
    public string Summry { get; set; } = null!;

    public string Body { get; set; } = null!;

    public DateOnly Date { get; set; }

    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [Column("ImageURLNo1")]
    [StringLength(255)]
    public string ImageUrlno1 { get; set; } = null!;

    [Column("ImageURLNo2")]
    [StringLength(255)]
    public string? ImageUrlno2 { get; set; }

    [Column("ImageURLNo3")]
    [StringLength(255)]
    public string? ImageUrlno3 { get; set; }

    public bool Slider { get; set; }

    public int ViewCount { get; set; }

    public bool IsPublished { get; set; }

    [StringLength(32)]
    public string CategoryName { get; set; } = null!;

    [StringLength(255)]
    public string? BodyTitle1 { get; set; }

    public string? BodyParagraph1 { get; set; }

    [StringLength(255)]
    public string? BodyTitle2 { get; set; }

    public string? BodyParagraph2 { get; set; }

    [StringLength(255)]
    public string? BodyTitle3 { get; set; }

    public string? BodyParagraph3 { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("TblNews")]
    public virtual TblCategories Category { get; set; } = null!;

    [InverseProperty("News")]
    public virtual ICollection<TblComments> TblComments { get; set; } = new List<TblComments>();

    [ForeignKey("NewId")]
    [InverseProperty("New")]
    public virtual ICollection<TblKeyword> Keyword { get; set; } = new List<TblKeyword>();
}
