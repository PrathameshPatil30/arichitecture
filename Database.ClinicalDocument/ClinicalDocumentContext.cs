using Database.ClinicalDocument.Entities;
using Microsoft.EntityFrameworkCore;


namespace Database.ClinicalDocument
{
    /// <summary>
    /// DB context for CLinical document, inherting dbcontext
    /// </summary>
    public partial class ClinicalDocumentContext : DbContext
    {
        /// <summary>
        /// Paramterless constructor of class
        /// </summary>
        public ClinicalDocumentContext()
        {
        }

        /// <summary>
        /// parametered contructor of class
        /// </summary>
        /// <param name="options"></param>
        public ClinicalDocumentContext(DbContextOptions<ClinicalDocumentContext> options)
            : base(options)
        {
        }


        public virtual DbSet<ClinicalDocumentMetadata> ClinicalDocumentMetadata { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<DocumentTypeXwalk> DocumentTypeXwalk { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClinicalDocumentMetadata>(entity =>
            {
                entity.Property(e => e.ClinicalDocumentMetadataId).HasColumnName("ClinicalDocumentMetadataID");

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FacilityCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MimeType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceSystem)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.DeActivatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.StandardDocumentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DocumentTypeXwalk>(entity =>
            {
                entity.ToTable("DocumentTypeXWalk");

                entity.Property(e => e.DocumentTypeXwalkId).HasColumnName("DocumentTypeXWalkID");

                entity.Property(e => e.DeActivatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.FacilityCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.DocumentTypeXwalk)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentTypeXWalk_DocumentTypeID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
