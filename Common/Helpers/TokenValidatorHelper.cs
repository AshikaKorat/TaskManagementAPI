using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public  class TokenValidator
    {
        private readonly string _secretKey;

        public TokenValidator(string secretKey)
        {
            _secretKey = secretKey;
        }
        public  bool ValidateToken(string encryptedToken)
        {
            // Decrypt token
            var decryptedToken = EncryptionHelper.Decrypt(encryptedToken);

            // Validate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            try
            {
                tokenHandler.ValidateToken(decryptedToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out var validatedToken);

                return validatedToken != null;
            }
            catch
            {
                return false;
            }
        }

    }
}
