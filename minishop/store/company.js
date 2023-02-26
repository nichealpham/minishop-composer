import { $httpClient } from '~/plugins/constants';

export const state = () => ({
    company: {
        sandrasoft: {}
    },
    current: 'sandrasoft'
});

export const getters = {
    getCompany: state => {
        return state.company[state.current];
    }
};

export const mutations = {
    ['SET_COMPANY'](state, payload) {
        var { current, company } = payload;
        state.current = current;
        state.company[current] = company;
    },
    ['CLEAR_COMPANY'](state, payload) {
        var { current } = payload;
        delete state.company[current];
    }
};

export const actions = {
    getCompany: async function({ commit, getters }, payload) {
        var { current } = payload;
        var result = await $httpClient.get(`/public/companies/${current}`);
        if (result.error) return console.error(result.error);
        var payload = { current, company: result };
        commit('SET_COMPANY', payload);
    }
};
