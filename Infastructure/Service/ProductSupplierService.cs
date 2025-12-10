using System.Net;
using Domain;
using Infastructure.Data;
using Infastructure.Filters;
using Infastructure.Interface;
using Infastructure.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Service;

public class ProductSupplierService(ApplicationDbContext context) : IProductSupplierService
{
    public async Task<Response<string>> AddProductSupplierAsync(int productId, int supplierId)
    {
        var product = await context.Products.FindAsync(productId);
        if (product == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Product not found!");
        }

        var supplier = await context.Suppliers.FindAsync(supplierId);
        if (supplier == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Supplier not found!");
        }

        var productSupplier = new ProductSupplier
        {
            ProductId = productId,
            SupplierId = supplierId
        };

        context.ProductSuppliers.Add(productSupplier);
        await context.SaveChangesAsync();
        
        return new Response<string>(HttpStatusCode.OK, "Product-Supplier association added successfully!", $"Product: {product.Name}, Supplier: {supplier.CompanyName}");
    }

    public async Task<Response<string>> UpdateProductSupplierAsync(int id)
    {
        var productSupplier = await context.ProductSuppliers.FindAsync(id);
        if (productSupplier == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Product-Supplier association not found!");
        }

        productSupplier.LastSupplyDate = DateTime.Now;
        context.ProductSuppliers.Update(productSupplier);
        await context.SaveChangesAsync();

        return new Response<string>(HttpStatusCode.OK, "Product-Supplier association updated successfully!", $"Id: {id}");
    }

    public async Task<Response<string>> DeleteProductSupplierAsync(int id)
    {
        var productSupplier = await context.ProductSuppliers.FindAsync(id);
        if (productSupplier == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Product-Supplier association not found!");
        }

        context.ProductSuppliers.Remove(productSupplier);
        await context.SaveChangesAsync();

        return new Response<string>(HttpStatusCode.OK, "Product-Supplier association deleted successfully!", $"Id: {id}");
    }

    public async Task<Response<PagedResponse<List<object>>>> GetProductSuppliersAsync(SupplierFilter filter)
    {
        var query = context.ProductSuppliers
            .Include(x => x.Product)
            .Include(x => x.Supplier)
            .AsQueryable();

        if (filter.Id.HasValue)
        {
            query = query.Where(x => x.Id == filter.Id.Value);
        }

        if (filter.ProductId.HasValue)
        {
            query = query.Where(x => x.ProductId == filter.ProductId.Value);
        }

        if (filter.SupplierId.HasValue)
        {
            query = query.Where(x => x.SupplierId == filter.SupplierId.Value);
        }

        var totalRecords = await query.CountAsync();

        var page = filter.Page > 0 ? filter.Page : 1;
        var size = filter.Size > 0 ? filter.Size : 20;

        var productSuppliers = await query
            .Skip((page - 1) * size)
            .Take(size)
            .Select(x => new
            {
                x.Id,
                ProductName = x.Product.Name,
                ProductPrice = x.Product.Price,
                SupplierName = x.Supplier.CompanyName,
                SupplierEmail = x.Supplier.EmailAddress,
                x.PurchasePrice,
                x.LeadTimeDays,
                x.LastSupplyDate
            })
            .ToListAsync();

        var result = productSuppliers.Cast<object>().ToList();

        var pagedResponse = new PagedResponse<List<object>>
        {
            Data = result,
            Page = page,
            Size = size,
            TotalRecords = totalRecords
        };

        return new Response<PagedResponse<List<object>>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Your list of product-supplier associations:",
            Data = pagedResponse
        };
    }

    public async Task<Response<object>> GetProductSupplierByIdAsync(int id)
    {
        var productSupplier = await context.ProductSuppliers
            .Include(x => x.Product)
            .Include(x => x.Supplier)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (productSupplier == null)
        {
            return new Response<object>(HttpStatusCode.NotFound, "Product-Supplier association not found!");
        }

        var result = new
        {
            productSupplier.Id,
            ProductName = productSupplier.Product.Name,
            ProductPrice = productSupplier.Product.Price,
            SupplierName = productSupplier.Supplier.CompanyName,
            SupplierEmail = productSupplier.Supplier.EmailAddress,
            productSupplier.PurchasePrice,
            productSupplier.LeadTimeDays,
            productSupplier.LastSupplyDate
        };

        return new Response<object>(HttpStatusCode.OK, "Your product-supplier association:", result);
    }
}
