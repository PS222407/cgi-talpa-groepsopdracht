namespace BusinessLogicLayer.Exceptions;

public class Auth0ForbiddenException : Exception
{
    public Auth0ForbiddenException()
    {
    }

    public Auth0ForbiddenException(string message)
        : base(message)
    {
    }

    public Auth0ForbiddenException(string message, Exception inner)
        : base(message, inner)
    {
    }
}