using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LinkShortener.Classes;
using LinkShortener.Classes.Mapper;
using LinkShortener.ViewModel;
using Newtonsoft.Json;

namespace LinkShortener.Services.Helper
{
    public class AdminModelHelper : IAdminModelHelper
    {
        #region Fields


        #endregion
        #region Methods

        public AdminListViewModel<TModel> GetIndexModel<TModel, TBase>(DbModelInfo<TBase> all, int page, int count)
        {
            Type type = typeof(Mapper);
            MethodInfo method = type?.GetMethod("ToModel", types: new[] { typeof(TBase) });
            if (method == null) return new AdminListViewModel<TModel>();
            var items = all.List.Select(x => (TModel)method.Invoke(this, new object?[] { x })).ToList();
            var s = JsonConvert.SerializeObject(items);
            var itemsModel = JsonConvert.DeserializeObject<List<TModel>>(s);
            var model = new AdminListViewModel<TModel>(hasNext: all.TotalCount > page * count, hasPre: page > 1, items: items, page: page, count: all.List.Count, pagesCount: ((all.TotalCount - 1) / count) + 1);
            return model;
        }
        #endregion
        #region Utilities


        #endregion
        #region Ctor

        #endregion


    }
}