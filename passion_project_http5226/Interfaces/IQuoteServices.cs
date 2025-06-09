using passion_project_http5226.Models;

namespace passion_project_http5226.Interfaces
{
    public interface IQuoteServices
    {
        // base CRUD
        Task<IEnumerable<QuoteDto>> ListQuotes();


        Task<QuoteDto?> FindQuote(int id);


        //Task<Models.ServiceResponse> UpdateQuote(QuoteDto QuoteDto);

        Task<Models.ServiceResponse> AddQuote(QuoteDto QuoteDto);

        //Task<Models.ServiceResponse> DeleteQuote(int id);

    }
}
