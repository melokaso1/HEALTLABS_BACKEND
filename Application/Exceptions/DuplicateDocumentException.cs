namespace Application.Exceptions;

public sealed class DuplicateDocumentException : Exception
{
    public DuplicateDocumentException()
        : base("Ya existe una persona registrada con ese tipo y número de documento.")
    {
    }
}
