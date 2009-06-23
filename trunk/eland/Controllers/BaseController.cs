using System.Web.Mvc;
using eland.api.Interfaces;

namespace eland.Controllers
{
    public abstract class BaseController : Controller
    {
        public IDataContext DataContext { get; set; }
    }
}
