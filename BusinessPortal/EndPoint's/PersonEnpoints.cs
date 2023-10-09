using BusinessPortal.IRepository;
using BusinessPortal.Models;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;
using System.Net;
using BusinessPortal.DTO_s;
using AutoMapper;
using FluentValidation;
using BusinessPortal.Validations;

namespace BusinessPortal.EndPoint_s
{
    public static class PersonEnpoints
    {
        public static void ConfigurePersonalEndpoints(this WebApplication app)
        {
            app.MapGet("api/GetAllPersonal", GetAllPersons).WithName("GetAll")
                .Produces<ApiResponse>(200).Produces<ApiResponse>(404);

            app.MapDelete("api/GetSinglePersonal", GetSinglePerson).WithName("GetSingle")
                .Produces<ApiResponse>(200).Produces<ApiResponse>(404);

            app.MapPost("api/CreateNewPersonal", CreateNewPersonal).WithName("NewPersonal")
                .Produces<ApiResponse>(201).Produces<ApiResponse>(400);

            app.MapPut("api/UpdatePersonal", UpdatePersonal).WithName("UpdatePersonal")
                .Produces<ApiResponse>(200).Produces<ApiResponse>(400);

            app.MapDelete("api/DeletePerson", DeletePerson).WithName("DeletePersonal")
                .Produces<ApiResponse>(200).Produces<ApiResponse>(400);
        }

        private async static Task<IResult> GetAllPersons(IRepository<Personal> personalRepo)
        {
            ApiResponse response = new ApiResponse() {IsSuccess = false,  StatusCode = System.Net.HttpStatusCode.NotFound};

            var persons = await personalRepo.GetAll();
            if (persons.Any())
            {
                response.IsSuccess = true;
                response.Data = persons;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Results.Ok(response);
            }

            return Results.NotFound(response);
        }

        private async static Task<IResult> GetSinglePerson(IRepository<Personal> personalRepo, int id)
        {
            ApiResponse response = new ApiResponse() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var person = await personalRepo.GetById(id);
            if(person != null)
            {
                response.IsSuccess = true;
                response.Data = person;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Results.Ok(response);
            }

            return Results.NotFound(response);
        }

        private async static Task<IResult> CreateNewPersonal([FromBody] PersonalCreateDTO p_create_DTO,[FromServices] IMapper _mapper,
            [FromServices] IRepository<Personal> personalRepo, [FromServices] IValidator<PersonalCreateDTO> _validator)
        {
            ApiResponse response = new ApiResponse() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var result = _validator.Validate(p_create_DTO);
            if(!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    response.ErrorMessages.Add(item.ToString());
                }
                
                return Results.BadRequest(response);
            }

            await personalRepo.Create(_mapper.Map<Personal>(p_create_DTO));
            await personalRepo.Save();
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.Created;
            return Results.Ok(response);
        }

        private async static Task<IResult> UpdatePersonal([FromServices] IRepository<Personal> personalRepo,
            [FromServices] IValidator<PersonalValidation> _validator, [FromBody] Personal personal)
        {
            ApiResponse response = new ApiResponse() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var result = _validator.Validate((IValidationContext)personal); // Take a closer look
            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    response.ErrorMessages.Add(item.ToString());
                }

                Results.BadRequest(response);
            }

            await personalRepo.Update(personal);
            await personalRepo.Save();
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.Created;
            return Results.Ok(response);
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
