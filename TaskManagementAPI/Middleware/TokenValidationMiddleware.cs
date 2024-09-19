using Common.Helpers;

namespace TaskManagementAPI.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenValidator _tokenValidator;

        public TokenValidationMiddleware(RequestDelegate next, TokenValidator tokenValidator)
        {
            _next = next;
            _tokenValidator = tokenValidator;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                if (!_tokenValidator.ValidateToken(token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized: Invalid Token");
                    return;
                }
            }

            await _next(context);
        }
    }
}
