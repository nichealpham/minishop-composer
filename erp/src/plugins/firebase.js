import firebase from "firebase";

var firebaseConfig = {
  apiKey: "AIzaSyCULRDlEazoWppblYBs21AeMXLsuQ1cLbo",
  authDomain: "minishop-public-open-source.firebaseapp.com",
  databaseURL:
    "https://minishop-public-open-source-default-rtdb.asia-southeast1.firebasedatabase.app",
  projectId: "minishop-public-open-source",
  storageBucket: "minishop-public-open-source.appspot.com",
  messagingSenderId: "960505131231",
  appId: "1:960505131231:web:91c4ac1a24997b4578f96a",
  measurementId: "G-RLEFXHJKC3",
};

// Initialize Firebase
firebase.initializeApp(firebaseConfig);

export const database = firebase.database();

// firebase authentication
firebase.auth().useDeviceLanguage();
var GoogleAuthProvider = new firebase.auth.GoogleAuthProvider();
var FacebookAuthProvider = new firebase.auth.FacebookAuthProvider();

export const OAuthProviderType = {
  Google: 1,
  Facebook: 2,
};

export function OAuthSignIn(providerType = OAuthProviderType.Google) {
  var provider;
  switch (providerType) {
    case OAuthProviderType.Google:
      provider = GoogleAuthProvider;
      break;
    case OAuthProviderType.Facebook:
      provider = FacebookAuthProvider;
      break;
    default:
      provider = null;
  }
  if (provider == null) {
    throw new Error("OAuthSignIn failed due to no provider supplied");
  }
  return new Promise((resolve, reject) => {
    firebase
      .auth()
      .signInWithPopup(provider)
      .then(() => {
        /** @type {firebase.auth.OAuthCredential} */
        firebase
          .auth()
          .currentUser.getIdToken(/* forceRefresh */ true)
          .then(function (idToken) {
            return resolve(idToken);
          })
          .catch(function (error) {
            return reject(error);
          });
      })
      .catch((error) => {
        return reject(error);
      });
  });
}

export function OAuthSignOut() {
  return new Promise((resolve, reject) => {
    firebase
      .auth()
      .signOut()
      .then(() => {
        return resolve(true);
      })
      .catch((error) => {
        return reject(error);
      });
  });
}

// Verify Phone Number
export async function getRecapthaVerifierInstance() {
  if (window.recaptchaVerifier) {
    return window.recaptchaVerifier;
  }
  window.recaptchaVerifier = new firebase.auth.RecaptchaVerifier(
    "verify-phone-number-btn",
    {
      size: "invisible",
    }
  );
  return window.recaptchaVerifier;
}

export async function sendConfirmationPhoneNumber(phoneNumber) {
  var appVerifier = window.recaptchaVerifier;
  return new Promise((resolve, reject) => {
    firebase
      .auth()
      .signInWithPhoneNumber(phoneNumber, appVerifier)
      .then((confirmationResult) => {
        // SMS sent. Prompt user to type the code from the message, then sign the
        // user in with confirmationResult.confirm(code).
        window.confirmationResult = confirmationResult;
        return resolve(confirmationResult);
        // ...
      })
      .catch((error) => {
        reject(error);
        appVerifier.reset(0);
      });
  });
}

export async function verifyConfirmationPhoneNumber(code) {
  return new Promise((resolve, reject) => {
    window.confirmationResult
      .confirm(code)
      .then((result) => {
        // User signed in successfully.
        const user = result.user;
        return resolve(user);
      })
      .catch((error) => {
        // User couldn't sign in (bad verification code?)
        return reject(error);
      });
  });
}

// firebase storage
// Get a reference to the storage service, which is used to create references in your storage bucket
var storage = firebase.storage();

// Create a storage reference from our storage service
var storageRef = storage.ref();

export async function firebaseUpload(file, name, onProgressFn) {
  var path = `public/${name}`;
  var uploadTask = storageRef.child(path).put(file);
  return new Promise((resolve, reject) => {
    uploadTask.on(
      firebase.storage.TaskEvent.STATE_CHANGED,
      // on uploadString progress,
      function (snapshot) {
        var progress = (snapshot.bytesTransferred / snapshot.totalBytes) * 100;
        if (onProgressFn) {
          onProgressFn(progress);
        }
      },
      function (error) {
        return reject(error);
      },
      function () {
        // Upload completed successfully, now we can get the download URL
        uploadTask.snapshot.ref.getDownloadURL().then(function (downloadURL) {
          return resolve(downloadURL);
        });
      }
    );
  });
}

export async function firebaseUploadBase64(base64Str, name, onProgressFn) {
  var path = `public/${name}`;
  var uploadTask = storageRef.child(path).putString(base64Str, "data_url");
  return new Promise((resolve, reject) => {
    uploadTask.on(
      firebase.storage.TaskEvent.STATE_CHANGED,
      // on upload progress,
      function (snapshot) {
        var progress = (snapshot.bytesTransferred / snapshot.totalBytes) * 100;
        if (onProgressFn) {
          onProgressFn(progress);
        }
      },
      function (error) {
        return reject(error);
      },
      function () {
        // Upload completed successfully, now we can get the download URL
        uploadTask.snapshot.ref.getDownloadURL().then(function (downloadURL) {
          return resolve(downloadURL);
        });
      }
    );
  });
}

// firebase messaging
var fcmWebPushToken =
  "BN9ZOHCHk-pGlb5k3KtaPqrulUTowyyxHXk3--ErHkXqnTHGFNmtDAbf4zqhxXdbfACZvsTKwnzPlDk70VIZ57A";

function getFcmInstance() {
  if (firebase.messaging.isSupported()) {
    return firebase.messaging();
  } else {
    return {
      onMessage: async () => {
        return true;
      },
    };
  }
}

export const FirebaseMessaging = getFcmInstance();

export async function getDeviceFcmToken() {
  return new Promise((resolve, reject) => {
    // Check browser support first hand
    if (firebase.messaging.isSupported()) {
      console.log("This browser có hỗ trợ Notification. Hooray!");
      FirebaseMessaging.getToken({ vapidKey: fcmWebPushToken })
        .then((currentToken) => {
          if (currentToken) {
            return resolve(currentToken);
          } else {
            console.error(
              "No registration token available. Request permission to generate one."
            );
          }
        })
        .catch((err) => {
          console.error("An error occurred while retrieving token. ", err);
          return reject(err);
        });
    } else {
      return resolve(null);
    }
  });
}
