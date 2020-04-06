using Microsoft.AspNetCore.Mvc;

namespace shop_balta.Controllers
{
    //https://localhost:44342
    
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [Route("")]
        public string MyMethod()
        {
            return "hello mundo!!";
        }
    }
}
