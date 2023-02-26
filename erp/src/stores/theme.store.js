import { BackgroundType } from "../plugins/contants";
// import store from "@/stores";

const state = {
  background: BackgroundType.window,
};

const getters = {
  getBackground(state) {
    // if (!store.getters["Authen/getToken"]) {
    //   return `url('${require(`@/assets/backgrounds/desert-b2.png`)}')`;
    // }
    return `url('${require(`@/assets/backgrounds/${state.background}`)}')`;
  },
};

const mutations = {
  SET_BACKGROUND(state, payload) {
    state.background = payload;
  },
  SET_BLUR(state, payload) {
    state.blur = payload;
  },
};

export default {
  namespaced: "Theme",
  state,
  mutations,
  getters,
};
