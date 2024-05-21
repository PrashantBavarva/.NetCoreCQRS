using Application.Exceptions;
using FastEndpoints;
using LanguageExt.Common;

namespace Irock.POTrackingSolution.Api.Extensions
{
    public static class MyEndpointExtension
    {
        public static void MatchResponse<T>(this BaseEndpoint endpoint, HttpContext context, Result<T> response,
            int statusCode, CancellationToken cancellation)
        {
            response.Match(
                Succ: async r =>
                {
                    if (r is FileStream)
                    {
                        var stream = r as FileStream;
                        await context.Response.SendStreamAsync(
                            stream: stream,
                            fileName: stream.Name,
                            fileLengthBytes: stream.Length, cancellation: cancellation);
                    }
                    else
                    {
                        await
                            context.Response.SendAsync(r, statusCode, cancellation: cancellation);
                    }
                }
                ,
                Fail: async e =>
                {
                    if (e is ApiException apiException)
                        await context.Response.SendAsync(new ApiErrorResponse(apiException), (int)apiException.StatusCode,
                            cancellation: cancellation);
                    else
                    {
                        await context.Response.SendAsync(e.Message, StatusCodes.Status500InternalServerError,
                            cancellation: cancellation);
                    }
                });
        }
    }
}
