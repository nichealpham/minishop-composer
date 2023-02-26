<template>
    <main class="main">
        <!-- <div class="page-content"> -->
        <div
            class="pa-0 ma-0"
            style="padding:0px;width:100%;text-align:center;"
        >
            <iframe
                id="iframe_order"
                :src="$crmUrlHome"
                class="account_iframe"
            />
        </div>
        <!-- </div> -->
    </main>
</template>
<script>
import { mapActions, mapMutations, mapGetters } from 'vuex';

export default {
    data: function() {
        return {
            redirect: ''
        };
    },
    computed: {
        ...mapGetters('auth', ['isRegister', 'redirectUrl'])
    },
    watch: {
        isRegister: async function(val) {
            if (val) {
                await this.sleep(250);
                document.getElementById(
                    'iframe_order'
                ).src = `${this.$crmUrlHome}/register`;
            }
            this.$store.commit('auth/SET_IS_REGISTER', false);
        },
        redirectUrl(val) {
            if (val) {
                this.redirect = val;
            }
        }
    },
    mounted() {
        window.onmessage = e => {
            var data = e.data;
            if (!data) return;
            data = JSON.parse(e.data);
            this.handleDataEvent(data);
        };
    },
    methods: {
        ...mapActions('auth', ['getProfile']),
        ...mapMutations('auth', ['SET_TOKEN']),
        ...mapMutations('auth', ['LOGOUT']),

        async handleDataEvent(data = {}) {
            var { channel, token } = data;
            if (token) {
                this.SET_TOKEN(token);
                await this.sleep(50);
                await this.getProfile();
                if (this.redirect) {
                    this.$store.commit('auth/SET_REDIRECT_URL', '');
                    window.location.href = this.redirect;
                }
            }
            if (channel == 'Logout') {
                this.LOGOUT();
            }
        }
    }
};
</script>
