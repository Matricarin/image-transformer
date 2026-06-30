using System.Reflection;

namespace ImageTransformer.Parameters;

public sealed class Transformation
{
    public TransformationType Type { get; set; }

    public static ValueTask<Transformation?> BindAsync(HttpContext context, ParameterInfo info)
    {
        throw new NotImplementedException();
    }
}