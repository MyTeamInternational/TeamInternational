using BLL.Abstract;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TeamProject.DAL;
using TeamProject.DAL.Entities;

namespace BLL.Managers
{
    public class PictureManager : IPictureManager
    {
        [Inject]
        ICinemaWork work;

        public PictureManager(ICinemaWork work)
        {
            this.work = work;
        }

        public void CreatePictures(int movieID, List<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (CheckFile(file))
                {
                    var picture = work.Pictures.Create(new Picture() { MovieID = movieID });
                    work.Save();
                    var extension = GetExtension(file);
                    var root = System.Web.Hosting.HostingEnvironment.MapPath("~/");
                    var parent = Directory.GetParent(Directory.GetParent(root).FullName).FullName;
                    file.SaveAs($"{parent}/Images/{picture.ID}{extension}");

                    //file.SaveAs($"../Images/{id}");
                }
                else
                {
                    // What should we do ?? Throw exception or log record ?
                    // We should create custom exception for invalid extensions.
                    throw new Exception("Invalid extension");
                }
            }
        }

        private bool CheckFile(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
                return false;

            string extension = GetExtension(file);

            return extension == ".jpg" ||
                    extension == ".png" ||
                    extension == ".gif" ||
                    extension == ".jpeg";

        }

        private string GetExtension(HttpPostedFileBase file)
            => Path.GetExtension(file.FileName).ToLower();

        public string GetPicture(int id)
        {
            return $"../Images/{id}";
        } 

    }
}
