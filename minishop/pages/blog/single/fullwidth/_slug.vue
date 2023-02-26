<template>
    <main class="main">
        <div class="page-content">
            <template v-if="blog">
                <div
                    class="blog_cover"
                    v-if="blog.image && blog.image[0]"
                    style="width:100vw;display:block;"
                    :style="{
                        'background-image': `url('${blog.image[0].url}')`,
                        'background-type': 'cover'
                    }"
                ></div>
            </template>
            <div
                class="container skeleton-body container_padding"
                :class="{ loaded: getLoaded }"
                style="background:white;border-radius:6px;"
            >
                <div class="skel-single-post"></div>
                <template v-if="blog">
                    <article class="entry single-entry entry-fullwidth">
                        <div class="entry-body">
                            <div class="entry-meta mb-1 center_div">
                                <span class="entry-author">
                                    Tác giả: {{ blog.author }}
                                </span>
                                <span class="meta-separator">|</span>
                                <span>Ngày: {{ date }}</span>
                                <span class="meta-separator">|</span>
                                <span>Lượt xem: {{ blog.views }}</span>
                            </div>

                            <h2
                                class="entry-title center_div"
                                style="font-size:200% !important"
                            >
                                {{ blog.title }}
                            </h2>
                            <div
                                class="entry-cats center_div mt-1"
                                v-if="blog.blog_categories"
                            >
                                <span
                                    v-for="(cat, index) of blog.blog_categories"
                                    :key="index"
                                    class="mr-2"
                                >
                                    •
                                    <nuxt-link
                                        :to="
                                            '/blog/single/fullWidth/' +
                                                blog.slug
                                        "
                                        >{{ cat.name }}</nuxt-link
                                    >
                                </span>
                            </div>

                            <div
                                class="blog_content_detail"
                                v-html="blog.detail"
                            ></div>

                            <div class="entry-meta mb-1 center_div">
                                <span class="entry-author">
                                    Tác giả: {{ blog.author }}
                                </span>
                                <span class="meta-separator">|</span>
                                <span>Ngày: {{ date }}</span>
                                <span class="meta-separator">|</span>
                                <span>Lượt xem: {{ blog.views }}</span>
                            </div>
                        </div>
                    </article>
                </template>
            </div>
        </div>
    </main>
</template>
<script>
import Sticky from 'vue-sticky-directive';
import BlogOne from '~/components/elements/blogs/BlogOne';
import Repository, { baseUrl } from '~/repositories/repository.js';
import { carouselSetting1, carouselSettingSingle } from '~/utilities/carousel';
import { mapGetters, mapMutations, mapActions } from 'vuex';
import moment from 'moment';

export default {
    components: {
        BlogOne
    },
    watch: {
        getCurrentItem(val) {
            this.blog = val;
        }
    },
    data: function() {
        return {
            blog: null,
            carouselSettingSingle: carouselSettingSingle,
            carouselSetting: {
                ...carouselSetting1,
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
    directives: {
        Sticky
    },
    computed: {
        date: function() {
            let options = {
                year: 'numeric',
                month: 'short',
                day: '2-digit',
                timeZone: 'UTC'
            };
            return moment(this.blog.date).format('DD-MM-YYYY');
            // return new Date(this.blog.date).toLocaleString('en-us', options);
        },
        ...mapGetters('blog', ['getCurrentItem', 'getLoaded'])
    },
    created: function() {
        this.getDetail(this.$route.params.slug);
    },
    methods: {
        ...mapActions('blog', ['getDetail'])
    }
};
</script>

<style lang="scss">
.center_div {
    width: 100%;
    display: inline-block;
    text-align: center;
    color: black;
    * {
        color: black !important;
    }
}
.blog_content_detail {
    max-width: 900px;
    margin-left: auto;
    margin-right: auto;
    * {
        max-width: 100%;
        // font-size: 130% !important;
        font-size: 1.8rem !important;
        // font-family: "IBM Plex Sans" !important;
        font-family: 'Roboto' !important;
        color: #424242;
        text-align: justify;
    }
    ul {
        list-style: inside;
    }
    b,
    strong {
        font-weight: 600;
    }
    b,
    strong * {
        font-weight: 600;
    }
    figure {
        margin: 0px auto 0px auto !important;
    }
    figure.media {
        display: block;
        width: 100% !important;
        height: auto !important;
        margin: 12px 0 20px 0;
    }
    figure.image img {
        margin: 12px 0 12px 0;
        width: 100% !important;
        border-radius: 12px;
    }
    img {
        border-radius: 12px;
        margin: 12px 0 12px 0;
        // border: 1px solid #bebebe;
    }
    table {
        border-collapse: collapse;
        margin: 20px 0px 20px 0px;
        width: 100%;
    }
    table,
    th,
    td {
        border: 1px solid #757575;
        padding: 0px 5px 0px 5px;
        min-width: 30px;
    }
    th {
        border-bottom: 0px;
    }
    p {
        margin: 10px 0px 10px 0px;
        padding: 0px;
    }
    th,
    td * {
        margin: 0px;
        padding: 4px;
    }
    p.note {
        opacity: 0.8;
        text-align: center;
    }
    p.note_title {
        text-align: center;
    }
    a {
        color: #2962ff;
        text-decoration: underline;
        * {
            color: #2962ff;
        }
    }

    .text-huge,
    h1 {
        font-size: 180%;
        * {
            font-size: 180%;
        }
    }

    .text-big,
    h2 {
        font-size: 160%;
        * {
            font-size: 160%;
        }
    }

    .text-small {
        font-size: 100%;
        * {
            font-size: 100%;
        }
    }
}
</style>
