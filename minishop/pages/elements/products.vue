<template>
    <main class="main">
        <page-header title="Products" subtitle="Elements"></page-header>

        <div class="page-content skeleton-body">
            <div class="container">
                <h2 class="title text-center mb-3">3 Columns Large</h2>

                <div class="row">
                    <template v-if="!loaded">
                        <div
                            class="col-6 col-md-4 col-lg-4 mb-2"
                            v-for="item in [1, 2, 3]"
                            :key="item"
                        >
                            <div class="skel-pro"></div>
                        </div>
                    </template>
                    <div
                        class="col-6 col-md-4 col-lg-4"
                        v-for="(product, index) in productGroup1"
                        :key="index"
                    >
                        <product-one
                            :product="product"
                            class="no-span"
                        ></product-one>
                    </div>
                </div>

                <hr class="mt-1 mb-5" />

                <h2 class="title text-center mb-3">4 Columns Carousel</h2>

                <div class="row" v-if="!loaded">
                    <div
                        class="col-6 col-md-3 col-lg-3 mb-2"
                        v-for="item in [1, 2, 3, 4]"
                        :key="item"
                    >
                        <div class="skel-pro"></div>
                    </div>
                </div>
                <div class="swiper-carousel carousel-with-shadow swiper-1">
                    <div v-swiper:swiper1="carouselSetting1">
                        <div class="swiper-wrapper">
                            <div
                                class="swiper-slide"
                                v-for="(product, index) in productGroup2"
                                :key="index"
                            >
                                <product-two :product="product"></product-two>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mt-3 mb-5" />
                <h2 class="title text-center mb-3">4 Columns Carousel 2</h2>

                <div class="row" v-if="!loaded">
                    <div
                        class="col-6 col-md-3 col-lg-3 mb-2"
                        v-for="item in [1, 2, 3, 4]"
                        :key="item"
                    >
                        <div class="skel-pro"></div>
                    </div>
                </div>
                <div>
                    <div class="swiper-carousel carousel-with-shadow swiper-2">
                        <div v-swiper:swiper5="carouselSetting1">
                            <div class="swiper-wrapper">
                                <div
                                    class="swiper-slide"
                                    v-for="(product, index) in productGroup2"
                                    :key="index"
                                >
                                    <product-three
                                        :product="product"
                                    ></product-three>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mt-3 mb-5" />

                <h2 class="title text-center mb-3">4 Columns Simple</h2>

                <div class="row" v-if="!loaded">
                    <div
                        class="col-6 col-md-3 col-lg-3 mb-2"
                        v-for="item in [1, 2, 3, 4]"
                        :key="item"
                    >
                        <div class="skel-pro"></div>
                    </div>
                </div>
                <div class="row">
                    <div
                        class="col-6 col-md-4 col-lg-3"
                        v-for="(product, index) in productGroup2"
                        :key="index"
                    >
                        <product-four :product="product"></product-four>
                    </div>
                </div>

                <hr class="mt-2 mb-5" />
                <h2 class="title text-center mb-3">5 Columns Simple</h2>

                <div class="row" v-if="!loaded">
                    <div
                        class="col-6 col-md-3 col-lg-3 col-xxl-5col mb-2"
                        v-for="item in [1, 2, 3, 4, 5]"
                        :key="item"
                    >
                        <div class="skel-pro"></div>
                    </div>
                </div>
                <div class="swiper-carousel carousel-with-shadow swiper-3 mb-2">
                    <div v-swiper:swiper3="carouselSetting2">
                        <div class="swiper-wrapper">
                            <div
                                class="swiper-slide"
                                v-for="(product, index) in productGroup3"
                                :key="index"
                            >
                                <product-five :product="product"></product-five>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="mt-0 mb-5" />
            </div>

            <div class="container-fluid">
                <h2 class="title text-center mb-3">Fullwidth</h2>

                <div class="row" v-if="!loaded">
                    <div
                        class="col-6 col-md-3 col-lg-3 col-xl-2 mb-2"
                        v-for="item in [1, 2, 3, 4, 5, 6]"
                        :key="item"
                    >
                        <div class="skel-pro"></div>
                    </div>
                </div>
                <div class="row">
                    <div
                        class="col-6 col-md-4 col-lg-3 col-xl-2"
                        v-for="(product, index) in products"
                        :key="index"
                    >
                        <product-six :product="product"></product-six>
                    </div>
                </div>
            </div>

            <div class="container">
                <hr class="mt-2 mb-5" />
                <h2 class="title text-center mb-3">4 Columns Without Space</h2>

                <div class="row no-gutters" v-if="!loaded">
                    <div
                        class="col-6 col-md-4 col-lg-4 col-xxl-5col mb-2"
                        v-for="item in [1, 2, 3, 4, 5]"
                        :key="item"
                    >
                        <div class="skel-pro"></div>
                    </div>
                </div>
                <div class="row no-gutters">
                    <div
                        class="col-sm-6 col-12 col-md-4 col-lg-3"
                        v-for="(product, index) in productGroup2"
                        :key="index"
                    >
                        <product-seven :product="product"></product-seven>
                    </div>
                </div>
            </div>
        </div>

        <element-list></element-list>
    </main>
</template>

<script>
import PageHeader from '~/components/elements/PageHeader';
import ElementList from '~/components/partial/elements/ElementList';
import ProductOne from '~/components/elements/products/ProductOne';
import ProductTwo from '~/components/elements/products/ProductTwo';
import ProductThree from '~/components/elements/products/ProductThree';
import ProductFour from '~/components/elements/products/ProductFour';
import ProductFive from '~/components/elements/products/ProductFive';
import ProductSix from '~/components/elements/products/ProductSix';
import ProductSeven from '~/components/elements/products/ProductSeven';
import Repository, { baseUrl } from '~/repositories/repository.js';
import { carouselSetting1 } from '~/utilities/carousel';

export default {
    components: {
        PageHeader,
        ElementList,
        ProductOne,
        ProductTwo,
        ProductThree,
        ProductFour,
        ProductFive,
        ProductSix,
        ProductSeven
    },
    data: function() {
        return {
            products: [],
            carouselSetting1: carouselSetting1,
            carouselSetting2: {
                ...carouselSetting1,
                slidesPerView: 5
            }
        };
    },
    computed: {
        productGroup1: function() {
            return this.products.slice(0, 3);
        },
        productGroup2: function() {
            return this.products.slice(0, 4);
        },
        productGroup3: function() {
            return this.products.slice(0, 5);
        }
    },
    created: function() {
        this.getProducts();
    },
    methods: {
        getProducts: async function() {
            this.loaded = false;
            await Repository.get(`${baseUrl}/elements/products`)
                .then(response => {
                    this.products = response.data;
                    this.loaded = true;
                })
                .catch(error => ({ error: JSON.stringify(error) }));
        }
    }
};
</script>
