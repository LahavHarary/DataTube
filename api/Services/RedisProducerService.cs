using api.Interfaces;
using shared_library.Models;
using StackExchange.Redis;

namespace api.Services;

public class RedisProducerService : IProducer
{
    private ConnectionMultiplexer _redis;
    private ISubscriber _pub;
    private ISubscriber _sub;

    public RedisProducerService()
    {
        _redis = ConnectionMultiplexer.Connect("localhost");
        _pub = _redis.GetSubscriber();
    }  

    public async Task Produce(CompletedText message)
    {
        await _pub.PublishAsync("mykey", message.ToString());
    } 
}