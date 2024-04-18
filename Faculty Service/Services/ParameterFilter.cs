using Faculty_Service.DBContext;
using Faculty_Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Numerics;

namespace Faculty_Service.Services
{
    public class ParameterFilter : IParameterFilter
    {
        readonly IServiceScopeFactory _serviceScopeFactory;

        public ParameterFilter(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Name.Equals("faculty_name", StringComparison.InvariantCultureIgnoreCase))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    IEnumerable<Faculty> faculties = _context.Faculties.ToArray();

                    parameter.Schema.Items.Enum = faculties.Select(p => new OpenApiString(p.name)).ToList<IOpenApiAny>();
                }
            }

            if (parameter.Name.Equals("education_level", StringComparison.InvariantCultureIgnoreCase))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    IEnumerable<Level> levels = _context.Levels.ToArray();

                    parameter.Schema.Items.Enum = levels.Select(p => new OpenApiString(p.name)).ToList<IOpenApiAny>();
                }
            }

            if (parameter.Name.Equals("education_form", StringComparison.InvariantCultureIgnoreCase))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    IEnumerable<string> forms = _context.Programs.Select(x => x.educationForm).Distinct().ToArray();

                    parameter.Schema.Enum = forms.Select(p => new OpenApiString(p)).ToList<IOpenApiAny>();
                }
            }

            if (parameter.Name.Equals("education_language", StringComparison.InvariantCultureIgnoreCase))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    IEnumerable<string> languages = _context.Programs.Select(x => x.language).Distinct().ToArray();

                    parameter.Schema.Enum = languages.Select(p => new OpenApiString(p)).ToList<IOpenApiAny>();
                }
            }
        }
    }
}
