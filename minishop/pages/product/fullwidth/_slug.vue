<template>
    <main class="main">
        <breadcrumb
            :prev-product="prevProduct"
            :next-product="nextProduct"
            current="Full Width"
            :full-width="true"
        ></breadcrumb>

        <div class="page-content">
            <div class="container-fluid skeleton-body horizontal">
                <div class="row">
                    <div class="col-xl-10 col-lg-9">
                        <div class="product-details-top detail-fullwidth">
                            <div class="row skel-pro-single" :class="{loaded: loaded}">
                                <div class="col-md-6 col-lg-7">
                                    <div class="skel-product-gallery"></div>
                                    <gallery-horizontal :product="product"></gallery-horizontal>
                                </div>

                                <div class="col-md-6 col-lg-5">
                                    <div class="entry-summary row">
                                        <div class="col-md-12">
                                            <div class="entry-summary1"></div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="entry-summary2"></div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="entry-summary3"></div>
                                        </div>
                                    </div>
                                    <detail-one :product="product" v-if="product" class="mb-0"></detail-one>

                                    <info-three v-if="loaded"></info-three>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-2 col-lg-3">
                        <sidebar :products="relatedProducts" :loaded="loaded"></sidebar>
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>
<script>
import { mapGetters } from 'vuex';

import GalleryHorizontal from '~/components/partial/product/gallery/GalleryHorizontal';
import DetailOne from '~/components/partial/product/details/DetailOne';
import InfoThree from '~/components/partial/product/info-tabs/InfoThree';
import Breadcrumb from '~/components/partial/product/BreadCrumb';
import Sidebar from '~/components/partial/product/sidebar/Sidebar';
import Repository, { baseUrl } from '~/repositories/repository.js';

export default {
    components: {
        DetailOne,
        InfoThree,
        Sidebar,
        Breadcrumb,
        GalleryHorizontal
    },
    data: function() {
        return {
            product: null,
            prevProduct: null,
            nextProduct: null,
            relatedProducts: [],
            loaded: false
        };
    },
    computed: {
        ...mapGetters('demo', ['currentDemo'])
    },
    created: function() {
        this.getProduct();
    },
    methods: {
        getProduct: async function() {
            this.loaded = false;
            await Repository.get(
                `${baseUrl}/products/${this.$route.params.slug}`,
                {
                    params: { demo: this.currentDemo }
                }
            )
                .then(response => {
                    this.product = { ...response.data.product };
                    this.relatedProducts = [...response.data.relatedProducts];
                    this.prevProduct = response.data.prevProduct;
                    this.nextProduct = response.data.nextProduct;
                    this.loaded = true;
                })
                .catch(error => ({ error: JSON.stringify(error) }));
        }
    }
};
</script>