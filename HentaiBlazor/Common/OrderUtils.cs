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

        public static IOrderedQueryable<T> Sort<T>(IQueryable<T> query, IEnumerable<SortableItem> sortables)
        {
            IOrderedQueryable<T> _query = null;

            foreach (SortableItem sortable in sortables)
            {
                if (sortable.Mode != 0)
                {
                    Console.WriteLine(sortable.Name);

                    var p = Expression.Parameter(typeof(T));
                    var f = Expression.Property(p, typeof(T).GetProperty(sortable.Name));

                    Console.WriteLine(typeof(T));
                    Console.WriteLine(typeof(T).GetProperty(sortable.Name).PropertyType.ToString());

                    switch (typeof(T).GetProperty(sortable.Name).PropertyType.ToString())
                    {
                        case ("System.String"):
                            {
                                var l = Expression.Lambda<Func<T, string>>(f, p);

                                By(ref query, ref _query, sortable.Mode, l);
                                

                                break;
                            }
                        case ("System.DateTime"):
                            {
                                var l = Expression.Lambda<Func<T, DateTime>>(f, p);

                                By(ref query, ref _query, sortable.Mode, l);
                                /*
                                if (_query == null)
                                {
                                    _query = (sortable.Mode > 0) ? query.OrderBy(l) : query.OrderByDescending(l);
                                }
                                else
                                {
                                    _query = (sortable.Mode > 0) ? _query.ThenBy(l) : _query.ThenByDescending(l);
                                }
                                */
                                break;
                            }
                        default:
                            break;
                    }
                }
            }

            Console.WriteLine("完成排序组装");

            return _query;
        }

        private static void By<T>(ref IQueryable<T> query, ref IOrderedQueryable<T> _query, int mode, Expression<Func<T, string>> l)
        {
            Console.WriteLine("排序字符串");

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
            Console.WriteLine("排序日期");

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
