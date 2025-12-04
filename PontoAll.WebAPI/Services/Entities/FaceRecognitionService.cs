using FaceRecognitionDotNet;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class FaceRecognitionService : IFaceRecognitionService, IDisposable
{
    private readonly FaceRecognition _faceRecognition;

    public FaceRecognitionService()
    {
        var modelsPath = Path.Combine(Directory.GetCurrentDirectory(), "FaceRecognition");

        if (!Directory.Exists(modelsPath))
            throw new DirectoryNotFoundException($"Pasta de modelos não encontrada: {modelsPath}");

        _faceRecognition = FaceRecognition.Create(modelsPath);
    }

    public double[] ExtractFaceEncodingFromBase64(string base64Image)
    {
        var cleaned = base64Image.Contains(",")
            ? base64Image.Split(',')[1]
            : base64Image;

        var bytes = Convert.FromBase64String(cleaned);

        return ExtractFaceEncoding(bytes);
    }

    public double[] ExtractFaceEncoding(byte[] imageBytes)
    {
        using var stream = new MemoryStream(imageBytes);
        using var bitmap = new System.Drawing.Bitmap(stream);
        using var image = FaceRecognition.LoadImage(bitmap);
        var locations = _faceRecognition.FaceLocations(image);

        if (!locations.Any())
            throw new InvalidOperationException("Nenhum rosto detectado na imagem");

        var encodings = _faceRecognition.FaceEncodings(image, locations);
        return encodings.First().GetRawEncoding();
    }

    public bool CompareFaces(double[] knownEncoding, double[] unknownEncoding, double threshold = 0.6)
    {
        var distance = CalculateFaceDistance(knownEncoding, unknownEncoding);
        return distance < threshold;
    }

    public double CalculateFaceDistance(double[] encoding1, double[] encoding2)
    {
        // Calcula distância euclidiana entre os encodings
        double sum = 0;
        for (int i = 0; i < encoding1.Length; i++)
        {
            double diff = encoding1[i] - encoding2[i];
            sum += diff * diff;
        }
        return Math.Sqrt(sum);
    }

    public void Dispose()
    {
        _faceRecognition?.Dispose();
    }
}
