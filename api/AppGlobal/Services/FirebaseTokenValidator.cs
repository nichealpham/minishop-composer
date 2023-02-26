using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AppGlobal.Services
{
    public class FirebaseTokenValidator
    {
        public FirebaseTokenValidator()
        {
            EnsureFirebaseInnitialized();
        }

        #region KHỞI TẠO FIREBASE INSTANCE
        public void EnsureFirebaseInnitialized()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                string serviceAccountPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebase.json");

                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(serviceAccountPath),
                });
            }
        }

        public FirebaseToken VerifyTokenAsync(string token)
        {
            var task = Task.Run(async () => await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token));
            return task.Result;
        }
        #endregion
    }
}
