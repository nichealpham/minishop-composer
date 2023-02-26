<template>
    <article class="entry">
        <figure
            class="entry-media"
            :class="blog.type === 'video' ? 'entry-video' : ''"
            v-if="blog.image.length <= 1"
        >
            <nuxt-link
                :to="'/blog/single/default/' + blog.slug"
                v-if="blog.type !== 'video'"
            >
                <img
                    v-lazy="`${baseUrl}${blog.image[0].url}`"
                    alt="blog"
                    :width="blog.image[0].width"
                    :height="blog.image[0].height"
                />
            </nuxt-link>

            <template v-else>
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
                    ><i class="icon-play"></i
                ></a>
            </template>
        </figure>
        <figure class="entry-media" v-else-if="blog.image.length > 1">
            <div class="swiper-carousel" :class="'swiper-media-' + blog.id">
                <div v-swiper:swiper1="carouselSetting">
                    <div class="swiper-wrapper">
                        <div
                            class="swiper-slide"
                            v-for="(image, index) in blog.image"
                            :key="index"
                        >
                            <nuxt-link
                                :to="'/blog/single/default/' + blog.slug"
                            >
                                <img
                                    v-lazy="`${baseUrl}${image.url}`"
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
        </figure>

        <div class="entry-body">
            <div class="entry-meta">
                <span class="entry-author" v-if="isAuthor">
                    by
                    <nuxt-link :to="'/blog/single/default/' + blog.slug">{{
                        blog.author
                    }}</nuxt-link>
                </span>
                <span class="meta-separator" v-if="isAuthor">|</span>
                <nuxt-link :to="'/blog/single/default/' + blog.slug">{{
                    date
                }}</nuxt-link>
                <span class="meta-separator">|</span>
                <nuxt-link :to="'/blog/single/default/' + blog.slug"
                    >{{ blog.comments }} Comments</nuxt-link
                >
            </div>

            <h2 class="entry-title">
                <nuxt-link :to="'/blog/single/default/' + blog.slug">{{
                    blog.title
                }}</nuxt-link>
            </h2>
            <div class="entry-cats" v-if="blog.blog_categories">
                in&nbsp;
                <span v-for="(cat, index) of blog.blog_categories" :key="index">
                    <nuxt-link :to="'/blog/single/default/' + blog.slug">{{
                        cat.name
                    }}</nuxt-link>
                    {{ blog.blog_categories.length - 1 > index ? ', ' : '' }}
                </span>
            </div>

            <div class="entry-content" v-if="isContent">
                <p>{{ blog.content }}</p>
                <nuxt-link
                    :to="'/blog/single/default/' + blog.slug"
                    class="read-more"
                    >Continue Reading</nuxt-link
                >
            </div>
        </div>
    </article>
</template>
<script>
import { baseUrl } from '~/repositories/repository';
import { carouselSettingSingle } from '~/utilities/carousel';

export default {
    props: {
        blog: Object,
        isContent: {
            type: Boolean,
            default: function() {
                return true;
            }
        },
        isAuthor: {
            type: Boolean,
            default: function() {
                return true;
            }
        }
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

            return new Date(this.blog.date).toLocaleString('en-us', options);
        }
    },
    methods: {
        openVideoModal: function() {
            this.$modal.show(
                () => import('~/components/elements/modals/VideoModal'),
                {},
                { width: '1060', height: '596', adaptive: true }
            );
        }
    }
};
</script>
