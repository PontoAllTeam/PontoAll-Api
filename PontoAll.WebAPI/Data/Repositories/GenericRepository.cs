<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
=======
using Microsoft.EntityFrameworkCore;
>>>>>>> origin/dev
using PontoAll.WebAPI.Data.Interfaces;

namespace PontoAll.WebAPI.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> Get()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChanges();
    }

    public async Task Update(T entity)
    {
<<<<<<< HEAD
        // Recupera a chave prim�ria (supondo que seja 'Id')
        var entityId = _context.Entry(entity).Property("Id").CurrentValue;

        // Verifica se a entidade com o mesmo Id j� est� sendo rastreada
        var trackedEntity = _context.ChangeTracker.Entries<T>()
            .FirstOrDefault(e => e.Property("Id").CurrentValue.Equals(entityId));

        // Se a entidade j� estiver sendo rastreada, desanexa
=======
        // Recupera a chave prim�ria (supondo que seja 'Id')
        var entityId = _context.Entry(entity).Property("Id").CurrentValue;

        // Verifica se a entidade com o mesmo Id j� est� sendo rastreada
        var trackedEntity = _context.ChangeTracker.Entries<T>()
            .FirstOrDefault(e => e.Property("Id").CurrentValue.Equals(entityId));

        // Se a entidade j� estiver sendo rastreada, desanexa
>>>>>>> origin/dev
        if (trackedEntity != null)
        {
            _context.Entry(trackedEntity.Entity).State = EntityState.Detached;
        }

        // Anexa a nova entidade e marca como 'Modified'
        _context.Entry(entity).State = EntityState.Modified;

<<<<<<< HEAD
        // Salva as altera��es no banco de dados
=======
        // Salva as altera��es no banco de dados
>>>>>>> origin/dev
        await SaveChanges();
    }

    public async Task Remove(T entity)
    {
        _dbSet.Remove(entity);
        await SaveChanges();
    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}