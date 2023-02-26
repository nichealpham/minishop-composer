<template>
    <main class="main">
        <div class="page-content">
            <div
                class="container container_padding"
                style="background:white;border-radius:6px;"
            >
                <div class="row skeleton-body" :class="{ loaded: getLoaded }">
                    <div class="col-lg-9">
                        <template v-if="!getLoaded">
                            <div
                                class="skel-list-post mb-6"
                                v-for="(i, index) in [1, 2, 3]"
                                :key="'skel' + index"
                            ></div>
                        </template>

                        <template v-else>
                            <p class="blogs-info" v-if="getItems.length === 0">
                                Chưa có bài viết nào.
                            </p>

                            <div v-for="(blog, index) in getItems" :key="index">
                                <blog-two :blog="blog"></blog-two>
                            </div>

                            <pagination
                                :per-page="getPagination.limit"
                                :total="getTotals"
                            ></pagination>
                        </template>
                    </div>

                    <aside class="col-lg-3 pl-5" sticky-container>
                        <div
                            class="sticky-content"
                            v-sticky="shouldSticky"
                            sticky-offset="{top: 69}"
                        >
                            <button
                                class="sidebar-fixed-toggler right"
                                @click="toggleSidebar"
                                v-if="isSidebar"
                            >
                                <i class="icon-cog"></i>
                            </button>

                            <div
                                class="sidebar-filter-overlay"
                                @click="hideSidebar"
                            ></div>
                            <div class="widget widget-search">
                                <h3 class="widget-title mb-2">
                                    <a href="#widget-2">Tìm kiếm</a>
                                </h3>
                                <label class="sr-only">Tìm kiếm bài viết</label>
                                <input
                                    type="search"
                                    class="form-control"
                                    placeholder="Tìm bài viết"
                                    @keyup.enter="searchText"
                                    v-model="keySearch"
                                />
                            </div>
                            <div class="widget pl-1 pr-2">
                                <h3 class="widget-title mb-2">
                                    <a href="#widget-2">Phân loại</a>
                                </h3>

                                <vue-slide-toggle
                                    :open="true"
                                    class="show pb-2"
                                >
                                    <div class="widget-body pt-0">
                                        <div class="filter-items">
                                            <p v-if="!categories.length">
                                                Chưa có phân loại
                                            </p>
                                            <div
                                                v-else
                                                class="filter-item"
                                                v-for="(item,
                                                index) in categories"
                                                :key="index"
                                            >
                                                <div
                                                    class="custom-control custom-checkbox"
                                                >
                                                    <input
                                                        type="checkbox"
                                                        class="custom-control-input"
                                                        :id="'size-' + index"
                                                        @click="
                                                            onCategoryClick(
                                                                item
                                                            )
                                                        "
                                                        v-model="
                                                            selectedCategories
                                                        "
                                                        :value="item.slug"
                                                    />
                                                    <label
                                                        class="custom-control-label"
                                                        :for="'size-' + index"
                                                        >{{ item.name }} ({{
                                                            item.count
                                                        }})</label
                                                    >
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </vue-slide-toggle>
                            </div>
                        </div>
                    </aside>
                </div>
            </div>
        </div>
    </main>
</template>
<script>
import Sticky from 'vue-sticky-directive';
import BlogTwo from '~/components/elements/blogs/BlogTwo';
import PageHeader from '~/components/elements/PageHeader';
import Pagination from '~/components/elements/Pagination';
import { VueSlideToggle } from 'vue-slide-toggle';
import { mapGetters, mapMutations, mapActions } from 'vuex';

export default {
    components: {
        BlogTwo,
        PageHeader,
        Pagination,
        VueSlideToggle
    },
    directives: {
        Sticky
    },
    computed: {
        ...mapGetters('category', ['categories']),
        ...mapGetters('blog', [
            'getItems',
            'getLoaded',
            'getTotals',
            'getPagination'
        ])
    },
    data: function() {
        return {
            selectedCategories: [],
            isSidebar: false,
            shouldSticky: false,
            keySearch: '',
            limit: 5
        };
    },
    watch: {
        $route: function(val) {
            this.getBlogs();
        }
    },
    created: function() {
        this.CLEAN_QUERY_OPTIONS();
        this.renderCategories(this.$companyName);
        this.getBlogs();
    },
    mounted: function() {
        this.resizeHandler();
        window.addEventListener('resize', this.resizeHandler, {
            passive: true
        });
        this.stickyHandle();
        window.addEventListener('resize', this.stickyHandle, { passive: true });
    },
    destroyed: function() {
        window.removeEventListener('resize', this.resizeHandler);
        window.removeEventListener('resize', this.stickyHandle);
    },
    methods: {
        ...mapActions('category', ['renderCategories']),
        ...mapActions('blog', ['search']),
        ...mapMutations('blog', [
            'CLEAN_QUERY_OPTIONS',
            'SET_PAGINATION',
            'SET_KEYSEARCH',
            'SET_CATEGORY'
        ]),
        async onCategoryClick(item) {
            this.SET_CATEGORY(item.slug);
            this.getBlogs();
        },
        async searchText() {
            this.CLEAN_QUERY_OPTIONS();
            this.SET_KEYSEARCH(this.keySearch);
            this.setPaginations();
            var query = Object.assign({}, this.$route.query);
            delete query.page;
            this.$router.replace({ query });
            await this.getBlogs();
        },
        getBlogs: async function() {
            this.setPaginations();
            await this.search({
                company: this.$companyName,
                reload: true
            });
        },
        async setPaginations() {
            var page = this.$route.query.page || 1;
            var limit = this.limit;
            var pagination = { page, limit };
            this.SET_PAGINATION(pagination);
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
            if (window.innerWidth > 992) this.isSidebar = false;
            else this.isSidebar = true;
        },
        stickyHandle: function() {
            if (window.innerWidth > 991) this.shouldSticky = true;
            else this.shouldSticky = false;
        }
    }
};
</script>
