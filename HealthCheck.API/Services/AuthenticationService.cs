using HealthCheck.Model;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.API.Services
{
    public class AuthenticationService
    {
        private readonly UserService userService;

        private const string EmailAttributeKey = "mail";
        private const string EmailAttributeValue = "mail";

        private const string NameAttributeKey = "name";
        private const string NameAttributeValue = "name";

        public AuthenticationService(UserService userService)
        {
            this.userService = userService;
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            var isAuthenticated = false;
            var contextName = String.Empty;

            try
            {
                using (var principalContext = new PrincipalContext(ContextType.Domain, "entelect"))
                {
                    contextName = principalContext.Name;
                    isAuthenticated = principalContext.ValidateCredentials(username, password);
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"There was an error while authenticating with the LDAP server [{contextName}],  {exception}");
            }

            IDictionary<String, Object> activeDirectoryUser = GetADUser(username, password);

            if (!isAuthenticated && !activeDirectoryUser.ContainsKey(NameAttributeKey))
            {
                return null;
            }

            var localUser = await userService.GetByEmail(username);
            if (localUser == null)
            {
                var name = ((string) activeDirectoryUser[NameAttributeKey]).Split(' ');
                var email = (activeDirectoryUser[EmailAttributeKey] as string);

                localUser = new User()
                {
                    Name = name.Length > 0 ? name[0] : username,
                    Email = email
                };

                await userService.Create(localUser);
            }

            return localUser;
        }

        private IDictionary<string, object> GetADUser(string username, string password)
        {
            var ldapUsername = username;
            var ldapPassword = password;

            var entry = new DirectoryEntry("LDAP://entelect.local", ldapUsername, ldapPassword);
            var mappings = new Dictionary<string, object>()
            {
                {EmailAttributeKey, EmailAttributeValue},
                {NameAttributeKey, NameAttributeValue}
            };

            var mySearcher = new DirectorySearcher(entry)
            {
                Filter = $"(&(objectClass=user)(|(mail={ldapUsername})(userPrincipalName={ldapUsername})))"
            };

            mySearcher.PropertiesToLoad.AddRange(GetPropertyList(mappings).Split(','));

            SearchResult searchResult = null;
            try
            {
                searchResult = mySearcher.FindOne();
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Active Directory login failed for username: {0}. Error: {1}", ldapUsername, exception.Message));
            }

            if (searchResult == null)
            {
                return new Dictionary<string, object>();
            }

            var user = MapActiveDirectoryResult(searchResult, mappings);

            entry.Close();
            entry.Dispose();
            mySearcher.Dispose();

            return user;
        }        

        private static string GetPropertyList(Dictionary<string, object> mapping)
        {
            var builder = new StringBuilder();
            var lastItem = mapping.Count - 1;

            for (int i = 0; i < mapping.Count; i++)
            {
                var value = mapping.ElementAt(i).Value.ToString();
                builder.Append(value);

                if (!string.IsNullOrEmpty(value))
                {
                    if (i != lastItem)
                    {
                        builder.Append(',');
                    }
                }
            }

            return builder.ToString();
        }

        private static IDictionary<string, object> MapActiveDirectoryResult(SearchResult searchResult, Dictionary<string, object> mappings)
        {
            var directoryObject = searchResult.GetDirectoryEntry();
            var resultMappings = new Dictionary<string, object>();
            foreach (var attributeKey in mappings.Keys)
            {
                resultMappings[attributeKey] = directoryObject.Properties[mappings[attributeKey].ToString()].Value;
            }

            return resultMappings;
        }
    }
}