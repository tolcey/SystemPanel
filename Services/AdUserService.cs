using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using Microsoft.Extensions.Logging;
using SystemPanel.Models;
using Microsoft.Extensions.Configuration;

namespace SystemPanel.Services
{
    public class AdUserService
    {
        private readonly string _domain;
        private readonly string _username;
        private readonly string _password;
        private readonly ILogger<AdUserService> _logger;

        public AdUserService(IConfiguration configuration, ILogger<AdUserService> logger)
        {
            _domain = configuration["AdSettings:Domain"];
            _username = configuration["AdSettings:Username"];
            _password = configuration["AdSettings:Password"];
            _logger = logger;
        }

        // AD'den tüm kullanıcıları almak için metod
        public List<AdUser> GetAllAdUsers()
        {
            var users = new List<AdUser>();

            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, _domain, _username, _password))
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll())
                    {
                        var directoryEntry = result.GetUnderlyingObject() as System.DirectoryServices.DirectoryEntry;
                        if (directoryEntry != null)
                        {
                            users.Add(new AdUser
                            {
                                UserName = directoryEntry.Properties["sAMAccountName"]?.Value?.ToString(),
                                DisplayName = directoryEntry.Properties["displayName"]?.Value?.ToString(),
                                Email = directoryEntry.Properties["mail"]?.Value?.ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching AD users: {ex.Message}");
            }

            return users;
        }

        // AD kullanıcısını silme işlemi
        public bool DeleteUser(string userName)
        {
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, _domain, _username, _password))
                {
                    var user = UserPrincipal.FindByIdentity(context, userName);
                    if (user != null)
                    {
                        user.Delete();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting AD user {userName}: {ex.Message}");
            }

            return false;
        }
    }
}
