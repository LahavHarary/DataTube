using shared_library.Models;

namespace api.Interfaces;

public interface IProducer
{
    public Task Produce(CompletedText message);
}