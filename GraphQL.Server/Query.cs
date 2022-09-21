namespace GraphQL.Server;

public sealed record Book(string Title, Author Author);

public sealed record Author(int Id, string Name);

public sealed class Query
{
    public Book GetBook() => new("C# in depth.", new Author(1, "Jon Skeet"));
}
