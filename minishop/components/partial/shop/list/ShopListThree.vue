<template>
    <div class="products mb-3 content-overlay">
        <p
            class="no-results"
            v-if="products.length === 0 && loaded"
        >No products matching your selection.</p>
        <div class="row">
            <template v-if="!loaded">
                <div :class="cols" v-for="item in fakeArray" :key="item">
                    <div class="skel-pro"></div>
                </div>
            </template>
            <template v-else>
                <div :class="cols" v-for="(product, index) in products" :key="index">
                    <product-ten :product="product"></product-ten>
                </div>
            </template>
        </div>
    </div>
</template>
<script>
import ProductTen from '~/components/elements/products/ProductTen';
export default {
    components: {
        ProductTen
    },
    props: {
        products: Array,
        loaded: Boolean
    },
    data: function() {
        return {
            fakeArray: [1, 2, 3, 4, 5, 6, 7, 8]
        };
    },
    computed: {
        cols: function() {
            if (this.$route.params.type == 'boxed')
                return 'col-6 col-md-4 col-lg-4 col-xl-3';
            else return 'col-6 col-md-4 col-lg-4 col-xl-3 col-xxl-2';
        }
    }
};
</script>