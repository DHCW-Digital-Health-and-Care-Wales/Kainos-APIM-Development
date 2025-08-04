namespace DHCW.PD.Helpers;

public static class ExceptionHelper
{
    public static void ExecuteThrowableIfEmptyOrNull<T>(T data, Action throwableFunction)
    {
        if (data == null || EqualityComparer<T>.Default.Equals(data, default))
            throwableFunction();
    }
}
