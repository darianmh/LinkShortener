using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Data.LinkShortener.Classes;
using Data.LinkShortener.Data;
using Data.LinkShortener.Models;

using Microsoft.EntityFrameworkCore;

namespace Services.LinkShortener.Services.Main
{
    /// <summary>
    /// main db service with crud
    /// </summary>
    /// <typeparam name="T">db object type</typeparam>
    /// <typeparam name="TDb">db context type</typeparam>
    /// <typeparam name="TId">id type</typeparam>
    public class MainService<T, TDb, TId> : IMainService<T, TDb, TId> where T : BaseEntity<TId> where TDb : DbContext
    {

        #region Fields

        private TDb Db { get; set; }

        protected DbSet<T> Set => Db.Set<T>();
        protected IQueryable<T> Queryable
        {
            get
            {
                var set = Set.AsQueryable();
                var navigations = Db.Model.FindEntityType(typeof(T))
                    .GetDerivedTypesInclusive()
                    .SelectMany(type => type.GetNavigations())
                    .Distinct();

                foreach (var property in navigations)
                    set = set.Include(property.Name);
                return set;

                return Db.Set<T>();
            }
        }

        #endregion

        #region Methodes
        public TId Insert(T entity)
        {
            entity.CreateDateTime = DateTime.Now;
            entity.UpdateDateTime = DateTime.Now;
            Set.Add(entity);
            Db.SaveChanges();
            return entity.Id;
        }

        public void Insert(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreateDateTime = DateTime.Now;
                entity.UpdateDateTime = DateTime.Now;
                Set.Add(entity);
            }
            Db.SaveChanges();
        }

        public void Update(T entity)
        {
            entity.UpdateDateTime = DateTime.Now;
            Db.Entry(entity).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void Update(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.UpdateDateTime = DateTime.Now;
                Db.Entry(entity).State = EntityState.Modified;
            }
            Db.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            DeleteNavigationProperties(entity);
            Db.Entry(entity).State = EntityState.Deleted;
            Db.SaveChanges();
        }


        public void Delete(TId id)
        {
            var entity = GetById(id);
            if (entity != null) Delete(entity);
        }

        public void DeleteAll()
        {
            var all = GetAll();
            DeleteAll(all);
        }

        public void DeleteAll(List<T> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        public bool CheckId(TId id)
        {
            return Queryable.Any(x => x.Id.Equals(id));
        }

        public IQueryable<T> Run(Expression<Func<T, bool>> query)
        {
            return Queryable.Where(query);
        }

        public T GetById(TId id)
        {
            var entity = Queryable.FirstOrDefault(x => x.Id.Equals(id));
            return entity;
        }

        public List<T> GetAll()
        {
            return Queryable.ToList().ToList();
        }

        public async Task<TId> InsertAsync(T entity)
        {
            entity.CreateDateTime = DateTime.Now;
            entity.UpdateDateTime = DateTime.Now;
            await Set.AddAsync(entity);
            await Db.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> InsertAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreateDateTime = DateTime.Now;
                entity.UpdateDateTime = DateTime.Now;
                await Set.AddAsync(entity);
            }
            return await Db.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            entity.UpdateDateTime = DateTime.Now;
            Db.Entry(entity).State = EntityState.Modified;
            return await Db.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.UpdateDateTime = DateTime.Now;
                Db.Entry(entity).State = EntityState.Modified;
            }
            return await Db.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            await DeleteNavigationPropertiesAsync(entity);
            Db.Entry(entity).State = EntityState.Deleted;
            return await Db.SaveChangesAsync();
        }

        //public async Task<int> DeleteAsync<TG>(TG entity) where TG : class
        //{
        //    return await (async () =>
        //    {

        //        var tempEn = entity;
        //        foreach (var temp in from property in (typeof(TG).GetProperties()) where property.PropertyType.Name == typeof(ICollection<>).Name select tempEn.GetType().GetProperty(property.Name) into propertyInfo where propertyInfo != null select propertyInfo.GetValue(tempEn) into navigation select ((IEnumerable<object>)navigation).ToList() into list from temp in list select temp)
        //            await DeleteAsync(temp);
        //        Db.Entry(entity).State = EntityState.Deleted;
        //        return await Db.SaveChangesAsync();
        //    });
        //}

        public async Task<int> DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            return await DeleteAsync(entity);
        }

        public async Task DeleteAllAsync()
        {
            var all = await GetAllAsync();
            await DeleteAllAsync(all);
        }

        public async Task DeleteAllAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                await DeleteAsync(entity);
            }
        }

        public virtual async Task<T> GetByIdAsync(TId id)
        {
            var entity = await Queryable.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return entity;
        }

        public async Task<T> GetByIdNoTrackAsync(TId id)
        {
            var entity = await Queryable.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
            return entity;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var all = (await Queryable.ToListAsync()).ToList();
            return all ?? new List<T>();
        }

        public async Task<List<T>> GetAllAsync(int page, int count)
        {
            page = page - 1;
            var all = (await Queryable.Skip(page * count).Take(count).ToListAsync()).ToList();
            return all ?? new List<T>();
        }

        public async Task<DbModelInfo<T>> GetAllInfoAsync(int page, int count)
        {
            page = page - 1;
            var all = await Pagination(Queryable.AsNoTracking(), page, count);
            return new DbModelInfo<T>
            {
                List = all ?? new List<T>(),
                TotalCount = await Queryable.CountAsync()
            };
        }

        public async Task<IQueryable<T>> RunAsync(Expression<Func<T, bool>> query)
        {
            return await Task.Run(function: () => Queryable.Where(query));
        }

        public async Task<bool> CheckIdAsync(TId id)
        {
            return await Queryable.AnyAsync(x => x.Id.Equals(id));
        }
        public async Task DeleteAsync(List<T> entity)
        {
            foreach (var item in entity)
            {
                await DeleteAsync(item);
            }
        }

        #endregion

        #region Utilities

        private void Delete<TG>(TG entity) where TG : class
        {
            var navigationProperties = GetNavigationProperties(entity);
            foreach (object temp in navigationProperties)
                Delete(temp);
            Db.Entry(entity).State = EntityState.Deleted;
            Db.SaveChanges();
        }
        private async Task DeleteNavigationPropertiesAsync(T entity)
        {
            var navigationProperties = GetNavigationProperties(entity);
            foreach (object temp in navigationProperties)
                Delete(temp);
        }

        private List<object> GetNavigationProperties(object entity)
        {
            var tempEntity = entity;
            foreach (var property in (typeof(T).GetProperties()))
            {
                if (property.PropertyType.Name == typeof(List<>).Name)
                {
                    var propertyInfo = tempEntity.GetType().GetProperty(property.Name);
                    if (propertyInfo != null)
                    {
                        var navigation = propertyInfo.GetValue(tempEntity);
                        List<object> list = ((IEnumerable<object>)navigation)?.ToList() ?? new List<object>();
                        return list;
                    }
                }
            }

            return new List<object>();
        }

        private void DeleteNavigationProperties(T entity)
        {
            var navigationProperties = GetNavigationProperties(entity);
            foreach (object temp in navigationProperties)
                Delete(temp);
        }
        protected async Task<List<T>> Pagination<T>(IQueryable<T> all, int page, int count)
        {
            return await all.Skip(page * count).Take(count).ToListAsync();
        }


        #endregion

        #region Ctor

        //public MainService()
        //{
        //    Db = new TDb();
        //    //AppDomain.CurrentDomain.FirstChanceException += FirstChanceExceptionHandler;
        //    //AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
        //}

        public MainService(TDb db)
        {
            Db = db;
        }

        #endregion
        public void Dispose()
        {
            List<FieldInfo> fields = GetType().GetFields(
                   BindingFlags.NonPublic | BindingFlags.Public
                   | BindingFlags.Instance | BindingFlags.Static | BindingFlags.SetProperty | BindingFlags.GetProperty)
                   .Where(x => x.Name.Contains("Service"))
                   .ToList();
            foreach (var field in fields)
            {
                var val = field.GetValue(this);
                var type = field.FieldType;
                var interfaceBase = type.GetInterface("IDisposable");
                if (interfaceBase != null)
                {
                    var method = interfaceBase.GetMethod("Dispose");
                    if (val != null) method?.Invoke(val, null);
                }
            }
            Db.Dispose();
        }


    }

    /// <summary>
    /// main db service with crud
    /// </summary>
    /// <typeparam name="T">db object type</typeparam>
    /// <typeparam name="TId"></typeparam>
    public class MainService<T, TId> : MainService<T, ApplicationDbContext, TId>, IMainService<T, TId> where T : BaseEntity<TId>
    {
        public MainService(ApplicationDbContext db) : base(db)
        {
        }
    }
    public class MainService<T> : MainService<T, int>, IMainService<T> where T : BaseEntity
    {

        #region Ctor

        public MainService(ApplicationDbContext db) : base(db)
        {
        }

        #endregion

    }
}