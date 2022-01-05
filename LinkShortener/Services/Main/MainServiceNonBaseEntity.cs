using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using LinkShortener.Classes;
using LinkShortener.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Services.Main
{
    /// <summary>
    /// کلاس پیشفرض برای آن هایی که از baseentity استفاده نمی کنند
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MainServiceNonBaseEntity<T> : MainServiceNonBaseEntity<T, ApplicationDbContext>, IMainService<T> where T : class
    {

        #region Ctor

        public MainServiceNonBaseEntity(ApplicationDbContext db) : base(db)
        {
        }

        #endregion

    }
    public class MainServiceNonBaseEntity<T, TDb> : IMainService<T, TDb, int> where TDb : DbContext where T : class
    {

        #region Fields

        private TDb Db { get; set; }

        protected DbSet<T> Queryable => Db.Set<T>();

        #endregion

        #region Methodes
        public int Insert(T entity)
        {
            Queryable.Add(entity);
            Db.SaveChanges();
            return 0;
        }

        public void Insert(List<T> entities)
        {
            foreach (var entity in entities)
            {
                Queryable.Add(entity);
            }
            Db.SaveChanges();
        }

        public void Update(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void Update(List<T> entities)
        {
            foreach (var entity in entities)
            {
                Db.Entry(entity).State = EntityState.Modified;
            }
            Db.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            Db.Entry(entity).State = EntityState.Deleted;
            Db.SaveChanges();
        }

        public void Delete<TG>(TG entity) where TG : class
        {
            var tempEn = entity;
            foreach (var temp in from property in (typeof(TG).GetProperties()) where property.PropertyType.Name == typeof(ICollection<>).Name select tempEn.GetType().GetProperty(property.Name) into propertyInfo where propertyInfo != null select propertyInfo.GetValue(tempEn) into navigation select ((IEnumerable<object>)navigation).ToList() into list from temp in list select temp)
                Delete(temp);
            Db.Entry(entity).State = EntityState.Deleted;
            Db.SaveChanges();
        }

        public void Delete(int id)
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
                Db.Entry(entity).State = EntityState.Deleted;
            }
            Db.SaveChanges();
        }

        public bool CheckId(int id)
        {
            return false;
        }

        public IQueryable<T> Run(Expression<Func<T, bool>> query)
        {
            return Queryable.Where(query);
        }

        public T GetById(int id)
        {
            var entity = Queryable.Find(id);
            return entity;
        }

        public List<T> GetAll()
        {
            return Queryable.ToList().ToList();
        }

        public async Task<int> InsertAsync(T entity)
        {
            return await Task.Run(async () =>
            {
                Queryable.Add(entity);
                await Db.SaveChangesAsync();
                return 0;
            });
        }

        public async Task<int> InsertAsync(List<T> entities)
        {
            return await Task.Run(async () =>
            {

                foreach (var entity in entities)
                {
                    Queryable.Add(entity);
                }
                return await Db.SaveChangesAsync();
            });
        }

        public async Task<int> UpdateAsync(T entity)
        {
            return await Task.Run(async () =>
            {
                Db.Entry(entity).State = EntityState.Modified;
                return await Db.SaveChangesAsync();
            });
        }

        public async Task<int> UpdateAsync(List<T> entities)
        {
            return await Task.Run(async () =>
            {

                foreach (var entity in entities)
                {
                    Db.Entry(entity).State = EntityState.Modified;
                }
                return await Db.SaveChangesAsync();
            });
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            return await Task.Run(async () =>
            {

                var tempEntity = entity;
                foreach (var temp in from property in (typeof(T).GetProperties()) where property.PropertyType.Name == typeof(ICollection<>).Name select tempEntity.GetType().GetProperty(property.Name) into propertyInfo where propertyInfo != null select propertyInfo.GetValue(tempEntity) into navigation select ((IEnumerable<object>)navigation).ToList() into list from temp in list select temp)
                    Delete(temp);
                Db.Entry(entity).State = EntityState.Deleted;
                return await Db.SaveChangesAsync();
            });
        }
        //public async Task<int> DeleteAsync<TG>(TG entity) where TG : class
        //{
        //    return await Task.Run(async () =>
        //    {

        //        var tempEn = entity;
        //        foreach (var temp in from property in (typeof(TG).GetProperties()) where property.PropertyType.Name == typeof(ICollection<>).Name select tempEn.GetType().GetProperty(property.Name) into propertyInfo where propertyInfo != null select propertyInfo.GetValue(tempEn) into navigation select ((IEnumerable<object>)navigation).ToList() into list from temp in list select temp)
        //            await DeleteAsync(temp);
        //        Db.Entry(entity).State = EntityState.Deleted;
        //        return await Db.SaveChangesAsync();
        //    });
        //}

        public async Task<int> DeleteAsync(int id)
        {
            return await Task.Run(async () =>
            {
                var entity = GetById(id);
                return await DeleteAsync(entity);
            });
        }

        public async Task DeleteAllAsync()
        {
            var all = await GetAllAsync();
            await DeleteAllAsync(all);
        }

        public Task DeleteAllAsync(List<T> entities)
        {
            return Task.Run(async () =>
            {

                foreach (var entity in entities)
                {
                    Db.Entry(entity).State = EntityState.Deleted;
                }
                return await Db.SaveChangesAsync();
            });
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await Queryable.FindAsync(id);
            return entity;
        }

        public async Task<T> GetByIdNoTrackAsync(int id)
        {
            return null;
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
            var all = (await Queryable.Skip(page * count).Take(count).ToListAsync()).ToList();
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

        public async Task<bool> CheckIdAsync(int id)
        {
            return false;
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




        #endregion

        #region Ctor

        //public MainService()
        //{
        //    Db = new TDb();
        //    //AppDomain.CurrentDomain.FirstChanceException += FirstChanceExceptionHandler;
        //    //AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
        //}

        public MainServiceNonBaseEntity(TDb db)
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
}