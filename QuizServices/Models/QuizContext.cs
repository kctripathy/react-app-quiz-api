using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuizServices.Models
{
    public partial class QuizContext : DbContext
    {
       
        public QuizContext(DbContextOptions<QuizContext> options)
            : base(options)
        {
        }

        public virtual DbSet<QuizAccounts> QuizAccounts { get; set; }
        public virtual DbSet<QuizAddresses> QuizAddresses { get; set; }
        public virtual DbSet<QuizClasses> QuizClasses { get; set; }
        public virtual DbSet<QuizClassesSubject> QuizClassesSubject { get; set; }
        public virtual DbSet<QuizCountries> QuizCountries { get; set; }
        public virtual DbSet<QuizQuestions> QuizQuestions { get; set; }
        public virtual DbSet<QuizQuestionsOptions> QuizQuestionsOptions { get; set; }
        public virtual DbSet<QuizQuestionsTypes> QuizQuestionsTypes { get; set; }
        public virtual DbSet<QuizResultDetails> QuizResultDetails { get; set; }
        public virtual DbSet<QuizResultMaster> QuizResultMaster { get; set; }
        public virtual DbSet<QuizStates> QuizStates { get; set; }
        public virtual DbSet<QuizSubjects> QuizSubjects { get; set; }
        public virtual DbSet<QuizUsers> QuizUsers { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuizAccounts>(entity =>
            {
                entity.ToTable("Quiz_Accounts");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OfficeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QuizAddresses>(entity =>
            {
                entity.ToTable("Quiz_Addresses");

                entity.Property(e => e.Address1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StateId).HasDefaultValueSql("((18))");
            });

            modelBuilder.Entity<QuizClasses>(entity =>
            {
                entity.ToTable("Quiz_Classes");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<QuizClassesSubject>(entity =>
            {
                entity.ToTable("Quiz_Classes_Subject");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasDefaultValueSql("((1))");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.QuizClassesSubject)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quiz_Classes_Subject_Quiz_Classes");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.QuizClassesSubject)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quiz_Classes_Subject_Quiz_Subjects");
            });

            modelBuilder.Entity<QuizCountries>(entity =>
            {
                entity.ToTable("Quiz_Countries");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QuizQuestions>(entity =>
            {
                entity.ToTable("Quiz_Questions");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ClassSubjectId).HasColumnName("ClassSubjectID");

                entity.Property(e => e.CorrectAnswerOptionId).HasColumnName("CorrectAnswerOptionID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.QuestionTypeId)
                    .HasColumnName("QuestionTypeID")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.QuizQuestions)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Quiz_Questions_Quiz_Accounts");

                entity.HasOne(d => d.ClassSubject)
                    .WithMany(p => p.QuizQuestions)
                    .HasForeignKey(d => d.ClassSubjectId)
                    .HasConstraintName("FK_Quiz_Questions_Quiz_Classes_Subject");

                entity.HasOne(d => d.QuestionType)
                    .WithMany(p => p.QuizQuestions)
                    .HasForeignKey(d => d.QuestionTypeId)
                    .HasConstraintName("FK_Quiz_Questions_Quiz_Questions_Types");
            });

            modelBuilder.Entity<QuizQuestionsOptions>(entity =>
            {
                entity.ToTable("Quiz_Questions_Options");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuizQuestionsOptions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quiz_Questions_Options_Quiz_Questions");
            });

            modelBuilder.Entity<QuizQuestionsTypes>(entity =>
            {
                entity.ToTable("Quiz_Questions_Types");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QuizResultDetails>(entity =>
            {
                entity.ToTable("Quiz_Result_Details");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuizResultMasterId).HasColumnName("QuizResultMasterID");
            });

            modelBuilder.Entity<QuizResultMaster>(entity =>
            {
                entity.ToTable("Quiz_Result_Master");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClassSubjectId).HasColumnName("ClassSubjectID");

                entity.Property(e => e.QuizAppearDate).HasColumnType("datetime");

                entity.Property(e => e.QuizMasterId).HasColumnName("QuizMasterID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<QuizStates>(entity =>
            {
                entity.ToTable("Quiz_States");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QuizSubjects>(entity =>
            {
                entity.ToTable("Quiz_Subjects");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QuizUsers>(entity =>
            {
                entity.ToTable("Quiz_Users");

                entity.HasIndex(e => e.UserName)
                    .HasName("IX_QuizUser")
                    .IsUnique();

                entity.Property(e => e.AccessLevel).HasDefaultValueSql("((100))");

                entity.Property(e => e.AccessToken)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AllowLogin)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.Salt)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserGender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword).HasMaxLength(255);

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.QuizUsers)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quiz_Users_Quiz_Accounts");
            });
        }
    }
}
