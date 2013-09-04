//using System;
//using System.DirectoryServices.AccountManagement;
//using DefectLog.Models;

//namespace DefectLog.Services
//{
//    public interface IActiveDirectoryService
//    {
//        User GetActiveDirectoryUser(string username);
//    }

//    public class ActiveDirectoryService : IActiveDirectoryService
//    {
//        public User GetActiveDirectoryUser(string username)
//        {
//            try
//            {
//                using (var ctx = new PrincipalContext(ContextType.Domain))
//                {
//                    var userPrincipal = UserPrincipal.FindByIdentity(ctx, username);
//                    if (userPrincipal == null) throw new Exception("user not found in active directory.");

//                    var user = new User
//                    {
//                        UserName = username,
//                        EmailAddress = userPrincipal.EmailAddress,
//                        FirstName = userPrincipal.GivenName,
//                        LastName = userPrincipal.Surname
//                    };

//                    return user;
//                }
//            }

//            // for development on machines without AD
//            catch (PrincipalServerDownException ex)
//            {
//                return new User
//                {
//                    UserName = username,
//                    EmailAddress = "devuser@email.com",
//                    FirstName = "Joe",
//                    LastName = "Developer"
//                };
//            }
//        }
//    }
//}