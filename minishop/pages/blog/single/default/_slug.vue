<template>
    <main class="main">
        <div class="page-content pt-3">
            <div class="container">
                <div class="row skeleton-body" :class="{ loaded: loaded }">
                    <div class="col-lg-9">
                        <div class="skel-single-post"></div>
                        <article class="entry single-entry" v-if="blog">
                            <figure
                                class="entry-media"
                                :class="
                                    blog.type === 'video' ? 'entry-video' : ''
                                "
                                v-if="blog.image.length <= 1"
                            >
                                <img
                                    v-lazy="`${baseUrl}${blog.image[0].url}`"
                                    alt="blog"
                                    :width="blog.image[0].width"
                                    :height="blog.image[0].height"
                                />
                                <a
                                    href="https://www.youtube.com/watch?v=vBPgmASQ1A0"
                                    @click.prevent="openVideoModal"
                                    class="btn-video btn-iframe"
                                    v-if="blog.type === 'video'"
                                >
                                    <i class="icon-play"></i>
                                </a>
                            </figure>
                            <figure
                                class="entry-media entry-gallery"
                                v-else-if="blog.image.length > 1"
                            >
                                <div
                                    class="swiper-carousel"
                                    :class="'swiper-single-media-' + blog.id"
                                >
                                    <div
                                        v-swiper:swiper2="carouselSettingSingle"
                                    >
                                        <div class="swiper-wrapper">
                                            <div
                                                class="swiper-slide"
                                                v-for="(image,
                                                index) in blog.image"
                                                :key="index"
                                            >
                                                <a href="#">
                                                    <img
                                                        v-lazy="
                                                            `${baseUrl}${image.url}`
                                                        "
                                                        alt="blog"
                                                        :width="image.width"
                                                        :height="image.height"
                                                    />
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </figure>

                            <div class="entry-body">
                                <div class="entry-meta">
                                    <span class="entry-author">
                                        by
                                        <nuxt-link
                                            :to="
                                                'blog/single/default/' +
                                                    blog.slug
                                            "
                                            >{{ blog.author }}</nuxt-link
                                        >
                                    </span>
                                    <span class="meta-separator">|</span>
                                    <nuxt-link
                                        :to="
                                            '/blog/single/default/' + blog.slug
                                        "
                                        >{{ date }}</nuxt-link
                                    >
                                    <span class="meta-separator">|</span>
                                    <nuxt-link
                                        :to="
                                            '/blog/single/default/' + blog.slug
                                        "
                                        >{{ blog.comments }} Comments</nuxt-link
                                    >
                                </div>

                                <h2 class="entry-title">
                                    <nuxt-link
                                        :to="
                                            '/blog/single/default/' + blog.slug
                                        "
                                        >{{ blog.title }}</nuxt-link
                                    >
                                </h2>
                                <div
                                    class="entry-cats"
                                    v-if="blog.blog_categories"
                                >
                                    in&nbsp;
                                    <span
                                        v-for="(cat,
                                        index) of blog.blog_categories"
                                        :key="index"
                                    >
                                        <nuxt-link
                                            :to="
                                                'blog/single/default/' +
                                                    blog.slug
                                            "
                                            >{{ cat.name }}</nuxt-link
                                        >
                                        {{
                                            blog.blog_categories.length - 1 >
                                            index
                                                ? ', '
                                                : ''
                                        }}
                                    </span>
                                </div>

                                <div class="entry-content editor-content">
                                    <p>{{ blog.content }}</p>

                                    <div class="pb-1"></div>

                                    <img
                                        v-lazy="'./images/blog/single/2.jpg'"
                                        alt="image"
                                        class="float-sm-left"
                                    />

                                    <h3>Quisque volutpat mattiseros.</h3>

                                    <ul>
                                        <li>
                                            Sed pretium, ligula sollicitudin
                                            laoreet viverra, tortor libero
                                            sodales leo, eget blandit nunc
                                            tortor eu nibh. Nullam mollis. Ut
                                            justo. Suspendisse potenti.
                                        </li>
                                        <li>
                                            Sed egestas, ante et vulputate
                                            volutpat, eros pede semper est,
                                            vitae luctus metus libero eu augue.
                                            Morbi purus libero, faucibus
                                            adipiscing, commodo quis, gravida
                                            id, est.
                                        </li>
                                        <li>
                                            Sed lectus. Praesent elementum
                                            hendrerit tortor. Sed semper lorem
                                            at felis. Vestibulum volutpat, lacus
                                            a ultrices sagittis, mi neque
                                            euismod dui, eu pulvinar nunc sapien
                                            ornare nisl. Phasellus pede arcu,
                                            dapibus eu, fermentum et, dapibus
                                            sed, urna.
                                        </li>
                                    </ul>

                                    <div class="pb-1 clearfix"></div>

                                    <p>
                                        Phasellus hendrerit. Pellentesque
                                        aliquet nibh nec urna. In nisi neque,
                                        aliquet vel, dapibus id, mattis vel,
                                        nisi. Sed pretium, ligula
                                        <a href="#">sollicitudin laoreet</a>
                                        viverra, tortor libero sodales leo, eget
                                        blandit nunc tortor eu nibh. Nullam
                                        mollis. Ut justo. Suspendisse potenti.
                                        Sed egestas, ante et vulputate volutpat,
                                        eros pede semper est, vitae luctus metus
                                        libero eu augue. Morbi purus libero,
                                        faucibus adipiscing, commodo quis,
                                        gravida id, est. Sed lectus. Praesent
                                        elementum hendrerit tortor. Sed semper
                                        lorem at felis.
                                    </p>

                                    <blockquote>
                                        <p>
                                            “ Sed egestas, ante et vulputate
                                            volutpat, eros pede semper est,
                                            vitae luctus metus libero eu augue.
                                            ”
                                        </p>
                                    </blockquote>

                                    <p>
                                        Morbi purus libero, faucibus adipiscing,
                                        commodo quis, gravida id, est. Sed
                                        lectus. Praesent elementum hendrerit
                                        tortor. Sed semper lorem at felis.
                                        Vestibulum volutpat, lacus a ultrices
                                        sagittis, mi neque euismod dui, eu
                                        pulvinar nunc sapien ornare nisl.
                                        Phasellus pede arcu, dapibus eu,
                                        fermentum et, dapibus sed, urna. Morbi
                                        interdum mollis sapien. Sed ac risus.
                                        Phasellus lacinia, magna a ullamcorper
                                        laoreet, lectus arcu pulvinar risus,
                                        vitae facilisis libero dolor a purus.
                                    </p>

                                    <div class="pb-1"></div>

                                    <h3>Morbi interdum mollis sapien.</h3>

                                    <img
                                        v-lazy="'./images/blog/single/3.jpg'"
                                        alt="image"
                                    />

                                    <p>
                                        Sed pretium, ligula sollicitudin laoreet
                                        viverra, tortor libero sodales leo, eget
                                        blandit nunc tortor eu nibh. Nullam
                                        mollis. Ut justo. Suspendisse potenti.
                                        Sed egestas, ante et vulputate volutpat,
                                        eros pede semper est, vitae luctus metus
                                        libero eu augue. Morbi purus libero,
                                        faucibus adipiscing, commodo quis,
                                        gravida id, est. Sed lectus. Praesent
                                        <a href="#">elementum hendrerit</a>
                                        tortor. Sed semper lorem at felis.
                                        Vestibulum volutpat, lacus a ultrices
                                        sagittis, mi neque euismod dui, eu
                                        pulvinar nunc sapien ornare nisl.
                                        Phasellus pede arcu, dapibus eu,
                                        fermentum et, dapibus sed, urna.
                                    </p>

                                    <p>
                                        Morbi interdum mollis sapien. Sed ac
                                        risus. Phasellus lacinia, magna a
                                        ullamcorper laoreet, lectus arcu
                                        pulvinar risus, vitae facilisis libero
                                        dolor a purus. Sed vel lacus. Mauris
                                        nibh felis, adipiscing varius,
                                        adipiscing in, lacinia vel, tellus.
                                        Suspendisse ac urna. Etiam pellentesque
                                        mauris ut lectus. Nunc tellus ante,
                                        mattis eget, gravida vitae, ultricies
                                        ac, leo. Integer leo pede, ornare a,
                                        lacinia eu, vulputate vel, nisl.
                                        Suspendisse mauris. Fusce accumsan
                                        mollis eros. Pellentesque a diam sit
                                        amet mi ullamcorper vehicula. Integer
                                        adipiscing risus a sem. Nullam quis
                                        massa sit amet nibh viverra malesuada.
                                        Nunc sem lacus, accumsan quis, faucibus
                                        non, congue vel, arcu.
                                    </p>
                                </div>

                                <div
                                    class="entry-footer row no-gutters flex-column flex-md-row"
                                >
                                    <div class="col-md">
                                        <div class="entry-tags">
                                            <span>Tags:</span>
                                            <a href="#">photography</a>
                                            <a href="#">style</a>
                                        </div>
                                    </div>

                                    <div class="col-md-auto mt-2 mt-md-0">
                                        <div
                                            class="social-icons social-icons-color"
                                        >
                                            <span class="social-label"
                                                >Share this post:</span
                                            >
                                            <a
                                                href="#"
                                                class="social-icon social-facebook"
                                                title="Facebook"
                                                target="_blank"
                                            >
                                                <i class="icon-facebook-f"></i>
                                            </a>
                                            <a
                                                href="#"
                                                class="social-icon social-twitter"
                                                title="Twitter"
                                                target="_blank"
                                            >
                                                <i class="icon-twitter"></i>
                                            </a>
                                            <a
                                                href="#"
                                                class="social-icon social-pinterest"
                                                title="Pinterest"
                                                target="_blank"
                                            >
                                                <i class="icon-pinterest"></i>
                                            </a>
                                            <a
                                                href="#"
                                                class="social-icon social-linkedin"
                                                title="Linkedin"
                                                target="_blank"
                                            >
                                                <i class="icon-linkedin"></i>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="entry-author-details">
                                <figure class="author-media">
                                    <a href="#">
                                        <img
                                            v-lazy="
                                                './images/blog/single/author.jpg'
                                            "
                                            alt="User name"
                                        />
                                    </a>
                                </figure>

                                <div class="author-body">
                                    <div
                                        class="author-header row no-gutters flex-column flex-md-row"
                                    >
                                        <div class="col">
                                            <h4>
                                                <a href="#">John Doe</a>
                                            </h4>
                                        </div>

                                        <div class="col-auto mt-1 mt-md-0">
                                            <a href="#" class="author-link">
                                                View all posts by John Doe
                                                <i
                                                    class="icon-long-arrow-right"
                                                ></i>
                                            </a>
                                        </div>
                                    </div>

                                    <div class="author-content">
                                        <p>
                                            Praesent dapibus, neque id cursus
                                            faucibus, tortor neque egestas
                                            auguae, eu vulputate magna eros eu
                                            erat. Aliquam erat volutpat.
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </article>

                        <nav class="pager-nav">
                            <nuxt-link
                                class="pager-link pager-link-prev"
                                :to="'/blog/single/default/' + prevBlog.slug"
                                v-if="prevBlog"
                            >
                                Previous Post
                                <span class="pager-link-title">{{
                                    prevBlog.title
                                }}</span>
                            </nuxt-link>
                            <nuxt-link
                                to="#"
                                class="pager-link"
                                v-else-if="!prevBlog"
                            >
                                <span class="page-link-title ml-4"
                                    >This is the first blog.</span
                                >
                            </nuxt-link>
                            <nuxt-link
                                class="pager-link pager-link-next"
                                :to="'/blog/single/default/' + nextBlog.slug"
                                v-if="nextBlog"
                            >
                                Next Post
                                <span class="pager-link-title">{{
                                    nextBlog.title
                                }}</span>
                            </nuxt-link>
                            <nuxt-link
                                to="#"
                                class="pager-link"
                                v-else-if="!nextBlog"
                            >
                                <span class="page-link-title mr-4"
                                    >This is the last blog.</span
                                >
                            </nuxt-link>
                        </nav>

                        <div class="related-posts">
                            <h3 class="title">Related Posts</h3>

                            <p
                                class="blogs-info"
                                v-if="relatedBlogs.length === 0"
                            >
                                No related posts were found.
                            </p>

                            <div class="swiper-carousel swiper-related">
                                <div v-swiper:swiper1="carouselSetting">
                                    <div class="swiper-wrapper">
                                        <div
                                            class="swiper-slide"
                                            v-for="(blog,
                                            index) in relatedBlogs"
                                            :key="index"
                                        >
                                            <blog-one
                                                :blog="blog"
                                                class="entry-grid"
                                                :isContent="false"
                                                :isAuthor="false"
                                            ></blog-one>
                                        </div>
                                    </div>
                                </div>
                                <div class="swiper-dots"></div>
                            </div>
                        </div>

                        <div class="comments">
                            <h3 class="title">3 Comments</h3>

                            <ul>
                                <li>
                                    <div class="comment">
                                        <figure class="comment-media">
                                            <a href="#">
                                                <img
                                                    v-lazy="
                                                        './images/blog/comments/1.jpg'
                                                    "
                                                    alt="User name"
                                                />
                                            </a>
                                        </figure>

                                        <div class="comment-body">
                                            <a href="#" class="comment-reply"
                                                >Reply</a
                                            >
                                            <div class="comment-user">
                                                <h4>
                                                    <a href="#"
                                                        >Jimmy Pearson</a
                                                    >
                                                </h4>
                                                <span class="comment-date"
                                                    >November 9, 2018 at 2:19
                                                    pm</span
                                                >
                                            </div>

                                            <div class="comment-content">
                                                <p>
                                                    Sed pretium, ligula
                                                    sollicitudin laoreet
                                                    viverra, tortor libero
                                                    sodales leo, eget blandit
                                                    nunc tortor eu nibh. Nullam
                                                    mollis. Ut justo.
                                                    Suspendisse potenti.
                                                </p>
                                            </div>
                                        </div>
                                    </div>

                                    <ul>
                                        <li>
                                            <div class="comment">
                                                <figure class="comment-media">
                                                    <a href="#">
                                                        <img
                                                            v-lazy="
                                                                './images/blog/comments/2.jpg'
                                                            "
                                                            alt="User name"
                                                        />
                                                    </a>
                                                </figure>

                                                <div class="comment-body">
                                                    <a
                                                        href="#"
                                                        class="comment-reply"
                                                        >Reply</a
                                                    >
                                                    <div class="comment-user">
                                                        <h4>
                                                            <a href="#"
                                                                >Lena Knight</a
                                                            >
                                                        </h4>
                                                        <span
                                                            class="comment-date"
                                                            >November 9, 2018 at
                                                            2:19 pm</span
                                                        >
                                                    </div>

                                                    <div
                                                        class="comment-content"
                                                    >
                                                        <p>
                                                            Morbi interdum
                                                            mollis sapien. Sed
                                                            ac risus.
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </li>

                                <li>
                                    <div class="comment">
                                        <figure class="comment-media">
                                            <a href="#">
                                                <img
                                                    v-lazy="
                                                        './images/blog/comments/3.jpg'
                                                    "
                                                    alt="User name"
                                                />
                                            </a>
                                        </figure>

                                        <div class="comment-body">
                                            <a href="#" class="comment-reply"
                                                >Reply</a
                                            >
                                            <div class="comment-user">
                                                <h4>
                                                    <a href="#"
                                                        >Johnathan Castillo</a
                                                    >
                                                </h4>
                                                <span class="comment-date"
                                                    >November 9, 2018 at 2:19
                                                    pm</span
                                                >
                                            </div>

                                            <div class="comment-content">
                                                <p>
                                                    Vestibulum volutpat, lacus a
                                                    ultrices sagittis, mi neque
                                                    euismod dui, eu pulvinar
                                                    nunc sapien ornare nisl.
                                                    Phasellus pede arcu, dapibus
                                                    eu, fermentum et, dapibus
                                                    sed, urna.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>

                        <div class="reply">
                            <div class="heading">
                                <h3 class="title">Leave A Reply</h3>

                                <p class="title-desc">
                                    Your email address will not be published.
                                    Required fields are marked *
                                </p>
                            </div>

                            <form action="#">
                                <label for="reply-message" class="sr-only"
                                    >Comment</label
                                >
                                <textarea
                                    name="reply-message"
                                    id="reply-message"
                                    cols="30"
                                    rows="4"
                                    class="form-control"
                                    required
                                    placeholder="Comment *"
                                ></textarea>

                                <div class="row">
                                    <div class="col-md-6">
                                        <label for="reply-name" class="sr-only"
                                            >Name</label
                                        >
                                        <input
                                            type="text"
                                            class="form-control"
                                            id="reply-name"
                                            name="reply-name"
                                            required
                                            placeholder="Name *"
                                        />
                                    </div>

                                    <div class="col-md-6">
                                        <label for="reply-email" class="sr-only"
                                            >Email</label
                                        >
                                        <input
                                            type="email"
                                            class="form-control"
                                            id="reply-email"
                                            name="reply-email"
                                            required
                                            placeholder="Email *"
                                        />
                                    </div>
                                </div>

                                <button
                                    type="submit"
                                    class="btn btn-outline-primary-2"
                                >
                                    <span>POST COMMENT</span>
                                    <i class="icon-long-arrow-right"></i>
                                </button>
                            </form>
                        </div>
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
import { mapGetters } from 'vuex';

import Sticky from 'vue-sticky-directive';
import BlogOne from '~/components/elements/blogs/BlogOne';
import PageHeader from '~/components/elements/PageHeader';
import BlogSidebar from '~/components/partial/blog/BlogSidebar';
import Repository, { baseUrl } from '~/repositories/repository.js';
import {
    carouselSettingDefault,
    carouselSettingSingle
} from '~/utilities/carousel';

export default {
    components: {
        BlogOne,
        PageHeader,
        BlogSidebar
    },
    directives: {
        Sticky
    },
    data: function() {
        return {
            blog: null,
            prevBlog: null,
            nextBlog: null,
            relatedBlogs: [],
            blogCategories: [],
            loaded: false,
            isSidebar: false,
            shouldSticky: false,
            baseUrl: baseUrl,
            carouselSettingSingle: carouselSettingSingle,
            carouselSetting: {
                ...carouselSettingDefault,
                slidesPerView: 3,
                breakpoints: {
                    400: {
                        slidesPerView: 1
                    },
                    768: {
                        slidesPerView: 2
                    },
                    992: {
                        slidesPerView: 3
                    }
                },
                navigation: {
                    nextEl: '.swiper-related .swiper-next',
                    prevEl: '.swiper-related .swiper-prev'
                },
                pagination: {
                    el: '.swiper-related .swiper-dots',
                    clickable: true
                }
            }
        };
    },
    computed: {
        ...mapGetters('demo', ['currentDemo']),
        date: function() {
            let options = {
                year: 'numeric',
                month: 'short',
                day: '2-digit',
                timeZone: 'UTC'
            };

            return new Date(this.blog.date).toLocaleString('en-us', options);
        }
    },
    created: function() {
        this.getBlog();
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
        getBlog: async function() {
            this.loaded = false;
            await Repository.get(
                `${baseUrl}/single/${this.$route.params.slug}`,
                {
                    params: { demo: this.currentDemo }
                }
            )
                .then(response => {
                    this.blog = { ...response.data.blog };
                    this.relatedBlogs = [...response.data.relatedBlogs];
                    this.prevBlog = response.data.prevBlog;
                    this.nextBlog = response.data.nextBlog;
                    this.blogCategories = response.data.categories;
                    this.loaded = true;
                })
                .catch(error => ({ error: JSON.stringify(error) }));
        },
        openVideoModal: function() {
            this.$modal.show(
                () => import('~/components/elements/modals/VideoModal'),
                {},
                { width: '1060', height: '596', adaptive: true }
            );
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
