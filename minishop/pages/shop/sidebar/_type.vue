<template>
    <main class="main">
        <div class="page-content">
            <div
                class="container container_padding"
                style="background:white;border-radius:6px;"
            >
                <div class="row">
                    <div
                        class="col-lg-9 skeleton-body skel-shop-products"
                        :class="{ loaded: getLoaded }"
                    >
                        <div class="toolbox pl-2 pr-2">
                            <div class="toolbox-left">
                                <div class="toolbox-info">
                                    Hiển thị
                                    <span
                                        >{{ getItems.length }} of
                                        {{ getTotals }}</span
                                    >
                                    kết quả
                                </div>
                            </div>

                            <div class="toolbox-right">
                                <div class="toolbox-sort">
                                    <input
                                        class="form-control"
                                        @keyup.enter="searchText"
                                        v-model="keySearch"
                                        placeholder="Tìm kiếm..."
                                    />
                                </div>
                                <div class="toolbox-layout">
                                    <nuxt-link
                                        :to="'/shop/sidebar/list'"
                                        class="btn-layout"
                                        :class="{ active: type === 'list' }"
                                    >
                                        <svg width="16" height="10">
                                            <rect
                                                x="0"
                                                y="0"
                                                width="4"
                                                height="4"
                                            />
                                            <rect
                                                x="6"
                                                y="0"
                                                width="10"
                                                height="4"
                                            />
                                            <rect
                                                x="0"
                                                y="6"
                                                width="4"
                                                height="4"
                                            />
                                            <rect
                                                x="6"
                                                y="6"
                                                width="10"
                                                height="4"
                                            />
                                        </svg>
                                    </nuxt-link>

                                    <nuxt-link
                                        :to="'/shop/sidebar/3cols'"
                                        class="btn-layout"
                                        :class="{ active: type === '3cols' }"
                                    >
                                        <svg width="16" height="10">
                                            <rect
                                                x="0"
                                                y="0"
                                                width="4"
                                                height="4"
                                            />
                                            <rect
                                                x="6"
                                                y="0"
                                                width="4"
                                                height="4"
                                            />
                                            <rect
                                                x="12"
                                                y="0"
                                                width="4"
                                                height="4"
                                            />
                                            <rect
                                                x="0"
                                                y="6"
                                                width="4"
                                                height="4"
                                            />
                                            <rect
                                                x="6"
                                                y="6"
                                                width="4"
                                                height="4"
                                            />
                                            <rect
                                                x="12"
                                                y="6"
                                                width="4"
                                                height="4"
                                            />
                                        </svg>
                                    </nuxt-link>

                                    <nuxt-link
                                        :to="'/shop/sidebar/4cols'"
                                        class="btn-layout"
                                        :class="{ active: type === '4cols' }"
                                    >
                                        <svg width="22" height="10">
                                            <rect
                                                x="0"
                                                y="0"
                                                width="4"
                                                height="4"
                                            />
                                            <rect
                                                x="6"
                                                y="0"
                                                width="4"
                                                height="4"
                                            />
                                            <rect
                                                x="12"
                                                y="0"
                                                width="4"
                                                height="4"
                                            />
                                            <rect
                                                x="18"
                                                y="0"
                                                width="4"
                                                height="4"
                                            />
                                            <rect
                                                x="0"
                                                y="6"
                                                width="4"
                                                height="4"
                                            />
                                            <rect
                                                x="6"
                                                y="6"
                                                width="4"
                                                height="4"
                                            />
                                            <rect
                                                x="12"
                                                y="6"
                                                width="4"
                                                height="4"
                                            />
                                            <rect
                                                x="18"
                                                y="6"
                                                width="4"
                                                height="4"
                                            />
                                        </svg>
                                    </nuxt-link>
                                </div>
                            </div>
                        </div>

                        <shop-list-one
                            :products="getItems"
                            :per-page="getPagination.limit"
                            :loaded="getLoaded"
                        ></shop-list-one>

                        <pagination
                            :per-page="getPagination.limit"
                            :total="getTotals"
                        ></pagination>
                    </div>

                    <aside class="col-lg-3 order-lg-first" sticky-container>
                        <div v-sticky="!isSidebar" sticky-offset="{ top: 69 }">
                            <button
                                class="sidebar-fixed-toggler"
                                @click="toggleSidebar"
                                v-if="isSidebar"
                            >
                                <i class="icon-cog"></i>
                            </button>

                            <div
                                class="sidebar-filter-overlay"
                                @click="hideSidebar"
                            ></div>
                            <shop-sidebar-one
                                :is-sidebar="isSidebar"
                            ></shop-sidebar-one>
                        </div>
                    </aside>
                </div>
            </div>
        </div>
    </main>
</template>
<script>
import { mapGetters, mapMutations, mapActions } from 'vuex';
import Sticky from 'vue-sticky-directive';

import PageHeader from '~/components/elements/PageHeader';
import ShopListOne from '~/components/partial/shop/list/ShopListOne';
import ShopSidebarOne from '~/components/partial/shop/sidebar/ShopSidebarOne';
import Pagination from '~/components/elements/Pagination';

import { scrollToPageContent } from '~/utilities/common';

export default {
    components: {
        PageHeader,
        ShopListOne,
        ShopSidebarOne,
        Pagination
    },
    directives: {
        Sticky
    },
    data: function() {
        return {
            limit: 5,
            type: 'list',
            totalCount: 0,
            orderBy: 'default',
            isSidebar: true,
            keySearch: this.$route.query.keySearch
        };
    },
    computed: {
        ...mapGetters('demo', ['currentDemo']),
        ...mapGetters('product', [
            'getItems',
            'getLoaded',
            'getTotals',
            'getPagination'
        ])
    },
    watch: {
        $route: function(val) {
            if (val.query.page) return this.searchPage(true);
            this.getProducts(true);
        }
    },
    created: function() {
        this.getProducts();
    },
    mounted: function() {
        this.resizeHandler();
        window.addEventListener('resize', this.resizeHandler, {
            passive: true
        });
    },
    destroyed: function() {
        window.removeEventListener('resize', this.resizeHandler);
    },
    methods: {
        ...mapMutations('product', [
            'SET_PAGINATION',
            'SET_KEYSEARCH',
            'CLEAN_QUERY_OPTIONS'
        ]),
        ...mapActions('product', ['search']),
        async searchText() {
            this.CLEAN_QUERY_OPTIONS();
            var page = 1;
            var limit = this.limit;
            var keySearch = this.keySearch || '';
            var pagination = { page, limit };
            this.SET_PAGINATION(pagination);
            this.SET_KEYSEARCH(keySearch);
            var query = Object.assign({}, this.$route.query);
            delete query.page;
            this.$router.replace({ query });
            await this.reRender();
        },
        async searchPage(reload = false) {
            this.CLEAN_QUERY_OPTIONS();
            var page = this.$route.query.page || 1;
            var limit = this.limit;
            var pagination = { page, limit };
            this.SET_PAGINATION(pagination);
            var company = this.$companyName;
            await this.search({ company, reload });
        },
        async reRender() {
            await this.search({
                company: this.$companyName,
                reload: true
            });
        },
        getProducts: async function(samePage = false) {
            this.CLEAN_QUERY_OPTIONS();
            this.type = this.$route.params.type;
            if (this.type == 'list') {
                this.limit = 5;
            } else if (this.type == '3cols') {
                this.limit = 9;
            } else if (this.type == '4cols') {
                this.limit = 12;
            }
            await this.searchPage();
            if (samePage) {
                scrollToPageContent();
            }
        },
        toggleSidebar: function() {
            if (
                document
                    .querySelector('body')
                    .classList.contains('sidebar-filter-active')
            ) {
                document
                    .querySelector('body')
                    .classList.remove('sidebar-filter-active');
            } else {
                document
                    .querySelector('body')
                    .classList.add('sidebar-filter-active');
            }
        },

        hideSidebar: function() {
            document
                .querySelector('body')
                .classList.remove('sidebar-filter-active');
        },
        resizeHandler: function() {
            if (window.innerWidth > 991) this.isSidebar = false;
            else this.isSidebar = true;
        }
    }
};
</script>
