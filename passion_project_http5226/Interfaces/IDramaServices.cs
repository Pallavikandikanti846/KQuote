using passion_project_http5226.Models;

namespace passion_project_http5226.Interfaces
{
    public interface IDramaServices
    {
        // base CRUD
        Task<IEnumerable<DramaDto>> ListDramas();


        Task<DramaDto?> FindDrama(int id);


        Task<Models.ServiceResponse> UpdateDrama(DramaDto dramaDto);

        Task<Models.ServiceResponse> AddDrama(DramaDto dramaDto);

        Task<Models.ServiceResponse> DeleteDrama(int id);
    }
}
