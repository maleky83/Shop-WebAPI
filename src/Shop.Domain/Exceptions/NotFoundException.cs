namespace Shop.Domain.Exceptions;

public class NotFoundException(string resourceType, string resourceIdentifier)
    : Exception($"{resourceType} : {resourceIdentifier} ")
{
}
