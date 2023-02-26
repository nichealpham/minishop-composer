export const HIDE_NEWSLETTER = 'HIDE_NEWSLETTER';
export const REFRESH_STORE = 'REFRESH_STORE';

export const state = () => (
    {
        current: 2,
        showNewsletter: true
    }
);

export const getters = {
    currentDemo: state => {
        return 'demo' + state.current;
    },
    newsletterShow: state => {
        return state.showNewsletter;
    }
};

export const mutations = {
    [ HIDE_NEWSLETTER ] ( state ) {
        state.showNewsletter = false;
    },
}