using Microsoft.AspNetCore.Mvc;
using main.Helpers;
using main.Models;
using main.Services;

namespace main.Controllers;

[Route("[controller]")]
public class ProductController : BaseController
{
    private IProductServices _productServices;
    public ProductController(IProductServices productServices)
    {
        _productServices = productServices;
    }

    [HttpGet]
    public IActionResult GetList()
    {
        var products = _productServices.GetAll().ToList();

        var response = ResultBase<List<Product>>.Success(products);

        return HandlerResult(response);
    }

    [HttpPut]
    public IActionResult Like(Product product)
    {
        _productServices.Like(product.Id);

        return Ok();
    }

}