using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcUi.Infrastructure
{
    public class UrlActionAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //
        }
        /// <summary>
        /// 
        ///перед каждым выполнением действия конролерра метод 
        ///проверяет не ссылка ли это из разрешенных в системе
        ///все ссылки имеют формат где у  параметра link значение true
        ///проходят и не меняют свойё направление 
        ///все другие адреса будь они из url строки будут перенаправлятся 
        ///для этого у конрроллера есть метод CanGO() который и проверяет можно ли переходить по адресу
        ///также конроллер должен сказать куда его перенаправить если не проходит адресс
        /// CanGo так устроен что содержит карту ключ значение где ключем есть имя действия контроллера, а значением можно ли переходить по этому роуту
        /// 
        /// не стоит приминять этот атрибут к методам передаваемыми Post
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var urlValues = filterContext.RouteData.Values;
                var controller = filterContext.Controller as IUrlFlow;
            if (controller != null)
            {
                if (!controller.CanGo(urlValues["action"].ToString()))
                {
                    filterContext.Result = controller.GetRedirect();
                }
            }
        }
    }
}