using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_GOPR_SERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        [HttpGet]
        public async void GetAuthFirebase(string phoneNumber)
        {
            if(FirebaseApp.DefaultInstance is null)
            {
                const string GOOGLE_APPLICATION_CREDENTIALS = "C:\\Users\\adm\\Downloads\\service-account-file.json";
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(GOOGLE_APPLICATION_CREDENTIALS),
                });
            }
            try
            {
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserByPhoneNumberAsync(phoneNumber);
                // See the UserRecord reference doc for the contents of userRecord.
                Console.WriteLine($"Successfully fetched user data: {userRecord.Uid}");               

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("all users")]
        public async void GetAllUsersFirebase()
        {
            if (FirebaseApp.DefaultInstance is null)
            {
                const string GOOGLE_APPLICATION_CREDENTIALS = "C:\\Users\\adm\\Downloads\\service-account-file.json";
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(GOOGLE_APPLICATION_CREDENTIALS),
                });
            }
            try
            {
                // Lookup the user associated with the specified uid.
                UserRecord client = await FirebaseAuth.DefaultInstance.GetUserAsync("IXtSq9qmBEO5Ouk0CZGh77Ymgde2");
                Console.WriteLine(client.CustomClaims["admin"]);
                // Start listing users from the beginning, 1000 at a time.
                var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
                var responses = pagedEnumerable.AsRawResponses().GetAsyncEnumerator();
                while (await responses.MoveNextAsync())
                {
                    ExportedUserRecords response = responses.Current;
                    foreach (ExportedUserRecord user in response.Users)
                    {
                        Console.WriteLine($"User: {user.Uid}");
                    }
                }

                // Iterate through all users. This will still retrieve users in batches,
                // buffering no more than 1000 users in memory at a time.
                var enumerator = FirebaseAuth.DefaultInstance.ListUsersAsync(null).GetAsyncEnumerator();
                while (await enumerator.MoveNextAsync())
                {
                    ExportedUserRecord user = enumerator.Current;
                    Console.WriteLine($"User: {user.Uid}");
                }


            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message); ;
            }
        }
        [HttpPost]
        public async void CreateUser()
        {
            if (FirebaseApp.DefaultInstance is null)
            {
                const string GOOGLE_APPLICATION_CREDENTIALS = "C:\\Users\\adm\\Downloads\\service-account-file.json";
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(GOOGLE_APPLICATION_CREDENTIALS),
                });
            }
            UserRecordArgs args = new UserRecordArgs()
            {
                Email = "user@example.com",
                EmailVerified = false,
                PhoneNumber = "+11234567890",
                DisplayName = "John Doe",
                PhotoUrl = "http://www.example.com/12345678/photo.png",
                Disabled = false,
            };
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);
            // See the UserRecord reference doc for the contents of userRecord.
            Console.WriteLine($"Successfully created new user: {userRecord.Uid}");
        }
        [HttpPut]
        public async void UpdateUser(string uid)
        {
            if (FirebaseApp.DefaultInstance is null)
            {
                const string GOOGLE_APPLICATION_CREDENTIALS = "C:\\Users\\adm\\Downloads\\service-account-file.json";
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(GOOGLE_APPLICATION_CREDENTIALS),
                });
            }
            UserRecordArgs args = new UserRecordArgs()
            {
                Uid = uid,
                Email = "modifiedUser@example.com",
                PhoneNumber = "+11234567890",
                EmailVerified = true,
                DisplayName = "Jane Doe",
                PhotoUrl = "http://www.example.com/12345678/photo.png",
                Disabled = true,
            };
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.UpdateUserAsync(args);
            // See the UserRecord reference doc for the contents of userRecord.
            Console.WriteLine($"Successfully updated user: {userRecord.Uid}");
        }
        [HttpDelete]
        public async void DeleteUser(string uid)
        {
            if (FirebaseApp.DefaultInstance is null)
            {
                const string GOOGLE_APPLICATION_CREDENTIALS = "C:\\Users\\adm\\Downloads\\service-account-file.json";
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(GOOGLE_APPLICATION_CREDENTIALS),
                });
            }
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(uid);
            Console.WriteLine("Successfully deleted user.");
        }
        [HttpDelete("several users(3)")]
        public async void DeleteUsers(string uid1, string uid2, string uid3)
        {
            DeleteUsersResult result = await FirebaseAuth.DefaultInstance.DeleteUsersAsync(new List<string>
                    {
                        uid1,
                        uid2,
                        uid3,
                    });
            if(result.SuccessCount > 0)
            Console.WriteLine($"Successfully deleted {result.SuccessCount} users.");
            else
            Console.WriteLine($"Failed to delete {result.FailureCount} users.");

            foreach (ErrorInfo err in result.Errors)
            {
                Console.WriteLine($"Error #{err.Index}, reason: {err.Reason}");
            }

        }
    }
}
