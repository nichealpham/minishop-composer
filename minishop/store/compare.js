// Compare
export const ADD_TO_COMPARE = 'ADD_TO_COMPARE';
export const REMOVE_FROM_COMPARE = 'REMOVE_FROM_COMPARE';
export const CLEAR_COMPARE = 'CLEAR_COMPARE';

export const state = () => (
    {
        data: []
    }
);

export const getters = {
    compareList: state => {
        return state.data
    },
    isInCompare: state => ( product ) => {
        return state.data.find( item => item.id == product.id ) ? true : false;
    }
}

export const actions = {
    addToCompare: function ( { commit, getters }, payload ) {
        if ( getters.isInCompare( payload.product ) ) {
            this._vm.$vToastify.removeToast();
            this._vm.$vToastify.setSettings( {
                withBackdrop: false,
                position: "top-right",
                errorDuration: 2000
            } );
            this._vm.$vToastify.error( "Product has already been in Compare." );
            return;
        }

        commit( ADD_TO_COMPARE, payload );
        this._vm.$vToastify.setSettings( {
            withBackdrop: false,
            position: "top-right",
            successDuration: 1500,
        } );
        this._vm.$vToastify.success( "Product added to compare" );
    },
    removeFromCompare: function ( { commit }, payload ) {
        commit( REMOVE_FROM_COMPARE, payload );
        this._vm.$vToastify.setSettings( {
            withBackdrop: false,
            position: "top-right",
            successDuration: 1500,
        } );
        this._vm.$vToastify.success( "Product removed from compare" );
    },
}

export const mutations = {
    [ ADD_TO_COMPARE ] ( state, payload ) {
        state.data = [ ...state.data, payload.product ];
    },

    [ REMOVE_FROM_COMPARE ] ( state, payload ) {
        state.data = state.data.filter( item => item.id !== payload.product.id );
    },

    [ CLEAR_COMPARE ] ( state ) {
        state.data = [];
    }
}