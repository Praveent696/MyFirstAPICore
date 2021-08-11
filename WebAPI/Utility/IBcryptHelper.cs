namespace WebAPI.Utility
{
    public interface IBcryptHelper
    {
        /// <summary>
        /// Encrypt Plain Text to BCrypt Hash
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        string EncryptHash(string plainText);

        /// <summary>
        /// Match Plain Text against Hash
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="plainText"></param>
        /// <returns>true or false</returns>
        bool IsStringMatchedToHash(string hash, string plainText);
    }
}