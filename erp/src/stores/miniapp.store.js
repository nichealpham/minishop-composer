const state = {
  openApp: null,
  currentEvent: null,
  hideMainApp: false,
};

const getters = {
  openApp(state) {
    return state.openApp;
  },
  currentEvent(state) {
    return state.currentEvent;
  },
  hideMainApp(state) {
    return state.hideMainApp;
  },
};

const mutations = {
  OPEN_APP(state, payload) {
    state.openApp = payload;
  },
  CURRENT_EVENT(state, payload) {
    state.currentEvent = payload;
  },
  HIDE_MAIN_APP(state, payload) {
    state.hideMainApp = payload;
  },
};

export default {
  namespaced: "MiniApp",
  state,
  mutations,
  getters,
};
