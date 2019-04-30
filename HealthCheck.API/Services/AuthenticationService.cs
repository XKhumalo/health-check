using HealthCheck.Model;
using Microsoft.Extensions.Configuration;
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
        private const string EmailAttributeKey = "mail";
        private const string EmailAttributeValue = "mail";

        private const string NameAttributeKey = "name";
        private const string NameAttributeValue = "name";

        private readonly IConfiguration config;

        public AuthenticationService(IConfiguration config)
        {
            this.config = config;
        }

        public User GetADUser(string username, string password)
        {
            //if (config.GetValue<string>("Environment").Equals("Development"))
            //{
            //    return new User()
            //    {
            //        Name = "Xolani",
            //        Email = username
            //    };
            //}
            var isAuthenticated = false;
            var contextName = string.Empty;

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

            IDictionary<String, Object> activeDirectoryUser = SearchADUser(username, password);

            if (!isAuthenticated && !activeDirectoryUser.ContainsKey(NameAttributeKey))
            {
                return null;
            }

            var name = ((string)activeDirectoryUser[NameAttributeKey]).Split(' ');
            var email = ((string)activeDirectoryUser[EmailAttributeKey]);

            var localUser = new User()
            {
                Name = name.Length > 0 ? name[0] : username,
                Email = email
            };

            return localUser;
        }

        private IDictionary<string, object> SearchADUser(string username, string password)
        {
            var ldapUsername = username;
            var ldapPassword = password;

            var entry = new DirectoryEntry("LDAP://entelect.local", ldapUsername, ldapPassword);
            var mappings = new Dictionary<string, object>()
            {
                {EmailAttributeKey, EmailAttributeValue},
                {NameAttributeKey, NameAttributeValue}
            };

            var directorySearcher = new DirectorySearcher(entry)
            {
                Filter = $"(&(objectClass=user)(|(mail={ldapUsername})(userPrincipalName={ldapUsername})))"
            };
            directorySearcher.Asynchronous = true;
            directorySearcher.PropertiesToLoad.AddRange(GetPropertyList(mappings).Split(','));

            SearchResult searchResult = null;
            try
            {
                searchResult = directorySearcher.FindOne();
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
            directorySearcher.Dispose();

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