import moment from 'moment';
import { $httpClient } from '~/plugins/constants';

export const state = () => ({
    query: {
        categoryIDs: '',
        keySearch: ''
    },
    pagination: {
        page: 1,
        limit: 5
    },
    loaded: false,
    totals: 0,
    items: [],
    cache: [],
    currentItem: null
});

function stringToSlug(str) {
    if (!str) return '';
    // remove accents
    var from =
            'àáãảạăằắẳẵặâầấẩẫậèéẻẽẹêềếểễệđùúủũụưừứửữựòóỏõọôồốổỗộơờớởỡợìíỉĩịäëïîöüûñçýỳỹỵỷ',
        to =
            'aaaaaaaaaaaaaaaaaeeeeeeeeeeeduuuuuuuuuuuoooooooooooooooooiiiiiaeiiouuncyyyyy';
    for (var i = 0, l = from.length; i < l; i++) {
        str = str.replace(RegExp(from[i], 'gi'), to[i]);
    }

    str = str
        .toLowerCase()
        .trim()
        .replace(/[^a-z0-9\-]/g, '-')
        .replace(/-+/g, '-')
        .split('-')
        .join('')
        .split(' ')
        .join('');

    return str;
}

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
    },
    searchCache: state => () => {
        var { keySearch, categoryIDs } = state.query;
        var { page, limit } = state.pagination;
        var categories = categoryIDs
            .split(',')
            .map(i => i.toLowerCase())
            .filter(i => !!i);

        var result = state.cache.filter(i => {
            return stringToSlug(i.title).includes(stringToSlug(keySearch));
        });
        if (categories.length) {
            result = result.filter(i =>
                i.blog_categories.find(c => categories.includes(c))
            );
        }
        result = JSON.parse(JSON.stringify(result)).splice(
            (page - 1) * limit,
            limit
        );
        return result;
    }
};

export const mutations = {
    ['SET_CATEGORY'](state, payload) {
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
            keySearch: ''
        };
        state.pagination = {
            page: 1,
            limit: 5
        };
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
    },
    ['SET_CURRENT_ITEM'](state, payload) {
        state.currentItem = payload;
    },
    ['SET_CURRENT_DETAIL'](state, payload) {
        var { id, detail } = payload;
        state.currentItem.detail = detail;
        var ind = state.cache.findIndex(c => c.id == id);
        if (ind != -1) {
            state.cache[ind].detail = detail;
        }
    },
    ['PUSH_CACHE'](state, payload) {
        if (!state.cache.find(i => i.id == payload.id)) {
            state.cache.push(payload);
        }
    }
};

export const actions = {
    getDetail: async function({ commit, getters }, payload) {
        commit('SET_LOADED', false);
        commit('SET_CURRENT_ITEM', null);
        var id = payload;
        var raw = await $httpClient.get(`/public/blogs/${id}`);
        var blog = convertBlog(raw);
        commit('SET_CURRENT_ITEM', blog);
        commit('PUSH_CACHE', blog);
        commit('SET_LOADED', true);
    },
    search: async function({ commit, getters }, payload) {
        commit('SET_LOADED', false);
        var { query, headers } = getters.getQueryOptions();
        var companyName = payload;
        if (typeof payload == 'object') {
            var { company, reload } = payload;
            companyName = company;
        }
        if (!companyName) return;
        var { items, totals, error } = await $httpClient.get(
            `/public/companies/${companyName}/blogs`,
            query,
            headers
        );
        if (error) return console.error(error);
        items = items.map(i => convertBlog(i));
        commit('SET_TOTALS', totals);
        commit('SET_ITEMS', items);
        commit('SET_LOADED', true);
    }
};

function convertBlog(i) {
    var image = i.images.map(ii => ({
        url: ii.imageUrl,
        width: 80,
        height: 80
    }));
    var blog_categories = i.categories.map(ii => ({
        name: ii.categoryName,
        slug: ii.categoryID
    }));
    return {
        blog_categories,
        image,
        id: i.blogID,
        slug: i.blogID,
        title: i.blogTitle,
        content: i.shortDescription,
        detail: i.detailDescription,
        views: i.viewCount,
        author: i.authorName,
        date: moment(i.dateUpdated).format('YYYY-MM-DD'),
        type: 'image',
        comments: '0'
    };
}
