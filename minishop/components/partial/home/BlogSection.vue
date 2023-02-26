<template>
    <div
        class="container container_padding"
        style="background:white;border-bottom-left-radius:6px;border-bottom-right-radius:6px;margin-top:-50px;"
    >
        <h2 class="title-lg text-center mb-3 mb-md-4">BÀI VIẾT GẦN ĐÂY</h2>
        <div class="swiper-carousel carousel-with-shadow swiper-blog">
            <div class="mb-0" v-swiper:swiper1="carouselSetting">
                <div class="swiper-wrapper">
                    <div
                        class="swiper-slide"
                        v-for="(blog, index) in blogs"
                        :key="index"
                    >
                        <blog-four :blog="blog" class="mb-2"></blog-four>
                    </div>
                </div>
            </div>
            <div class="swiper-dots swiper-dots-inner d-block d-sm-none"></div>
        </div>

        <div class="text-center">
            <nuxt-link
                :to="'/blog/listing'"
                class="btn btn-outline-darker btn-more"
            >
                <span>Xem thêm bài viết</span>
                <i class="icon-long-arrow-right"></i>
            </nuxt-link>
        </div>
    </div>
</template>
<script>
import BlogFour from '~/components/elements/blogs/BlogFour';
import { carouselSettingDefault } from '~/utilities/carousel';

export default {
    components: {
        BlogFour
    },
    props: {
        blogs: Array
    },
    data: function() {
        return {
            carouselSetting: {
                ...carouselSettingDefault,
                slidesPerView: 3,
                breakpoints: {
                    480: {
                        slidesPerView: 1
                    },
                    768: {
                        slidesPerView: 2
                    },
                    992: {
                        slidesPerView: 3
                    }
                },
                pagination: {
                    el: '.blog-posts .swiper-dots',
                    clickable: true
                }
            }
        };
    }
};
</script>
