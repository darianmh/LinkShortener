using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Data.LinkShortener.Classes;
using Data.LinkShortener.Data;
using Data.LinkShortener.Models;
using Data.LinkShortener.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Services.LinkShortener.Services.Helper;

namespace Admin.LinkShortener.ViewComponents
{
    public class AdminIndexListing : ViewComponent
    {
        #region Fields

        private readonly ApplicationDbContext _dbContext;


        #endregion
        #region Methods
        public async Task<IViewComponentResult> InvokeAsync(ListPaginationModel paging, object items)
        {
            var type = items.GetType();
            var modelType = type.GetGenericArguments()[0];
            var result = GetShowModel(paging, (IEnumerable)items, modelType);
            return View(result);
        }

        #endregion
        #region Utilities
        private AdminShowListViewModel GetShowModel(ListPaginationModel paging, IEnumerable items, Type type)
        {
            var propertyList = GetPropertyList(type);
            var keys = GetPropertiesKey(propertyList);
            var values = GetPropertiesValue(items, propertyList);
            return new AdminShowListViewModel()
            {
                Values = values,
                AdminListPaginationModel = paging,
                Keys = keys,
                Controller = Request.RouteValues["Controller"]?.ToString()
            };
        }

        private List<AdminShowListKeyValues> GetPropertiesValue(IEnumerable modelItems, List<AdminShowItemAttributeInfo> propertyList)
        {
            var result = new List<AdminShowListKeyValues>();

            foreach (object item in modelItems)
            {
                var temp = new List<string>();
                foreach (var property in propertyList)
                {
                    string? value;
                    var attr = property.PropertyInfo.GetCustomAttribute<DbOptionListAttribute>();
                    var currentValue = property.PropertyInfo.GetValue(item)?.ToString();
                    if (attr != null)
                    {
                        value = GetValueFromDb(attr, property.PropertyInfo, currentValue);
                    }
                    else
                    {
                        value = currentValue;
                    }
                    if (value?.Length > 30) value = string.Join(string.Empty, value.Take(70)) + "...";
                    temp.Add(value);
                }

                var id = GetKey(item);
                var model = new AdminShowListKeyValues
                {
                    Value = temp,
                    Key = id
                };
                result.Add(model);
            }

            return result;
        }

        private string GetValueFromDb(DbOptionListAttribute attr, PropertyInfo propertyInfo,
            string currentValue)
        {
            var queryAble = _dbContext.Set(attr.NavigationProperty);
            if (queryAble == null) return "";
            MethodInfo findMethod = attr.NavigationProperty.GetMethod(nameof(BaseEntity.Find));
            MethodInfo nameMethod = attr.NavigationProperty.GetMethod("GetShowTextById");
            if (findMethod == null || nameMethod == null) return "";
            var item = queryAble.ToList().FirstOrDefault(x => (bool)findMethod.Invoke(x, new[] { currentValue }));
            if (item == null) return "";
            var text = nameMethod.Invoke(item, new[] { currentValue });
            return text?.ToString() ?? "";
        }

        /// <summary>
        /// دریافت کلید برا اساس کلید های دیتابیس
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetKey(object item)
        {
            var type = item.GetType();

            var text = "";
            var check = false;
            foreach (var property in type.GetProperties())
            {
                var attr = (AdminKeyAttribute)property.GetCustomAttribute<AdminKeyAttribute>();
                if (attr == null) continue;
                check = true;
                var temp = $"{property.Name}={property.GetValue(item)}";
                text += temp + "&";
            }

            if (!check)
                return "Id=" + type?.GetProperty("Id")?.GetValue(item)?.ToString() ?? "";

            return text;
        }

        private List<string> GetPropertiesKey(List<AdminShowItemAttributeInfo> propertyList)
        {
            var result = new List<string>();
            foreach (var property in propertyList)
            {
                var attr = property.PropertyInfo.GetCustomAttribute(typeof(DisplayAttribute));
                var name = attr == null ? property.PropertyInfo.Name : ((DisplayAttribute)attr).Name;
                result.Add(name);
            }
            return result;
        }

        private static List<AdminShowItemAttributeInfo> GetPropertyList(Type type)
        {
            var properties = type.GetProperties();
            var propertyList = new List<AdminShowItemAttributeInfo>();
            foreach (var property in properties)
            {
                var attrToShow = property.GetCustomAttribute(typeof(AdminShowItemAttribute));
                if (attrToShow == null) continue;
                var temp = new AdminShowItemAttributeInfo
                {
                    Order = ((AdminShowItemAttribute)attrToShow).Order,
                    PropertyInfo = property
                };
                propertyList.Add(temp);
            }

            propertyList = propertyList.OrderBy(x => x.Order).ToList();
            return propertyList;
        }

        #endregion
        #region Ctor

        public AdminIndexListing(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion




    }
}