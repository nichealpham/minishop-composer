<template>
    <article class="entry entry-display">
        <figure
            class="entry-media mb-0"
            style="height:260px;overflow:hidden;border-radius:12px;"
        >
            <nuxt-link :to="'/blog/single/fullWidth/' + blog.slug">
                <img
                    v-if="blog.image && blog.image[0]"
                    v-lazy="`${blog.image[0].url}`"
                    alt="blog"
                    :width="blog.image[0].width"
                />
            </nuxt-link>
        </figure>

        <div class="entry-body text-center">
            <h2 class="entry-title" style="margin-top:12px;">
                <nuxt-link :to="'/blog/single/fullWidth/' + blog.slug">{{
                    blog.title
                }}</nuxt-link>
            </h2>
            <div class="entry-content">
                <span
                    v-for="(cat, index) of blog.blog_categories"
                    :key="index"
                    class="mr-2"
                >
                    •
                    <nuxt-link :to="'/blog/single/fullWidth/' + blog.slug">{{
                        cat.name
                    }}</nuxt-link>
                </span>

                <div class="blog_content_item" v-html="blog.content"></div>
                <nuxt-link
                    :to="'/blog/single/fullWidth/' + blog.slug"
                    class="read-more mt-1"
                    >Xem chi tiết</nuxt-link
                >
            </div>
        </div>
    </article>
</template>
<script>
import { baseUrl } from '~/repositories/repository';
import moment from 'moment';

export default {
    props: {
        blog: Object
    },
    data: function() {
        return {
            baseUrl: baseUrl
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
            // return new Date(this.blog.date).toLocaleString('en-us', options);
        }
    }
};
</script>
