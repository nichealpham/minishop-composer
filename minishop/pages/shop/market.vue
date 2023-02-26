<template>
  <main class="main shop-market">
    <page-header title="Shop Market" subtitle="Shop"></page-header>
    <nav aria-label="breadcrumb" class="breadcrumb-nav mb-3">
      <div class="container">
        <ol class="breadcrumb">
          <li class="breadcrumb-item">
            <nuxt-link to="/">Home</nuxt-link>
          </li>
          <li class="breadcrumb-item">
            <nuxt-link :to="'/shop/sidebar/list'">Shop</nuxt-link>
          </li>
          <li class="breadcrumb-item active">Market</li>
        </ol>
      </div>
    </nav>

    <div class="page-content">
      <div class="container">
        <div class="row">
          <div
            class="col-lg-9 col-xl-4-5col skeleton-body skel-shop-products"
            :class="{ loaded: loaded }"
          >
            <div class="swiper-carousel swiper-1">
              <div v-swiper:swiper1="carouselSetting1">
                <div class="swiper-wrapper">
                  <div class="swiper-slide">
                    <div class="banner banner-poster">
                      <a href="#">
                        <img
                          src="~/static/images/market/slider/slider-1.jpg"
                          alt="Banner"
                        />
                      </a>

                      <div class="banner-content banner-content-right">
                        <h3 class="banner-subtitle">
                          <a href="#">Amazing Value</a>
                        </h3>

                        <h2 class="banner-title">
                          <a href="#">High Performance 4K TVs</a>
                        </h2>

                        <a href="#" class="banner-link">
                          Shop Now
                          <i class="icon-long-arrow-right"></i>
                        </a>
                      </div>
                    </div>
                  </div>
                  <div class="swiper-slide">
                    <div class="banner banner-poster">
                      <a href="#">
                        <img
                          src="~/static/images/market/slider/slider-2.jpg"
                          alt="Banner"
                        />
                      </a>

                      <div class="banner-content">
                        <h3 class="banner-subtitle">
                          <a href="#">Weekend Deal</a>
                        </h3>

                        <h2 class="banner-title">
                          <a href="#">Apple & Accessories</a>
                        </h2>

                        <a href="#" class="banner-link">
                          Shop Now
                          <i class="icon-long-arrow-right"></i>
                        </a>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="swiper-nav nav-inside">
                <div class="swiper-prev">
                  <i class="icon-angle-left"></i>
                </div>
                <div class="swiper-next">
                  <i class="icon-angle-right"></i>
                </div>
              </div>
            </div>
            <div class="swiper-carousel pt-2 pb-2 brand-carousel swiper-2">
              <div v-swiper:swiper2="carouselSetting2">
                <div class="swiper-wrapper">
                  <div class="swiper-slide">
                    <a href="#" class="brand">
                      <img
                        v-lazy="'./images/brands/1.png'"
                        class="bg-white"
                        alt="Brand"
                      />
                    </a>
                  </div>
                  <div class="swiper-slide">
                    <a href="#" class="brand">
                      <img
                        v-lazy="'./images/brands/2.png'"
                        class="bg-white"
                        alt="Brand"
                      />
                    </a>
                  </div>
                  <div class="swiper-slide">
                    <a href="#" class="brand">
                      <img
                        v-lazy="'./images/brands/3.png'"
                        class="bg-white"
                        alt="Brand"
                      />
                    </a>
                  </div>
                  <div class="swiper-slide">
                    <a href="#" class="brand">
                      <img
                        v-lazy="'./images/brands/4.png'"
                        class="bg-white"
                        alt="Brand"
                      />
                    </a>
                  </div>
                  <div class="swiper-slide">
                    <a href="#" class="brand">
                      <img
                        v-lazy="'./images/brands/5.png'"
                        class="bg-white"
                        alt="Brand"
                      />
                    </a>
                  </div>
                  <div class="swiper-slide">
                    <a href="#" class="brand">
                      <img
                        v-lazy="'./images/brands/6.png'"
                        class="bg-white"
                        alt="Brand"
                      />
                    </a>
                  </div>
                  <div class="swiper-slide">
                    <a href="#" class="brand">
                      <img
                        v-lazy="'./images/brands/1.png'"
                        class="bg-white"
                        alt="Brand"
                      />
                    </a>
                  </div>
                </div>
              </div>
              <div class="swiper-nav">
                <div class="swiper-prev">
                  <i class="icon-angle-left"></i>
                </div>
                <div class="swiper-next">
                  <i class="icon-angle-right"></i>
                </div>
              </div>
            </div>
            <div class="cat-blocks-container">
              <div class="row">
                <div class="col-6 col-md-4 col-lg-3">
                  <a href="javascript:;" class="cat-block">
                    <figure>
                      <span>
                        <img
                          v-lazy="'./images/market/cats/1.jpg'"
                          alt="Category image"
                        />
                      </span>
                    </figure>

                    <h3 class="cat-block-title">Desktop Computers</h3>
                  </a>
                </div>

                <div class="col-6 col-md-4 col-lg-3">
                  <a href="javascript:;" class="cat-block">
                    <figure>
                      <span>
                        <img
                          v-lazy="'./images/market/cats/2.jpg'"
                          alt="Category image"
                        />
                      </span>
                    </figure>

                    <h3 class="cat-block-title">Monitors</h3>
                  </a>
                </div>

                <div class="col-6 col-md-4 col-lg-3">
                  <a href="javascript:;" class="cat-block">
                    <figure>
                      <span>
                        <img
                          v-lazy="'./images/market/cats/3.jpg'"
                          alt="Category image"
                        />
                      </span>
                    </figure>

                    <h3 class="cat-block-title">Laptops</h3>
                  </a>
                </div>

                <div class="col-6 col-md-4 col-lg-3">
                  <a href="javascript:;" class="cat-block">
                    <figure>
                      <span>
                        <img
                          v-lazy="'./images/market/cats/4.jpg'"
                          alt="Category image"
                        />
                      </span>
                    </figure>

                    <h3 class="cat-block-title">iPads & Tablets</h3>
                  </a>
                </div>

                <div class="col-6 col-md-4 col-lg-3">
                  <a href="javascript:;" class="cat-block">
                    <figure>
                      <span>
                        <img
                          v-lazy="'./images/market/cats/5.jpg'"
                          alt="Category image"
                        />
                      </span>
                    </figure>

                    <h3 class="cat-block-title">Hard Drives & Storage</h3>
                  </a>
                </div>

                <div class="col-6 col-md-4 col-lg-3">
                  <a href="javascript:;" class="cat-block">
                    <figure>
                      <span>
                        <img
                          v-lazy="'./images/market/cats/6.jpg'"
                          alt="Category image"
                        />
                      </span>
                    </figure>

                    <h3 class="cat-block-title">Printers & Supplies</h3>
                  </a>
                </div>

                <div class="col-6 col-md-4 col-lg-3">
                  <a href="javascript:;" class="cat-block">
                    <figure>
                      <span>
                        <img
                          v-lazy="'./images/market/cats/7.jpg'"
                          alt="Category image"
                        />
                      </span>
                    </figure>

                    <h3 class="cat-block-title">Computer Accessories</h3>
                  </a>
                </div>
              </div>
            </div>
            <div class="mb-2"></div>
            <div class="toolbox">
              <div class="toolbox-left">
                <div class="toolbox-info">
                  Showing
                  <span>{{ products.length }} of {{ totalCount }}</span>
                  Products
                </div>
              </div>

              <div class="toolbox-right">
                <div class="toolbox-sort">
                  <label for="sortby">Sort by:</label>
                  <div class="select-custom">
                    <select
                      name="sortby"
                      id="sortby"
                      class="form-control"
                      @change.prevent="getProducts"
                      v-model="orderBy"
                    >
                      <option value="default">Default</option>
                      <option value="featured">Most Popular</option>
                      <option value="rating">Most Rated</option>
                      <option value="new">Date</option>
                    </select>
                  </div>
                </div>
              </div>
            </div>

            <shop-list-two
              :products="products"
              :loaded="loaded"
            ></shop-list-two>

            <pagination :per-page="8" :total="totalCount"></pagination>
          </div>

          <aside class="col-lg-3 col-xl-5col order-lg-first">
            <button
              class="sidebar-fixed-toggler"
              @click="toggleSidebar"
              v-if="isSidebar"
            >
              <i class="icon-cog"></i>
            </button>

            <div class="sidebar-filter-overlay" @click="hideSidebar"></div>
            <shop-sidebar-three :is-sidebar="isSidebar"></shop-sidebar-three>
          </aside>
        </div>
      </div>
    </div>
  </main>
</template>
<script>
import PageHeader from "~/components/elements/PageHeader";
import ShopListTwo from "~/components/partial/shop/list/ShopListTwo";
import ShopSidebarThree from "~/components/partial/shop/sidebar/ShopSidebarThree";
import Pagination from "~/components/elements/Pagination";
import Repository, { baseUrl } from "~/repositories/repository.js";
import {
  carouselSettingSingle,
  carouselSettingDefault,
} from "~/utilities/carousel";

export default {
  components: {
    PageHeader,
    ShopListTwo,
    ShopSidebarThree,
    Pagination,
  },
  data: function () {
    return {
      products: [],
      type: "list",
      totalCount: 0,
      orderBy: "default",
      isSidebar: true,
      loaded: false,
      carouselSetting1: {
        ...carouselSettingSingle,
        navigation: {
          nextEl: ".swiper-1 .swiper-next",
          prevEl: ".swiper-1 .swiper-prev",
        },
      },
      carouselSetting2: {
        ...carouselSettingDefault,
        slidesPerView: 6,
        navigation: {
          nextEl: ".swiper-2 .swiper-next",
          prevEl: ".swiper-2 .swiper-prev",
        },
        breakpoints: {
          992: {
            slidesPerView: 4,
          },
          768: {
            slidesPerView: 3,
          },
          480: {
            slidesPerView: 2,
          },
        },
      },
    };
  },
  watch: {
    $route: function () {
      this.getProducts();
    },
  },
  created: function () {
    this.getProducts();
  },
  mounted: function () {
    this.resizeHandler();
    window.addEventListener("resize", this.resizeHandler, {
      passive: true,
    });
  },
  destroyed: function () {
    window.removeEventListener("resize", this.resizeHandler);
  },
  methods: {
    getProducts: async function () {
      this.loaded = false;
      await Repository.get(`${baseUrl}/shop`, {
        params: {
          ...this.$route.query,
          orderBy: this.orderBy,
          perPage: 8,
        },
      })
        .then((response) => {
          this.products = response.data.products;
          this.totalCount = response.data.totalCount;
          this.loaded = true;
        })
        .catch((error) => ({ error: JSON.stringify(error) }));
    },
    toggleSidebar: function () {
      if (
        document
          .querySelector("body")
          .classList.contains("sidebar-filter-active")
      ) {
        document
          .querySelector("body")
          .classList.remove("sidebar-filter-active");
      } else {
        document.querySelector("body").classList.add("sidebar-filter-active");
      }
    },

    hideSidebar: function () {
      document.querySelector("body").classList.remove("sidebar-filter-active");
    },
    resizeHandler: function () {
      if (window.innerWidth > 991) this.isSidebar = false;
      else this.isSidebar = true;
    },
  },
};
</script>
