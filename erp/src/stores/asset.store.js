const state = {
  assetID: null,
  serviceID: null,
  showScanner: false,
  categoryOptions: [],
  categoryOptionsLoaded: false,
};

const getters = {
  getAssetID(state) {
    return state.assetID;
  },
  getServiceID(state) {
    return state.serviceID;
  },
  getShowScanner(state) {
    return state.showScanner;
  },
  getCategoryOptions(state) {
    return state.categoryOptions;
  },
  getCategoryOptionsLoaded(state) {
    return state.categoryOptionsLoaded;
  },
};

const mutations = {
  SET_ASSET_ID(state, payload) {
    state.assetID = payload;
  },
  SET_SERVICE_ID(state, payload) {
    state.serviceID = payload;
  },
  OPEN_SCANNER() {
    state.showScanner = true;
  },
  CLOSE_SCANNER() {
    state.showScanner = false;
  },
  SET_CATEGORY_OPTIONS(state, payload) {
    state.categoryOptions = payload;
    state.categoryOptionsLoaded = true;
  },
  PUSH_CATEGORY_OPTIONS(state, payload) {
    state.categoryOptions.push(payload);
  },
};

export default {
  namespaced: "Asset",
  state,
  mutations,
  getters,
};
