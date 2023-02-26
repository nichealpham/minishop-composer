<template>
    <div>
        <h2 class="title text-center mb-4">CÓ THỂ BẠN QUAN TÂM</h2>

        <div v-if="products.length == 0">
            <div class="swiper-carousel carousel-with-shadow">
                <div v-swiper:swiper2="carouselSetting">
                    <div class="swiper-wrapper">
                        <div
                            class="swiper-slide"
                            v-for="item in [1, 2, 3, 4]"
                            :key="item"
                        >
                            <div class="skel-pro"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="swiper-carousel carousel-with-shadow swiper-1" v-else>
            <div v-swiper:swiper1="carouselSetting">
                <div class="swiper-wrapper">
                    <div
                        class="swiper-slide"
                        v-for="(product, index) in products"
                        :key="index"
                    >
                        <product-nine :product="product"></product-nine>
                    </div>
                </div>
            </div>
            <div class="swiper-nav">
                <div class="swiper-prev">
                    <i class="icon-angle-left"></i>
                </div>
                <div class="swiper-next">
                    <i class="icon-angle-right"></i>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import ProductNine from '~/components/elements/products/ProductNine';
import { carouselSetting1 } from '~/utilities/carousel';

export default {
    components: {
        ProductNine
    },
    props: {
        products: {
            type: Array,
            default: function() {
                return [];
            }
        }
    },
    data: function() {
        return {
            carouselSetting: {
                ...carouselSetting1,
                navigation: {
                    nextEl: '.swiper-1 .swiper-next',
                    prevEl: '.swiper-1 .swiper-prev'
                },
                pagination: {
                    el: '.swiper-1 .swiper-dots',
                    clickable: true
                }
            }
        };
    }
};
</script>
