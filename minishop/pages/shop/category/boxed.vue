<template>
  <main class="main">
    <page-header title="Product Category Boxed"></page-header>

    <nav class="breadcrumb-nav breadcrumb-with-filter">
      <div class="container">
        <button class="sidebar-toggler p-0" @click="openSidebar">
          <i class="icon-bars"></i>Filters
        </button>
        <ol class="breadcrumb">
          <li class="breadcrumb-item">
            <nuxt-link to="/">Home</nuxt-link>
          </li>
          <li class="breadcrumb-item">
            <nuxt-link :to="'/shop/sidebar/list'">Shop</nuxt-link>
          </li>
          <li class="breadcrumb-item">Product Category</li>
          <li class="breadcrumb-item active">Boxed</li>
        </ol>
      </div>
    </nav>

    <div class="page-content">
      <div class="categories-page">
        <div class="container">
          <div class="row">
            <div class="col-md-6">
              <div class="banner banner-cat banner-badge">
                <nuxt-link :to="'/shop/sidebar/list'">
                  <img
                    v-lazy="'./images/category/boxed/banner-1.jpg'"
                    alt="Banner"
                  />
                </nuxt-link>

                <nuxt-link class="banner-link" :to="'/shop/sidebar/3cols'">
                  <h3 class="banner-title">Dresses</h3>

                  <h4 class="banner-subtitle">3 Products</h4>

                  <span class="banner-link-text">Shop Now</span>
                </nuxt-link>
              </div>

              <div class="banner banner-cat banner-badge">
                <nuxt-link :to="'/shop/sidebar/list'">
                  <img
                    v-lazy="'./images/category/boxed/banner-2.jpg'"
                    alt="Banner"
                  />
                </nuxt-link>

                <nuxt-link class="banner-link" :to="'/shop/sidebar/3cols'">
                  <h3 class="banner-title">Jackets</h3>

                  <h4 class="banner-subtitle">2 Products</h4>

                  <span class="banner-link-text">Shop Now</span>
                </nuxt-link>
              </div>
            </div>

            <div class="col-md-6">
              <div class="row">
                <div class="col-sm-6">
                  <div class="banner banner-cat banner-badge">
                    <nuxt-link :to="'shop/sidebar/list'">
                      <img
                        v-lazy="'./images/category/boxed/banner-3.jpg'"
                        alt="Banner"
                      />
                    </nuxt-link>

                    <nuxt-link class="banner-link" :to="'shop/sidebar/3cols'">
                      <h3 class="banner-title">T-shirts</h3>

                      <h4 class="banner-subtitle">0 Products</h4>

                      <span class="banner-link-text">Shop Now</span>
                    </nuxt-link>
                  </div>
                </div>

                <div class="col-sm-6">
                  <div class="banner banner-cat banner-badge">
                    <nuxt-link :to="'shop/sidebar/list'">
                      <img
                        v-lazy="'./images/category/boxed/banner-4.jpg'"
                        alt="Banner"
                      />
                    </nuxt-link>

                    <nuxt-link class="banner-link" :to="'shop/sidebar/3cols'">
                      <h3 class="banner-title">Jeans</h3>

                      <h4 class="banner-subtitle">1 Products</h4>

                      <span class="banner-link-text">Shop Now</span>
                    </nuxt-link>
                  </div>
                </div>
              </div>

              <div class="banner banner-cat banner-badge">
                <nuxt-link :to="'/shop/sidebar/list'">
                  <img
                    v-lazy="'./images/category/boxed/banner-5.jpg'"
                    alt="Banner"
                  />
                </nuxt-link>

                <nuxt-link class="banner-link" :to="'/shop/sidebar/3cols'">
                  <h3 class="banner-title">Bags</h3>

                  <h4 class="banner-subtitle">4 Products</h4>

                  <span class="banner-link-text">Shop Now</span>
                </nuxt-link>
              </div>
            </div>

            <div class="col-sm-6 col-md-3">
              <div class="banner banner-cat banner-badge">
                <nuxt-link :to="'/shop/sidebar/list'">
                  <img
                    v-lazy="'./images/category/boxed/banner-6.jpg'"
                    alt="Banner"
                  />
                </nuxt-link>

                <nuxt-link class="banner-link" :to="'/shop/sidebar/3cols'">
                  <h3 class="banner-title">Sportwear</h3>

                  <h4 class="banner-subtitle">0 Products</h4>

                  <span class="banner-link-text">Shop Now</span>
                </nuxt-link>
              </div>
            </div>

            <div class="col-sm-6 col-md-3 order-md-last">
              <div class="banner banner-cat banner-badge">
                <nuxt-link :to="'/shop/sidebar/list'">
                  <img
                    v-lazy="'./images/category/boxed/banner-8.jpg'"
                    alt="Banner"
                  />
                </nuxt-link>

                <nuxt-link class="banner-link" :to="'/shop/sidebar/3cols'">
                  <h3 class="banner-title">Jumpers</h3>

                  <h4 class="banner-subtitle">1 Products</h4>

                  <span class="banner-link-text">Shop Now</span>
                </nuxt-link>
              </div>
            </div>

            <div class="col-md-6">
              <div class="banner banner-cat banner-badge">
                <nuxt-link :to="'/shop/sidebar/list'">
                  <img
                    v-lazy="'./images/category/boxed/banner-7.jpg'"
                    alt="Banner"
                  />
                </nuxt-link>

                <nuxt-link class="banner-link" :to="'/shop/sidebar/3cols'">
                  <h3 class="banner-title">Shoes</h3>

                  <h4 class="banner-subtitle">2 Products</h4>

                  <span class="banner-link-text">Shop Now</span>
                </nuxt-link>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="sidebar-filter-overlay" @click="closeSidebar"></div>

      <shop-sidebar-two></shop-sidebar-two>
    </div>
  </main>
</template>

<script>
import PageHeader from "~/components/elements/PageHeader";
import ShopSidebarTwo from "~/components/partial/shop/sidebar/ShopSidebarTwo";

export default {
  components: {
    PageHeader,
    ShopSidebarTwo,
  },
  methods: {
    openSidebar: function () {
      document.querySelector("body").classList.add("sidebar-filter-active");
    },
    closeSidebar: function () {
      document.querySelector("body").classList.remove("sidebar-filter-active");
    },
  },
};
</script>
