<template>
    <main class="main">
        <page-header title="Shopping Cart" subtitle="Shop"></page-header>

        <div class="page-content">
            <div class="cart">
                <div
                    class="container mt-2 container_padding"
                    style="background:white;border-radius:6px;"
                >
                    <div class="row" v-if="cartList.length > 0" key="hasCart">
                        <div class="col-lg-9">
                            <table class="table table-cart table-mobile">
                                <thead>
                                    <tr>
                                        <th>Sản phẩm</th>
                                        <th>Giá tiền</th>
                                        <th>Số lượng</th>
                                        <th>Tổng cộng</th>
                                        <th></th>
                                    </tr>
                                </thead>

                                <tbody>
                                    <tr
                                        v-for="(product, index) in cartItems"
                                        :key="index"
                                    >
                                        <td class="product-col">
                                            <div class="product">
                                                <figure class="product-media">
                                                    <nuxt-link
                                                        :to="
                                                            '/product/default/' +
                                                                product.slug
                                                        "
                                                        v-if="
                                                            product.pictures[0]
                                                        "
                                                    >
                                                        <img
                                                            v-lazy="
                                                                `${product.pictures[0].url}`
                                                            "
                                                            alt="Product"
                                                            :width="
                                                                product
                                                                    .sm_pictures[0]
                                                                    .width
                                                            "
                                                            :height="
                                                                product
                                                                    .sm_pictures[0]
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
                                                        >{{
                                                            product.name
                                                        }}</nuxt-link
                                                    >
                                                </h3>
                                            </div>
                                        </td>
                                        <td class="price-col pr-2">
                                            {{ formatPrice(product.price) }}
                                        </td>
                                        <td class="quantity-col">
                                            <quantity-input
                                                :product="product"
                                                @change-qty="changeQty"
                                                class="cart-product-quantity"
                                            ></quantity-input>
                                        </td>
                                        <td class="total-col pr-2">
                                            {{ formatPrice(product.sum) }}
                                        </td>
                                        <td class="remove-col">
                                            <button
                                                @click.prevent="
                                                    removeFromCart({
                                                        product: product
                                                    })
                                                "
                                                class="btn-remove"
                                            >
                                                <i class="icon-close"></i>
                                            </button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                            <div class="cart-bottom">
                                <a
                                    href="/shop/sidebar/list"
                                    class="btn btn-outline-dark-2"
                                >
                                    <!-- <span>Cập nhật số lượng</span> -->
                                    <span>TIẾP TỤC MUA HÀNG</span>
                                    <i class="icon-refresh"></i>
                                </a>
                                <!-- <nuxt-link
                                :to="'/shop/sidebar/list'"
                                class="btn btn-outline-dark-2 btn-block mb-3"
                            >
                                <span>TIẾP TỤC MUA HÀNG</span>
                                <i class="icon-refresh"></i>
                            </nuxt-link> -->
                            </div>
                        </div>

                        <aside class="col-lg-3">
                            <div class="summary summary-cart">
                                <h3 class="summary-title">Giá trị đơn hàng</h3>

                                <table class="table table-summary">
                                    <tbody>
                                        <tr class="summary-subtotal">
                                            <td>Tổng cộng:</td>
                                            <td>
                                                {{ formatPrice(priceTotal) }}
                                            </td>
                                        </tr>

                                        <tr class="summary-shipping">
                                            <td>Phí vận chuyển:</td>
                                            <td>&nbsp;</td>
                                        </tr>

                                        <tr class="summary-shipping-row">
                                            <td colspan="2">
                                                <div
                                                    class="custom-control custom-radio"
                                                >
                                                    <input
                                                        type="radio"
                                                        id="free-shipping"
                                                        name="shipping"
                                                        v-model="shipping"
                                                        :value="0"
                                                        class="custom-control-input"
                                                    />
                                                    <label
                                                        class="custom-control-label"
                                                        for="free-shipping"
                                                        >COD - Thanh toán khi
                                                        nhận hàng</label
                                                    >
                                                </div>
                                            </td>
                                            <!-- <td>~15,000 VND</td> -->
                                        </tr>

                                        <tr class="summary-total">
                                            <td>Tổng cộng:</td>
                                            <td>
                                                {{
                                                    formatPrice(
                                                        priceTotal + shipping
                                                    )
                                                }}
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                                <nuxt-link
                                    :to="'/shop/checkout'"
                                    class="btn btn-outline-primary-2 btn-order btn-block"
                                    >TIẾN HÀNH ĐẶT HÀNG</nuxt-link
                                >
                            </div>

                            <!-- <nuxt-link
                                :to="'/shop/sidebar/list'"
                                class="btn btn-outline-dark-2 btn-block mb-3"
                            >
                                <span>TIẾP TỤC MUA HÀNG</span>
                                <i class="icon-refresh"></i>
                            </nuxt-link> -->
                        </aside>
                    </div>

                    <div class="row" v-else key="noCart">
                        <div class="col-12">
                            <div class="cart-empty-page text-center">
                                <i class="cart-empty icon-shopping-cart"></i>
                                <p class="px-3 py-2 cart-empty mb-3">
                                    Chưa có sản phẩm trong giỏ hàng
                                </p>
                                <p class="return-to-shop mb-0">
                                    <nuxt-link
                                        :to="'/shop/sidebar/list'"
                                        class="btn btn-primary"
                                        >Tiếp tục mua hàng</nuxt-link
                                    >
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>
<script>
import { mapGetters, mapActions, mapMutations } from 'vuex';
import PageHeader from '~/components/elements/PageHeader';
import QuantityInput from '~/components/elements/QuantityInput';
import { baseUrl } from '~/repositories/repository';

export default {
    components: {
        PageHeader,
        QuantityInput
    },
    data: function() {
        return {
            cartItems: [],
            baseUrl: baseUrl,
            shipping: 0
        };
    },
    computed: {
        ...mapGetters('cart', ['cartList', 'priceTotal', 'qtyTotal'])
    },
    watch: {
        cartList: function() {
            this.cartItems = [...this.cartList];
        }
    },
    created: function() {
        this.cartItems = [...this.cartList];
    },
    methods: {
        ...mapActions('cart', ['removeFromCart']),
        ...mapActions('cart', ['updateCart']),
        changeQty: function(value, product) {
            this.cartItems = this.cartItems.reduce((acc, cur) => {
                if (cur.name == product.name)
                    return [
                        ...acc,
                        {
                            ...cur,
                            qty: value,
                            sum:
                                (product.sale_price
                                    ? product.sale_price
                                    : product.price) * value
                        }
                    ];
                return [...acc, cur];
            }, []);
        }
    }
};
</script>
