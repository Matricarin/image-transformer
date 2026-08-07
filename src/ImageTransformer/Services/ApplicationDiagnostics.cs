using System.Diagnostics.Metrics;

namespace ImageTransformer.Services;

public static class ApplicationDiagnostics
{
    public const string ServiceName = nameof(ImageTransformer);

    public static readonly Meter Meter = new(ServiceName, "1.0.0");

    public static readonly Counter<long> HttpRequestsTotal = Meter.CreateCounter<long>
    (
        "http.server.request.total",
        "requests",
        "Количество пришедших запросов"
    );

    public static readonly Histogram<double> TransformationDuration =
        Meter.CreateHistogram<double>("transformation.duration", "ms", "Время выполнения Transform()");

    public static readonly Histogram<long> TransformationBitmapSize =
        Meter.CreateHistogram<long>("transformation.bitmap.size", "bytes", "Размер исходного изображения");

    public static readonly Counter<long> TransformationOperationCount =
        Meter.CreateCounter<long>("transformation.operation.count", "ops", "Общее количество трансформаций");

    public static readonly Histogram<double> CropDuration =
        Meter.CreateHistogram<double>("crop.duration", "ms", "Время выполнения Crop()");

    public static readonly Histogram<long> CropOutputSize =
        Meter.CreateHistogram<long>("crop.output.size", "bytes", "Размер результата");

    public static readonly Counter<long> CropOperationCount =
        Meter.CreateCounter<long>("crop.operation.count", "ops", "Общее количество crop операций");
}