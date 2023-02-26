<template>
    <div class="products mb-3 content-overlay">
        <p
            class="no-results"
            v-if="products.length === 0 && loaded"
        >No products matching your selection.</p>
        <div class="row">
            <template v-if="!loaded">
                <div class="col-6 col-md-4 col-xl-3 mb-2" v-for="item in fakeArray" :key="item">
                    <div class="skel-pro"></div>
                </div>
            </template>
            <template v-else>
                <div
                    class="col-6 col-md-4 col-xl-3"
                    v-for="(product, index) in products"
                    :key="index"
                >
                    <product-five :product="product"></product-five>
                </div>
            </template>
        </div>
    </div>
</template>
<script>
import ProductFive from '~/components/elements/products/ProductFive';
export default {
    components: {
        ProductFive
    },
    props: {
        products: Array,
        loaded: Boolean
    },
    data: function() {
        return {
            fakeArray: [1, 2, 3, 4, 5, 6, 7, 8]
        };
    }
};
</script>