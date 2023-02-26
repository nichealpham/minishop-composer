import {
  getLocalStorage,
  removeLocalStorage,
  setLocalStorage,
} from "@/plugins/helpers";

var registerObject =
  getLocalStorage(process.env.VUE_APP_REGISTER_OBJECT) || "{}";

var { phone, fullName, email, dob, genderType, token, password } = JSON.parse(
  registerObject
);
var saveProfileToLocalStorage = (profile = {}) => {
  setLocalStorage(process.env.VUE_APP_REGISTER_OBJECT, JSON.stringify(profile));
};

const state = {
  profile: {
    phone,
    fullName,
    email,
    dob,
    genderType,
    token,
    password,
  },
};

const getters = {
  isDoneVerifyPhone(state) {
    return state.profile.phone || false;
  },
  isDoneCreateUser(state) {
    // return getLocalStorage(process.env.VUE_APP_TOKEN_NAME) || false;
    var { fullName, email, password, token } = state.profile;
    if (token) return true;
    if (fullName && email && password) return true;
    return false;
  },
  isDoneAddMoreInfo(state) {
    return state.profile.genderType && state.profile.dob;
  },
  userRegister(state) {
    return state.profile;
  },
};

const mutations = {
  SET_PHONE_NUMBER(state, payload) {
    state.profile.phone = payload;
    saveProfileToLocalStorage(state.profile);
  },
  SET_INFO(state, payload) {
    var { fullName, password, email } = payload;
    state.profile.fullName = fullName;
    state.profile.password = password;
    state.profile.email = email;
    saveProfileToLocalStorage(state.profile);
  },
  SET_MORE_INFO(state, payload) {
    var { dob, genderType } = payload;
    state.profile.dob = dob;
    state.profile.genderType = genderType;
    saveProfileToLocalStorage(state.profile);
  },
  SET_OAUTH_TOKEN(state, payload) {
    token = payload;
    state.profile.token = token;
    saveProfileToLocalStorage(state.profile);
  },
  CLEAR_REGISTER(state) {
    state.profile = {};
    removeLocalStorage(process.env.VUE_APP_REGISTER_OBJECT);
  },
};

export default {
  namespaced: "Signup",
  state,
  mutations,
  getters,
};
