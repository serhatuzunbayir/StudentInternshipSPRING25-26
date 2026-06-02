using System.Security.Cryptography;
using System.Text;

namespace Shared.Services;

// Simple utility class that hashes passwords using SHA256 to store them securely.
public static class PasswordHasher
{
    // Encrypts (hashes) a plain password string into a hex string.
    public static string Hash(string value)
    {
        // Convert the string password to raw UTF8 bytes, hash it, and convert bytes to Hexadecimal string.
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(bytes);
    }

    // Compares a plain password attempt with the stored hashed password.
    public static bool Verify(string plainText, string hash)
    {
        // Hash the incoming plain text and check if it matches the stored hash (case insensitive).
        return string.Equals(Hash(plainText), hash, StringComparison.OrdinalIgnoreCase);
    }
}

