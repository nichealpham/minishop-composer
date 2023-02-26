<template>
    <main class="main">
        <breadcrumb :prev-product="prevProduct" :next-product="nextProduct" current="Default"></breadcrumb>

        <div class="page-content">
            <div class="container skeleton-body">
                <div class="product-details-top">
                    <div class="row skel-pro-single sticky" :class="{loaded: loaded}">
                        <div class="col-md-6">
                            <div class="skel-product-gallery"></div>
                            <gallery-sticky :product="product"></gallery-sticky>
                        </div>

                        <div class="col-md-6" sticky-container>
                            <div v-sticky="shouldSticky" sticky-offset="{ top: 69 }">
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

                <related-products-one :products="relatedProducts"></related-products-one>
            </div>
        </div>
    </main>
</template>
<script>
import { mapGetters } from 'vuex';
import Sticky from 'vue-sticky-directive';

import GallerySticky from '~/components/partial/product/gallery/GallerySticky';
import DetailOne from '~/components/partial/product/details/DetailOne';
import InfoThree from '~/components/partial/product/info-tabs/InfoThree';
import Breadcrumb from '~/components/partial/product/BreadCrumb';
import RelatedProductsOne from '~/components/partial/product/related/RelatedProductsOne';
import Repository, { baseUrl } from '~/repositories/repository.js';

export default {
    components: {
        DetailOne,
        InfoThree,
        Breadcrumb,
        GallerySticky,
        RelatedProductsOne
    },
    directives: {
        Sticky
    },
    data: function() {
        return {
            product: null,
            prevProduct: null,
            nextProduct: null,
            relatedProducts: [],
            loaded: false,
            shouldSticky: true
        };
    },
    computed: {
        ...mapGetters('demo', ['currentDemo'])
    },
    created: function() {
        this.getProduct();
    },
    mounted: function() {
        this.stickyHandle();
        window.addEventListener('resize', this.stickyHandle, { passive: true });
    },
    destroyed: function() {
        window.removeEventListener('resize', this.stickyHandle);
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
        },
        stickyHandle: function() {
            if (window.innerWidth > 991) this.shouldSticky = true;
            else this.shouldSticky = false;
        }
    }
};
</script>