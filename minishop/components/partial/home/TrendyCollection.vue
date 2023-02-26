<template>
    <div class="container trendy-collection">
        <div class="heading heading-center mb-3">
            <h2 class="title-lg">Trendy Products</h2>

            <tabs
                class="nav-pills nav-pills nav-border-anim nav-big justify-content-center nav-dark"
                :data="tabsData"
                id="tab1"
            ></tabs>
        </div>
        <div class="tab-content tab-content-carousel">
            <div class="tab-pane fade show active" id="tab1-all">
                <div class="swiper-carousel carousel-with-shadow swiper-1">
                    <div class="mb-0" v-swiper:swiper1="carouselSetting1">
                        <div class="swiper-wrapper">
                            <div
                                class="swiper-slide"
                                v-for="(product, index) in products.slice(0, 6)"
                                :key="index"
                            >
                                <product-twelve :product="product"></product-twelve>
                            </div>
                        </div>
                    </div>
                    <div class="swiper-dots swiper-dots-inner d-md-none"></div>
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

            <div class="tab-pane fade" id="tab1-furniture">
                <div class="swiper-carousel carousel-with-shadow swiper-2">
                    <div class="mb-0" v-swiper:swiper2="carouselSetting2">
                        <div class="swiper-wrapper">
                            <div
                                class="swiper-slide"
                                v-for="(product, index) in furniture.slice(0, 4)"
                                :key="index"
                            >
                                <product-twelve :product="product"></product-twelve>
                            </div>
                        </div>
                    </div>
                    <div class="swiper-dots swiper-dots-inner d-md-none"></div>
                </div>
            </div>

            <div class="tab-pane fade" id="tab1-decoration">
                <div class="swiper-carousel carousel-with-shadow swiper-3">
                    <div class="mb-0" v-swiper:swiper3="carouselSetting3">
                        <div class="swiper-wrapper">
                            <div
                                class="swiper-slide"
                                v-for="(product, index) in decoration"
                                :key="index"
                            >
                                <product-twelve :product="product"></product-twelve>
                            </div>
                        </div>
                    </div>
                    <div class="swiper-dots swiper-dots-inner d-md-none"></div>
                </div>
            </div>
            <div class="tab-pane fade" id="tab1-lighting">
                <div class="swiper-carousel carousel-with-shadow swiper-4">
                    <div class="mb-0" v-swiper:swiper4="carouselSetting4">
                        <div class="swiper-wrapper">
                            <div
                                class="swiper-slide"
                                v-for="(product, index) in lighting"
                                :key="index"
                            >
                                <product-twelve :product="product"></product-twelve>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import Tabs from '~/components/elements/Tabs';
import ProductTwelve from '~/components/elements/products/ProductTwelve';

import { catFilter } from '~/utilities/common';
import { carouselSetting1 } from '~/utilities/carousel';

export default {
    components: {
        Tabs,
        ProductTwelve
    },
    props: {
        products: Array
    },
    data: function() {
        return {
            tabsData: [
                {
                    id: 'all',
                    title: 'All',
                    active: true
                },
                {
                    id: 'furniture',
                    title: 'Furniture'
                },
                {
                    id: 'decoration',
                    title: 'Decoration'
                },
                {
                    id: 'lighting',
                    title: 'Lighting'
                }
            ],
            carouselSetting1: {
                ...carouselSetting1,
                slidesPerView: 4,
                pagination: {
                    el: '.trendy-collection .swiper-1 .swiper-dots',
                    clickable: true
                },
                navigation: {
                    nextEl: '.trendy-collection .swiper-1 .swiper-next',
                    prevEl: '.trendy-collection .swiper-1 .swiper-prev'
                }
            },
            carouselSetting2: {
                ...carouselSetting1,
                slidesPerView: 4,
                pagination: {
                    el: '.trendy-collection .swiper-2 .swiper-dots',
                    clickable: true
                }
            },
            carouselSetting3: {
                ...carouselSetting1,
                slidesPerView: 4,
                pagination: {
                    el: '.trendy-collection .swiper-3 .swiper-dots',
                    clickable: true
                }
            },
            carouselSetting4: {
                ...carouselSetting1,
                slidesPerView: 4,
                pagination: {
                    el: '.trendy-collection .swiper-4 .swiper-dots',
                    clickable: true
                }
            },
            fakeArray: [1, 2, 3, 4]
        };
    },
    computed: {
        furniture: function() {
            return catFilter(this.products, ['furniture']);
        },
        decoration: function() {
            return catFilter(this.products, ['decoration']);
        },
        lighting: function() {
            return catFilter(this.products, ['lighting']);
        }
    }
};
</script>
