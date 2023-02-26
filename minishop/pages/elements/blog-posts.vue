<template>
    <main class="main">
        <page-header title="Blog Posts" subtitle="Elements"></page-header>

        <div class="page-content">
            <div class="container skeleton-body">
                <h2 class="title text-center mb-2">Classic</h2>

                <template v-if="!loaded">
                    <div class="skel-list-post mb-6"></div>
                    <div class="skel-list-post"></div>
                </template>

                <blog-two
                    v-for="(blog, index) in blogs.slice(0, 2)"
                    :key="index"
                    :blog="blog"
                    :isElements="true"
                ></blog-two>

                <hr class="mb-5" />

                <h2 class="title text-center mb-2">Grid 2 Columns</h2>

                <div class="row max-col-2">
                    <template v-if="!loaded">
                        <div class="col-md-6 skel-grid-post"></div>
                        <div class="col-md-6 skel-grid-post"></div>
                    </template>
                    <div
                        class="col-md-6"
                        v-for="(blog, index) in blogs.slice(2, 4)"
                        :key="index"
                    >
                        <blog-one
                            :blog="blog"
                            class="text-center entry-grid"
                        ></blog-one>
                    </div>
                </div>

                <hr class="mb-5" />

                <h2 class="title text-center mb-2">Grid 3 Columns</h2>

                <div class="row justify-content-center">
                    <template v-if="!loaded">
                        <div class="col-sm-6 col-md-4 skel-grid-post"></div>
                        <div class="col-sm-6 col-md-4 skel-grid-post"></div>
                        <div class="col-sm-6 col-md-4 skel-grid-post"></div>
                    </template>
                    <div
                        class="col-sm-6 col-md-4"
                        v-for="(blog, index) in blogs.slice(4, 7)"
                        :key="index"
                    >
                        <blog-one
                            :blog="blog"
                            class="text-center entry-grid"
                        ></blog-one>
                    </div>
                </div>

                <hr class="mb-5" />

                <h2 class="title text-center mb-2">Grid 4 Columns</h2>

                <div class="row justify-content-center">
                    <template v-if="!loaded">
                        <div
                            class="col-sm-6 col-md-4 col-lg-3 skel-grid-post"
                        ></div>
                        <div
                            class="col-sm-6 col-md-4 col-lg-3 skel-grid-post"
                        ></div>
                        <div
                            class="col-sm-6 col-md-4 col-lg-3 skel-grid-post"
                        ></div>
                        <div
                            class="col-sm-6 col-md-4 col-lg-3 skel-grid-post"
                        ></div>
                    </template>
                    <div
                        class="col-sm-6 col-md-4 col-lg-3"
                        v-for="(blog, index) in blogs.slice(7, 11)"
                        :key="index"
                    >
                        <blog-one
                            :blog="blog"
                            class="text-center entry-grid"
                        ></blog-one>
                    </div>
                </div>

                <hr class="mb-5" />

                <h2 class="title text-center mb-2">
                    Grid 3 Columns
                    <span class="title-separator">/</span> Mask
                </h2>

                <div class="row justify-content-center">
                    <template v-if="!loaded">
                        <div class="col-sm-6 col-md-4 skel-mask-post"></div>
                        <div class="col-sm-6 col-md-4 skel-mask-post"></div>
                        <div class="col-sm-6 col-md-4 skel-mask-post"></div>
                    </template>
                    <div
                        class="col-sm-6 col-md-4"
                        v-for="(blog, index) in blogs.slice(11, 14)"
                        :key="index"
                    >
                        <blog-three :blog="blog"></blog-three>
                    </div>
                </div>
            </div>
        </div>

        <element-list></element-list>
    </main>
</template>
<script>
import PageHeader from '~/components/elements/PageHeader';
import ElementList from '~/components/partial/elements/ElementList';
import BlogOne from '~/components/elements/blogs/BlogOne';
import BlogTwo from '~/components/elements/blogs/BlogTwo';
import BlogThree from '~/components/elements/blogs/BlogThree';
import Repository, { baseUrl } from '~/repositories/repository.js';

export default {
    components: {
        PageHeader,
        ElementList,
        BlogOne,
        BlogTwo,
        BlogThree
    },
    data: function() {
        return {
            blogs: [],
            loaded: false
        };
    },
    created: function() {
        this.getBlogs();
    },
    methods: {
        getBlogs: async function() {
            this.loaded = false;
            await Repository.get(`${baseUrl}/elements/blogs`)
                .then(response => {
                    this.blogs = response.data;

                    this.loaded = true;
                })
                .catch(error => ({ error: JSON.stringify(error) }));
        }
    }
};
</script>
