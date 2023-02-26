<template>
    <footer class="footer footer-light">
        <div class="footer-middle">
            <div :class="isFullwidth ? 'container-fluid' : 'container'">
                <div class="row">
                    <div class="col-sm-6 col-lg-4">
                        <div class="widget widget-about">
                            <nuxt-link to="/">
                                <img
                                    :src="getCompany.logo"
                                    class="bg-white mb-1"
                                    alt="Molla Logo"
                                    width="48"
                                />
                                <h3 class="widget-title">
                                    {{ getCompany.companyName }}
                                </h3>
                            </nuxt-link>
                            <p>
                                {{ getCompany.shortDescription }}
                            </p>

                            <div class="social-icons">
                                <a
                                    href="#"
                                    class="social-icon"
                                    target="_blank"
                                    title="Facebook"
                                >
                                    <i class="icon-facebook-f"></i>
                                </a>
                                <a
                                    href="#"
                                    class="social-icon"
                                    target="_blank"
                                    title="Twitter"
                                >
                                    <i class="icon-twitter"></i>
                                </a>
                                <!-- <a
                                    href="#"
                                    class="social-icon"
                                    target="_blank"
                                    title="Instagram"
                                >
                                    <i class="icon-instagram"></i>
                                </a>
                                <a
                                    href="#"
                                    class="social-icon"
                                    target="_blank"
                                    title="Youtube"
                                >
                                    <i class="icon-youtube"></i>
                                </a>
                                <a
                                    href="#"
                                    class="social-icon"
                                    target="_blank"
                                    title="Pinterest"
                                >
                                    <i class="icon-pinterest"></i>
                                </a> -->
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6 col-lg-4">
                        <div class="widget">
                            <h4 class="widget-title">Địa chỉ văn phòng</h4>

                            <ul class="widget-list">
                                <li>
                                    {{ getCompany.address }}
                                </li>
                            </ul>
                        </div>

                        <div class="widget">
                            <h4 class="widget-title">Thông tin liên hệ</h4>

                            <ul class="widget-list">
                                <li>HOTLINE 1: {{ getCompany.phone }}</li>
                            </ul>
                        </div>
                    </div>

                    <div class="col-sm-12 col-lg-1"></div>

                    <div class="col-sm-6 col-lg-3">
                        <div class="widget">
                            <h4 class="widget-title">Tiện ích nhanh</h4>

                            <ul class="widget-list">
                                <li>
                                    <nuxt-link :to="'/shop/cart'"
                                        >Xem giỏ hàng</nuxt-link
                                    >
                                </li>
                                <li>
                                    <nuxt-link :to="'/shop/wishlist'"
                                        >Sản phẩm yêu thích</nuxt-link
                                    >
                                </li>
                                <li>
                                    <nuxt-link to="/account"
                                        >Theo dõi đơn hàng</nuxt-link
                                    >
                                </li>
                                <li>
                                    <nuxt-link to="/blog/listing"
                                        >Xem bài viết</nuxt-link
                                    >
                                </li>
                                <li>
                                    <nuxt-link to="#"
                                        ><a @click="callPhone(getCompany.phone)"
                                            >Gọi điện tư vấn</a
                                        ></nuxt-link
                                    >
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="footer-bottom">
            <div
                :class="isFullwidth ? 'container-fluid' : 'container'"
                class="pb-2 pt-2"
            >
                <p class="footer-copyright">
                    Copyright © {{ new Date().getFullYear() }}
                    {{ getCompany.companyName }}. All Rights Reserved.
                </p>

                <figure class="footer-payments">
                    <!-- <img
                        src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRN9D8t5mPLaNnS45l-Z9cV3qDxM8TaMsAzpqxjRGS7jUjFtB_dHWmwDtFEFB-1bTshiik&usqp=CAU"
                        style="height:80px"
                    /> -->
                </figure>
            </div>
        </div>
        <div class="mb-10" v-if="bottomSticky"></div>
    </footer>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';

export default {
    data: function() {
        return {
            bottomSticky: false
        };
    },
    computed: {
        ...mapGetters('company', ['getCompany']),
        isFullwidth: function() {
            return this.$route.path.includes('fullwidth');
        }
    },
    watch: {
        $route: function() {
            this.handleBottomSticky();
        }
    },
    mounted: function() {
        this.handleBottomSticky();
        window.addEventListener('resize', this.handleBottomSticky, {
            passive: true
        });
    },
    destroyed: function() {
        window.removeEventListener('resize', this.handleBottomSticky);
    },
    methods: {
        handleBottomSticky: function() {
            this.bottomSticky =
                this.$route.path.includes('/product/default') &&
                window.innerWidth > 991;
        }
    }
};
</script>
