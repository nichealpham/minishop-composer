<template>
    <div
        class="swiper-carousel product-gallery-carousel swiper-1"
        v-if="product"
    >
        <div v-swiper:swiper1="carouselSetting">
            <div class="swiper-wrapper">
                <div
                    class="swiper-slide"
                    v-for="(picture, index) in product.pictures"
                    :key="index"
                >
                    <figure class="product-gallery-image p-0">
                        <img
                            v-lazy="`${baseUrl}${picture.url}`"
                            alt="product"
                            :width="picture.width"
                            :height="picture.height"
                        />
                    </figure>
                </div>
            </div>
        </div>
        <div class="swiper-nav nav-side">
            <div class="swiper-prev">
                <i class="icon-angle-left"></i>
            </div>
            <div class="swiper-next">
                <i class="icon-angle-right"></i>
            </div>
        </div>
    </div>
</template>
<script>
import { VueLazyloadImage } from 'vue-lazyload';
import { carouselSettingDefault } from '~/utilities/carousel';
import { baseUrl } from '~/repositories/repository.js';

export default {
    components: {},
    props: {
        product: {
            type: Object,
            default: function() {
                return {
                    pictures: []
                };
            }
        }
    },
    data: function() {
        return {
            baseUrl: baseUrl,
            currentIndex: 0,
            carouselSetting: {
                ...carouselSettingDefault,
                spaceBetween: 10,
                slidesPerView: 3,
                navigation: {
                    nextEl: '.swiper-1 .swiper-next',
                    prevEl: '.swiper-1 .swiper-prev'
                },
                breakpoints: {
                    768: {
                        slidesPerView: 2
                    },
                    576: {
                        slidesPerView: 1
                    }
                }
            }
        };
    },
    computed: {
        lightBoxMedia: function() {
            return this.product.pictures.reduce((acc, cur) => {
                return [
                    ...acc,
                    {
                        thumb: `${cur.url}`,
                        src: `${cur.url}`,
                        caption: this.product.name
                    }
                ];
            }, []);
        }
    },
    methods: {}
};
</script>
