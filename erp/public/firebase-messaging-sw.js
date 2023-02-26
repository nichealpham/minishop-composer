//firebase-messaging-sw.js
/*eslint-disable*/
importScripts("https://www.gstatic.com/firebasejs/8.2.7/firebase-app.js");
importScripts("https://www.gstatic.com/firebasejs/8.2.7/firebase-messaging.js");

self.addEventListener("notificationclick", function (event) {
  console.debug("SW notification click event", event);
  const url = event.notification.data.link;
  event.waitUntil(
    clients.matchAll({ type: "window" }).then((windowClients) => {
      // Check if there is already a window/tab open with the target URL
      for (var i = 0; i < windowClients.length; i++) {
        var client = windowClients[i];
        // If so, just focus it.
        if (client.url === url && "focus" in client) {
          return client.focus();
        }
      }
      // If not, then open the target URL in a new window/tab.
      if (clients.openWindow) {
        return clients.openWindow(url);
      }
    })
  );
});

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

firebase.initializeApp(firebaseConfig);

const messaging = firebase.messaging();

messaging.setBackgroundMessageHandler(function (payload) {
  var { redirect, message } = transformNotificationPayload(payload);
  const notificationTitle = "Sandrasoft";
  const notificationOptions = {
    title: "Sandrasoft",
    body: message,
    icon: "https://his.sandrasoft.app/favicon2.ico",
    data: {
      link: redirect,
    },
    actions: [{ action: "open_url", title: "View" }],
  };
  return self.registration.showNotification(
    notificationTitle,
    notificationOptions
  );
});

const ServiceCode = {
  User: 0,
  Booking: 1,
  Episode: 2,
  Doctor: 3,
  Service: 4,
  Clinic: 5,
};
const ServiceCodeNames = Object.keys(ServiceCode);
const ServiceCodeRedirects = [
  "/patient",
  "/booking",
  "/episode",
  "/setting",
  "/setting",
  "/setting",
];
const ActionCode = {
  Deleted: 0,
  Created: 1,
  Booked: 2,
  Invited: 3,
  Accepted: 4,
  Checkin: 5,
  Success: 6,
};
const ActionCodeMessages = [
  "{{item}}: {{addition}} has been cancelled!",
  "{{name}} has successfully created a new {{item}}: {{addition}}.",
  "{{name}} has been appointed new {{item}}: {{addition}}.",
  "{{name}} has been invited to {{item}}: {{addition}}.",
  "{{item}}: {{addition}} has been accepted!",
  "{{item}}: {{addition}} has been checked-in!",
  "{{item}}: {{addition}} has completed successfully!",
];

function transformNotificationPayload(payload) {
  var objold = JSON.parse(payload.data.metadata);
  var obj = {};
  for (var key of Object.keys(objold)) {
    var val = objold[key];
    var newKey = (key.charAt(0).toLowerCase() + key.slice(1) || key).toString();
    obj[newKey] = val;
  }
  var { serviceCode, actionCode, targetItemID, metadata } = obj;

  if (typeof metadata == "string") metadata = JSON.parse(metadata);

  var redirect = ServiceCodeRedirects[serviceCode] + "?id=" + targetItemID;
  var item = `${ServiceCodeNames[serviceCode]}`;
  var addition = "";

  if (serviceCode == ServiceCode.Booking) {
    var { Service, UserCreate, UserAppoint } = metadata;
    if (Service && UserCreate && UserAppoint) {
      addition = `${Service.ServiceName}`;
      addition += `, patient: ${UserCreate.FullName}`;
      addition += `, doctor: ${UserAppoint.FullName}`;
    }
  }

  if (serviceCode == ServiceCode.Episode) {
    var { Records, UserAdmittedID, UserAdmitted } = metadata;
    if (Records && Records[0] && UserAdmittedID && UserAdmitted) {
      addition = `${Records[0].Service.ServiceName}`;
      addition += `, patient ${UserAdmitted.FullName}`;
      addition += `, doctor ${Records[0].UserAppoint.FullName}`;
    }
  }

  var name = "You";
  var message = ActionCodeMessages[actionCode]
    .replace("{{name}}", name)
    .replace("{{addition}}", addition)
    .replace("{{item}}", item);

  return {
    redirect,
    message,
  };
}
