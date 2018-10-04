﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WpEmpresas.Domains.Generics
{
    public interface IDomain<T> where T : class
    {
        Task<T> SaveAsync(T entity, string token);
        Task<T> UpdateAsync(T entity, string token);
        Task<IEnumerable<T>> GetAllAsync(int idCliente, string token);
        Task<T> GetByIdAsync(int entityId, int idCliente, string token);
        Task DeleteAsync(T entity, string token);
    }
}
