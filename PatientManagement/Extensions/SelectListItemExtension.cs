using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Web.Extensions
{
    public static class SelectListItemExtension
    {
        public static List<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items, Func<T, string> valueSelector, Func<T, string> textSelector)
        {
            return items.Select(item => new SelectListItem
            {
                Value = valueSelector(item),
                Text = textSelector(item),
            }).ToList();
        }
    }
}
