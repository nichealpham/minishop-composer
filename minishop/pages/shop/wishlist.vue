<template>
    <main class="main">
        <page-header title="Wishlist" subtitle="Shop"></page-header>
        <div class="page-content">
            <div
                class="container mt-2 container_padding"
                v-if="wishItems.length > 0"
                key="hasWishlist"
                style="background:white;border-radius:6px;"
            >
                <table class="table table-wishlist table-mobile">
                    <thead>
                        <tr>
                            <th>Sản phẩm</th>
                            <th>Giá tiền</th>
                            <th>Trạng thái</th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>

                    <tbody>
                        <tr v-for="(product, index) in wishItems" :key="index">
                            <td class="product-col">
                                <div class="product">
                                    <figure class="product-media">
                                        <nuxt-link
                                            :to="
                                                '/product/default/' +
                                                    product.slug
                                            "
                                            v-if="product.pictures[0]"
                                        >
                                            <img
                                                v-lazy="
                                                    `${product.pictures[0].url}`
                                                "
                                                alt="Product"
                                                :width="
                                                    product.sm_pictures[0].width
                                                "
                                                :height="
                                                    product.sm_pictures[0]
                                                        .height
                                                "
                                            />
                                        </nuxt-link>
                                    </figure>

                                    <h3 class="product-title">
                                        <nuxt-link
                                            :to="
                                                '/product/default/' +
                                                    product.slug
                                            "
                                            >{{ product.name }}</nuxt-link
                                        >
                                    </h3>
                                </div>
                            </td>
                            <td class="price-col">
                                <div
                                    class="product-price mb-0"
                                    v-if="product.stock == 0"
                                    key="outPrice"
                                >
                                    <span class="out-price">{{
                                        formatPrice(product.price)
                                    }}</span>
                                </div>

                                <template v-else>
                                    <div
                                        class="product-price d-inline-block mb-0"
                                        v-if="
                                            product.minPrice == product.maxPrice
                                        "
                                    >
                                        {{ formatPrice(product.minPrice) }}
                                    </div>
                                    <template v-else>
                                        <div
                                            class="product-price d-inline-block mb-0"
                                            v-if="product.variants.length == 0"
                                        >
                                            <span class="new-price">{{
                                                formatPrice(product.minPrice)
                                            }}</span>
                                            <span class="old-price">{{
                                                formatPrice(product.maxPrice)
                                            }}</span>
                                        </div>
                                        <div
                                            class="product-price d-inline-block mb-0"
                                            v-else
                                        >
                                            {{
                                                formatPrice(product.minPrice)
                                            }}&ndash;{{
                                                formatPrice(product.maxPrice)
                                            }}
                                        </div>
                                    </template>
                                </template>
                            </td>
                            <td class="stock-col">
                                <span
                                    :class="
                                        product.stock && product.stock == 0
                                            ? 'out-of-stock'
                                            : 'in-stock'
                                    "
                                    >{{
                                        product.stock && product.stock == 0
                                            ? 'Hết hàng'
                                            : 'Còn hàng'
                                    }}</span
                                >
                            </td>
                            <td class="action-col">
                                <div class="dropdown">
                                    <nuxt-link
                                        class="btn btn-block btn-outline-primary-2"
                                        :to="'/product/default/' + product.slug"
                                        v-if="product.variants.length > 0"
                                    >
                                        <i class="icon-list-alt"></i>Chọn thông
                                        số
                                    </nuxt-link>
                                    <button
                                        class="btn btn-block btn-outline-primary-2"
                                        @click.prevent="
                                            moveToCart({ product: product })
                                        "
                                        v-else
                                    >
                                        <i class="icon-cart-plus"></i>Thêm vào
                                        giỏ hàng
                                    </button>
                                </div>
                            </td>
                            <td class="remove-col">
                                <button
                                    class="btn-remove"
                                    @click.prevent="
                                        removeFromWishlist({ product: product })
                                    "
                                >
                                    <i class="icon-close"></i>
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <!-- <div class="wishlist-share">
                    <div class="social-icons social-icons-sm mb-2">
                        <label class="social-label">Chia sẻ:</label>
                        <a
                            href="#"
                            class="social-icon"
                            title="Facebook"
                            target="_blank"
                        >
                            <i class="icon-facebook-f"></i>
                        </a>
                        <a
                            href="#"
                            class="social-icon"
                            title="Twitter"
                            target="_blank"
                        >
                            <i class="icon-twitter"></i>
                        </a>
                        <a
                            href="#"
                            class="social-icon"
                            title="Instagram"
                            target="_blank"
                        >
                            <i class="icon-instagram"></i>
                        </a>
                        <a
                            href="#"
                            class="social-icon"
                            title="Youtube"
                            target="_blank"
                        >
                            <i class="icon-youtube"></i>
                        </a>
                        <a
                            href="#"
                            class="social-icon"
                            title="Pinterest"
                            target="_blank"
                        >
                            <i class="icon-pinterest"></i>
                        </a>
                    </div>
                </div> -->
            </div>

            <div class="container" v-else key="noWishlist">
                <div class="text-center">
                    <i class="icon-heart-o wishlist-empty d-block"></i>
                    <span class="d-block mt-2">Chưa có sản phẩm yêu thích</span>
                    <nuxt-link
                        :to="'/shop/sidebar/list'"
                        class="btn btn-primary mt-2"
                        >Bắt đầu mua hàng</nuxt-link
                    >
                </div>
            </div>
        </div>
    </main>
</template>
<script>
import { mapGetters, mapActions } from 'vuex';
import PageHeader from '~/components/elements/PageHeader';
import { baseUrl } from '~/repositories/repository.js';

export default {
    components: {
        PageHeader
    },
    data: function() {
        return {
            baseUrl: baseUrl
        };
    },
    computed: {
        ...mapGetters('wishlist', ['wishlistQty', 'wishlist']),
        wishItems: function() {
            return this.wishlist.reduce((acc, product) => {
                let max = 0;
                let min = 999999;
                product.variants.map(item => {
                    if (min > item.price) min = item.price;
                    if (max < item.price) max = item.price;
                }, []);

                if (product.variants.length == 0) {
                    min = product.sale_price
                        ? product.sale_price
                        : product.price;
                    max = product.price;
                }

                return [
                    ...acc,
                    {
                        ...product,
                        minPrice: min,
                        maxPrice: max
                    }
                ];
            }, []);
        }
    },
    methods: {
        ...mapActions('wishlist', ['removeFromWishlist', 'moveToCart'])
    }
};
</script>
