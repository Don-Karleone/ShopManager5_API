using ShopManager5.Api.Data.Models;

namespace ShopManager5.Data.Storage_providers
{
    public interface IInvoiceStorageProvider
    {
        Task<Invoice> GetInvoice(int id);
        Task<List<Invoice>> GetInvoices();
        Task<int> AddInvoice(Invoice invoice);
        Task<bool> DeleteInvoice(int id);
        Task<bool> EditInvoice(int id, Invoice invoice);
    }
}
