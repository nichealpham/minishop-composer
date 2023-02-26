<template>
    <div class="product">
        <figure class="product-media">
            <span class="product-label label-new" v-if="product.new">New</span>
            <span class="product-label label-sale" v-if="product.sale_price"
                >Sale</span
            >
            <span class="product-label label-top" v-if="product.top">Top</span>
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

            <div
                class="product-action action-icon-top"
                v-if="product.stock !== 0"
            >
                <nuxt-link
                    :to="'/product/default/' + product.slug"
                    class="btn-product btn-cart btn-select"
                    v-if="product.variants.length > 0"
                >
                    <span>select options</span>
                </nuxt-link>
                <button
                    class="btn-product btn-cart"
                    @click.prevent="addToCart({ product: product })"
                    v-else
                >
                    <span>thêm vào giỏ hàng</span>
                </button>
                <button
                    class="btn-product btn-quickview"
                    title="Quick view"
                    @click.prevent="quickView({ product: product })"
                >
                    <span>quick view</span>
                </button>
            </div>
        </figure>

        <div class="product-body product-action-inner">
            <nuxt-link
                :to="'/shop/wishlist'"
                class="btn-product btn-wishlist added-to-wishlist"
                v-if="isInWishlist(product)"
                key="inWishlist"
            >
                <span>Xem trong yêu thích</span>
            </nuxt-link>
            <a
                href="javascript:;"
                class="btn-product btn-wishlist"
                @click.prevent="addToWishlist({ product: product })"
                v-else
                key="notInWishlist"
            >
                <span>Thêm vào yêu thích</span>
            </a>
            <div class="product-cat">
                <span v-for="(cat, index) of product.category" :key="index">
                    <nuxt-link
                        :to="{
                            path: '/shop/sidebar/list',
                            query: { category: cat.slug }
                        }"
                        >{{ cat.name }}</nuxt-link
                    >
                    {{ index < product.category.length - 1 ? ',' : '' }}
                </span>
            </div>
            <h3 class="product-title">
                <nuxt-link :to="'/product/default/' + product.slug">{{
                    product.name
                }}</nuxt-link>
            </h3>

            <div class="product-price" v-if="product.stock == 0" key="outPrice">
                <span class="out-price">{{ formatPrice(product.price) }}</span>
            </div>

            <template v-else>
                <div class="product-price" v-if="minPrice == maxPrice">
                    {{ formatPrice(minPrice) }}
                </div>
                <template v-else>
                    <div
                        class="product-price"
                        v-if="product.variants.length == 0"
                    >
                        <span class="new-price">{{
                            formatPrice(minPrice)
                        }}</span>
                        <span class="old-price">{{
                            formatPrice(maxPrice)
                        }}</span>
                    </div>
                    <div class="product-price" v-else>
                        {{ formatPrice(minPrice) }}&ndash;{{
                            formatPrice(maxPrice)
                        }}
                    </div>
                </template>
            </template>

            <div class="ratings-container">
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
            </div>

            <div
                class="product-nav product-nav-dots"
                v-if="product.variants.length > 0"
            >
                <div class="row no-gutters">
                    <a
                        href="javascript:;"
                        :style="{ 'background-color': item.color }"
                        v-for="(item, index) in product.variants"
                        :key="index"
                    >
                        <span class="sr-only">Color name</span>
                    </a>
                </div>
            </div>
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
        ...mapGetters('wishlist', ['isInWishlist'])
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
