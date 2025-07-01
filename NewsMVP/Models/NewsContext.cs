using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NewsMVP.MOdels;

public partial class NewsContext : DbContext
{
    public NewsContext()
    {
    }

    public NewsContext(DbContextOptions<NewsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCategories> TblCategories { get; set; }

    public virtual DbSet<TblComments> TblComments { get; set; }

    public virtual DbSet<TblKeyword> TblKeyword { get; set; }

    public virtual DbSet<TblNews> TblNews { get; set; }

    public virtual DbSet<TblRole> TblRole { get; set; }

    public virtual DbSet<TblUser> TblUser { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=NewsDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCategories>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC27A7CD8A00");

            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<TblComments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comments__3214EC2732D8D896");

            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.News).WithMany(p => p.TblComments).HasConstraintName("FK_Comments_News");
        });

        modelBuilder.Entity<TblKeyword>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblNews>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__News__3214EC27CB579F3A");

            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Category).WithMany(p => p.TblNews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__News__CategoryID__3B75D760");

            entity.HasMany(d => d.Keyword).WithMany(p => p.New)
                .UsingEntity<Dictionary<string, object>>(
                    "TblKeywordRel",
                    r => r.HasOne<TblKeyword>().WithMany()
                        .HasForeignKey("KeywordId")
                        .HasConstraintName("FK_TblKeywordRel_TblKeyword"),
                    l => l.HasOne<TblNews>().WithMany()
                        .HasForeignKey("NewId")
                        .HasConstraintName("FK_TblKeywordRel_News"),
                    j =>
                    {
                        j.HasKey("NewId", "KeywordId");
                        j.IndexerProperty<int>("NewId").HasColumnName("NewID");
                        j.IndexerProperty<int>("KeywordId").HasColumnName("KeywordID");
                    });
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasOne(d => d.Role).WithMany(p => p.TblUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblUser_TblRole");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
