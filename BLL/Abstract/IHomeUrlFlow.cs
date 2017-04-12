using CONSTANTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IHomeUrlFlow
    {
         IDictionary<string,bool> CanUseAction { get; set; }
        string GetRedirect();
        bool CanGo(string action, MyStatusFlow status);
    }
}
