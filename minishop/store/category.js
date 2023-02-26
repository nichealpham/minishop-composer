import { $httpClient } from '~/plugins/constants';

export const state = () => ({
    query: {
        keySearch: '',
        deleted: false
    },
    headers: {
        page: 1,
        limit: 100
    },
    categories: []
});

export const getters = {
    categories: state => state.categories,
    getQueryOptions: state => () => ({
        query: state.query,
        headers: state.headers
    })
};

export const mutations = {
    ['SET_CATEGORIES'](state, payload) {
        state.categories = payload || [];
    }
};

export const actions = {
    renderCategories: async function({ commit, getters }, payload) {
        var companyName = payload;
        var { query, headers } = getters.getQueryOptions();
        var { items, error } = await $httpClient.get(
            `/public/companies/${companyName}/categories`,
            query,
            headers
        );
        if (error) return console.error(error);
        items = items.map(i => convertCategory(i));
        commit('SET_CATEGORIES', items);
    }
};

function convertCategory(i) {
    return {
        name: i.categoryName,
        slug: i.categoryID,
        count: i.itemCounts
    };
}
