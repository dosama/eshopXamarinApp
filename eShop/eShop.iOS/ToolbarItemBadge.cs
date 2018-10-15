using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eShop.CustomRenders;
using eShop.iOS;
using eShop.iOS.Util;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(ToolbarItemBadge))]
namespace eShop.iOS
{
   
    public class ToolbarItemBadge: IToolbarItemBadge
    {
        public void SetBadge(Page page, ToolbarItem item, string value, Color backgroundColor, Color textColor)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var renderer = Platform.GetRenderer(page);
                if (renderer == null)
                {
                    renderer = Platform.CreateRenderer(page);
                    Platform.SetRenderer(page, renderer);
                }
                var vc = renderer.ViewController;

                var rightButtomItems = vc?.ParentViewController?.NavigationItem?.RightBarButtonItems;
                var idx = page.ToolbarItems.IndexOf(item);
                if (rightButtomItems != null && rightButtomItems.Length > idx)
                {
                    var barItem = rightButtomItems[idx];
                    if (barItem != null)
                    {
                        barItem.UpdateBadge(value, backgroundColor.ToUIColor(), textColor.ToUIColor());
                    }
                }

            });
        }
    }
}