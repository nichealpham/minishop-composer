<template>
    <div class="mobile-menu-container">
        <div class="mobile-menu-wrapper">
            <span class="mobile-menu-close" @click="hideMobileMenu">
                <i class="icon-close"></i>
            </span>
            <!-- <form
                action="#"
                method="get"
                class="mobile-search"
                @submit.prevent="submitSearchForm"
            >
                <label for="mobile-search" class="sr-only">Search</label>
                <input
                    type="search"
                    class="form-control"
                    name="mobile-search"
                    id="mobile-search"
                    v-model="searchTerm"
                    placeholder="Search product ..."
                    required
                />
                <button class="btn btn-primary" type="submit">
                    <i class="icon-search"></i>
                </button>
            </form> -->
            <nav class="mobile-nav">
                <ul class="mobile-menu">
                    <li>
                        <nuxt-link :to="'/'" class="sf-with-ul"
                            >Trang chủ</nuxt-link
                        >
                    </li>
                    <li>
                        <nuxt-link :to="'/gallery'" class="sf-with-ul"
                            >Thư viện ảnh</nuxt-link
                        >
                    </li>
                    <li>
                        <nuxt-link :to="'/shop/sidebar/list'" class="sf-with-ul"
                            >Cửa hàng</nuxt-link
                        >
                    </li>
                    <li>
                        <nuxt-link :to="'/blog/listing'" class="sf-with-ul"
                            >Bài viết</nuxt-link
                        >
                    </li>
                    <li>
                        <nuxt-link :to="'/account'" class="sf-with-ul"
                            >Tài khoản</nuxt-link
                        >
                    </li>
                </ul>
            </nav>
        </div>
    </div>
</template>
<script>
import { mobileMenu } from '~/utilities/common.js';

export default {
    data: function() {
        return {
            searchTerm: ''
        };
    },
    mounted: function() {
        mobileMenu();
    },
    methods: {
        hideMobileMenu: function() {
            document.querySelector('body').classList.remove('mmenu-active');
        },
        submitSearchForm: function() {
            this.hideMobileMenu();
            this.$router.push({
                path: '/shop/sidebar/4cols',
                query: {
                    searchTerm: this.searchTerm
                }
            });
        }
    }
};
</script>
