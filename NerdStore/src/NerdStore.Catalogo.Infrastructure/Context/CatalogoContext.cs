﻿using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Domain.AggregationObjects.ProdutoAggregate;
using NerdStore.Core.Data;
using NerdStore.Core.DomainObjects.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Infrastructure.Context
{
    public class CatalogoContext : DbContext, IUnitOfWork
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options)
        { }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                if (property.GetMaxLength() == null)
                    property.SetMaxLength(100);
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.CurrentValues[nameof(Entity.DataCadastro)] = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("DataCadastro").IsModified = false;
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}