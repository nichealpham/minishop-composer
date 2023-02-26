<template>
    <div class="product-details" v-if="product">
        <h1 class="product-title">{{ product.name }}</h1>

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
            <span class="ratings-text mt-0"
                >( {{ product.review }} Reviews )</span
            >
        </div> -->

        <div class="product-price" v-if="product.stock == 0" key="outPrice">
            <span class="out-price">{{ productPrice || 'Miễn phí' }}</span>
        </div>

        <template v-else>
            <div class="product-price" v-if="minPrice == maxPrice">
                {{ productPrice || 'Miễn phí' }}
            </div>
            <template v-else>
                <div class="product-price" v-if="product.variants.length == 0">
                    <span class="new-price">{{ formatPrice(minPrice) }}</span>
                    <span class="old-price">{{ formatPrice(maxPrice) }}</span>
                </div>
                <div class="product-price" v-else>
                    {{ formatPrice(minPrice) }} -
                    {{ formatPrice(maxPrice) }}
                </div>
            </template>
        </template>

        <div class="product-content">
            <p>{{ product.short_desc }}</p>
        </div>

        <div class="details-filter-row details-row-size">
            <label for="qty">Qty:</label>
            <quantity-input
                :product="product"
                @change-qty="changeQty"
            ></quantity-input>
        </div>

        <div class="product-details-action">
            <a
                href="javascript:;"
                class="btn-product btn-cart"
                @click.prevent="addCart(0)"
            >
                <span>thêm vào giỏ hàng</span>
            </a>

            <div class="details-action-wrapper">
                <a
                    href="#"
                    class="btn-product btn-wishlist"
                    title="Wishlist"
                    @click.prevent="addToWishlist({ product: product })"
                    v-if="!isInWishlist(product)"
                    key="notInWishlist"
                >
                    <span>Thêm vào yêu thích</span>
                </a>
                <nuxt-link
                    :to="'/shop/wishlist'"
                    class="btn-product btn-wishlist added-to-wishlist"
                    title="Wishlist"
                    v-else
                    key="inWishlist"
                >
                    <span>Xem trong yêu thích</span>
                </nuxt-link>
            </div>
        </div>

        <div class="product-details-footer">
            <div class="product-cat w-100 text-truncate">
                <span>Phân loại:</span>
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

            <div class="social-icons social-icons-sm">
                <span class="social-label">Chia sẻ:</span>
                <a
                    href="javascript:;"
                    class="social-icon"
                    title="Facebook"
                    target="_blank"
                >
                    <i class="icon-facebook-f"></i>
                </a>
                <a
                    href="javascript:;"
                    class="social-icon"
                    title="Twitter"
                    target="_blank"
                >
                    <i class="icon-twitter"></i>
                </a>
                <a
                    href="javascript:;"
                    class="social-icon"
                    title="Instagram"
                    target="_blank"
                >
                    <i class="icon-instagram"></i>
                </a>
                <!-- <a
                    href="javascript:;"
                    class="social-icon"
                    title="Pinterest"
                    target="_blank"
                >
                    <i class="icon-pinterest"></i>
                </a> -->
            </div>
        </div>
    </div>
</template>
<script>
import { mapGetters, mapActions } from 'vuex';
import { VueSlideToggle } from 'vue-slide-toggle';
import QuantityInput from '~/components/elements/QuantityInput';

export default {
    components: {
        VueSlideToggle,
        QuantityInput
    },
    props: {
        product: {
            type: Object
        }
    },
    watch: {
        product: {
            deep: true,
            handler: function() {
                this.productPrice = this.formatPrice(this.product.price);
                this.$forceUpdate();
            }
        }
    },
    data: function() {
        return {
            variationGroup: [],
            selectableGroup: [],
            sizeArray: [],
            colorArray: [],
            selectedVariant: {
                color: null,
                colorName: null,
                price: null,
                size: null
            },
            maxPrice: 0,
            minPrice: 99999,
            qty: 1,
            qty2: 1,
            productPrice: 0
        };
    },
    computed: {
        ...mapGetters('cart', ['canAddToCart']),
        ...mapGetters('wishlist', ['isInWishlist']),
        ...mapGetters('compare', ['isInCompare']),
        showClear: function() {
            return this.selectedVariant.color || this.selectedVariant.size
                ? true
                : false;
        },
        showVariationPrice: function() {
            return this.selectedVariant.color && this.selectedVariant.size
                ? true
                : false;
        },
        isCartSticy: function() {
            return this.$route.path.includes('/product/default');
        }
    },
    created: function() {
        let min = this.minPrice;
        let max = this.maxPrice;
        this.variationGroup = this.product.variants.reduce((acc, cur) => {
            cur.size.map(item => {
                acc.push({
                    color: cur.color,
                    colorName: cur.color_name,
                    size: item.name,
                    price: cur.price
                });
            });
            if (min > cur.price) min = cur.price;
            if (max < cur.price) max = cur.price;
            return acc;
        }, []);

        if (this.product.variants.length == 0) {
            min = this.product.sale_price
                ? this.product.sale_price
                : this.product.price;
            max = this.product.price;
        }

        this.minPrice = min;
        this.maxPrice = max;

        this.refreshSelectableGroup();
    },
    updated() {},
    methods: {
        ...mapActions('cart', ['addToCart']),
        ...mapActions('wishlist', ['addToWishlist']),
        ...mapActions('compare', ['addToCompare']),
        refreshSelectableGroup: function() {
            let tempArray = [...this.variationGroup];
            if (this.selectedVariant.color) {
                tempArray = this.variationGroup.reduce((acc, cur) => {
                    if (this.selectedVariant.color !== cur.color) {
                        return acc;
                    }
                    return [...acc, cur];
                }, []);
            }

            this.sizeArray = tempArray.reduce((acc, cur) => {
                if (acc.findIndex(item => item.size == cur.size) !== -1)
                    return acc;
                return [...acc, cur];
            }, []);

            tempArray = [...this.variationGroup];
            if (this.selectedVariant.size) {
                tempArray = this.variationGroup.reduce((acc, cur) => {
                    if (this.selectedVariant.size !== cur.size) {
                        return acc;
                    }
                    return [...acc, cur];
                }, []);
            }

            this.colorArray = this.product.variants.reduce((acc, cur) => {
                if (
                    tempArray.findIndex(item => item.color == cur.color) == -1
                ) {
                    return [
                        ...acc,
                        {
                            color: cur.color,
                            colorName: cur.color_name,
                            price: cur.price,
                            disabled: true
                        }
                    ];
                }
                return [
                    ...acc,
                    {
                        color: cur.color,
                        colorName: cur.color_name,
                        price: cur.price,
                        disabled: false
                    }
                ];
            }, []);
        },
        selectColor: function(item) {
            if (item.color == this.selectedVariant.color) {
                this.selectedVariant = {
                    ...this.selectedVariant,
                    color: null,
                    colorName: null,
                    price: item.price
                };
            } else {
                this.selectedVariant = {
                    ...this.selectedVariant,
                    color: item.color,
                    colorName: item.colorName,
                    price: item.price
                };
            }
            this.refreshSelectableGroup();
        },
        selectSize: function() {
            if (this.selectedVariant.size == 'null') {
                this.selectedVariant = { ...this.selectedVariant, size: null };
            }
            this.refreshSelectableGroup();
        },
        changeQty: function(current) {
            this.qty = current;
        },
        changeQty2: function(current) {
            this.qty2 = current;
        },
        clearSelection: function() {
            this.selectedVariant = {
                ...this.selectedVariant,
                color: null,
                colorName: null,
                size: null
            };
            this.refreshSelectableGroup();
        },
        addCart: function(index = 0) {
            let newProduct = { ...this.product };
            this.addToCart({
                product: newProduct,
                qty: index == 0 ? this.qty : this.qty2
            });
        }
    }
};
</script>
