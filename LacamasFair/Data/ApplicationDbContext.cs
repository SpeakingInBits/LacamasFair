using System;
using System.Collections.Generic;
using System.Text;
using LacamasFair.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LacamasFair.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DepartmentModel> Departments { get; set; }
        public DbSet<SubDeptIdModel> SubDepartments { get; set; }
        public DbSet<SubDeptClassModel> SubDepartmentClasses { get; set; }
        public DbSet<EntryFormModel> EntryForms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //SubDeptIdModel (DepartmentId property) -> DepartmentModel (DepartmentId property)
            modelBuilder.Entity<SubDeptIdModel>() //The dependent entity
                        .HasOne<DepartmentModel>() //The principal entity
                        .WithMany()
                        .HasForeignKey(subDepartments => subDepartments.DepartmentId); //The foreign key that you want to set up as from the SubDeptIdModel class

            //SubDeptClassModel (SubDeptId property) -> SubDeptIdModel (SubDeptId property)
            modelBuilder.Entity<SubDeptClassModel>()
                        .HasOne<SubDeptIdModel>()
                        .WithMany()
                        .HasForeignKey(subDeptClass => subDeptClass.SubDeptId);

            //EntryFormModel (SubDeptId property) -> SubDeptIdModel (SubDeptId property)
            modelBuilder.Entity<EntryFormModel>()
                        .HasOne<SubDeptIdModel>()
                        .WithMany()
                        .HasForeignKey(entryForm => entryForm.SubDeptId);
        }
    }
}
