using passion_project_http5226.Models;

namespace passion_project_http5226.Interfaces
{
    public interface IMoodServices
    {
        // base CRUD
        Task<IEnumerable<MoodDto>> ListMoods();


        Task<MoodDto?> FindMood(int id);


        Task<Models.ServiceResponse> UpdateMood(MoodDto moodDto);

        Task<Models.ServiceResponse> AddMood(MoodDto moodDto);

        Task<Models.ServiceResponse> DeleteMood(int id);
    }
}
