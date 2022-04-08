using Infrastructure.DataContext;
using Microsoft.AspNetCore.Mvc;
using Skinet.Errors;

namespace Skinet.Controllers
{
    public class BuggyController : BaseAPiController
    {
        public StoreContext _storeContext { get; }
        public BuggyController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            var thing = _storeContext.Products.Find(42);
            if(thing == null)
            {
                return NotFound(new ApiResponse(404));  
            }
            return Ok();
        }
        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            var thing = _storeContext.Products.Find(42);
            var thingToReturn = thing.ToString();
            return Ok();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
