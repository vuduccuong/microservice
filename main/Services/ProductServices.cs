using main.Helpers;
using main.Models;
using main.Producer;

namespace main.Services;

public interface IProductServices
{
    IEnumerable<Product> GetAll();
    void Like(int id);
    void Create(Product product);
}

public class ProductServices : IProductServices
{
    private MainDBContext _context;
    private readonly IMessageProducer _messagePublisher;
    public ProductServices(MainDBContext context, IMessageProducer messagePublisher)
    {
        _context = context;
        _messagePublisher = messagePublisher;
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products;
    }

    public void Like(int id)
    {
        var product = _context.Products.Find(id);
        _messagePublisher.SendMessage(product);
    }

    public void Create(Product product)
    {
        _context.Products.Add(product);

        _context.SaveChanges();
    }
}