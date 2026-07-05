using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ImageTransformer.Models;

namespace ImageTransformer.Apis.Parameters;

public sealed class Transformation 
{
    public TransformationType Type { get; set; }

    public static ValueTask<Transformation?> BindAsync(HttpContext context, ParameterInfo info)
    {
        throw new NotImplementedException();
    }

}