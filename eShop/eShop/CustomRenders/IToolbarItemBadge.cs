using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace eShop.CustomRenders
{
    public interface IToolbarItemBadge
    {
        void SetBadge(Page page, ToolbarItem item, string value, Color backgroundColor, Color textColor);
    }
}
