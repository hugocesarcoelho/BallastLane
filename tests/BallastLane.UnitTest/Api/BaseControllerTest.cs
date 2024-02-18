using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BallastLane.UnitTest.Api
{
    public class BaseControllerTest<T>
    {
        protected static int? GetStatusCode<T>(ActionResult<T?> actionResult)
        {
            IConvertToActionResult convertToActionResult = actionResult;
            var actionResultWithStatusCode = convertToActionResult.Convert() as IStatusCodeActionResult;
            return actionResultWithStatusCode?.StatusCode;
        }
    }
}
