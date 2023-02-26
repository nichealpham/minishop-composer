<template>
    <header class="header">
        <sticky-header>
            <div class="header-middle sticky-header">
                <div class="container">
                    <div class="header-left">
                        <button
                            class="mobile-menu-toggler"
                            @click="openMobileMenu"
                        >
                            <span class="sr-only">Toggle mobile menu</span>
                            <i class="icon-bars"></i>
                        </button>

                        <nuxt-link
                            to="/"
                            class="logo mr-5"
                            style="display:flex;"
                        >
                            <img
                                v-if="getCompany.logo"
                                :src="getCompany.logo"
                                alt="Company Logo"
                                width="38"
                            />
                            <span
                                style="font-size:1.6rem;color:black;font-weight:600;line-height:36px"
                                class="ml-3"
                                >{{ getCompany.companyName }}</span
                            >
                        </nuxt-link>

                        <main-menu></main-menu>
                    </div>

                    <div class="header-right">
                        <div class="header-right mr-2">
                            <ul class="top-menu">
                                <li>
                                    <ul>
                                        <li>
                                            <wishlist-menu></wishlist-menu>
                                        </li>
                                        <li>
                                            <nuxt-link to="#"
                                                ><a
                                                    style="color:#333"
                                                    @click="
                                                        callPhone(
                                                            getCompany.phone
                                                        )
                                                    "
                                                >
                                                    <i
                                                        class="icon-phone mr-2"
                                                    ></i>
                                                    {{ getCompany.phone }}
                                                </a></nuxt-link
                                            >
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                        <cart-menu></cart-menu>
                    </div>
                </div>
            </div>
        </sticky-header>
    </header>
</template>

<script>
import CartMenu from '~/components/partial/headers/shared/CartMenu';
import WishlistMenu from '~/components/partial/headers/shared/WishlistMenu';
import MainMenu from '~/components/partial/headers/shared/MainMenu';
import HeaderSearch from '~/components/partial/headers/shared/HeaderSearch';
import StickyHeader from '~/components/elements/StickyHeader';
import { mapGetters, mapActions } from 'vuex';

export default {
    components: {
        CartMenu,
        WishlistMenu,
        MainMenu,
        HeaderSearch,
        StickyHeader
    },
    computed: {
        ...mapGetters('company', ['getCompany']),
        isFullwidth: function() {
            return this.$route.path.includes('fullwidth');
        }
    },
    methods: {
        openSignInModal: function() {
            this.$modal.show(
                () => import('~/components/elements/modals/SignInModal'),
                {},
                { width: '575', height: 'auto', adaptive: true }
            );
        },
        openMobileMenu: function() {
            document.querySelector('body').classList.add('mmenu-active');
        }
    }
};
</script>
