﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Persistence.Repository.EntityFramework.Model
{
    using MySql.Data.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public partial class AthletesDbContext : DbContext
    {
        public AthletesDbContext()
            : base("name=AthletesDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<person> people { get; set; }
        public virtual DbSet<probe> probes { get; set; }
        public virtual DbSet<registereduser> registeredusers { get; set; }
    }
}
