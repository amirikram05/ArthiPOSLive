using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows;

namespace ArthiPOS.Utill
{
    class FirebaseApi
    {
        private string apiKey = "AIzaSyCobZ9Q4-HOgme_GP2U4R2CbikoC7TRLyQ";

        public async Task<string> GetEmailFromUserId(string userId)
        {
            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync($"https://identitytoolkit.googleapis.com/v1/accounts:lookup?key={apiKey}");
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var lookupResponse = JsonConvert.DeserializeObject<FirebaseUserLookupResponse>(responseContent);
                    var user = lookupResponse.users.FirstOrDefault(u => u.localId == userId);

                    if (user != null)
                    {
                        return user.email;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                // Handle network or other exceptions
                throw new Exception("Error retrieving user details.", ex);
            }
        }
        public async Task<string> SignInWithEmailAndPassword(string email, string password)
    {

        try
        {
            var request = new
            {
                email,
                password,
                returnSecureToken = true
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Successful sign-in
                var authResult = JsonConvert.DeserializeObject<FirebaseAuthResponse>(responseContent);
                var token = authResult.idToken;

                return token;
            }
            else
            {
                // Handle sign-in error
                var errorResponse = JsonConvert.DeserializeObject<FirebaseErrorResponse>(responseContent);
                throw new Exception(errorResponse.error.message);
            }
        }
        catch (Exception ex)
        {
            // Handle network or other exceptions
            throw new Exception("Sign-in failed.", ex);
        }
    }

    private class FirebaseAuthResponse
    {
        public string idToken { get; set; }
        // Other properties as needed
    }

    private class FirebaseErrorResponse
    {
        public FirebaseError error { get; set; }
    }

    private class FirebaseError
    {
        public string message { get; set; }
        // Other properties as needed
    }
        private class FirebaseUserLookupResponse
        {
            public List<FirebaseUser> users { get; set; }
        }

        private class FirebaseUser
        {
            public string localId { get; set; }
            public string email { get; set; }
            // Other user properties as needed
        }

    }
}
