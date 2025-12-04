namespace PontoAll.WebAPI.Services.Interfaces;

public interface IFaceRecognitionService
{
    double[] ExtractFaceEncoding(byte[] imageBytes);
    public double[] ExtractFaceEncodingFromBase64(string base64Image);
    bool CompareFaces(double[] knownEncoding, double[] unknownEncoding, double threshold = 0.6);
    double CalculateFaceDistance(double[] encoding1, double[] encoding2);
}