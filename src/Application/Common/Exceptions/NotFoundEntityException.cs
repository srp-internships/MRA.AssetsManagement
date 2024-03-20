namespace MRA.AssetsManagement.Application.Common.Exceptions;

public class NotFoundEntityException : Exception
{
    public NotFoundEntityException() { }
    
    public NotFoundEntityException(string type, string id)
        : base($"{type} with provided {id} was not found.")
    {
    }
}