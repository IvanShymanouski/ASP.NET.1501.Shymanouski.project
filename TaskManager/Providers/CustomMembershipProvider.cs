using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.Helpers;
using BLL.Interfaces;
using BLL;
using TaskManager.Infrastructure;

namespace TaskManager.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public override bool ValidateUser(string emailOrLogin, string password)
        {
            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));
            
            bool isValid = false;

            if (null != users.Find(x => (x.Email == emailOrLogin || x.Login == emailOrLogin) && Crypto.VerifyHashedPassword(x.Password, password))) isValid = true;

            return isValid;
        }

        public UserEntity ValidateUserAndReturn(string emailOrLogin, string password)
        {
            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));
            
            return users.Find(x => (x.Email == emailOrLogin || x.Login == emailOrLogin) && Crypto.VerifyHashedPassword(x.Password, password));
        }

        public MembershipUser CreateUser(string login, string email, string password)
        {
            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));
            MembershipUser membershipUser = null;

            if (null == FindUser(login, email))
            {
                UserEntity user = new UserEntity()
                            {
                                Id = Guid.NewGuid(),
                                Login = login,
                                Email = email,
                                Password = Crypto.HashPassword(password),
                                RoleId = RoleKeysNames.keys[0]
                            };
                users.Add(user);
                membershipUser = GetUser(email);
            }

            return membershipUser;
        }

        public MembershipUser GetUser(string emailOrLogin)
        {
            UserEntity user = FindUser(emailOrLogin, emailOrLogin);
            MembershipUser memberUser = null;
            if (null != user)
            {
                memberUser = new MembershipUser("CustomMembershipProvider", user.Login, null, user.Email, null, null,
                    false, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.MinValue);
            }
            return memberUser;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            return GetUser(email);
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }
        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        private UserEntity FindUser(string login, string email)
        {
            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));
            return users.Find(x => x.Email == email || x.Login == login);
        }
    }
}