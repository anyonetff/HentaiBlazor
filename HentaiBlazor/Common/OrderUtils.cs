using HentaiBlazor.Data;
using HentaiBlazor.Ezcomp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HentaiBlazor.Common
{
    public class OrderUtils
    {

        public static IQueryable<T> Sort<T>(IQueryable<T> query, IEnumerable<SortableItem> sortables) where T : AbstractEntity
        {
            IOrderedQueryable<T> _query = null;

            foreach (SortableItem sortable in sortables)
            {
                if (sortable.Mode != 0)
                {
                    Console.WriteLine(sortable.Name);

                    var type = typeof(T);
                    var property = type.GetProperty(sortable.Name); 

                    var _parameter = Expression.Parameter(type);
                    var _property = Expression.Property(_parameter, property);

                    Console.WriteLine(type + ":" + property.PropertyType.ToString());

                    switch (property.PropertyType.ToString())
                    {
                        case ("System.Int32"):
                            {
                                By(ref query, ref _query, sortable.Mode, 
                                    Expression.Lambda<Func<T, int>>(_property, _parameter));

                                break;
                            }
                        case ("System.Int64"):
                            {
                                By(ref query, ref _query, sortable.Mode, 
                                    Expression.Lambda<Func<T, long>>(_property, _parameter));

                                break;
                            }
                        case ("System.String"):
                            {
                                By(ref query, ref _query, sortable.Mode, 
                                    Expression.Lambda<Func<T, string>>(_property, _parameter));

                                break;
                            }
                        case ("System.DateTime"):
                            {
                                By(ref query, ref _query, sortable.Mode, 
                                    Expression.Lambda<Func<T, DateTime>>(_property, _parameter));

                                break;
                            }
                        default:
                            Console.WriteLine("未支持的排序类型.");
                            break;
                    }
                }
            }

            Console.WriteLine("完成排序组装");

            if (_query != null)
            {
                return _query.AsQueryable();
            }

            return query;
        }

        private static void By<T>(ref IQueryable<T> query, ref IOrderedQueryable<T> _query, int mode, Expression<Func<T, int>> l)
        {
            // Console.WriteLine("排序字符串");

            if (_query == null)
            {
                _query = (mode > 0) ? query.OrderBy(l) : query.OrderByDescending(l);
            }
            else
            {
                _query = (mode > 0) ? _query.ThenBy(l) : _query.ThenByDescending(l);
            }
        }

        private static void By<T>(ref IQueryable<T> query, ref IOrderedQueryable<T> _query, int mode, Expression<Func<T, long>> l)
        {
            // Console.WriteLine("排序字符串");

            if (_query == null)
            {
                _query = (mode > 0) ? query.OrderBy(l) : query.OrderByDescending(l);
            }
            else
            {
                _query = (mode > 0) ? _query.ThenBy(l) : _query.ThenByDescending(l);
            }
        }

        private static void By<T>(ref IQueryable<T> query, ref IOrderedQueryable<T> _query, int mode, Expression<Func<T, string>> l)
        {
            // Console.WriteLine("排序字符串");

            if (_query == null)
            {
                _query = (mode > 0) ? query.OrderBy(l) : query.OrderByDescending(l);
            }
            else
            {
                _query = (mode > 0) ? _query.ThenBy(l) : _query.ThenByDescending(l);
            }
        }

        private static void By<T>(ref IQueryable<T> query, ref IOrderedQueryable<T> _query, int mode, Expression<Func<T, DateTime>> l)
        {
            // Console.WriteLine("排序日期");

            if (_query == null)
            {
                _query = (mode > 0) ? query.OrderBy(l) : query.OrderByDescending(l);
            }
            else
            {
                _query = (mode > 0) ? _query.ThenBy(l) : _query.ThenByDescending(l);
            }
        }

    }
}
