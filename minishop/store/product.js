import { $httpClient } from '~/plugins/constants';

export const state = () => ({
    query: {
        categoryIDs: '',
        priceLower: 0,
        priceUpper: 200000000,
        keySearch: '',
        productType: ''
    },
    pagination: {
        page: 1,
        limit: 8
    },
    loaded: false,
    totals: 0,
    items: [],
    cache: [],
    currentItem: null
});

export const getters = {
    getPagination: state => state.pagination,
    getItems: state => state.items,
    getLoaded: state => state.loaded,
    getTotals: state => state.totals,
    getCurrentItem: state => state.currentItem,
    getQueryOptions: state => () => ({
        query: state.query,
        headers: state.pagination
    }),
    getByID: state => id => {
        return state.cache.find(i => i.id == id) || null;
    }
};

export const mutations = {
    ['SET_PRODUCT_PRICE_RANGE'](state, payload) {
        payload = payload || [];
        state.query.priceLower = payload[0] || 0;
        state.query.priceUpper = payload[1] || 200000000;
    },
    ['SET_PRODUCT_CATEGORY'](state, payload) {
        var id = payload.toLowerCase();
        var categories = state.query.categoryIDs
            .split(',')
            .map(i => i.toLowerCase())
            .filter(i => !!i);
        var ind = categories.findIndex(c => c == id);
        if (ind != -1) {
            categories.splice(ind, 1);
        } else {
            categories.push(id);
        }
        state.query.categoryIDs = categories.join(',');
    },
    ['CLEAN_QUERY_OPTIONS'](state, payload) {
        state.query = {
            categoryIDs: '',
            priceLower: 0,
            priceUpper: 200000000,
            keySearch: '',
            productType: ''
        };
        state.pagination = {
            page: 1,
            limit: 8
        };
    },
    ['SET_PRODUCT_TYPE'](state, payload) {
        state.query.productType = payload;
    },
    ['SET_KEYSEARCH'](state, payload) {
        state.query.keySearch = payload;
    },
    ['SET_PAGINATION'](state, payload) {
        state.pagination = payload;
    },
    ['SET_TOTALS'](state, payload) {
        state.totals = payload;
    },
    ['SET_LOADED'](state, payload) {
        state.loaded = payload;
    },
    ['SET_ITEMS'](state, payload) {
        state.items = payload;
        // load into cache
        var ids = state.cache.map(i => i.id);
        state.cache = state.cache.concat(
            payload.filter(i => !ids.includes(i.id))
        );
        // stupid bug dont know why!!!
        state.cache = state.cache.map(c => ({ ...c, variants: [] }));
    },
    ['SET_CURRENT_ITEM'](state, payload) {
        state.currentItem = payload;
    },
    ['SET_CURRENT_DETAIL'](state, payload) {
        var { id, detail_desc } = payload;
        state.currentItem.detail_desc = detail_desc;
        var ind = state.cache.findIndex(c => c.id == id);
        if (ind != -1) {
            state.cache[ind].detail_desc = detail_desc;
        }
    },
    ['PUSH_PRODUCT_CACHE'](state, payload) {
        if (!state.cache.find(i => i.id == payload.id)) {
            state.cache.push(payload);
        }
    }
};

export const actions = {
    getDetail: async function({ commit, getters }, payload) {
        commit('SET_LOADED', false);
        var id = payload;
        // var product = getters.getByID(id);
        // if (product) {
        //     commit('SET_CURRENT_ITEM', product);
        //     var { detail_desc } = product;
        //     if (!detail_desc || detail_desc.error) {
        //         detail_desc = await $httpClient.get(
        //             `/public/products/${id}/details`
        //         );
        //     }
        //     commit('SET_CURRENT_DETAIL', { id, detail_desc });
        // } else {
        //     var rawProduct = await $httpClient.get(`/public/products/${id}`);
        //     product = convertProduct(rawProduct);
        //     commit('SET_CURRENT_ITEM', product);
        //     commit('PUSH_PRODUCT_CACHE', product);
        // }

        var rawProduct = await $httpClient.get(`/public/products/${id}`);
        var product = convertProduct(rawProduct);
        commit('SET_CURRENT_ITEM', product);
        commit('PUSH_PRODUCT_CACHE', product);
        commit('SET_LOADED', true);
    },
    search: async function({ commit, getters }, payload) {
        commit('SET_LOADED', false);
        var { query, headers } = getters.getQueryOptions();
        var companyName = payload;
        if (typeof payload == 'object') {
            var { company, reload } = payload;
            companyName = company;
            if (reload) commit('SET_LOADED', false);
        }
        if (!companyName) return;
        var { items, totals, error } = await $httpClient.get(
            `/public/companies/${companyName}/products`,
            query,
            headers
        );
        if (error) return console.error(error);
        items = items.map(i => convertProduct(i));
        commit('SET_TOTALS', totals);
        commit('SET_ITEMS', items);
        commit('SET_LOADED', true);
    }
};

function convertProduct(i) {
    var pictures = i.images.map(ii => ({
        url: ii.imageUrl,
        width: 80,
        height: 80
    }));
    var category = i.categories.map(ii => ({
        name: ii.categoryName,
        slug: ii.categoryID
    }));
    return {
        category,
        pictures,
        id: i.productID,
        slug: i.productID,
        name: i.productName,
        short_desc: i.shortDescription,
        detail_desc: i.detailDescription,
        sm_pictures: pictures,
        price: i.price,
        minPrice: i.price,
        maxPrice: i.price,
        sale_price: i.price,
        stock: i.amount,
        ratings: null,
        review: null,
        variants: [],
        brands: []
    };
}
