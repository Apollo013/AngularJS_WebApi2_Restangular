using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace RestangularGrid.Helpers
{
    /// <summary>
    /// A class used to extend IQueryable when sorting data
    /// </summary>
    public static class QuerySortingExtensions
    {
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, IEnumerable<string> sort) where T : class
        {
            if (sort != null)
            {
                List<string> sortFields = new List<string>();

                foreach (string sortField in sort)
                {
                    if (sortField.StartsWith("+"))
                    {
                        sortFields.Add($"{sortField.TrimStart('+')} ASC");
                    }
                    else if (sortField.StartsWith("-"))
                    {
                        sortFields.Add($"{sortField.TrimStart('-')} DESC");
                    }
                    else
                    {
                        sortFields.Add(sortField);
                    }
                }

                return query.OrderBy(string.Join(",", sortFields));
            }

            return query;
        }
    }
}
