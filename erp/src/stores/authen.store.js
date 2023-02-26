import {
  getLocalStorage,
  removeLocalStorage,
  setLocalStorage,
} from "@/plugins/helpers";
import { RoleType } from "@/plugins/contants";
import store from "@/stores";

var defaultRole = {
  clinicID: "",
  clinicName: "",
  roleType: RoleType.Patient,
};
function getUserStr() {
  return getLocalStorage(process.env.VUE_APP_USER_OBJECT) || "{}";
}

function getTokenStr() {
  return getLocalStorage(process.env.VUE_APP_TOKEN_NAME) || null;
}

function getFcmStr() {
  return getLocalStorage(process.env.VUE_APP_FCM_TOKEN) || null;
}

function getRoleStr() {
  var role = defaultRole;

  var roleStr = getLocalStorage(process.env.VUE_APP_ROLE_OBJECT);
  if (roleStr) {
    return roleStr;
  }
  try {
    var { roles } = JSON.parse(getUserStr());
    if (!roles || !roles.length) return JSON.stringify(role);

    role = roles[0];
    return JSON.stringify(role);
  } catch {
    return JSON.stringify(defaultRole);
  }
}

const state = {
  user: getUserStr(),
  token: getTokenStr(),
  fcm: getFcmStr(),
  role: getRoleStr(),
  clinicProfile: {},
};

const getters = {
  getUser(state) {
    return JSON.parse(state.user);
  },
  getToken(state) {
    return state.token;
  },
  getFcm(state) {
    return state.fcm;
  },
  getRole(state) {
    return JSON.parse(state.role);
  },
  isRolePatient(state) {
    return JSON.parse(state.role).roleType == RoleType.Patient;
  },
  isClinicStaff(state) {
    return JSON.parse(state.role).roleType != RoleType.Patient;
  },
  getClinicProfile(state) {
    return state.clinicProfile;
  },
};

const mutations = {
  SET_USER(state, payload) {
    var strUserObj = JSON.stringify(payload);
    setLocalStorage(process.env.VUE_APP_USER_OBJECT, strUserObj);
    state.user = strUserObj;
    // set role = first role
    var { roles } = payload;
    var roleStr = getLocalStorage(process.env.VUE_APP_ROLE_OBJECT);
    if (roleStr) {
      var role = JSON.parse(roleStr);
      if (
        roles.find(
          (r) => r.clinicID.toLowerCase() == role.clinicID.toLowerCase()
        )
      ) {
        store.commit("Authen/SET_ROLE", role.clinicID);
      }
    } else if (roles && roles.length) {
      var { clinicID } = roles[0];
      store.commit("Authen/SET_ROLE", clinicID);
    }
  },
  SET_CLINIC_PROFILE(state, payload) {
    state.clinicProfile = payload;
  },
  SET_TOKEN(state, payload) {
    setLocalStorage(process.env.VUE_APP_TOKEN_NAME, payload);
    state.token = payload;
  },
  SET_FCM(state, payload) {
    setLocalStorage(process.env.VUE_APP_FCM_TOKEN, payload);
    state.fcm = payload;
  },
  SET_ROLE(state, payload) {
    var clinicID = payload;
    var { roles } = JSON.parse(getUserStr());
    roles = roles || [];
    var role =
      roles.find((r) => r.clinicID.toLowerCase() == clinicID.toLowerCase()) ||
      defaultRole;
    var roleStr = JSON.stringify(role);
    setLocalStorage(process.env.VUE_APP_ROLE_OBJECT, roleStr);
    state.role = roleStr;
  },
  LOGOUT(state) {
    state.user = "{}";
    state.token = null;
    state.fcm = null;
    removeLocalStorage(process.env.VUE_APP_USER_OBJECT);
    removeLocalStorage(process.env.VUE_APP_TOKEN_NAME);
    removeLocalStorage(process.env.VUE_APP_ROLE_OBJECT);
    removeLocalStorage(process.env.VUE_APP_FCM_TOKEN);
  },
};

export default {
  namespaced: "Authen",
  state,
  mutations,
  getters,
};
