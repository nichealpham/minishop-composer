<template>
    <article class="entry entry-list">
        <div class="row align-items-center">
            <div :class="isElements ? 'col-md-4' : 'col-md-5'">
                <figure class="entry-media mb-0" style="width:100%;">
                    <nuxt-link :to="'/blog/single/fullWidth/' + blog.slug">
                        <img
                            v-if="blog.image && blog.image[0]"
                            v-lazy="`${blog.image[0].url}`"
                            alt="blog"
                            :width="blog.image[0].width"
                        />
                    </nuxt-link>
                </figure>
                <!-- <figure class="entry-media" v-else-if="blog.image.length > 1">
                    <div
                        class="swiper-carousel"
                        :class="'swiper-media-' + blog.id"
                    >
                        <div v-swiper:swiper1="carouselSetting">
                            <div class="swiper-wrapper">
                                <div
                                    class="swiper-slide"
                                    v-for="(image, index) in blog.image"
                                    :key="index"
                                >
                                    <nuxt-link
                                        :to="
                                            '/blog/single/fullWidth/' +
                                                blog.slug
                                        "
                                    >
                                        <img
                                            v-lazy="`${image.url}`"
                                            alt="blog"
                                            :width="image.width"
                                            :height="image.height"
                                        />
                                    </nuxt-link>
                                </div>
                            </div>
                        </div>
                        <div class="swiper-nav entry-nav">
                            <div class="swiper-prev">
                                <i class="icon-angle-left"></i>
                            </div>
                            <div class="swiper-next">
                                <i class="icon-angle-right"></i>
                            </div>
                        </div>
                    </div>
                </figure> -->
            </div>
            <div :class="isElements ? 'col-md-8' : 'col-md-7'">
                <div class="entry-body">
                    <div class="entry-meta mt-1">
                        <span class="entry-author">
                            Tác giả: {{ blog.author }}
                        </span>
                        <span class="meta-separator">|</span>
                        {{ date }}
                        <span class="meta-separator">|</span>
                        Lượt xem: {{ blog.views }}
                    </div>
                    <h2 class="entry-title">
                        <nuxt-link
                            :to="'/blog/single/fullWidth/' + blog.slug"
                            >{{ blog.title }}</nuxt-link
                        >
                    </h2>
                    <div class="entry-cats mt-1" v-if="blog.blog_categories">
                        <span
                            v-for="(cat, index) of blog.blog_categories"
                            :key="index"
                            class="mr-2"
                        >
                            •
                            <nuxt-link
                                :to="'/blog/single/fullWidth/' + blog.slug"
                                >{{ cat.name }}</nuxt-link
                            >
                        </span>
                    </div>
                    <div class="entry-content">
                        <div
                            class="blog_content_item"
                            v-html="blog.content"
                        ></div>
                        <nuxt-link
                            :to="'/blog/single/fullWidth/' + blog.slug"
                            class="read-more mt-1"
                            >Xem chi tiết</nuxt-link
                        >
                    </div>
                </div>
            </div>
        </div>
    </article>
</template>
<script>
import { baseUrl } from '~/repositories/repository';
import { carouselSettingSingle } from '~/utilities/carousel';
import moment from 'moment';

export default {
    props: {
        blog: Object,
        isElements: Boolean
    },
    data: function() {
        return {
            baseUrl: baseUrl,
            carouselSetting: {
                ...carouselSettingSingle,
                navigation: {
                    nextEl: '.swiper-media-' + this.blog.id + ' .swiper-next',
                    prevEl: '.swiper-media-' + this.blog.id + ' .swiper-prev'
                },
                pagination: {
                    el: '.swiper-media-' + this.blog.id + ' .swiper-dots',
                    clickable: true
                }
            }
        };
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
            // return new Date(this.blog.date).toLocaleString('en-vi', options);
        }
    },
    methods: {}
};
</script>

<style lang="scss">
.entry-title {
    font-weight: 500 !important;
    font-size: 1.7rem;
}
.entry-title * {
    font-weight: 500 !important;
    font-size: 1.7rem;
}
.blog_content_item {
    max-width: 900px;
    margin-left: auto;
    margin-right: auto;
    * {
        max-width: 100%;
        // font-family: "IBM Plex Sans" !important;
        font-family: 'Roboto' !important;
        color: #424242;
        text-align: justify;
    }
    .content_big {
        * {
            font-size: 125% !important;
        }
    }
    ul {
        list-style: inside;
    }
    b,
    strong {
        font-weight: 600;
        font-size: 105%;
    }
    b,
    strong * {
        font-weight: 600;
        font-size: 105%;
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
        font-size: 140%;
        line-height: 180%;
        * {
            font-size: 120%;
        }
    }

    .text-big,
    h2 {
        font-size: 120%;
        * {
            font-size: 110%;
        }
    }

    .text-small {
        font-size: 90%;
        * {
            font-size: 90%;
        }
    }
}
</style>
