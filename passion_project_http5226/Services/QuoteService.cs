using Microsoft.EntityFrameworkCore;
using passion_project_http5226.Data;
using passion_project_http5226.Interfaces;
using passion_project_http5226.Models;

namespace passion_project_http5226.Services
{
    public class QuoteService: IQuoteServices
    {
        private readonly ApplicationDbContext _context;
        // dependency injection of database context
        public QuoteService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<QuoteDto>> ListQuotes()
        {
            // all Quotes
            List<Quote> Quotes = await _context.Quotes
                .ToListAsync();
            // empty list of data transfer object QuoteDto
            List<QuoteDto> QuoteDtos = new List<QuoteDto>();
            // foreach Quote record in database
            foreach (Quote Quote in Quotes)
            {
                // create new instance of QuoteDto, add to list
                QuoteDtos.Add(new QuoteDto()
                {
                    quote_id = Quote.quote_id,
                    content = Quote.content,
                    actor=Quote.actor,
                    episode=Quote.episode,
                });
            }
            // return QuoteDtos
            return QuoteDtos;

        }


        public async Task<QuoteDto?> FindQuote(int id)
        {

            var Quote = await _context.Quotes
                .FirstOrDefaultAsync(c => c.quote_id == id);

            // no Quote found
            if (Quote == null)
            {
                return null;
            }
            // create an instance of QuoteDto
            QuoteDto QuoteDtos = new QuoteDto()
            {
                quote_id = Quote.quote_id,
                content = Quote.content,
                actor = Quote.actor,
                episode = Quote.episode,
            };
            return QuoteDtos;

        }


        //public async Task<ServiceResponse> UpdateQuote(QuoteDto QuoteDto)
        //{
        //    ServiceResponse serviceResponse = new();


        //    // Create instance of Quote
        //    Quote Quote = new Quote()
        //    {
        //        quote_id = QuoteDto.quote_id,
        //        content = QuoteDto.content,
        //        actor = QuoteDto.actor,
        //        episode = QuoteDto.episode
        //    };
        //        // flags that the object has changed
        //        _context.Entry(Quote).State = EntityState.Modified;

        //    try
        //    {
        //        // SQL Equivalent: Update Quotes set ... where QuoteId={id}
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
        //        serviceResponse.Messages.Add("An error occurred updating the record");
        //        return serviceResponse;
        //    }

        //    serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
        //    return serviceResponse;
        //}


        public async Task<ServiceResponse> AddQuote(QuoteDto QuoteDto)
        {
            ServiceResponse serviceResponse = new();


            // Create instance of Quote
            Quote Quote = new Quote()
            {
                //quote_id = QuoteDto.quote_id,
                content = QuoteDto.content,
                actor = QuoteDto.actor,
                episode = QuoteDto.episode,
                drama_id= QuoteDto.drama_id,
            };
            // SQL Equivalent: Insert into Quote (..) values (..)

            try
            {
                _context.Quotes.Add(Quote);
                var rows = await _context.SaveChangesAsync();
                Console.WriteLine($"SaveChanges affected {rows} rows.");
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("There was an error adding the Quote.");
                serviceResponse.Messages.Add(ex.Message);
            }


            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = Quote.quote_id;
            return serviceResponse;
        }


        //public async Task<ServiceResponse> DeleteQuote(int id)
        //{
        //    ServiceResponse response = new();
        //    // Quote must exist in the first place
        //    var Quote = await _context.Quotes.FindAsync(id);
        //    if (Quote == null)
        //    {
        //        response.Status = ServiceResponse.ServiceStatus.NotFound;
        //        response.Messages.Add("Quote cannot be deleted because it does not exist.");
        //        return response;
        //    }

        //    try
        //    {
        //        _context.Quotes.Remove(Quote);
        //        await _context.SaveChangesAsync();

        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = ServiceResponse.ServiceStatus.Error;
        //        response.Messages.Add("Error encountered while deleting the Quote");
        //        return response;
        //    }

        //    response.Status = ServiceResponse.ServiceStatus.Deleted;

        //    return response;

        //}
    }
    }
