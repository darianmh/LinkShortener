using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.LinkShortener.Classes;
using Data.LinkShortener.Data;
using Microsoft.EntityFrameworkCore;

namespace Services.LinkShortener.Services.Main
{
    /// <summary>
    /// main db service with crud
    /// </summary>
    /// <typeparam name="T">db object type</typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IMainService<T, TId> : IMainService<T, ApplicationDbContext, TId>
    {

    }
    public interface IMainService<T> : IMainService<T, int>
    {

    }

    /// <summary>
    /// main db service with crud
    /// </summary>
    /// <typeparam name="T">db object type</typeparam>
    /// <typeparam name="TDb">db context type</typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IMainService<T, TDb, TId> : IDisposable where TDb : DbContext
    {
        //TDb Db { get; set; }
        TId Insert(T entity);
        void Insert(List<T> entities);
        void Update(T entity);
        void Update(List<T> entities);
        void Delete(T entity);
        void Delete(TId id);
        void DeleteAll();
        void DeleteAll(List<T> entities);
        bool CheckId(TId id);
        IQueryable<T> Run(Expression<Func<T, bool>> query);
        T GetById(TId id);
        List<T> GetAll();
        Task<TId> InsertAsync(T entity);
        Task<int> InsertAsync(List<T> entities);
        Task<int> UpdateAsync(T entity);
        Task<int> UpdateAsync(List<T> entities);
        Task<int> DeleteAsync(T entity);
        Task<int> DeleteAsync(TId id);
        Task DeleteAllAsync();
        Task DeleteAllAsync(List<T> entities);
        Task<T> GetByIdAsync(TId id);
        Task<T> GetByIdNoTrackAsync(TId id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(int page, int count);
        Task<DbModelInfo<T>> GetAllInfoAsync(int page, int count);
        Task<IQueryable<T>> RunAsync(Expression<Func<T, bool>> query);
        Task<bool> CheckIdAsync(TId id);
        Task DeleteAsync(List<T> entity);
    }

    public interface IBaseDbServices
    {

    }
}