<template>
    <div
        class="product-details product-details-centered product-details-separator"
        v-if="product"
    >
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <h1 class="product-title">{{ product.name }}</h1>

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
                        <span class="ratings-text mt-0"
                            >( {{ product.review }} Reviews )</span
                        >
                    </div>

                    <div
                        class="product-price"
                        v-if="product.stock == 0"
                        key="outPrice"
                    >
                        <span class="out-price">{{
                            formatPrice(product.price)
                        }}</span>
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
                                {{ formatPrice(minPrice) }} -
                                {{ formatPrice(maxPrice) }}
                            </div>
                        </template>
                    </template>

                    <div class="product-content">
                        <p>{{ product.short_desc }}</p>
                    </div>

                    <template v-if="product.variants.length > 0">
                        <div class="details-filter-row details-row-size">
                            <label>Color:</label>

                            <div class="product-nav product-nav-dots">
                                <a
                                    href="#"
                                    :class="{
                                        active:
                                            item.color == selectedVariant.color,
                                        disabled: item.disabled
                                    }"
                                    :style="{ 'background-color': item.color }"
                                    v-for="(item, index) in colorArray"
                                    :key="index"
                                    @click.prevent="selectColor(item)"
                                ></a>
                            </div>
                        </div>

                        <div class="details-filter-row details-row-size">
                            <label for="size">Size:</label>
                            <div class="select-custom">
                                <select
                                    name="size"
                                    id="size"
                                    class="form-control"
                                    v-model="selectedVariant.size"
                                    @change="selectSize"
                                >
                                    <option value="null">Select a size</option>
                                    <option
                                        :value="item.size"
                                        v-for="(item, index) in sizeArray"
                                        :key="index"
                                        >{{ item.size }}</option
                                    >
                                </select>
                            </div>

                            <a href="javascript:;" class="size-guide mr-4">
                                <i class="icon-th-list"></i>size guide
                            </a>
                            <a
                                href="#"
                                @click.prevent="clearSelection"
                                v-if="showClear"
                                >clear</a
                            >
                        </div>
                        <vue-slide-toggle :open="showVariationPrice">
                            <div class="product-price">
                                {{ formatPrice(selectedVariant.price) }}
                            </div>
                        </vue-slide-toggle>
                    </template>
                </div>
                <div class="col-md-6">
                    <div class="product-details-action mb-1">
                        <div class="details-action-col">
                            <quantity-input
                                :product="product"
                                @change-qty="changeQty"
                                class="mr-3 mr-sm-4"
                            ></quantity-input>
                            <a
                                href="#"
                                class="btn-product btn-cart ml-sm-2"
                                :class="{
                                    'btn-disabled':
                                        !canAddToCart(product, qty) ||
                                        (product.variants.length > 0 &&
                                            !showVariationPrice)
                                }"
                                @click.prevent="addCart"
                            >
                                <span>thêm vào giỏ hàng</span>
                            </a>
                        </div>

                        <div class="details-action-wrapper">
                            <a
                                href="#"
                                class="btn-product btn-wishlist"
                                title="Wishlist"
                                @click.prevent="
                                    addToWishlist({ product: product })
                                "
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

                    <div class="product-details-footer details-footer-col">
                        <div class="product-cat">
                            <span>Category:</span>
                            <span
                                v-for="(cat, index) of product.category"
                                :key="index"
                            >
                                <nuxt-link
                                    :to="{
                                        path: '/shop/sidebar/list',
                                        query: { category: cat.slug }
                                    }"
                                    >{{ cat.name }}</nuxt-link
                                >
                                {{
                                    index < product.category.length - 1
                                        ? ','
                                        : ''
                                }}
                            </span>
                        </div>

                        <div class="social-icons social-icons-sm">
                            <span class="social-label">Share:</span>
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
                            <a
                                href="javascript:;"
                                class="social-icon"
                                title="Pinterest"
                                target="_blank"
                            >
                                <i class="icon-pinterest"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import { mapGetters, mapActions } from 'vuex';
import { VueSlideToggle } from 'vue-slide-toggle';
import QuantityInput from '~/components/elements/QuantityInput';
import { baseUrl } from '~/repositories/repository.js';

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
    data: function() {
        return {
            baseUrl: baseUrl,
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
            qty: 1
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
        clearSelection: function() {
            this.selectedVariant = {
                ...this.selectedVariant,
                color: null,
                colorName: null,
                size: null
            };
            this.refreshSelectableGroup();
        },
        addCart: function() {
            let newProduct = { ...this.product };
            if (this.product.variants.length > 0) {
                newProduct = {
                    ...this.product,
                    name:
                        this.product.name +
                        ' - ' +
                        this.selectedVariant.colorName +
                        ', ' +
                        this.selectedVariant.size,
                    price: this.selectedVariant.price
                };
            }
            this.addToCart({ product: newProduct, qty: this.qty });
        }
    }
};
</script>
