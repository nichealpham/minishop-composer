const state = {
  episodeNameUrl: null,
};

const getters = {
  getEpisodeName(state) {
    return state.episodeNameUrl;
  },
};

const mutations = {
  SET_EPISODE_NAME(state, payload) {
    state.episodeNameUrl = payload;
  },
};

export default {
  namespaced: "Public",
  state,
  mutations,
  getters,
};
