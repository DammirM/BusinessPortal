using BusinessPortal.IRepository;
using BusinessPortal.Models;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;
using System.Net;

namespace BusinessPortal.EndPoint_s
{
    public static class PersonEnpoints
    {
        public static void ConfigurePersonEndpoints(this WebApplication app)
        {
            app.MapGet("api/GetAllPersons", GetAllPersons).WithName("GetAllPersons")
                .Produces<ApiResponse>(200).Produces<ApiResponse>(404);

            app.MapDelete("api/DeletePerson", DeletePerson);
        }

        private async static Task<IResult> GetAllPersons(IRepository<Personal> personRepo)
        {
            ApiResponse response = new ApiResponse() {IsSuccess = false,  StatusCode = System.Net.HttpStatusCode.NotFound};

            var persons = await personRepo.GetAll();
            if (persons.Any())
            {
                response.IsSuccess = true;
                response.Data = persons;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Results.Ok(response);
            }

            return Results.NotFound(response);
        }

        private async static Task<IResult> DeletePerson(IRepository<Personal> personRepo, int id)
        {
            ApiResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            Personal PersonFromDb = await personRepo.GetById(id);

            if (PersonFromDb != null)
            {
                await personRepo.Delete(PersonFromDb);
                await personRepo.Save();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("INvalid ID");
                return Results.BadRequest(response);
            }

        }
    }
}
