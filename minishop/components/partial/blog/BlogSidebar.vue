<template>
    <div class="sidebar sidebar-blog">
        <div class="widget widget-search">
            <h3 class="widget-title">Search</h3>

            <form action="#">
                <label for="ws" class="sr-only">Search in blog</label>
                <input
                    type="search"
                    class="form-control"
                    name="ws"
                    id="ws"
                    placeholder="Search in blog"
                    required
                />
                <button type="submit" class="btn">
                    <i class="icon-search"></i>
                    <span class="sr-only">Search</span>
                </button>
            </form>
        </div>

        <div class="widget widget-cats">
            <h3 class="widget-title">Categories</h3>

            <div
                class="skel-post-sidebar-category"
                v-if="blogCategories.length === 0"
            ></div>

            <ul>
                <li v-for="(cat, index) in blogCategories" :key="index">
                    <nuxt-link
                        :to="{
                            path: '/blog/' + type,
                            query: { category: cat.slug }
                        }"
                    >
                        {{ cat.name }}
                        <span>{{ cat.count }}</span>
                    </nuxt-link>
                </li>
            </ul>
        </div>

        <div class="widget">
            <h3 class="widget-title">Popular Posts</h3>

            <ul class="posts-list">
                <li>
                    <figure>
                        <nuxt-link
                            :to="
                                '/blog/single/default/cras-ornare-tristique-elit'
                            "
                        >
                            <img
                                v-lazy="`./images/blog/sidebar/post-1.jpg`"
                                alt="post"
                                width="80"
                                height="80"
                            />
                        </nuxt-link>
                    </figure>

                    <div>
                        <span>Nov 22, 2018</span>
                        <h4>
                            <nuxt-link
                                :to="
                                    '/blog/single/default/cras-ornare-tristique-elit'
                                "
                                >Aliquam tincidunt mauris eurisus.</nuxt-link
                            >
                        </h4>
                    </div>
                </li>
                <li>
                    <figure>
                        <nuxt-link
                            :to="
                                '/blog/single/default/cras-ornare-tristique-elit'
                            "
                        >
                            <img
                                v-lazy="`./images/blog/sidebar/post-2.jpg`"
                                alt="post"
                                width="80"
                                height="80"
                            />
                        </nuxt-link>
                    </figure>

                    <div>
                        <span>Nov 19, 2018</span>
                        <h4>
                            <nuxt-link
                                :to="
                                    '/blog/single/default/cras-ornare-tristique-elit'
                                "
                                >Cras ornare tristique elit.</nuxt-link
                            >
                        </h4>
                    </div>
                </li>
                <li>
                    <figure>
                        <nuxt-link
                            :to="
                                '/blog/single/default/cras-ornare-tristique-elit'
                            "
                        >
                            <img
                                v-lazy="`./images/blog/sidebar/post-3.jpg`"
                                alt="post"
                                width="80"
                                height="80"
                            />
                        </nuxt-link>
                    </figure>

                    <div>
                        <span>Nov 12, 2018</span>
                        <h4>
                            <nuxt-link
                                :to="
                                    '/blog/single/default/cras-ornare-tristique-elit'
                                "
                                >Vivamus vestibulum ntulla nec ante.</nuxt-link
                            >
                        </h4>
                    </div>
                </li>
                <li>
                    <figure>
                        <nuxt-link
                            :to="
                                '/blog/single/default/cras-ornare-tristique-elit'
                            "
                        >
                            <img
                                v-lazy="`./images/blog/sidebar/post-4.jpg`"
                                alt="post"
                                width="80"
                                height="80"
                            />
                        </nuxt-link>
                    </figure>

                    <div>
                        <span>Nov 25, 2018</span>
                        <h4>
                            <nuxt-link
                                :to="
                                    '/blog/single/default/cras-ornare-tristique-elit'
                                "
                                >Donec quis dui at dolor tempor
                                interdum.</nuxt-link
                            >
                        </h4>
                    </div>
                </li>
            </ul>
        </div>

        <div class="widget widget-banner-sidebar">
            <div class="banner-sidebar-title">ad box 280 x 280</div>

            <div class="banner-sidebar banner-overlay">
                <nuxt-link :to="'/shop/sidebar/list'">
                    <img
                        v-lazy="`./images/blog/sidebar/banner.jpg`"
                        alt="banner"
                        width="280"
                        height="280"
                    />
                </nuxt-link>
            </div>
        </div>

        <div class="widget">
            <h3 class="widget-title">Browse Tags</h3>

            <div class="tagcloud">
                <a href="#">fashion</a>
                <a href="#">style</a>
                <a href="#">women</a>
                <a href="#">photography</a>
                <a href="#">travel</a>
                <a href="#">shopping</a>
                <a href="#">hobbies</a>
            </div>
        </div>

        <div class="widget widget-text">
            <h3 class="widget-title">About Blog</h3>

            <div class="widget-text-content">
                <p>
                    Vestibulum volutpat, lacus a ultrices sagittis, mi neque
                    euismod dui, pulvinar nunc sapien ornare nisl.
                </p>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data: function() {
        return {
            category: this.$route.query.category
        };
    },
    props: {
        type: String,
        blogCategories: {
            type: Array,
            default: function() {
                return [];
            }
        }
    }
};
</script>
