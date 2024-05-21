using Ardalis.SmartEnum;
using Irock.POTrackingSolution.Api.Extensions;
using FastEndpoints;
using LanguageExt.Common;
using MediatR;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Irock.POTrackingSolution.Api.Endpoints.Base
{

    public class MyEndpoint<TRequest> : Endpoint<TRequest, object> where TRequest : notnull
    {
        protected Task SendResultAsync<T>(Result<T> response, int statusCode = 200,
            CancellationToken cancellation = default)
        {
            this.MatchResponse(HttpContext, response, statusCode, cancellation);
            return Task.CompletedTask;
        }


        protected async Task SendResultAsync<T>(T response, int statusCode = 200,
            CancellationToken cancellation = default) => await
            HttpContext.Response.SendAsync(response, statusCode, cancellation: cancellation);
    }

    public class MyEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse> where TRequest : notnull
    {
        protected readonly IMediator Mediator;

        public MyEndpoint(IMediator mediator)
        {
            Mediator = mediator;
        }


        public override async Task HandleAsync(TRequest req, CancellationToken ct)
        {
            await HandleRequestAsync(req, ct);
        }

        protected virtual async Task HandleRequestAsync(TRequest req, CancellationToken ct)
        {
            var result = (Result<TResponse>)(await Mediator.Send(req, ct))!;
            await SendResultAsync(result, cancellation: ct);
        }

        private Task SendResultAsync(Result<TResponse> response, int statusCode = 200,
            CancellationToken cancellation = default)
        {
            this.MatchResponse(HttpContext, response, statusCode, cancellation);
            ;
            return Task.CompletedTask;
        }
    }

    public sealed class SmartEnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var type = context.Type;
            if (!IsTypeDerivedFromGenericType(type, typeof(SmartEnum<>)) &&
                !IsTypeDerivedFromGenericType(type, typeof(SmartEnum<,>)))
            {
                return;
            }

            var enumValues = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)
                .Select(d => d.Name);
            var openApiValues = new OpenApiArray();
            openApiValues.AddRange(enumValues.Select(d => new OpenApiString(d)));

            // See https://swagger.io/docs/specification/data-models/enums/
            schema.Type = "string";
            schema.Enum = openApiValues;
            schema.Properties = null;
        }

        private static bool IsTypeDerivedFromGenericType(Type typeToCheck, Type genericType)
        {
            while (true)
            {
                if (typeToCheck == typeof(object))
                {
                    return false;
                }

                if (typeToCheck == null)
                {
                    return false;
                }

                if (typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }

                typeToCheck = typeToCheck.BaseType;
            }
        }
    }
}
