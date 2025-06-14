﻿using Microsoft.EntityFrameworkCore;
using passion_project_http5226.Data;
using passion_project_http5226.Interfaces;
using passion_project_http5226.Models;

namespace passion_project_http5226.Services
{
    public class MoodService : IMoodServices
    {
        private readonly ApplicationDbContext _context;
        // dependency injection of database context
        public MoodService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<MoodDto>> ListMoods()
        {
            // all Moods
            List<Mood> Moods = await _context.Moods
                .ToListAsync();
            // empty list of data transfer object MoodDto
            List<MoodDto> MoodDtos = new List<MoodDto>();
            // foreach Mood record in database
            foreach (Mood Mood in Moods)
            {
                // create new instance of MoodDto, add to list
                MoodDtos.Add(new MoodDto()
                {
                    mood_id = Mood.mood_id,
                    type = Mood.type
                });
            }
            // return MoodDtos
            return MoodDtos;

        }


        public async Task<MoodDto?> FindMood(int id)
        {

            var Mood = await _context.Moods
                .FirstOrDefaultAsync(c => c.mood_id == id);

            // no Mood found
            if (Mood == null)
            {
                return null;
            }
            // create an instance of MoodDto
            MoodDto MoodDtos = new MoodDto()
            {
                mood_id = Mood.mood_id,
                type = Mood.type
            };
            return MoodDtos;

        }


        public async Task<ServiceResponse> UpdateMood(MoodDto MoodDto)
        {
            ServiceResponse serviceResponse = new();


            // Create instance of Mood
            Mood Mood = new Mood()
            {
                mood_id = MoodDto.mood_id,
                type = MoodDto.type
            };
            // flags that the object has changed
            _context.Entry(Mood).State = EntityState.Modified;

            try
            {
                // SQL Equivalent: Update Moods set ... where MoodId={id}
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


        public async Task<ServiceResponse> AddMood(MoodDto MoodDto)
        {
            ServiceResponse serviceResponse = new();


            // Create instance of Mood
            Mood Mood = new Mood()
            {
                //mood_id = Mood.mood_id,
                type = MoodDto.type
            };
            // SQL Equivalent: Insert into Mood (..) values (..)

            try
            {
                _context.Moods.Add(Mood);
                var rows = await _context.SaveChangesAsync();
                Console.WriteLine($"SaveChanges affected {rows} rows.");
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("There was an error adding the Mood.");
                serviceResponse.Messages.Add(ex.Message);
            }


            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = Mood.mood_id;
            return serviceResponse;
        }


        public async Task<ServiceResponse> DeleteMood(int id)
        {
            ServiceResponse response = new();
            // Mood must exist in the first place
            var Mood = await _context.Moods.FindAsync(id);
            if (Mood == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Mood cannot be deleted because it does not exist.");
                return response;
            }

            try
            {
                _context.Moods.Remove(Mood);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.Status = ServiceResponse.ServiceStatus.Error;
                response.Messages.Add("Error encountered while deleting the Mood");
                return response;
            }

            response.Status = ServiceResponse.ServiceStatus.Deleted;

            return response;

        }

}
}
