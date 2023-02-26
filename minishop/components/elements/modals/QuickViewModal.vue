<template>
    <div class="quickView-container m-4">
        <div class="quickView-content horizontal skeleton-body">
            <div class="row skel-pro-single skel-quickview m-0" :class="{loaded: loaded}">
                <div class="col-lg-6 p-0 pr-2">
                    <div class="skel-product-gallery"></div>
                    <div class="product-lg mb-1 position-relative">
                        <span class="product-label label-new" v-if="product.new">New</span>
                        <span class="product-label label-sale" v-if="product.sale_price">Sale</span>
                        <span class="product-label label-top" v-if="product.top">Top</span>
                        <span
                            class="product-label label-out"
                            v-if="product.stock === 0"
                        >Out Of Stock</span>

                        <div class="swiper-carousel swiper-1">
                            <div
                                v-swiper:swiper1="carouselSetting"
                                v-on:slideChangeTransitionEnd="slideChange"
                            >
                                <div class="swiper-wrapper">
                                    <div
                                        class="swiper-slide"
                                        v-for="(picture, index) in product.pictures"
                                        :key="index"
                                    >
                                        <img
                                            v-lazy="`${baseUrl}${picture.url}`"
                                            alt="product"
                                            :width="picture.width"
                                            :height="picture.height"
                                        />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="product-sm row px-2">
                        <div
                            class="col-3 px-2"
                            v-for="(picture, index) in product.sm_pictures"
                            :key="index"
                        >
                            <a
                                class="carousel-dot h-100 d-block"
                                :class="{active: index == currentIndex}"
                                href="#"
                                @click.prevent="changePicture(index)"
                            >
                                <img
                                    v-lazy="`${baseUrl}${picture.url}`"
                                    alt="dot"
                                    :width="picture.width"
                                    :height="picture.height"
                                />
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6 quickview-desc pl-0 pl-lg-4 pr-0 mt-3 mt-lg-0">
                    <div class="entry-summary row">
                        <div class="col-md-12">
                            <div class="entry-summary1"></div>
                        </div>
                        <div class="col-md-12">
                            <div class="entry-summary2"></div>
                        </div>
                    </div>
                    <div class="product-summary pr-4">
                        <detail-one :product="product" class="mb-0"></detail-one>
                    </div>
                </div>
            </div>
        </div>
        <button title="Close (Esc)" type="button" class="mfp-close" @click="$emit('close')">
            <span>×</span>
        </button>
    </div>
</template>
<script>
import DetailOne from '~/components/partial/product/details/DetailOne';
import { carouselSettingSingle } from '~/utilities/carousel';
import { baseUrl } from '~/repositories/repository';
import { getIndex } from '~/utilities/common';

export default {
    components: {
        DetailOne
    },
    props: {
        product: Object
    },
    data: function() {
        return {
            baseUrl: baseUrl,
            carouselSetting: {
                ...carouselSettingSingle,
                loop: false
            },
            loaded: false,
            currentIndex: 0
        };
    },
    watch: {
        $route: function() {
            this.$modal.hideAll();
        }
    },
    mounted: function() {
        setTimeout(() => {
            this.loaded = true;
        }, 300);
    },
    methods: {
        changePicture: function(index) {
            this.currentIndex = index;
            this.swiper1.slideTo(this.currentIndex, 1000, false);
        },
        slideChange: function() {
            var activeSlide = document.querySelector(
                '.quickView-content .swiper-slide-active'
            );
            this.currentIndex = getIndex(activeSlide);
        }
    }
};
</script>