<template>
    <main class="main">
        <page-header
            title="Blog Grid With Sidebar"
            subtitle="Blog"
        ></page-header>

        <div class="page-content">
            <div class="container" v-images-loaded.on="handleIsotope">
                <div class="row skeleton-body" :class="{ loaded: loaded }">
                    <div class="col-lg-9">
                        <div class="row">
                            <div
                                class="skel-grid-post col-sm-6"
                                v-for="(i, index) in [1, 2, 3, 4, 5, 6]"
                                :key="'skel' + index"
                            ></div>
                        </div>

                        <p
                            class="blogs-info"
                            v-if="loaded && blogs.length === 0"
                        >
                            No posts were found matching your selection.
                        </p>

                        <isotope
                            class="entry-container max-col-2"
                            ref="isotope"
                            :list="blogs"
                            :options="isotopeOptions"
                        >
                            <div
                                class="entry-item col-sm-6"
                                v-for="(blog, index) in blogs"
                                :key="index"
                            >
                                <blog-one
                                    :blog="blog"
                                    class="text-center"
                                ></blog-one>
                            </div>
                        </isotope>

                        <pagination
                            :per-page="perPage"
                            :total="totalCount"
                        ></pagination>
                    </div>

                    <aside class="col-lg-3">
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
                            <blog-sidebar
                                :blog-categories="blogCategories"
                                type="listing"
                                :class="isSidebar ? 'sidebar-filter right' : ''"
                            ></blog-sidebar>
                        </div>
                    </aside>
                </div>
            </div>
        </div>
    </main>
</template>
<script>
import isotope from 'vueisotope';
import imagesLoaded from 'vue-images-loaded';
import Sticky from 'vue-sticky-directive';
import BlogOne from '~/components/elements/blogs/BlogOne';
import PageHeader from '~/components/elements/PageHeader';
import BlogSidebar from '~/components/partial/blog/BlogSidebar';
import Pagination from '~/components/elements/Pagination';
import Repository, { baseUrl } from '~/repositories/repository.js';

export default {
    components: {
        isotope,
        BlogOne,
        PageHeader,
        BlogSidebar,
        Pagination
    },
    directives: {
        imagesLoaded,
        Sticky
    },
    data: function() {
        return {
            blogs: [],
            blogCategories: [],
            totalCount: 0,
            page: 1,
            perPage: 8,
            certainTag: null,
            certainCategory: null,
            filterCategory: '',
            loaded: false,
            isSidebar: false,
            shouldSticky: false,
            isotopeOptions: {
                itemSelector: '.entry-item',
                layoutMode: 'masonry',
                percentPosition: false
            }
        };
    },
    watch: {
        $route: function() {
            this.getBlogs();
        }
    },
    created: function() {
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
        getBlogs: async function() {
            this.loaded = false;

            let params = {
                page: this.$route.query.page
                    ? this.$route.query.page
                    : this.page,
                perPage: this.perPage
            };

            await Repository.get(`${baseUrl}/blogs/grid-sidebar`, {
                params: params
            })
                .then(response => {
                    this.blogs = response.data.blogs;
                    this.blogCategories = response.data.categories;
                    this.totalCount = response.data.totalCount;
                    this.loaded = true;
                })
                .catch(error => ({ error: JSON.stringify(error) }));
        },
        handleIsotope: function() {
            this.$refs['isotope'] && this.$refs['isotope'].layout('masonry');
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
