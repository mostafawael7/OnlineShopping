using OnlineShopping.Models;

namespace OnlineShopping.Repositories.Interfaces
{
    public interface ISupplierInterface
    {
        bool isSupplierExists(int id);
        ICollection<Supplier> getAllSuppliers();
        Supplier getSupplierById(int supplierId);
        Supplier getSupplierByName(string supplierName);
        bool addSupplier(Supplier supplier);
        bool updateSupplier(Supplier supplier);
        bool removeSupplier(Supplier supplier);
        bool save();
    }
}
/*
        getAllSuppliers()
        getSupplierById(int supplierId)
        getSupplierByName(string supplierName)
        addSupplier(Supplier supplier)
        updateSupplier(Supplier supplier)
        removeSupplier(Supplier supplier)
        removeSupplierById(int supplierId)
         */