using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DringSpot.WebApi.Controllers
{
    public class DringControllerBase: ControllerBase
    {
        protected string GetUserId() {
            return HttpContext.User.Identities.First().Claims.Single(x => x.Type == "user_id").Value;
        }
    }
}