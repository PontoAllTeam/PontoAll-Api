namespace PontoAll.WebAPI.Services.Utils;

public class ConverterUtils
{
    public static byte[] DoubleArrayToByteArray(double[] doubles)
    {
        var bytes = new byte[doubles.Length * sizeof(double)];
        Buffer.BlockCopy(doubles, 0, bytes, 0, bytes.Length);
        return bytes;
    }

    public static double[] ByteArrayToDoubleArray(byte[] bytes)
    {
        var doubles = new double[bytes.Length / sizeof(double)];
        Buffer.BlockCopy(bytes, 0, doubles, 0, bytes.Length);
        return doubles;
    }
}
