<template lang="html">
    <div>
        <div class="page-wrapper">
            <header-default></header-default>
            <nuxt></nuxt>
            <footer-default></footer-default>
            <button
                id="scroll-top"
                ref="scrollTop"
                title="Back to Top"
                @click.prevent="scrollTop"
            >
                <i class="icon-arrow-up"></i>
            </button>
        </div>
        <div class="mobile-menu-overlay" @click="hideMobileMenu"></div>
        <mobile-menu></mobile-menu>
    </div>
</template>

<script>
import HeaderDefault from '~/components/partial/headers/HeaderDefault';
import FooterDefault from '~/components/partial/footers/FooterDefault';
import { isSafariBrowser, isEdgeBrowser } from '~/utilities/common';
import { mapActions, mapMutations } from 'vuex';

export default {
    components: {
        HeaderDefault,
        FooterDefault,
        MobileMenu: () => import('~/components/partial/home/MobileMenu')
    },
    mounted: async function() {
        let scrollTop = this.$refs.scrollTop;
        document.addEventListener(
            'scroll',
            function() {
                if (window.pageYOffset >= 400) {
                    scrollTop.classList.add('show');
                } else {
                    scrollTop.classList.remove('show');
                }
            },
            false
        );
        this.getCompany({ current: this.$companyName });
        this.checkTokenAndGetProfile();
    },
    methods: {
        ...mapActions('auth', ['getProfile']),
        ...mapActions('company', ['getCompany']),
        ...mapMutations('auth', ['SET_TOKEN']),
        async checkTokenAndGetProfile() {
            var token = this.$route.query.token;
            if (token) {
                this.SET_TOKEN(token);
                await this.sleep(50);
                var query = JSON.parse(JSON.stringify(this.$route.query));
                delete query.token;
                this.$router.push({ query });
            }
            this.getProfile();
        },
        scrollTop: function() {
            if (isSafariBrowser() || isEdgeBrowser()) {
                let pos = window.pageYOffset;
                let timerId = setInterval(() => {
                    if (pos <= 0) clearInterval(timerId);
                    window.scrollBy(0, -120);
                    pos -= 120;
                }, 1);
            } else {
                window.scrollTo({
                    top: 0,
                    behavior: 'smooth'
                });
            }
        },
        hideMobileMenu: function() {
            document.querySelector('body').classList.remove('mmenu-active');
        }
    }
};
</script>
