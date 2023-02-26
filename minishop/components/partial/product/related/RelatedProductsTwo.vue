<template>
    <div class="widget widget-products">
        <h4 class="widget-title">Related Product</h4>

        <div class="products">
            <product-eleven
                :product="product"
                v-for="(product, index) in products"
                :key="index"
            ></product-eleven>
        </div>

        <nuxt-link
            :to="'/shop/sidebar/4cols'"
            class="btn btn-outline-dark-3 text-truncate"
        >
            <span>View More Products</span>
            <i class="icon-long-arrow-right"></i>
        </nuxt-link>
    </div>
</template>
<script>
import ProductEleven from '~/components/elements/products/ProductEleven';
export default {
    props: {
        products: Array
    },
    components: {
        ProductEleven
    }
};
</script>
