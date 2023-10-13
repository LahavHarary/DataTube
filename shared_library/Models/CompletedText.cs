namespace shared_library.Models;

public sealed record CompletedText
{
    public string Id { get; init; }
    public string Title { get; init; }
    public string TextData { get; init; }
    public string Author { get; init; }
    public DateTime Date { get; }

    public CompletedText(string id, string title, string textData, string author)
    {
        Id = id;
        Title = title;
        TextData = textData;
        Author = author;
        Date = DateTime.Now;
    }

    public bool Equals(CompletedText? other)
    {
        if (other is null)
            return false;

        if (Object.ReferenceEquals(this, other))
            return true;

        if (this.GetType() != other.GetType())
            return false;

        return other.Id.Equals(this.Id) &&
            other.Title.Equals(this.Title) &&
            other.TextData.Equals(this.TextData) &&
            other.Author.Equals(this.Author);
    }

    public override int GetHashCode() => this.GetHashCode();
}