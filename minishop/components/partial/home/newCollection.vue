<template>
    <div
        class="container container_padding"
        style="background:white;border-radius:6px;"
    >
        <div class="heading heading-center">
            <h2 class="title">SẢN PHẨM MỚI</h2>
        </div>
        <tabs
            class="nav-pills nav-pills nav-border-anim justify-content-center mb-3"
            :data="tabsData"
        ></tabs>
        <div class="tab-content">
            <div class="tab-pane fade show active p-0">
                <div class="products">
                    <div
                        class="container skeleton-body horizontal"
                        v-if="!getLoaded"
                    >
                        <div
                            v-for="n in 4"
                            :key="n"
                            class="skel-pro col-6 col-md-3 col-lg-3 loaded"
                            style="float:left;"
                        ></div>
                    </div>
                    <div class="row justify-content-center" v-else>
                        <div
                            class="col-6 col-md-4 col-lg-3"
                            v-for="(product, index) in getItems"
                            :key="index"
                        >
                            <product-twelve :product="product"></product-twelve>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="load-more-container text-center mb-5">
            <a
                class="btn btn-outline-darker btn-load-more"
                @click="$router.push(`/shop/sidebar/4cols`)"
                style="cursor:pointer;"
            >
                <span class="mr-3">Xem thêm sản phẩm</span>
                <i class="icon-refresh"></i>
            </a>
        </div>
    </div>
</template>
<script>
import Tabs from '~/components/elements/Tabs';

import ProductTwelve from '~/components/elements/products/ProductTwelve';
import { mapGetters, mapActions, mapMutations } from 'vuex';

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
                    title: 'Tất cả',
                    active: true,
                    onClick: async () => {
                        this.CLEAN_QUERY_OPTIONS();
                        this.SET_PRODUCT_TYPE('');
                        await this.search({
                            company: this.$companyName,
                            reload: true
                        });
                    }
                },
                {
                    id: 'product',
                    title: 'Sản phẩm',
                    onClick: async () => {
                        this.CLEAN_QUERY_OPTIONS();
                        this.SET_PRODUCT_TYPE(2);
                        await this.search({
                            company: this.$companyName,
                            reload: true
                        });
                    }
                },
                {
                    id: 'service',
                    title: 'Dịch vụ',
                    onClick: async () => {
                        this.CLEAN_QUERY_OPTIONS();
                        this.SET_PRODUCT_TYPE(1);
                        await this.search({
                            company: this.$companyName,
                            reload: true
                        });
                    }
                }
            ]
        };
    },
    computed: {
        ...mapGetters('product', ['getItems', 'getLoaded'])
    },
    methods: {
        ...mapMutations('product', ['CLEAN_QUERY_OPTIONS', 'SET_PRODUCT_TYPE']),
        ...mapActions('product', ['search'])
    }
};
</script>

<style lang="scss">
.product.product-11 .product-body {
    padding-bottom: 2.6rem;
}
</style>
