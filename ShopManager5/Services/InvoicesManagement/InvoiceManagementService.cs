using ShopManager5.Api.Data.Models;
using ShopManager5.Data.Storage_providers;

namespace ShopManager5.Api.Services.InvoicesManagement
{
    public class InvoiceManagementService : IInvoiceManagementService
    {
        private readonly IInvoiceStorageProvider _invoiceStorageProvider;

        public InvoiceManagementService(IInvoiceStorageProvider invoiceStorageProvider)
        {
            _invoiceStorageProvider = invoiceStorageProvider;
        }

        public async Task<int> AddInvoice(Invoice invoice)
        {
            return await _invoiceStorageProvider.AddInvoice(invoice);
        }

        public async Task<bool> DeleteInvoice(int id)
        {
            return await _invoiceStorageProvider.DeleteInvoice(id);
        }

        public async Task<bool> EditInvoice(int id, Invoice invoice)
        {
            return await _invoiceStorageProvider.EditInvoice(id, invoice);
        }

        public async Task<List<Invoice>> GetInvoices()
        {
            return await _invoiceStorageProvider.GetInvoices();
        }

        public async Task<Invoice> GetInvoice(int id)
        {
            return await _invoiceStorageProvider.GetInvoice(id);
        }
    }
}
