<template>
    <article class="entry entry-mask">
        <figure
            class="entry-media"
            :class="blog.type === 'video' ? 'entry-video' : ''"
            v-if="blog.image.length <= 1"
        >
            <nuxt-link :to="'/blog/single/default/' + blog.slug">
                <img
                    v-lazy="`${baseUrl}${blog.image[0].url}`"
                    alt="blog"
                    :width="blog.image[0].width"
                    :height="blog.image[0].height"
                />
            </nuxt-link>
        </figure>
        <figure
            class="entry-media entry-gallery"
            v-else-if="blog.image.length > 1"
        >
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
            </div>
        </figure>

        <div class="entry-body">
            <div class="entry-meta">
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
    }
};
</script>
