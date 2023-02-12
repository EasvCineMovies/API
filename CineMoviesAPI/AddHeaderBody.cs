using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DevOpsCineMovies;

public abstract class AddHeaderBody : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.RequestBody = new OpenApiRequestBody
        {
            Content = new Dictionary<string, OpenApiMediaType>
            {
                {
                    "application/json", new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                {
                                    "id", new OpenApiSchema
                                    {
                                        Type = "integer"
                                    }
                                },
                                {
                                    "name", new OpenApiSchema
                                    {
                                        Type = "string"
                                    }
                                },
                                {
                                    "phone", new OpenApiSchema
                                    {
                                        Type = "string"
                                    }
                                },
                                {
                                    "email", new OpenApiSchema
                                    {
                                        Type = "string"
                                    }
                                },
                                {
                                    "password", new OpenApiSchema
                                    {
                                        Type = "string"
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
    }
}