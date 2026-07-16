using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using HRMS.Services.Identity.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HRMS.Services.Identity.Infrastructure.Services;

public interface IFirebaseAuthService
{
    Task InitializeAppAsync();
    Task<FirebaseToken?> VerifyIdTokenAsync(string idToken);
    Task<UserRecord?> GetUserByEmailAsync(string email);
    Task<UserRecord?> CreateUserAsync(string email, string password, string displayName);
    Task SetCustomClaimsAsync(string uid, IDictionary<string, object> claims);
    Task DeleteUserAsync(string uid);
}

public class FirebaseAuthService : IFirebaseAuthService
{
    private readonly FirebaseAuthSettings _settings;
    private readonly ILogger<FirebaseAuthService> _logger;
    private bool _initialized;

    public FirebaseAuthService(
        IOptions<FirebaseAuthSettings> settings,
        ILogger<FirebaseAuthService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public Task InitializeAppAsync()
    {
        if (_initialized) return Task.CompletedTask;

        if (FirebaseApp.DefaultInstance is not null)
        {
            _initialized = true;
            return Task.CompletedTask;
        }

        try
        {
            FirebaseApp? app;

            if (!string.IsNullOrEmpty(_settings.ServiceAccountKeyJson))
            {
                var credential = GoogleCredential.FromJson(_settings.ServiceAccountKeyJson);
                app = FirebaseApp.Create(new AppOptions
                {
                    Credential = credential,
                    ProjectId = _settings.ProjectId
                });
            }
            else if (!string.IsNullOrEmpty(_settings.ServiceAccountKeyPath) &&
                     File.Exists(_settings.ServiceAccountKeyPath))
            {
                var credential = GoogleCredential.FromFile(_settings.ServiceAccountKeyPath);
                app = FirebaseApp.Create(new AppOptions
                {
                    Credential = credential,
                    ProjectId = _settings.ProjectId
                });
            }
            else
            {
                _logger.LogWarning("Firebase service account not configured. Firebase features will be disabled.");
                return Task.CompletedTask;
            }

            _initialized = true;
            _logger.LogInformation("Firebase Auth initialized for project {ProjectId}", _settings.ProjectId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize Firebase Auth");
            throw;
        }

        return Task.CompletedTask;
    }

    public async Task<FirebaseToken?> VerifyIdTokenAsync(string idToken)
    {
        EnsureInitialized();

        try
        {
            var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;
            var token = await auth.VerifyIdTokenAsync(idToken);
            return token;
        }
        catch (FirebaseAuthException ex)
        {
            _logger.LogWarning(ex, "Failed to verify Firebase ID token");
            return null;
        }
    }

    public async Task<UserRecord?> GetUserByEmailAsync(string email)
    {
        EnsureInitialized();

        try
        {
            var auth = FirebaseAuth.DefaultInstance;
            var userRecord = await auth.GetUserByEmailAsync(email);
            return userRecord;
        }
        catch (FirebaseAuthException ex)
        {
            _logger.LogWarning(ex, "Firebase user not found for email {Email}", email);
            return null;
        }
    }

    public async Task<UserRecord?> CreateUserAsync(string email, string password, string displayName)
    {
        EnsureInitialized();

        try
        {
            var auth = FirebaseAuth.DefaultInstance;
            var args = new UserRecordArgs
            {
                Email = email,
                Password = password,
                DisplayName = displayName,
                EmailVerified = false,
                Disabled = false
            };

            var userRecord = await auth.CreateUserAsync(args);
            _logger.LogInformation("Created Firebase user {Uid} for email {Email}", userRecord.Uid, email);
            return userRecord;
        }
        catch (FirebaseAuthException ex)
        {
            _logger.LogError(ex, "Failed to create Firebase user for email {Email}", email);
            return null;
        }
    }

    public async Task SetCustomClaimsAsync(string uid, IDictionary<string, object> claims)
    {
        EnsureInitialized();

        try
        {
            var auth = FirebaseAuth.DefaultInstance;
            await auth.SetCustomUserClaimsAsync(uid, claims.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
            _logger.LogInformation("Set custom claims for Firebase user {Uid}", uid);
        }
        catch (FirebaseAuthException ex)
        {
            _logger.LogError(ex, "Failed to set custom claims for Firebase user {Uid}", uid);
            throw;
        }
    }

    public async Task DeleteUserAsync(string uid)
    {
        EnsureInitialized();

        try
        {
            var auth = FirebaseAuth.DefaultInstance;
            await auth.DeleteUserAsync(uid);
            _logger.LogInformation("Deleted Firebase user {Uid}", uid);
        }
        catch (FirebaseAuthException ex)
        {
            _logger.LogError(ex, "Failed to delete Firebase user {Uid}", uid);
            throw;
        }
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException(
                "Firebase Auth has not been initialized. Call InitializeAppAsync first.");
    }
}

public class FirebaseAuthSettings
{
    public string ProjectId { get; set; } = string.Empty;
    public string ServiceAccountKeyPath { get; set; } = string.Empty;
    public string ServiceAccountKeyJson { get; set; } = string.Empty;
}
