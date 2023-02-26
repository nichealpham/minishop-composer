<template>
    <main class="main">
        <page-header title="Checkout" subtitle="Order"></page-header>

        <div class="page-content">
            <div class="checkout">
                <div
                    class="container mt-2 container_padding"
                    style="background:white;border-radius:6px;"
                >
                    <div class="row">
                        <div class="col-lg-9" v-if="!isLogin">
                            <h5 class="mb-2 mt-3">
                                Bạn cần tạo tài khoản để đặt hàng
                            </h5>
                            <button
                                class="btn btn-outline-primary-2 btn-order mr-4"
                                @click="openCrmLoginUrl"
                            >
                                <span class="btn-text">Đăng nhập</span>
                                <span class="btn-hover-text">Đăng nhập</span>
                            </button>
                            <button
                                class="btn btn-outline-primary-2 btn-order"
                                @click="openCrmRegisterUrl"
                            >
                                <span class="btn-text">Tạo tài khoản</span>
                                <span class="btn-hover-text"
                                    >Tạo tài khoản</span
                                >
                            </button>
                        </div>

                        <div class="col-lg-9" v-else>
                            <h2 class="checkout-title">
                                Thông tin người mua:
                            </h2>

                            <label>Họ tên *</label>
                            <input
                                type="text"
                                class="form-control"
                                v-model="user.fullName"
                                disabled
                            />

                            <div class="row">
                                <div class="col-sm-6">
                                    <label>Số điện thoại *</label>
                                    <input
                                        type="tel"
                                        class="form-control"
                                        required
                                        v-model="user.phone"
                                        disabled
                                    />
                                </div>
                                <div class="col-sm-6">
                                    <label>Email</label>
                                    <input
                                        type="text"
                                        class="form-control"
                                        required
                                        v-model="user.email"
                                        disabled
                                    />
                                </div>
                            </div>

                            <h2 class="checkout-title">
                                Thông tin thanh toán:
                            </h2>

                            <label>Địa chỉ nhận hàng *</label>
                            <input
                                type="text"
                                class="form-control"
                                placeholder="Appartments, suite, unit etc ..."
                                v-model="user.shipAddress"
                                @change="updateUserProfile"
                                style="background-color:#fefefe;"
                            />

                            <label>Ghi chú đơn hàng</label>
                            <textarea
                                class="form-control"
                                cols="30"
                                rows="4"
                                placeholder="Notes about your order, e.g. special notes for delivery"
                                style="background-color:#fefefe;"
                                v-model="orderNote"
                            ></textarea>
                        </div>

                        <aside class="col-lg-3">
                            <div class="summary">
                                <h3 class="summary-title">
                                    Thông tin đơn hàng:
                                </h3>

                                <table class="table table-summary">
                                    <thead>
                                        <tr>
                                            <th>Sản phẩm</th>
                                            <th>Giá tiền</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        <tr
                                            v-for="(product, index) in cartList"
                                            :key="index"
                                        >
                                            <td>
                                                <nuxt-link
                                                    :to="
                                                        '/product/default/' +
                                                            product.slug
                                                    "
                                                    >{{
                                                        product.name
                                                    }}</nuxt-link
                                                >
                                            </td>
                                            <td>
                                                {{ formatPrice(product.sum) }}
                                            </td>
                                        </tr>

                                        <tr class="summary-subtotal">
                                            <td>Tạm tính:</td>
                                            <td>
                                                {{ formatPrice(priceTotal) }}
                                            </td>
                                        </tr>

                                        <tr class="summary-total">
                                            <td>Tổng cộng:</td>
                                            <td>
                                                {{ formatPrice(priceTotal) }}
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                                <button
                                    class="btn btn-outline-primary-2 btn-order btn-block"
                                    @click="createOrder"
                                    v-if="!loading"
                                >
                                    <span class="btn-text">Đặt hàng</span>
                                    <span class="btn-hover-text">Đặt hàng</span>
                                </button>
                                <div
                                    v-else
                                    style="width:100%;display:block;text-align:center;"
                                >
                                    <circle-spin />
                                </div>
                            </div>
                        </aside>
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>
<script>
import { mapGetters, mapMutations } from 'vuex';
import { VueSlideToggle } from 'vue-slide-toggle';
import PageHeader from '~/components/elements/PageHeader';
import { $httpClient } from '@/plugins/constants';
import { Circle } from 'vue-loading-spinner';
import { getLocalStorage } from '@/plugins/constants';

export default {
    components: {
        VueSlideToggle,
        PageHeader,
        CircleSpin: Circle
    },
    computed: {
        ...mapGetters('cart', ['cartList', 'priceTotal', 'qtyTotal']),
        ...mapGetters('auth', ['profile', 'token'])
    },
    data: function() {
        return {
            toggleState: [true, false, false, false, false],
            user: {},
            orderNote: '',
            loading: false,
            isLogin: false
        };
    },
    watch: {
        profile() {
            this.isLogin =
                getLocalStorage(process.env.NUXT_APP_TOKEN_NAME) || false;
            this.getUserProfile();
        }
    },
    created() {
        this.isLogin =
            getLocalStorage(process.env.NUXT_APP_TOKEN_NAME) || false;
        this.getUserProfile();
    },
    methods: {
        ...mapMutations('auth', ['SET_PROFILE']),
        ...mapMutations('cart', ['CLEAR_CART']),
        async createOrder() {
            if (!this.user.shipAddress) {
                this.$vToastify.error('Shipping Address cannot be empty!');
                return;
            }
            var body = {
                shippingAddress: this.user.shipAddress,
                orderNote: this.orderNote,
                orderItems: this.cartList.map(i => ({
                    productID: i.id,
                    amount: i.qty
                }))
            };
            this.loading = true;
            var result = await $httpClient.post(`/public/orders`, {}, body, {
                Authorization: this.token
            });
            if (result) {
                this.CLEAR_CART();
                await this.sleep(100);
                this.$router.push('/account');
            }
            this.loading = false;
        },
        getUserProfile() {
            var result = JSON.parse(JSON.stringify(this.profile));
            if (!result.shipAddress) result.shipAddress = result.address || '';
            this.user = result;
        },
        updateUserProfile() {
            this.SET_PROFILE(this.user);
        },
        updateCart: function() {
            this.updateCart(this.cartItems);
        },
        clickCoupon(event) {
            event.currentTarget.parentElement.querySelector('label').style =
                'opacity: 0';
        },
        changeToggle: function(index1) {
            this.toggleState = this.toggleState.reduce((acc, cur, index) => {
                if (index == index1) return [...acc, !cur];
                return [...acc, false];
            }, []);
        }
    }
};
</script>
