using AutoMapper;
using BB.Domain.Enums;
using BB.Interfaces;
using BB.UnitOfWorkEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.EncryptionUtilities;

namespace BB.BusinessLogicEntityFramework.Logic
{
    public class TokenBusinessLogic : ITokenBusinessLogic
    {
        private readonly IUnitOfWork _unitOfWork;

        public TokenBusinessLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public TokenResult IsUserTokenValid(Guid userToken)
        {
            var isValid = _unitOfWork.GetAll<User>().Any(i => i.Token == userToken);

            return isValid ? TokenResult.Valid : TokenResult.NotFound;
        }

        public Guid? authenticateUsernameAndPassword(String username, String password)
        {
            var encryptedPassword = BasicEncryptDecryptUtilities.Encrypt(password);
            var obj = _unitOfWork.GetAll<User>().SingleOrDefault(i => i.Username == username && i.Password == encryptedPassword);

            if(obj != null)
            {
                try
                {
                    obj.Token = Guid.NewGuid();
                    _unitOfWork.Update<User>(obj);
                    _unitOfWork.SaveChanges();
                    return obj.Token;
                }
                catch (Exception)
                {
                    //An error has occurred.
                    //We don't want to return the over the API as it could
                    //expose sensitive information, code snippets and / or stack trace.
                    return null;
                }
            }

            return null;
        }
    }
}
