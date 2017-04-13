using BLL.Abstract;
using CONSTANTS;
using System.Collections.Generic;
using System.Linq;
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
            StatusFlow = MyStatusFlow.Not_Registred;
            CanUseAction.Add(Constans_Cinema.HOME_INDEX, true);
            CanUseAction.Add(Constans_Cinema.HOME_PAGE2, false);
            CanUseAction.Add(Constans_Cinema.ACCOUNT_LOGOUT, false);
            CanUseAction.Add(Constans_Cinema.ACCOUNT_REGISTRATION, true);
            CanUseAction.Add(Constans_Cinema.LAST_PAGE_INDEX, false);
        }
        public string GetRedirect()
        {
            //Home мне не нравиться
            return "/" + Constans_Cinema.HOME_CONTROLLER + "/" + this.CanUseAction.FirstOrDefault((KeyValuePair<string, bool> v) => v.Value == true).Key;
        }
        public MyStatusFlow StatusFlow  {get; set;}
        public bool CanGo(string action)
        {
            switch (StatusFlow)
            {
                case MyStatusFlow.Registred:
                    {
                        this.canUseAction[Constans_Cinema.ACCOUNT_REGISTRATION] = false;
                        this.canUseAction[Constans_Cinema.ACCOUNT_LOGOUT] = false;
                        this.CanUseAction[Constans_Cinema.HOME_INDEX] = false;
                        this.CanUseAction[Constans_Cinema.HOME_PAGE2] = true;
                        this.CanUseAction[Constans_Cinema.LAST_PAGE_INDEX] = false;

                        break;
                    }
                case MyStatusFlow.Not_Registred:
                    {
                        this.canUseAction[Constans_Cinema.ACCOUNT_REGISTRATION] = true;
                        this.canUseAction[Constans_Cinema.ACCOUNT_LOGOUT] = true;
                        this.CanUseAction[Constans_Cinema.HOME_INDEX] = true;
                        this.CanUseAction[Constans_Cinema.HOME_PAGE2] = false;
                        this.CanUseAction[Constans_Cinema.LAST_PAGE_INDEX] = false;
                        break;
                    }
                case MyStatusFlow.Smile:
                    {
                        this.canUseAction[Constans_Cinema.ACCOUNT_REGISTRATION] = false;
                        this.canUseAction[Constans_Cinema.ACCOUNT_LOGOUT] = false;
                        this.CanUseAction[Constans_Cinema.HOME_INDEX] = false;
                        this.CanUseAction[Constans_Cinema.HOME_PAGE2] = false;
                        this.CanUseAction[Constans_Cinema.LAST_PAGE_INDEX] = true;
                        break; }
            }
            return (this.CanUseAction.ContainsKey(action)) ? this.CanUseAction[action] : false;
        }
    }
}
