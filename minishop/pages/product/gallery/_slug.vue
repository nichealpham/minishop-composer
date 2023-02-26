<template>
    <main class="main">
        <breadcrumb :prev-product="prevProduct" :next-product="nextProduct" current="Extended"></breadcrumb>

        <div class="page-content">
            <div class="container skeleton-body">
                <div class="product-details-top">
                    <div class="skel-pro-single gallery mb-4" :class="{loaded: loaded}">
                        <div class="row">
                            <div class="col-12">
                                <div class="skel-product-gallery"></div>
                                <gallery-extended :product="product"></gallery-extended>
                            </div>
                            <div class="col-12">
                                <div class="entry-summary row mt-5">
                                    <div class="col-6">
                                        <div class="entry-summary1"></div>
                                    </div>
                                    <div class="col-6">
                                        <div class="entry-summary2"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <detail-three :product="product" v-if="product"></detail-three>
                </div>

                <info-one></info-one>

                <related-products-one :products="relatedProducts"></related-products-one>
            </div>
        </div>
    </main>
</template>
<script>
import { mapGetters } from 'vuex';

import GalleryExtended from '~/components/partial/product/gallery/GalleryExtended';
import DetailThree from '~/components/partial/product/details/DetailThree';
import InfoOne from '~/components/partial/product/info-tabs/InfoOne';
import Breadcrumb from '~/components/partial/product/BreadCrumb';
import RelatedProductsOne from '~/components/partial/product/related/RelatedProductsOne';
import Repository, { baseUrl } from '~/repositories/repository.js';

export default {
    components: {
        DetailThree,
        InfoOne,
        Breadcrumb,
        GalleryExtended,
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