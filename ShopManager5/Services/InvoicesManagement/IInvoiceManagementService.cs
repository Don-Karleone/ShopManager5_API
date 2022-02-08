using ShopManager5.Api.Data.Models;

namespace ShopManager5.Api.Services.InvoicesManagement
{
    public interface IInvoiceManagementService
    {
        Task<List<Invoice>> GetInvoices();
        Task<Invoice> GetInvoice(int id);
        Task<int> AddInvoice(Invoice invoice);
        Task<bool> DeleteInvoice(int id);
        Task<bool> EditInvoice(int id, Invoice invoice);
    }
}
