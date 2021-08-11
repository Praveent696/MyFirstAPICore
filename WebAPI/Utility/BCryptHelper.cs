namespace WebAPI.Utility
{
    public class BCryptHelper : IBcryptHelper
    {
        /// <summary>
        /// Encrypt Plain Text to BCrypt Hash
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string EncryptHash(string plainText) => BCrypt.Net.BCrypt.HashPassword(plainText);

        /// <summary>
        /// Match Plain Text against Hash
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="plainText"></param>
        /// <returns>true or false</returns>
        public bool IsStringMatchedToHash(string hash, string plainText)
        {
            return BCrypt.Net.BCrypt.Verify(plainText, hash);
        }
    }
}
