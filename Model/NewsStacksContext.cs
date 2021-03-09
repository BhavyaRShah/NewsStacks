using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NewsStacks.Model
{
    public partial class NewsStacksContext : DbContext
    {
        public NewsStacksContext()
        {
        }

        public NewsStacksContext(DbContextOptions<NewsStacksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Timezone> Timezones { get; set; }
        public virtual DbSet<Userdetail> Userdetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("article");

                entity.Property(e => e.Articleid).HasColumnName("articleid");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.Createdbyid).HasColumnName("createdbyid");

                entity.Property(e => e.Createddate)
                    .HasColumnType("datetime")
                    .HasColumnName("createddate");

                entity.Property(e => e.Headline)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("headline");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Lastmodifieddate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastmodifieddate");

                entity.Property(e => e.Publishedbyid).HasColumnName("publishedbyid");

                entity.Property(e => e.Publisheddate)
                    .HasColumnType("datetime")
                    .HasColumnName("publisheddate");

                entity.Property(e => e.Submitteddate)
                    .HasColumnType("datetime")
                    .HasColumnName("submitteddate");

                entity.Property(e => e.Submittedtoid).HasColumnName("submittedtoid");

            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");

                entity.Property(e => e.Tagid).HasColumnName("tagid");

                entity.Property(e => e.Articleid).HasColumnName("articleid");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Lastmodifieddate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastmodifieddate");

                entity.Property(e => e.Tagname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tagname");

                //entity.HasOne(d => d.Article)
                //    .WithMany(p => p.Tags)
                //    .HasForeignKey(d => d.Articleid)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("fk_tag_articleid");
            });

            modelBuilder.Entity<Timezone>(entity =>
            {
                entity.ToTable("timezone");

                entity.Property(e => e.Timezoneid).HasColumnName("timezoneid");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Offset)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("offset");
            });

            modelBuilder.Entity<Userdetail>(entity =>
            {
                entity.ToTable("userdetail");

                entity.Property(e => e.Userdetailid).HasColumnName("userdetailid");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("firstname");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Ispublisher).HasColumnName("ispublisher");

                entity.Property(e => e.Iswriter).HasColumnName("iswriter");

                entity.Property(e => e.Lastmodifieddate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastmodifieddate");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("lastname");

                entity.Property(e => e.Timezoneid).HasColumnName("timezoneid");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Userpassword)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("userpassword");

                //entity.HasOne(d => d.Timezone)
                //    .WithMany(p => p.Userdetails)
                //    .HasForeignKey(d => d.Timezoneid)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("fk_userdetail_timezoneid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
