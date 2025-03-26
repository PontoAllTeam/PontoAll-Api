<<<<<<< HEAD
﻿namespace PontoAll.WebAPI.Data.Interfaces;
=======
namespace PontoAll.WebAPI.Data.Interfaces;
>>>>>>> origin/dev

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> Get();
    Task<T> GetById(int id);
    Task Add(T entity);
    Task Update(T entity);
    Task Remove(T entity);
    Task<bool> SaveChanges();
}