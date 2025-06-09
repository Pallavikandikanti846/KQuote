using Microsoft.EntityFrameworkCore;
using passion_project_http5226.Data;
using passion_project_http5226.Interfaces;
using passion_project_http5226.Models;
using System;

namespace passion_project_http5226.Services
{
    public class DramaService: IDramaServices
    {
        private readonly ApplicationDbContext _context;
        // dependency injection of database context
        public DramaService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<DramaDto>> ListDramas()
        {
            // all dramas
            List<Drama> dramas = await _context.Dramas
                .ToListAsync();
            // empty list of data transfer object DramaDto
            List<DramaDto> DramaDtos = new List<DramaDto>();
            // foreach drama record in database
            foreach (Drama Drama in dramas)
            {
                // create new instance of DramaDto, add to list
                DramaDtos.Add(new DramaDto()
                {
                    drama_id = Drama.drama_id,
                    title = Drama.title,
                    release_year = Drama.release_year,
                    genre = Drama.genre,
                    synopsis=Drama.synopsis
                });
            }
            // return DramaDtos
            return DramaDtos;

        }


        public async Task<DramaDto?> FindDrama(int id)
        {
            
            var drama = await _context.Dramas
                .FirstOrDefaultAsync(c => c.drama_id == id);

            // no drama found
            if (drama == null)
            {
                return null;
            }
            // create an instance of DramaDto
            DramaDto DramaDtos = new DramaDto()
            {
                drama_id = drama.drama_id,
                title = drama.title,
                release_year = drama.release_year,
                genre = drama.genre,
                synopsis = drama.synopsis
            };
            return DramaDtos;

        }


        public async Task<ServiceResponse> UpdateDrama(DramaDto DramaDto)
        {
            ServiceResponse serviceResponse = new();


            // Create instance of Drama
            Drama drama = new Drama()
            {
                drama_id = DramaDto.drama_id,
                title = DramaDto.title,
                release_year = DramaDto.release_year,
                genre = DramaDto.genre,
                synopsis = DramaDto.synopsis
            };
            // flags that the object has changed
            _context.Entry(drama).State = EntityState.Modified;

            try
            {
                // SQL Equivalent: Update Dramas set ... where DramaId={id}
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("An error occurred updating the record");
                return serviceResponse;
            }

            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            return serviceResponse;
        }


        public async Task<ServiceResponse> AddDrama(DramaDto DramaDto)
        {
            ServiceResponse serviceResponse = new();


            // Create instance of Drama
            Drama drama = new Drama()
            {
                //drama_id = DramaDto.drama_id,
                title = DramaDto.title,
                release_year = DramaDto.release_year,
                genre = DramaDto.genre,
                synopsis = DramaDto.synopsis
            };
            // SQL Equivalent: Insert into Drama (..) values (..)

            try
            {
                _context.Dramas.Add(drama);
                var rows=await _context.SaveChangesAsync();
                Console.WriteLine($"SaveChanges affected {rows} rows.");
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("There was an error adding the Drama.");
                serviceResponse.Messages.Add(ex.Message);
            }


            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = drama.drama_id;
            return serviceResponse;
        }


        public async Task<ServiceResponse> DeleteDrama(int id)
        {
            ServiceResponse response = new();
            // Drama must exist in the first place
            var Drama = await _context.Dramas.FindAsync(id);
            if (Drama == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Drama cannot be deleted because it does not exist.");
                return response;
            }

            try
            {
                _context.Dramas.Remove(Drama);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.Status = ServiceResponse.ServiceStatus.Error;
                response.Messages.Add("Error encountered while deleting the Drama");
                return response;
            }

            response.Status = ServiceResponse.ServiceStatus.Deleted;

            return response;

        }

        //public async Task<IEnumerable<DramaDto>> ListCategoriesForProduct(int id)
        //{
            
        //    List<Category> Categories = await _context.Categories
        //        .Where(c => c.Products.Any(p => p.ProductId == id))
        //        .ToListAsync();

        //    // empty list of data transfer object CategoryDto
        //    List<CategoryDto> CategoryDtos = new List<CategoryDto>();
        //    // foreach Order Item record in database
        //    foreach (Category Category in Categories)
        //    {
        //        // create new instance of CategoryDto, add to list
        //        CategoryDtos.Add(new CategoryDto()
        //        {
        //            CategoryId = Category.CategoryId,
        //            CategoryName = Category.CategoryName,
        //            CategoryDescription = Category.CategoryDescription,
        //            CategoryColor = Category.CategoryColor
        //        });
        //    }
        //    // return CategoryDtos
        //    return CategoryDtos;

        //}

        //public async Task<ServiceResponse> LinkCategoryToProduct(int categoryId, int productId)
        //{
        //    ServiceResponse serviceResponse = new();

        //    Category? category = await _context.Categories
        //        .Include(c => c.Products)
        //        .Where(c => c.CategoryId == categoryId)
        //        .FirstOrDefaultAsync();
        //    Product? product = await _context.Products.FindAsync(productId);

        //    // Data must link to a valid entity
        //    if (product == null || category == null)
        //    {
        //        serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
        //        if (product == null)
        //        {
        //            serviceResponse.Messages.Add("Product was not found. ");
        //        }
        //        if (category == null)
        //        {
        //            serviceResponse.Messages.Add("Category was not found.");
        //        }
        //        return serviceResponse;
        //    }
        //    try
        //    {
        //        category.Products.Add(product);
        //        _context.SaveChanges();
        //    }
        //    catch (Exception Ex)
        //    {
        //        serviceResponse.Messages.Add("There was an issue linking the product to the category");
        //        serviceResponse.Messages.Add(Ex.Message);
        //    }


        //    serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
        //    return serviceResponse;
        //}

        //public async Task<ServiceResponse> UnlinkCategoryFromProduct(int categoryId, int productId)
        //{
        //    ServiceResponse serviceResponse = new();

        //    Category? category = await _context.Categories
        //        .Include(c => c.Products)
        //        .Where(c => c.CategoryId == categoryId)
        //        .FirstOrDefaultAsync();
        //    Product? product = await _context.Products.FindAsync(productId);

        //    // Data must link to a valid entity
        //    if (product == null || category == null)
        //    {
        //        serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
        //        if (product == null)
        //        {
        //            serviceResponse.Messages.Add("Product was not found. ");
        //        }
        //        if (category == null)
        //        {
        //            serviceResponse.Messages.Add("Category was not found.");
        //        }
        //        return serviceResponse;
        //    }
        //    try
        //    {
        //        category.Products.Remove(product);
        //        _context.SaveChanges();
        //    }
        //    catch (Exception Ex)
        //    {
        //        serviceResponse.Messages.Add("There was an issue unlinking the product to the category");
        //        serviceResponse.Messages.Add(Ex.Message);
        //    }


        //    serviceResponse.Status = ServiceResponse.ServiceStatus.Deleted;
        //    return serviceResponse;

        }
    }
