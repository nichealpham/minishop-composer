const state = {
  diaglog: {
    state: false,
    title: "Detail",
  },
};

const getters = {
  diaglogState(state) {
    return state.diaglog.state;
  },
  diaglogTitle(state) {
    return state.diaglog.title;
  },
};

const mutations = {
  SET_DIAGLOG_STATE(state, payload) {
    state.diaglog.state = payload;
  },
  SET_DIAGLOG_TITLE(state, payload) {
    state.diaglog.title = payload;
  },
};

export default {
  namespaced: "Mobile",
  state,
  mutations,
  getters,
};
