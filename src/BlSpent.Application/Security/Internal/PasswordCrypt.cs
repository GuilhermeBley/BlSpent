using System.Security.Cryptography;
using System.Text;

namespace BlSpent.Application.Security.Internal;

internal static class PasswordCrypt
{
    private const int KEY_SIZE = 64;
    private const int ITERATIONS = 123834;
    private readonly static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
    
    [System.Diagnostics.DebuggerHidden]
    public static string HashPasword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(KEY_SIZE);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            ITERATIONS,
            hashAlgorithm,
            KEY_SIZE);
        return Convert.ToHexString(hash);
    }

    [System.Diagnostics.DebuggerHidden]
    public static HashResult HashPasword(string password)
    {
        var hashString = HashPasword(password, out byte[] salt);
        var saltString = Convert.ToHexString(salt);
        return new HashResult(hashString, saltString);
    }

    [System.Diagnostics.DebuggerHidden]
    public static bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, hashAlgorithm, KEY_SIZE);
        return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
    }

    [System.Diagnostics.DebuggerHidden]
    public static bool VerifyPassword(string password, string hash, string salt)
    {
        var saltByte = StringToByteArray(salt);
        return VerifyPassword(password, hash, saltByte);
    }

    [System.Diagnostics.DebuggerHidden]
    private static byte[] StringToByteArray(string hex)
    {
        return Enumerable.Range(0, hex.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            .ToArray();
    }

    public class HashResult
    {
        public string Hash { get; }
        public string Salt { get; }

        public HashResult(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }
    }
}