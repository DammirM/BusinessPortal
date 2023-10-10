using AutoMapper;
using Azure;
using BusinessPortal.IRepository;
using BusinessPortal.Models;
using BusinessPortal.Models.DTO_s;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BusinessPortal.EndPoint_s
{
    public static class RequestTypeEndpoints
    {
        public static void ConfigureEndpoints(this WebApplication app)
        {
            app.MapGet("api/GetAllRequestTypes", GetAllTypes).WithName("GetAllTypes").Produces<ApiResponse>(200)
                .Produces<ApiResponse>(404);

            app.MapGet("api/GetRequestTypeById/{id:int}", GetTypeById).WithName("GetSingleRequestType")
                .Produces<ApiResponse>(200).Produces<ApiResponse>(404);

            app.MapPost("api/CreateNewRequestType", CreateNewType).WithName("NewRequestType")
                .Produces<ApiResponse>(201).Produces<ApiResponse>(400);

            app.MapPut("api/UpdateRequestType", UpdateRequestType).WithName("UpdateRequestType")
                .Produces<ApiResponse>(200).Produces<ApiResponse>(400);

            app.MapDelete("api/DeleteRequestType/{id:int}", DeleteType).WithName("DeleteRequestType")
                .Produces<ApiResponse>(200).Produces<ApiResponse>(400);
        }

        private async static Task<IResult> GetAllTypes(IRepository<RequestType> typeRepo)
        {
            ApiResponse response = new ApiResponse() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var types = await typeRepo.GetAll();
            if (types.Any())
            {
                response.IsSuccess = true;
                response.Data = types;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Results.Ok(response);
            }

            return Results.NotFound(response);
        }

        private async static Task<IResult> GetTypeById(IRepository<RequestType> typeRepo, int id)
        {
            ApiResponse response = new ApiResponse() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var type = await typeRepo.GetById(id);
            if(type != null)
            {
                response.IsSuccess = true;
                response.Data = type;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Results.Ok(response);
            }

            return Results.NotFound(response);
        }

        private async static Task<IResult> CreateNewType([FromServices] IRepository<RequestType> typeRepo, [FromServices] IMapper _mapper,
            [FromBody] RequestTypeCreateDTO u_Create_DTO, [FromServices] IValidator<RequestTypeCreateDTO> _validator)
        {
            ApiResponse response = new ApiResponse() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var result = await _validator.ValidateAsync(u_Create_DTO);
            if(!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    response.ErrorMessages.Add(item.ToString());
                }

                return Results.BadRequest(response);
            }

            await typeRepo.Create(_mapper.Map<RequestType>(u_Create_DTO));
            await typeRepo.Save();
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.Created;
            return Results.Ok(response);
        }

        private async static Task<IResult> UpdateRequestType([FromServices] IRepository<RequestType> typeRepo, [FromServices] IMapper _mapper,
            [FromBody] RequestTypeUpdateDTO u_Update_DTO, [FromServices] IValidator<RequestTypeUpdateDTO> _validator)
        {
            ApiResponse response = new ApiResponse() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var result = await _validator.ValidateAsync(u_Update_DTO);
            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    response.ErrorMessages.Add(item.ToString());
                }

                return Results.BadRequest(response);
            }
            else
            {
                await typeRepo.Update(_mapper.Map<RequestType>(u_Update_DTO));
                await typeRepo.Save();
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Results.Ok(response);
            }
        }
        private async static Task<IResult> DeleteType(IRepository<RequestType> typeRepo, int id)
        {
            ApiResponse response = new ApiResponse() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var toDelete = await typeRepo.GetById(id);
            if (toDelete != null)
            {
                await typeRepo.Delete(toDelete);
                await typeRepo.Save();
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Results.Ok(response);
            }

            return Results.BadRequest(response);
        }
    }
}
