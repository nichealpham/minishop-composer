<template>
  <main class="main element-category-page">
    <page-header title="Product Category" subtitle="Elements"></page-header>

    <div class="page-content">
      <div class="container">
        <h2 class="title text-center mb-3">2 Columns</h2>

        <div class="row">
          <div class="col-md-6">
            <div class="banner banner-cat">
              <a href="javascript:;">
                <img v-lazy="'./images/category/banner-1.jpg'" alt="Banner" />
              </a>

              <div class="banner-content">
                <h3 class="banner-title">Women</h3>

                <h4 class="banner-subtitle">18 Products</h4>

                <nuxt-link :to="'/shop/sidebar/list'" class="banner-link"
                  >Shop Now</nuxt-link
                >
              </div>
            </div>
          </div>

          <div class="col-md-6">
            <div class="banner banner-cat">
              <a href="javascript:;">
                <img v-lazy="'./images/category/banner-2.jpg'" alt="Banner" />
              </a>

              <div class="banner-content">
                <h3 class="banner-title">Men</h3>

                <h4 class="banner-subtitle">12 Products</h4>

                <nuxt-link :to="'/shop/sidebar/list'" class="banner-link"
                  >Shop Now</nuxt-link
                >
              </div>
            </div>
          </div>
        </div>

        <hr class="mb-4" />

        <h2 class="title text-center mb-3">3 Columns Badge Style</h2>

        <div class="row justify-content-center">
          <div class="col-md-6 col-lg-4">
            <div class="banner banner-cat">
              <a href="javascript:;">
                <img
                  v-lazy="'./images/category/3cols/banner-1.jpg'"
                  alt="Banner"
                />
              </a>

              <div class="banner-content banner-content-overlay text-center">
                <h3 class="banner-title">Women</h3>

                <h4 class="banner-subtitle">18 Products</h4>

                <nuxt-link :to="'/shop/sidebar/list'" class="banner-link"
                  >Shop Now</nuxt-link
                >
              </div>
            </div>
          </div>

          <div class="col-md-6 col-lg-4">
            <div class="banner banner-cat">
              <a href="javascript:;">
                <img
                  v-lazy="'./images/category/3cols/banner-2.jpg'"
                  alt="Banner"
                />
              </a>

              <div class="banner-content banner-content-overlay text-center">
                <h3 class="banner-title">Men</h3>

                <h4 class="banner-subtitle">12 Products</h4>

                <nuxt-link :to="'/shop/sidebar/list'" class="banner-link"
                  >Shop Now</nuxt-link
                >
              </div>
            </div>
          </div>

          <div class="col-md-6 col-lg-4">
            <div class="banner banner-cat">
              <a href="javascript:;">
                <img
                  v-lazy="'./images/category/3cols/banner-3.jpg'"
                  alt="Banner"
                />
              </a>

              <div class="banner-content banner-content-overlay text-center">
                <h3 class="banner-title">Accessories</h3>

                <h4 class="banner-subtitle">8 Products</h4>

                <nuxt-link :to="'/shop/sidebar/list'" class="banner-link"
                  >Shop Now</nuxt-link
                >
              </div>
            </div>
          </div>
        </div>

        <hr class="mb-4" />

        <h2 class="title text-center mb-3">4 Columns Carousel</h2>

        <div class="swiper-carousel swiper-simple swiper-1">
          <div v-swiper:swiper="carouselSetting">
            <div class="swiper-wrapper">
              <div class="swiper-slide">
                <div class="banner banner-cat">
                  <a href="javascript:;">
                    <img
                      v-lazy="'./images/category/4cols/banner-1.jpg'"
                      alt="Banner"
                    />
                  </a>

                  <div class="banner-content banner-content-static text-center">
                    <h3 class="banner-title">Women</h3>

                    <h4 class="banner-subtitle">18 Products</h4>

                    <nuxt-link :to="'shop/sidebar/list'" class="banner-link"
                      >Shop Now</nuxt-link
                    >
                  </div>
                </div>
              </div>

              <div class="swiper-slide">
                <div class="banner banner-cat">
                  <a href="javascript:;">
                    <img
                      v-lazy="'./images/category/4cols/banner-2.jpg'"
                      alt="Banner"
                    />
                  </a>

                  <div class="banner-content banner-content-static text-center">
                    <h3 class="banner-title">Men</h3>

                    <h4 class="banner-subtitle">12 Products</h4>

                    <nuxt-link :to="'shop/sidebar/list'" class="banner-link"
                      >Shop Now</nuxt-link
                    >
                  </div>
                </div>
              </div>

              <div class="swiper-slide">
                <div class="banner banner-cat">
                  <a href="javascript:;">
                    <img
                      v-lazy="'./images/category/4cols/banner-3.jpg'"
                      alt="Banner"
                    />
                  </a>

                  <div class="banner-content banner-content-static text-center">
                    <h3 class="banner-title">Shoes & Boots</h3>

                    <h4 class="banner-subtitle">15 Products</h4>

                    <nuxt-link :to="'shop/sidebar/list'" class="banner-link"
                      >Shop Now</nuxt-link
                    >
                  </div>
                </div>
              </div>

              <div class="swiper-slide">
                <div class="banner banner-cat">
                  <a href="javascript:;">
                    <img
                      v-lazy="'./images/category/4cols/banner-4.jpg'"
                      alt="Banner"
                    />
                  </a>

                  <div class="banner-content banner-content-static text-center">
                    <h3 class="banner-title">Accessories</h3>

                    <h4 class="banner-subtitle">8 Products</h4>

                    <nuxt-link :to="'shop/sidebar/list'" class="banner-link"
                      >Shop Now</nuxt-link
                    >
                  </div>
                </div>
              </div>

              <div class="swiper-slide">
                <div class="banner banner-cat">
                  <a href="javascript:;">
                    <img
                      v-lazy="'./images/category/4cols/banner-1.jpg'"
                      alt="Banner"
                    />
                  </a>

                  <div class="banner-content banner-content-static text-center">
                    <h3 class="banner-title">Women</h3>

                    <h4 class="banner-subtitle">18 Products</h4>

                    <nuxt-link :to="'shop/sidebar/list'" class="banner-link"
                      >Shop Now</nuxt-link
                    >
                  </div>
                </div>
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

        <hr class="mb-4" />
      </div>

      <div class="container-fluid">
        <h2 class="title text-center mb-3">3 Columns Fullwidth</h2>

        <div class="row justify-content-center">
          <div class="col-md-6 col-lg-4">
            <div class="banner banner-cat">
              <a href="javascript:;">
                <img
                  v-lazy="'./images/category/fullwidth/banner-1.jpg'"
                  alt="Banner"
                />
              </a>

              <div class="banner-content">
                <h3 class="banner-title">Women</h3>

                <h4 class="banner-subtitle">18 Products</h4>

                <nuxt-link :to="'/shop/sidebar/list'" class="banner-link"
                  >Shop Now</nuxt-link
                >
              </div>
            </div>
          </div>

          <div class="col-md-6 col-lg-4">
            <div class="banner banner-cat">
              <a href="javascript:;">
                <img
                  v-lazy="'./images/category/fullwidth/banner-2.jpg'"
                  alt="Banner"
                />
              </a>

              <div class="banner-content">
                <h3 class="banner-title">Men</h3>

                <h4 class="banner-subtitle">12 Products</h4>

                <nuxt-link :to="'/shop/sidebar/list'" class="banner-link"
                  >Shop Now</nuxt-link
                >
              </div>
            </div>
          </div>

          <div class="col-md-6 col-lg-4">
            <div class="banner banner-cat">
              <a href="javascript:;">
                <img
                  v-lazy="'./images/category/fullwidth/banner-3.jpg'"
                  alt="Banner"
                />
              </a>

              <div class="banner-content">
                <h3 class="banner-title">Accessories</h3>

                <h4 class="banner-subtitle">12 Products</h4>

                <nuxt-link :to="'/shop/sidebar/list'" class="banner-link"
                  >Shop Now</nuxt-link
                >
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="container">
        <hr class="mb-4" />

        <h2 class="title text-center mb-3">Masonry</h2>

        <div class="row justify-content-center">
          <div class="col-sm-6 col-lg-3">
            <div class="banner banner-cat banner-link-anim banner-large">
              <a href="javascript:;">
                <img
                  v-lazy="'./images/category/grid/banner-1.jpg'"
                  alt="Banner"
                />
              </a>

              <div class="banner-content banner-content-bottom">
                <h3 class="banner-title">Accessories</h3>

                <h4 class="banner-subtitle">8 Products</h4>

                <nuxt-link :to="'/shop/sidebar/list'" class="banner-link"
                  >Shop Now</nuxt-link
                >
              </div>
            </div>
          </div>

          <div class="col-sm-6 col-lg-3 order-lg-last">
            <div class="banner banner-cat banner-link-anim banner-large">
              <a href="javascript:;">
                <img
                  v-lazy="'./images/category/grid/banner-4.jpg'"
                  alt="Banner"
                />
              </a>

              <div class="banner-content banner-content-top">
                <h3 class="banner-title">Shoes & Boots</h3>

                <h4 class="banner-subtitle">15 Products</h4>

                <nuxt-link :to="'/shop/sidebar/list'" class="banner-link"
                  >Shop Now</nuxt-link
                >
              </div>
            </div>
          </div>

          <div class="col-lg-6">
            <div class="row">
              <div class="col-sm-6 col-lg-12">
                <div class="banner banner-cat banner-link-anim">
                  <a href="javascript:;">
                    <img
                      v-lazy="'./images/category/grid/banner-2.jpg'"
                      alt="Banner"
                    />
                  </a>

                  <div class="banner-content">
                    <h3 class="banner-title">Women</h3>

                    <h4 class="banner-subtitle">18 Products</h4>

                    <nuxt-link :to="'shop/sidebar/list'" class="banner-link"
                      >Shop Now</nuxt-link
                    >
                  </div>
                </div>
              </div>

              <div class="col-sm-6 col-lg-12">
                <div class="banner banner-cat banner-link-anim">
                  <a href="javascript:;">
                    <img
                      v-lazy="'./images/category/grid/banner-3.jpg'"
                      alt="Banner"
                    />
                  </a>

                  <div class="banner-content">
                    <h3 class="banner-title">Men</h3>

                    <h4 class="banner-subtitle">12 Products</h4>

                    <nuxt-link :to="'shop/sidebar/list'" class="banner-link"
                      >Shop Now</nuxt-link
                    >
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <hr class="mb-4" />

        <h2 class="title text-center mb-3">Grid Badge Style</h2>

        <div class="row">
          <div class="col-md-6">
            <div class="banner banner-cat banner-badge">
              <a href="javascript:;">
                <img
                  v-lazy="'./images/category/grid2/banner-1.jpg'"
                  alt="Banner"
                />
              </a>

              <nuxt-link class="banner-link" :to="'/shop/sidebar/list'">
                <h3 class="banner-title">Accessories</h3>

                <h4 class="banner-subtitle">8 Products</h4>

                <span class="banner-link-text">Shop Now</span>
              </nuxt-link>
            </div>
          </div>

          <div class="col-md-6">
            <div class="banner banner-cat banner-badge">
              <a href="javascript:;">
                <img
                  v-lazy="'./images/category/grid2/banner-2.jpg'"
                  alt="Banner"
                />
              </a>

              <nuxt-link class="banner-link" :to="'/shop/sidebar/list'">
                <h3 class="banner-title">Women</h3>

                <h4 class="banner-subtitle">15 Products</h4>

                <span class="banner-link-text">Shop Now</span>
              </nuxt-link>
            </div>

            <div class="banner banner-cat banner-badge">
              <a href="javascript:;">
                <img
                  v-lazy="'./images/category/grid2/banner-3.jpg'"
                  alt="Banner"
                />
              </a>

              <nuxt-link class="banner-link" :to="'/shop/sidebar/list'">
                <h3 class="banner-title">Men</h3>

                <h4 class="banner-subtitle">12 Products</h4>

                <span class="banner-link-text">Shop Now</span>
              </nuxt-link>
            </div>
          </div>
        </div>
      </div>
    </div>

    <element-list></element-list>
  </main>
</template>

<script>
import PageHeader from "~/components/elements/PageHeader";
import ElementList from "~/components/partial/elements/ElementList";
import { carouselSettingDefault } from "~/utilities/carousel";

export default {
  components: {
    PageHeader,
    ElementList,
  },
  data: function () {
    return {
      carouselSetting: {
        ...carouselSettingDefault,
        navigation: {
          nextEl: ".swiper-1 .swiper-next",
          prevEl: ".swiper-1 .swiper-prev",
        },
        breakpoints: {
          480: {
            slidesPerView: 1,
          },
          768: {
            slidesPerView: 2,
          },
          992: {
            slidesPerView: 3,
          },
        },
      },
    };
  },
};
</script>
