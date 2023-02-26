const state = {
  notification: null,
  new: false,
  count: false,
};

const getters = {
  notification(state) {
    return state.notification;
  },
  new(state) {
    return state.new;
  },
  count(state) {
    return state.count;
  },
};

const mutations = {
  SET_NEW(state, payload) {
    state.notification = payload;
    state.new = true;
    setTimeout(() => {
      state.new = false;
    }, 2000);
  },
  SET_COUNT(state) {
    state.count = true;
    setTimeout(() => {
      state.count = false;
    }, 2000);
  },
};

export default {
  namespaced: "Fcm",
  state,
  mutations,
  getters,
};
