using Elasticsearch.Net;
using Nest;
using shared_library.Models;

/// <summary>
/// Implementation Elastic client
/// </summary>
public class ElasticService : IElasticService
{
    #region fields
    private readonly ElasticClient _elasticClient;
    private readonly string _elasticIndexName;
    #endregion

    #region ctor
    public ElasticService(string elasticHostName, string elasticIndexName)
    {
        _elasticIndexName = elasticIndexName;

        var nodes = new Uri[] { new Uri(elasticHostName), };
        var connectionPool = new StaticConnectionPool(nodes);
        var connectionSettings = new ConnectionSettings(connectionPool).DisableDirectStreaming();
        _elasticClient = new ElasticClient(connectionSettings.DefaultIndex(_elasticIndexName));

        CreateIndexIfNotExists();
    }
    #endregion

    private void CreateIndexIfNotExists()
    {
        var indexExists = _elasticClient.Indices.Exists(_elasticIndexName);
        if (!indexExists.Exists)
        {
            var response = _elasticClient.Indices.Create(_elasticIndexName,
               index => index.Map<CompletedText>(
                   x => x.AutoMap()));
        }
    }

    #region Create
    public void Create(CompletedText completedText)
    {
        IndexResponse indexResponse = _elasticClient.IndexDocument(completedText);
        var result = indexResponse.Result;
        if (result != Result.Created && result != Result.Updated)
            throw new Exception($"Create failed. Result: {result}");
    }

    public async Task CreateAsync(CompletedText completedText)
    {
        IndexResponse indexResponse = await _elasticClient.IndexDocumentAsync(completedText);
        var result = indexResponse.Result;
        if (result != Result.Created && result != Result.Updated)
            throw new Exception($"Create failed. Result: {result}");
    }
    #endregion

    #region Read
    public CompletedText Read(string id)
    {
        var searchResponse = _elasticClient.Search<CompletedText>(ct => ct
        .From(0)
        .Size(1)
        .Query(q => q
            .Match(m => m
                .Field(f => f.Id)
                .Query(id))));

        var completedText = searchResponse.Documents.First();
        return completedText;
    }

    public async Task<CompletedText> ReadAsync(string id)
    {
        var gg = Read(id);
        var searchResponse = await _elasticClient.SearchAsync<CompletedText>(q => q
        .Index(_elasticIndexName)
        .Query(qs => qs
            .Match(m => m
                .Field(f => f.Id)
                .Query(id))));

        var completedText = searchResponse.Documents.First();
        return completedText;
    }
    #endregion

    #region Update
    public void Update(CompletedText completedText) => Create(completedText);
    public async Task UpdateAsync(CompletedText completedText) => await CreateAsync(completedText);
    #endregion

    #region Delete
    public void Delete(string id)
    {
        var response = _elasticClient.DeleteByQuery<CompletedText>(q => q
        .Index(_elasticIndexName)
        .Query(qs => qs
            .Match(m => m
                .Field(f => f.Id)
                .Query(id))));
    }

    public async Task DeleteAsync(string id)
    {
        var response = await _elasticClient.DeleteByQueryAsync<CompletedText>(q => q
        .Index(_elasticIndexName)
        .Query(qs => qs
            .Match(m => m
                .Field(f => f.Id)
                .Query(id))));
    }
    #endregion

    #region Search
    public List<CompletedText> Search(List<string> keywords)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CompletedText>> SearchAsync(List<string> keywords)
    {
        throw new NotImplementedException();
    }
    #endregion
}