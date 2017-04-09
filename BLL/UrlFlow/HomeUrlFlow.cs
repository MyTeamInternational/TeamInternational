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
        public HomeUrlFlow() {
            canUseAction = new Dictionary<string, bool>();  
        }
    }
}
