<template>
    <main class="main">
        <breadcrumb :prev-product="prevProduct" :next-product="nextProduct" current="Extended"></breadcrumb>

        <div class="page-content">
            <div class="container skeleton-body horizontal">
                <div class="product-details-top">
                    <div class="row skel-pro-single" :class="{loaded: loaded}">
                        <div class="col-md-6">
                            <div class="skel-product-gallery"></div>
                            <gallery-horizontal :product="product"></gallery-horizontal>
                        </div>

                        <div class="col-md-6">
                            <div class="entry-summary row">
                                <div class="col-md-12">
                                    <div class="entry-summary1 mt-2 mt-md-0"></div>
                                </div>
                                <div class="col-md-12">
                                    <div class="entry-summary2"></div>
                                </div>
                            </div>
                            <detail-one :product="product" v-if="product"></detail-one>
                        </div>
                    </div>
                </div>
            </div>

            <info-two></info-two>

            <div class="container">
                <related-products-one :products="relatedProducts"></related-products-one>
            </div>
        </div>
    </main>
</template>
<script>
import { mapGetters } from 'vuex';

import GalleryHorizontal from '~/components/partial/product/gallery/GalleryHorizontal';
import DetailOne from '~/components/partial/product/details/DetailOne';
import InfoTwo from '~/components/partial/product/info-tabs/InfoTwo';
import Breadcrumb from '~/components/partial/product/BreadCrumb';
import RelatedProductsOne from '~/components/partial/product/related/RelatedProductsOne';
import Repository, { baseUrl } from '~/repositories/repository.js';

export default {
    components: {
        DetailOne,
        InfoTwo,
        Breadcrumb,
        GalleryHorizontal,
        RelatedProductsOne
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