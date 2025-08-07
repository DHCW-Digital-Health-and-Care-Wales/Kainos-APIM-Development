using Microsoft.AspNetCore.Mvc;

namespace UnitTests
{
    public static class TestUtility
    {
        public static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}
