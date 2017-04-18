using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Abstract
{
    public interface IPictureManager
    {
        void CreatePictures(int movieID, List<HttpPostedFileBase> files);
        string GetPicture(int id);
    }
}
