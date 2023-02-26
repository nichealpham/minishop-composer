import moment from "moment";
import { getBrowserLanguage } from "@/lang/i18n";
import { getLocalStorage } from "./helpers";

export const UserType = {
  patient: {
    name: "Patient",
    value: 1,
  },
  receptionist: {
    name: "Receptionist",
    value: 2,
  },
  doctor: {
    name: "Doctor",
    value: 3,
  },
  owner: {
    name: "Owner",
    value: 4,
  },
};

export const ViewMode = {
  List: 1,
  Table: 2,
};

export const TargetItemType = {
  Service: 1,
  Asset: 2,
  Episode: 3,
};

export const AttachmentType = {
  Image: 1,
};

export const RoleType = {
  Patient: 1,
  Receptionist: 2,
  Doctor: 3,
  Owner: 4,
};

export const AssetType = {
  Asset: 1,
  Consumable: 2,
};

export const BackgroundType = {
  rocket: "rocket.png",
  window: "window.png",
  event: "event.png",
  mountain: "mountain.png",
  earth: "earth.png",
  lake: "lake.png",
  seaside: "seaside.png",
  road: "road.png",
  desert: "desert-b2.png",
  room: "room.png",
};

export const EpisodeStatusType = {
  // Soft delete
  Deleted: 0,
  // Checked-in => Doctor or Owner checkin patient
  CheckedIn: 1,
  // Success => Finised exam
  Success: 2,
  // Failed => Cancel
  Canceled: 3,
};

export const ServiceCode = {
  User: 0,
  Booking: 1,
  Episode: 2,
  Doctor: 3,
  Service: 4,
  Clinic: 5,
};

import enConfigs from "@/lang/en.js";
import vnConfigs from "@/lang/vn.js";

function getServiceCodeNames() {
  if (getBrowserLanguage() == "vn") {
    return [
      vnConfigs.nav.patient,
      vnConfigs.nav.booking,
      vnConfigs.nav.episode,
      vnConfigs.nav.doctor,
      vnConfigs.nav.service,
      vnConfigs.nav.clinic,
    ];
  }
  return [
    enConfigs.nav.patient,
    enConfigs.nav.booking,
    enConfigs.nav.episode,
    enConfigs.nav.doctor,
    enConfigs.nav.service,
    enConfigs.nav.clinic,
  ];
}

export const ServiceCodeNames = getServiceCodeNames();

Object.keys(ServiceCode);
export const ServiceCodeIcons = [
  "mdi-account-outline",
  "mdi-calendar-month-outline",
  "mdi-clipboard-file-outline",
  "mdi-doctor",
  "mdi-clipboard-file-outline",
  "mdi-home-plus-outline",
];
export const ServiceCodeRedirects = [
  "/patient",
  "/booking",
  "/episode",
  "/setting",
  "/setting",
  "/setting",
];

export const ActionCode = {
  Deleted: 0,
  Created: 1,
  Booked: 2,
  Invited: 3,
  Accepted: 4,
  Checkin: 5,
  Success: 6,
};

function getActionCodeMessages() {
  if (getBrowserLanguage() == "vn") {
    return [
      "{{item}}: {{addition}} đã bị hủy hoặc dời lại!",
      "{{name}} đã tạo thành công {{item}}: {{addition}}.",
      "{{name}} được đặt một {{item}}: {{addition}}.",
      "{{name}} được mời tham dự vào {{item}}: {{addition}}.",
      "{{item}}: {{addition}} đã được chấp nhận!",
      "{{item}}: {{addition}} đã được tiếp nhận!",
      "{{item}}: {{addition}} đã hoàn thành!",
    ];
  }
  return [
    "{{item}}: {{addition}} has been cancelled!",
    "{{name}} has successfully created a new {{item}}: {{addition}}.",
    "{{name}} has been appointed new {{item}}: {{addition}}.",
    "{{name}} has been invited to {{item}}: {{addition}}.",
    "{{item}}: {{addition}} has been accepted!",
    "{{item}}: {{addition}} has been checked-in!",
    "{{item}}: {{addition}} has completed successfully!",
  ];
}
export const ActionCodeMessages = getActionCodeMessages();

export const StatusCode = {
  Danger: 0,
  Info: 1,
  Warning: 2,
  Success: 3,
};
export const StatusCodeColors = ["#E91E63", "#605bff", "#FFC107", "#4CAF50"];

export function TransformFcmNotificationItem(notiObj) {
  var obj = {};
  for (var key of Object.keys(notiObj)) {
    var val = notiObj[key];
    var newKey = (key.charAt(0).toLowerCase() + key.slice(1) || key).toString();
    obj[newKey] = val;
  }
  var result = TransformNotificationItem(obj);
  result.message = result.message.split("<b>").join("").split("</b>").join("");
  return result;
}

export function TransformNotificationItem(notiObj) {
  var withMessage = getBrowserLanguage() == "vn" ? "với" : "with";
  var youMessage = getBrowserLanguage() == "vn" ? "Bạn" : "You";

  var { userID } = JSON.parse(
    getLocalStorage(process.env.VUE_APP_USER_OBJECT) || `{ 'userID': ''}`
  );
  userID = userID.toLowerCase();
  var {
    serviceCode,
    actionCode,
    statusCode,
    targetItemID,
    dateCreated,
    isRead,
    activityID,
    metadata,
  } = notiObj;
  if (typeof metadata == "string") metadata = JSON.parse(metadata);
  var icon = ServiceCodeIcons[serviceCode];
  var redirect = ServiceCodeRedirects[serviceCode] + "?id=" + targetItemID;
  var color = StatusCodeColors[statusCode];
  var item = `${ServiceCodeNames[serviceCode]}`;
  var addition = "";
  if (serviceCode == ServiceCode.Booking) {
    var { Service, UserCreate, UserAppoint, UserAppointID } = metadata;
    if (Service && UserCreate && UserAppoint) {
      addition = `<b>${Service.ServiceName}</b>`;
      if (userID == UserAppointID) {
        addition += `, ${withMessage} <b>${UserCreate.FullName}</b>`;
      } else {
        addition += `, ${withMessage} <b>${UserAppoint.FullName}</b>`;
      }
    }
  }
  if (serviceCode == ServiceCode.Episode) {
    var { Records, UserAdmittedID, UserAdmitted } = metadata;
    if (Records && Records[0] && UserAdmittedID && UserAdmitted) {
      addition = `<b>${Records[0].Service.ServiceName}</b>`;
      if (userID == UserAdmittedID) {
        addition += `, ${withMessage} <b>${Records[0].UserAppoint.FullName}</b>`;
      } else {
        addition += `, ${withMessage} <b>${UserAdmitted.FullName}</b>`;
      }
    }
  }
  var name = `<b>${youMessage}</b>`;
  var message = ActionCodeMessages[actionCode]
    .replace("{{name}}", name)
    .replace("{{addition}}", addition)
    .replace("{{item}}", item);
  if (serviceCode) var time = moment(dateCreated).format("HH:mm, DD/MM/YYYY");
  return {
    id: activityID,
    icon,
    redirect,
    color,
    isRead,
    time,
    message,
    statusCode,
    targetItemID,
    serviceCode,
  };
}
