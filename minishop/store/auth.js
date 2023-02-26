import {
    getLocalStorage,
    setLocalStorage,
    removeLocalStorage
} from '@/plugins/constants';
import { $httpClient } from '@/plugins/constants';

export const state = () => ({
    profile: {},
    isRegister: false,
    redirectUrl: ''
});

export const getters = {
    token: () => getLocalStorage(process.env.NUXT_APP_TOKEN_NAME),
    profile: state => state.profile,
    isRegister: state => state.isRegister,
    redirectUrl: state => state.redirectUrl,
    getToken: () => () => getLocalStorage(process.env.NUXT_APP_TOKEN_NAME),
    getProfile: state => () => state.profile
};

export const mutations = {
    ['LOGOUT'](state, payload) {
        state.profile = {};
        removeLocalStorage(process.env.NUXT_APP_USER_OBJECT);
        removeLocalStorage(process.env.NUXT_APP_TOKEN_NAME);
    },
    ['SET_TOKEN'](state, payload) {
        setLocalStorage(process.env.NUXT_APP_TOKEN_NAME, payload);
    },
    ['SET_PROFILE'](state, payload) {
        state.profile = payload;
    },
    ['SET_IS_REGISTER'](state, payload) {
        state.isRegister = payload;
    },
    ['SET_REDIRECT_URL'](state, payload) {
        state.redirectUrl = payload;
    }
};

export const actions = {
    getProfile: async function({ commit, getters }, payload) {
        var token = getters.getToken();
        if (!token) return;
        var result = await $httpClient.get(
            `/user/profile`,
            {},
            { Authorization: token }
        );
        if (!result) return;
        if (result.error) return console.error(result.error);
        var profile = getters.getProfile();
        var body = { ...profile, ...result };
        commit('SET_PROFILE', body);
    }
};
