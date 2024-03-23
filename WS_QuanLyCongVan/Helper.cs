using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WS_QuanLyCongVan
{
    public class Helper
    {
        public static string RenderRazorViewToString(Controller controller, string viewName, object model = null)
        {
            controller.ViewData.Model = model;

            using (var output = new StringWriter())
            {
                var viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                var viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

                var viewContext = new ViewContext(
                  controller.ControllerContext,
                  viewResult.View,
                  controller.ViewData,
                  controller.TempData,
                  output,
                  new HtmlHelperOptions()
                );

                viewResult.View.RenderAsync(viewContext).Wait();

                return output.ToString();
            }
        }
    }
}
