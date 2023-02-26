<template>
    <main class="main">
        <div class="page-content">
            <div
                class="container skeleton-body container_padding"
                style="background:white;border-radius:6px;"
            >
                <div class="product-details-top">
                    <div
                        class="row skel-pro-single"
                        :class="{ loaded: loaded }"
                    >
                        <div class="col-md-6">
                            <div class="skel-product-gallery"></div>
                            <gallery-vertical
                                :product="getCurrentItem"
                                v-if="getCurrentItem"
                            ></gallery-vertical>
                        </div>

                        <div class="col-md-6">
                            <div class="entry-summary row">
                                <div class="col-md-12">
                                    <div
                                        class="entry-summary1 mt-2 mt-md-0"
                                    ></div>
                                </div>
                                <div class="col-md-12">
                                    <div class="entry-summary2"></div>
                                </div>
                            </div>
                            <detail-one
                                :product="getCurrentItem"
                                v-if="getCurrentItem"
                            ></detail-one>
                        </div>
                    </div>
                </div>

                <info-one></info-one>

                <related-products-one
                    :products="getItems"
                ></related-products-one>
            </div>
        </div>
    </main>
</template>
<script>
import { mapGetters, mapActions } from 'vuex';

import GalleryVertical from '~/components/partial/product/gallery/GalleryVertical';
import DetailOne from '~/components/partial/product/details/DetailOne';
import InfoOne from '~/components/partial/product/info-tabs/InfoOne';
import Breadcrumb from '~/components/partial/product/BreadCrumb';
import RelatedProductsOne from '~/components/partial/product/related/RelatedProductsOne';

export default {
    components: {
        DetailOne,
        InfoOne,
        Breadcrumb,
        GalleryVertical,
        RelatedProductsOne
    },
    data: function() {
        return {
            product: null,
            relatedProducts: [],
            loaded: true
        };
    },
    computed: {
        ...mapGetters('product', ['getCurrentItem']),
        ...mapGetters('product', ['getItems'])
    },
    created() {
        this.getDetail(this.$route.params.slug);
    },
    methods: {
        ...mapActions('product', ['getDetail'])
    }
};
</script>
