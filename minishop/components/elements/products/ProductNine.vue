<template>
    <div class="product product-7 text-center">
        <figure class="product-media">
            <span class="product-label label-out" v-if="product.stock === 0"
                >Out Of Stock</span
            >
            <nuxt-link
                :to="'/product/default/' + product.slug"
                v-if="product.pictures[0]"
            >
                <img
                    v-lazy="`${product.pictures[0].url}`"
                    alt="Product"
                    :width="product.sm_pictures[0].width"
                    :height="product.sm_pictures[0].height"
                    class="product-image"
                />
                <img
                    v-lazy="`${product.pictures[1].url}`"
                    alt="Product"
                    :width="product.sm_pictures[1].width"
                    :height="product.sm_pictures[1].height"
                    class="product-image-hover"
                    v-if="product.sm_pictures[1]"
                />
            </nuxt-link>

            <div class="product-action-vertical" v-if="product.stock !== 0">
                <nuxt-link
                    :to="'/shop/wishlist'"
                    class="btn-product-icon btn-wishlist btn-expandable added-to-wishlist"
                    v-if="isInWishlist(product)"
                    key="inWishlist"
                >
                    <span>Xem trong yêu thích</span>
                </nuxt-link>
                <a
                    href="javascript:;"
                    class="btn-product-icon btn-wishlist btn-expandable"
                    @click.prevent="addToWishlist({ product: product })"
                    v-else
                    key="notInWishlist"
                >
                    <span>Thêm vào yêu thích</span>
                </a>
            </div>

            <div class="product-action" v-if="product.stock !== 0">
                <a
                    href="javascript:;"
                    class="btn btn-product btn-cart"
                    @click.prevent="addToCart({ product: product })"
                >
                    <span>thêm vào giỏ hàng</span>
                </a>
            </div>
        </figure>

        <div class="product-body">
            <div class="product-cat mb-1">
                <span
                    v-for="(cat, index) of product.category"
                    :key="index"
                    class="mr-2"
                >
                    •
                    <nuxt-link
                        :to="{
                            path: '/shop/sidebar/list',
                            query: { category: cat.slug }
                        }"
                        >{{ cat.name }}</nuxt-link
                    >
                </span>
            </div>
            <h3 class="product-title mb-1">
                <nuxt-link :to="'/product/default/' + product.slug">{{
                    product.name
                }}</nuxt-link>
            </h3>

            <div class="product-price" v-if="product.stock == 0" key="outPrice">
                <span class="out-price">{{ formatPrice(product.price) }}</span>
            </div>

            <template v-else>
                <template>
                    <div class="product-price">
                        <span class="new-price">{{
                            formatPrice(minPrice)
                        }}</span>
                        <span
                            class="old-price"
                            v-if="maxPrice != minPrice"
                            style="text-decoration:line-through"
                            >{{ formatPrice(maxPrice) }}</span
                        >
                    </div>
                </template>
            </template>

            <!-- <div class="ratings-container">
                <div class="ratings">
                    <div
                        class="ratings-val"
                        :style="{ width: product.ratings * 20 + '%' }"
                    ></div>
                    <span class="tooltip-text">{{
                        product.ratings.toFixed(0)
                    }}</span>
                </div>
                <span class="ratings-text"
                    >( {{ product.review }} Reviews )</span
                >
            </div> -->
        </div>
    </div>
</template>
<script>
import { mapGetters, mapActions } from 'vuex';
import { baseUrl } from '~/repositories/repository';
export default {
    props: {
        product: Object
    },
    data: function() {
        return {
            baseUrl: baseUrl,
            maxPrice: 0,
            minPrice: 99999
        };
    },
    computed: {
        ...mapGetters('cart', ['canAddToCart']),
        ...mapGetters('wishlist', ['isInWishlist']),
        ...mapGetters('compare', ['isInCompare'])
    },

    created: function() {
        let min = this.minPrice;
        let max = this.maxPrice;
        this.product.variants.map(item => {
            if (min > item.price) min = item.price;
            if (max < item.price) max = item.price;
        }, []);

        if (this.product.variants.length == 0) {
            min = this.product.sale_price
                ? this.product.sale_price
                : this.product.price;
            max = this.product.price;
        }

        this.minPrice = min;
        this.maxPrice = max;
    },
    methods: {
        ...mapActions('cart', ['addToCart']),
        ...mapActions('wishlist', ['addToWishlist']),
        ...mapActions('compare', ['addToCompare']),
        quickView: function() {
            this.$modal.show(
                () => import('~/components/elements/modals/QuickViewModal'),
                {
                    product: this.product
                },
                { width: '1030', height: 'auto', adaptive: true }
            );
        }
    }
};
</script>
