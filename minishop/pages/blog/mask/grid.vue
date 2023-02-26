<template>
    <main class="main">
        <page-header title="Blog Mask Grid" subtitle="Blog"></page-header>

        <div class="page-content">
            <div
                class="container skeleton-body"
                :class="{ loaded: loaded }"
                v-images-loaded.on="handleIsotope"
            >
                <nav class="blog-nav">
                    <ul class="menu-cat entry-filter justify-content-center">
                        <li class="active">
                            <a
                                href="#1"
                                @click.prevent="isotopeFilter('', $event)"
                            >
                                All Blog Posts
                                <span>{{ totalCount }}</span>
                            </a>
                        </li>

                        <li v-for="(cat, index) in blogCategories" :key="index">
                            <a
                                href="#"
                                @click.prevent="isotopeFilter(cat.slug, $event)"
                            >
                                {{ cat.name }}
                                <span>{{ cat.count }}</span>
                            </a>
                        </li>
                    </ul>
                </nav>

                <div class="row">
                    <div
                        class="skel-mask-post col-sm-6 col-lg-4"
                        v-for="(i, index) in [1, 2, 3, 4, 5, 6]"
                        :key="'skel' + index"
                    ></div>
                </div>

                <p class="blogs-info" v-if="loaded && blogs.length === 0">
                    No posts were found matching your selection.
                </p>

                <isotope
                    class="entry-container max-col-3"
                    ref="isotope"
                    :list="blogs"
                    :options="isotopeOptions"
                >
                    <div
                        class="entry-item col-sm-6 col-lg-4"
                        :class="addClass(blog)"
                        v-for="(blog, index) in blogs"
                        :key="index"
                    >
                        <blog-three
                            :blog="blog"
                            class="text-center"
                        ></blog-three>
                    </div>
                </isotope>

                <pagination
                    :per-page="perPage"
                    :total="totalCount"
                ></pagination>
            </div>
        </div>
    </main>
</template>
<script>
import isotope from 'vueisotope';
import imagesLoaded from 'vue-images-loaded';
import BlogThree from '~/components/elements/blogs/BlogThree';
import PageHeader from '~/components/elements/PageHeader';
import Pagination from '~/components/elements/Pagination';
import Repository, { baseUrl } from '~/repositories/repository.js';

export default {
    components: {
        isotope,
        BlogThree,
        PageHeader,
        Pagination
    },
    directives: {
        imagesLoaded
    },
    data: function() {
        return {
            blogs: [],
            blogCategories: [],
            loaded: false,
            totalCount: 0,
            page: 1,
            perPage: 9,
            certainTag: null,
            certainCategory: null,
            filterCategory: '',
            isotopeOptions: {
                itemSelector: '.entry-item',
                layoutMode: 'masonry',
                percentPosition: false,
                getFilterData: {
                    filterByClass: function(elem) {
                        if (!this.$parent) return false;
                        return (
                            this.$parent.filterCategory === '' ||
                            this.$parent
                                .getCategories(elem)
                                .includes(this.$parent.filterCategory)
                        );
                    }
                }
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
    methods: {
        getBlogs: async function() {
            this.loaded = false;

            let params = {
                page: this.$route.query.page
                    ? this.$route.query.page
                    : this.page,
                perPage: this.perPage
            };

            await Repository.get(`${baseUrl}/blogs/mask-grid`, {
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
        getCategories: function(blog) {
            return blog.blog_categories.reduce((acc, cur) => {
                return [...acc, cur.slug];
            }, []);
        },
        addClass: function(blog) {
            return this.getCategories(blog).join(' ');
        },
        handleIsotope: function() {
            this.$refs['isotope'] && this.$refs['isotope'].layout('masonry');
        },
        isotopeFilter: function(filterBy, e) {
            this.filterCategory = filterBy;
            this.$refs['isotope'].filter('filterByClass');
            let prevActive = e.currentTarget.parentElement.parentElement.querySelector(
                '.active'
            );
            if (prevActive) {
                prevActive.classList.remove('active');
            }

            e.currentTarget.parentElement.classList.add('active');
        }
    }
};
</script>
