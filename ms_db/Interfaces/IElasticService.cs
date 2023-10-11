using shared_library.Models;

/// <summary>
/// Elastic client contracts
/// </summary>
public interface IElasticService
{
    /// <summary>
    /// Create Index
    /// </summary>
    /// <param name="completedText">text to index</param>
    public void Create(CompletedText completedText);

    /// <summary>
    /// Create Index Async
    /// </summary>
    /// <param name="completedText">text to index</param>
    /// <returns></returns>
    public Task CreateAsync(CompletedText completedText);

    /// <summary>
    /// Read text
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>text</returns>
    public CompletedText Read(string id);

    /// <summary>
    /// Read text Async
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>text</returns>
    public Task<CompletedText> ReadAsync(string id);

    /// <summary>
    /// Update text
    /// </summary>
    /// <param name="completedText">updated text</param>
    public void Update(CompletedText completedText);

    /// <summary>
    /// Update text Async
    /// </summary>
    /// <param name="completedText">updated text</param>
    public Task UpdateAsync(CompletedText completedText);

    /// <summary>
    /// Delete text
    /// </summary>
    /// <param name="id">text id to be deleted</param>
    public void Delete(string id);

    /// <summary>
    /// Delete text Async
    /// </summary>
    /// <param name="id">text id to be deleted</param>
    public Task DeleteAsync(string id);

    /// <summary>
    /// Search text by keywords
    /// </summary>
    /// <param name="keywords">keywords for search</param>
    /// <returns></returns>
    public List<CompletedText> Search(List<string> keywords);

    /// <summary>
    /// Search text by keywords Async
    /// </summary>
    /// <param name="keywords">keywords for search</param>
    /// <returns></returns>
    public Task<List<CompletedText>> SearchAsync(List<string> keywords);
}