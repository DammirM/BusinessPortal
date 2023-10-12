using AutoMapper;
using BusinessPortal.Data;
using BusinessPortal.DTO_s;
using BusinessPortal.IRepository;
using BusinessPortal.Models;
using BusinessPortal.Models.DTO_s;
using BusinessPortal.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.NetworkInformation;

namespace BusinessPortal.EndPoint_s
{
    public static class RequestEndpoints
    {

        public static void ConfigureRequestEndPoints(this WebApplication app)
        {
            app.MapGet("api/GetAllRequests", GetAllRequests).WithName("GetAllRequeusts")
                .Produces<ApiResponse>(200).Produces<ApiResponse>(404);

            app.MapGet("api/GetRequestById/{id:int}", GetSingleRequest).WithName("GetRequestById")
                .Produces<ApiResponse>(200).Produces<ApiResponse>(404);

            app.MapPost("api/CreateNewRequest", CreateNewRequest).WithName("NewRequest")
                .Accepts<RequestCreateDTO>("application/json")
                .Produces<ApiResponse>(201).Produces<ApiResponse>(400);

            app.MapPut("api/UpdateRequest", UpdateRequest).WithName("Update")
                .Accepts<RequestUpdateDTO>("application/json")
                .Produces<ApiResponse>(201).Produces<ApiResponse>(400);

            app.MapDelete("api/DeleteRequest/{id:int}", DeleteRequest).WithName("DeleteRequest")
                .Produces<ApiResponse>(200).Produces<ApiResponse>(400);

            app.MapGet("api/GetByIdFiltered/{id:int}", GetAllWithIdFilter);
        }

        private async static Task<IResult> GetAllRequests(IRepository<Request> repo)
        {
            ApiResponse response = new ApiResponse() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var request = await repo.GetAll();
            if (request != null)
            {
                response.IsSuccess = true;
                response.Data = request;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Results.Ok(response);
            }

            return Results.NotFound(response);
        }

        private async static Task<IResult> GetSingleRequest(IRepository<Request> repo, int id)
        {
            ApiResponse response = new ApiResponse() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var request = await repo.GetById(id);
            if (request != null)
            {
                response.IsSuccess = true;
                response.Data = request;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Results.Ok(response);
            }

            return Results.NotFound(response);
        }

        private async static Task<IResult> CreateNewRequest([FromBody] RequestCreateDTO Request_DTO, [FromServices] IMapper _mapper,
        [FromServices] IRepository<Request> repo, [FromServices] IValidator<RequestCreateDTO> _validator)
        {
            ApiResponse response = new ApiResponse() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var result = _validator.Validate(Request_DTO);
            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    response.ErrorMessages.Add(item.ToString());
                }

                return Results.BadRequest(response);
            }

            var request = _mapper.Map<Request>(Request_DTO);

            await repo.Create(request);
            await repo.Save();
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.Created;
            return Results.Ok(response);
        }


        private async static Task<IResult> UpdateRequest([FromServices] IRepository<Request> requestRepo,
         [FromServices] IValidator<RequestUpdateDTO> validator, [FromBody] RequestUpdateDTO request)
        {
            ApiResponse response = new ApiResponse() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var existingRequest = await requestRepo.GetById(request.Id);

            if (existingRequest == null)
            {
                response.ErrorMessages.Add("ID not found.");
                return Results.NotFound(response);
            }


            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    response.ErrorMessages.Add(error.ErrorMessage);
                }

                return Results.BadRequest(response);
            }

            

            existingRequest.RequestTypeId = request.RequestTypeId;
            existingRequest.Period = request.Period;
            existingRequest.PersonalId = request.PersonalId;

            await requestRepo.Update(existingRequest);
            await requestRepo.Save();

            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> DeleteRequest(IRepository<Request> repo, int id)
        {
            ApiResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            Request reqfromDb = await repo.GetById(id);

            if (reqfromDb != null)
            {
                await repo.Delete(reqfromDb);
                await repo.Save();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("ID Missing");
                return Results.BadRequest(response);
            }
        }

        private async static Task<IResult> GetAllWithIdFilter( BusinessContext _db, int id)
        {
            ApiResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.NotFound };

            var result = await _db.Requests.Where(x => x.PersonalId == id).ToListAsync();

            if (result.Any())
            {
                response.IsSuccess = true;
                response.Data = result;

                return Results.Ok(response);
            }

            return Results.NotFound(response);
        }
    }
}
