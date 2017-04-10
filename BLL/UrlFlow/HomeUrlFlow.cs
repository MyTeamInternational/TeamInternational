using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UrlFlow
{
    public class HomeUrlFlow : IHomeUrlFlow
    {
        private IDictionary<string, bool> canUseAction;
        public IDictionary<string, bool> CanUseAction
        {
            get
            {
                return canUseAction;
            }
            set
            {
                canUseAction = value;
            }
        }
        public HomeUrlFlow()
        {
            canUseAction = new Dictionary<string, bool>();
        }
        public string GetRedirect()
        {
            //Home мне не нравиться
            return "/" + "Home" + "/" + this.CanUseAction.FirstOrDefault((v) => v.Value == true).Key;
        }

        public bool CanGo(string action,bool can)
        {
            if (can)
            {
                this.CanUseAction["Page1"] = false;
                this.CanUseAction["Page2"] = true;

            }
            else {
                this.CanUseAction["Page1"] = true;
                this.CanUseAction["Page2"] = false;

            }
            return this.CanUseAction[action];
        }
    }
}
