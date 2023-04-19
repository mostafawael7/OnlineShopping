using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models;
using OnlineShopping.Repositories.Interfaces;

namespace OnlineShopping.Repositories.Services
{
    public class SupplierService : ISupplierInterface
    {
        private readonly ApplicationDbContext _context;
        public SupplierService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool isSupplierExists(int id)
        {
            return _context.Suppliers.Any(s => s.Id == id);
        }
        public ICollection<Supplier> getAllSuppliers()
        {
            var suppliers = _context.Suppliers
                .Include(s => s.Products)
                .Select(s => new Supplier
                {
                    Id = s.Id,
                    CompanyName = s.CompanyName,
                    ContactName = s.ContactName,
                    ContactTitle = s.ContactTitle,
                    Country = s.Country,
                    Fax = s.Fax,
                    Phone = s.Phone,
                    City = s.City,
                    Products = s.Products
                    .Select(p => new Product
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        UnitPrice = p.UnitPrice,
                        Package = p.Package,
                        IsDiscontinued = p.IsDiscontinued,
                        OrderItems = p.OrderItems
                    }).ToList()
                }).ToList();
            return suppliers;
        }
        public Supplier getSupplierById(int supplierId)
        {
            var supplier = _context.Suppliers
                .Where(s => s.Id == supplierId)
                .Include(s => s.Products)
                .Select(s => new Supplier
                {
                    Id = s.Id,
                    CompanyName = s.CompanyName,
                    ContactName = s.ContactName,
                    ContactTitle = s.ContactTitle,
                    Country = s.Country,
                    Fax = s.Fax,
                    Phone = s.Phone,
                    City = s.City,
                    Products = s.Products
                    .Select(p => new Product
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        UnitPrice = p.UnitPrice,
                        Package = p.Package,
                        IsDiscontinued = p.IsDiscontinued,
                        OrderItems = p.OrderItems
                    }).ToList()
                }).FirstOrDefault();
            return supplier;
        }
        public Supplier getSupplierByName(string supplierName)
        {
            var supplier = _context.Suppliers
                .Where(s => s.CompanyName.Trim().ToLower() == supplierName.Trim().ToLower())
                .Include(s => s.Products)
                .Select(s => new Supplier
                {
                    Id = s.Id,
                    CompanyName = s.CompanyName,
                    ContactName = s.ContactName,
                    ContactTitle = s.ContactTitle,
                    Country = s.Country,
                    Fax = s.Fax,
                    Phone = s.Phone,
                    City = s.City,
                    Products = s.Products
                    .Select(p => new Product
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        UnitPrice = p.UnitPrice,
                        Package = p.Package,
                        IsDiscontinued = p.IsDiscontinued,
                        OrderItems = p.OrderItems
                    }).ToList()
                }).FirstOrDefault();
            return supplier;
        }
        public bool addSupplier(Supplier supplier)
        {
            _context.Add(supplier);
            return save();
        }
        public bool updateSupplier(Supplier supplier)
        {
            _context.Update(supplier);
            return save();
        }
        public bool removeSupplier(Supplier supplier)
        {
            _context.Remove(supplier);
            return save();
        }
        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
