using BusinessPortal.IRepository;
using BusinessPortal.Models;

namespace BusinessPortal.EndPoint_s
{
    public static class PersonEnpoints
    {
        public static void ConfigurePersonEndpoints(this WebApplication app)
        {
            app.MapGet("api/GetAll", GetAllPersons).WithName("GetAllPersons")
                .Produces<ApiResponse>(200).Produces<ApiResponse>(404);
        }

        private async static Task<IResult> GetAllPersons(PersonalRepository personRepo)
        {
            ApiResponse response = new ApiResponse() {IsSuccess = false,  StatusCode = System.Net.HttpStatusCode.NotFound};

            var persons = await personRepo.GetAll();
            if (persons != null)
            {
                response.IsSuccess = true;
                response.Data = persons;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Results.Ok(response);
            }

            return Results.NotFound(response);
        }
    }
}
