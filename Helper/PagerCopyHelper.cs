using System.Collections.Generic;
using P.Pager;

namespace DeputiTigaKemenpora.ViewModels
{
    public static class PagerCopyHelper
    {
        public static IPager<T> ViewModelCopy<T, U>(
            this IList<U> source,
            int page)
        where T : IModels<U>, new()
        {
            List<T> tempResult = new List<T>();

            foreach (U item in source)
            {
                tempResult.Add(new T
                {
                    Models = item
                });
            }

            return tempResult.ToPagerList(page, PagerUrlHelper.ItemPerPage);
        }
    }
}