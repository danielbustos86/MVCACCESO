﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CaboFrowardMVC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CaboFroward2018Entities : DbContext
    {
        public CaboFroward2018Entities()
            : base("name=CaboFroward2018Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DISPOSITIVOS> DISPOSITIVOS { get; set; }
        public virtual DbSet<EMPRESAS> EMPRESAS { get; set; }
        public virtual DbSet<LOCACIONES> LOCACIONES { get; set; }
        public virtual DbSet<MENUS> MENUS { get; set; }
        public virtual DbSet<MOVIMIENTOS> MOVIMIENTOS { get; set; }
        public virtual DbSet<MOVIMIENTOS_HISTORICOS> MOVIMIENTOS_HISTORICOS { get; set; }
        public virtual DbSet<NACIONALIDADES> NACIONALIDADES { get; set; }
        public virtual DbSet<NAVES> NAVES { get; set; }
        public virtual DbSet<NAVES_PROGRAMADAS> NAVES_PROGRAMADAS { get; set; }
        public virtual DbSet<PERF_US_ADMIN_LOC> PERF_US_ADMIN_LOC { get; set; }
        public virtual DbSet<PERFILES> PERFILES { get; set; }
        public virtual DbSet<PERSONAS> PERSONAS { get; set; }
        public virtual DbSet<PERSONAS_APROBADAS> PERSONAS_APROBADAS { get; set; }
        public virtual DbSet<PERSONAS_EN_SOLICITUDES> PERSONAS_EN_SOLICITUDES { get; set; }
        public virtual DbSet<PUERTOS> PUERTOS { get; set; }
        public virtual DbSet<SOLICITUDES> SOLICITUDES { get; set; }
        public virtual DbSet<TIPOS_EMPRESAS> TIPOS_EMPRESAS { get; set; }
        public virtual DbSet<TIPOS_INGRESOS> TIPOS_INGRESOS { get; set; }
        public virtual DbSet<TIPOS_VEHICULOS> TIPOS_VEHICULOS { get; set; }
        public virtual DbSet<USUARIOS> USUARIOS { get; set; }
        public virtual DbSet<VEHICULOS> VEHICULOS { get; set; }
    }
}
