<template>
  <main class="main">
    <page-header title="Product Category Fullwidth"></page-header>

    <nav class="breadcrumb-nav breadcrumb-with-filter">
      <div class="container-fluid">
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
          <li class="breadcrumb-item active">Fullwidth</li>
        </ol>
      </div>
    </nav>

    <div class="page-content">
      <div class="categories-page">
        <div class="container-fluid">
          <div class="row">
            <div class="col-lg-6">
              <div class="row">
                <div class="col-sm-8">
                  <div class="banner banner-cat banner-badge">
                    <nuxt-link :to="'shop/sidebar/list'">
                      <img
                        v-lazy="'./images/category/fullwidth-page/banner-1.jpg'"
                        alt="Banner"
                      />
                    </nuxt-link>

                    <nuxt-link class="banner-link" :to="'shop/sidebar/3cols'">
                      <h3 class="banner-title">Jackets</h3>

                      <h4 class="banner-subtitle">2 Products</h4>

                      <span class="banner-link-text">Shop Now</span>
                    </nuxt-link>
                  </div>
                </div>

                <div class="col-sm-4">
                  <div class="banner banner-cat banner-badge">
                    <nuxt-link :to="'shop/sidebar/list'">
                      <img
                        v-lazy="'./images/category/fullwidth-page/banner-2.jpg'"
                        alt="Banner"
                      />
                    </nuxt-link>

                    <nuxt-link class="banner-link" :to="'shop/sidebar/3cols'">
                      <h3 class="banner-title">Jeans</h3>

                      <h4 class="banner-subtitle">1 Product</h4>

                      <span class="banner-link-text">Shop Now</span>
                    </nuxt-link>
                  </div>
                </div>

                <div class="col-sm-4">
                  <div class="banner banner-cat banner-badge">
                    <nuxt-link :to="'shop/sidebar/list'">
                      <img
                        v-lazy="'./images/category/fullwidth-page/banner-3.jpg'"
                        alt="Banner"
                      />
                    </nuxt-link>

                    <nuxt-link class="banner-link" :to="'shop/sidebar/3cols'">
                      <h3 class="banner-title">Sportwear</h3>

                      <h4 class="banner-subtitle">0 Product</h4>

                      <span class="banner-link-text">Shop Now</span>
                    </nuxt-link>
                  </div>
                </div>

                <div class="col-sm-8">
                  <div class="banner banner-cat banner-badge">
                    <nuxt-link :to="'shop/sidebar/list'">
                      <img
                        v-lazy="'./images/category/fullwidth-page/banner-4.jpg'"
                        alt="Banner"
                      />
                    </nuxt-link>

                    <nuxt-link class="banner-link" :to="'shop/sidebar/3cols'">
                      <h3 class="banner-title">Bags</h3>

                      <h4 class="banner-subtitle">4 Products</h4>

                      <span class="banner-link-text">Shop Now</span>
                    </nuxt-link>
                  </div>
                </div>
              </div>
            </div>

            <div class="col-lg-6">
              <div class="row">
                <div class="col-sm-8">
                  <div class="banner banner-cat banner-badge">
                    <nuxt-link :to="'shop/sidebar/list'">
                      <img
                        v-lazy="'./images/category/fullwidth-page/banner-5.jpg'"
                        alt="Banner"
                      />
                    </nuxt-link>

                    <nuxt-link class="banner-link" :to="'shop/sidebar/3cols'">
                      <h3 class="banner-title">Dresses</h3>

                      <h4 class="banner-subtitle">3 Products</h4>

                      <span class="banner-link-text">Shop Now</span>
                    </nuxt-link>
                  </div>

                  <div class="banner banner-cat banner-badge">
                    <nuxt-link :to="'shop/sidebar/list'">
                      <img
                        v-lazy="'./images/category/fullwidth-page/banner-6.jpg'"
                        alt="Banner"
                      />
                    </nuxt-link>

                    <nuxt-link class="banner-link" :to="'shop/sidebar/3cols'">
                      <h3 class="banner-title">Shoes</h3>

                      <h4 class="banner-subtitle">2 Products</h4>

                      <span class="banner-link-text">Shop Now</span>
                    </nuxt-link>
                  </div>
                </div>

                <div class="col-sm-4">
                  <div class="banner banner-cat banner-badge">
                    <nuxt-link :to="'shop/sidebar/list'">
                      <img
                        v-lazy="'./images/category/fullwidth-page/banner-7.jpg'"
                        alt="Banner"
                      />
                    </nuxt-link>

                    <nuxt-link class="banner-link" :to="'shop/sidebar/3cols'">
                      <h3 class="banner-title">T-shirts</h3>

                      <h4 class="banner-subtitle">0 Products</h4>

                      <span class="banner-link-text">Shop Now</span>
                    </nuxt-link>
                  </div>

                  <div class="banner banner-cat banner-badge">
                    <nuxt-link :to="'shop/sidebar/list'">
                      <img
                        v-lazy="'./images/category/fullwidth-page/banner-8.jpg'"
                        alt="Banner"
                      />
                    </nuxt-link>

                    <nuxt-link class="banner-link" :to="'shop/sidebar/3cols'">
                      <h3 class="banner-title">Jumpers</h3>

                      <h4 class="banner-subtitle">1 Product</h4>

                      <span class="banner-link-text">Shop Now</span>
                    </nuxt-link>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="sidebar-filter-overlay" @click="closeSidebar"></div>

      <aside class="sidebar-shop sidebar-filter sidebar-filter-banner">
        <div class="sidebar-filter-wrapper">
          <div class="widget widget-clean">
            <label @click="closeSidebar">
              <i class="icon-close"></i>Filters
            </label>
            <a
              href="#"
              class="sidebar-filter-clear"
              @click.prevent="cleanAllFilter"
              >Clean All</a
            >
          </div>
          <div class="widget">
            <h3 class="widget-title">Browse Category</h3>

            <div class="widget-body">
              <div class="filter-items filter-items-count">
                <div class="filter-item">
                  <div class="custom-control custom-checkbox">
                    <input
                      type="checkbox"
                      class="custom-control-input"
                      id="cat-1"
                    />
                    <label class="custom-control-label" for="cat-1"
                      >Women</label
                    >
                  </div>

                  <span class="item-count">3</span>
                </div>

                <div class="filter-item">
                  <div class="custom-control custom-checkbox">
                    <input
                      type="checkbox"
                      class="custom-control-input"
                      id="cat-2"
                    />
                    <label class="custom-control-label" for="cat-2">Men</label>
                  </div>

                  <span class="item-count">0</span>
                </div>

                <div class="filter-item">
                  <div class="custom-control custom-checkbox">
                    <input
                      type="checkbox"
                      class="custom-control-input"
                      id="cat-3"
                    />
                    <label class="custom-control-label" for="cat-3"
                      >Holiday Shop</label
                    >
                  </div>

                  <span class="item-count">0</span>
                </div>

                <div class="filter-item">
                  <div class="custom-control custom-checkbox">
                    <input
                      type="checkbox"
                      class="custom-control-input"
                      id="cat-4"
                    />
                    <label class="custom-control-label" for="cat-4"
                      >Gifts</label
                    >
                  </div>

                  <span class="item-count">0</span>
                </div>

                <div class="filter-item">
                  <div class="custom-control custom-checkbox">
                    <input
                      type="checkbox"
                      class="custom-control-input"
                      id="cat-5"
                    />
                    <label class="custom-control-label" for="cat-5"
                      >Homeware</label
                    >
                  </div>

                  <span class="item-count">0</span>
                </div>

                <div class="filter-item">
                  <div class="custom-control custom-checkbox">
                    <input
                      type="checkbox"
                      class="custom-control-input"
                      id="cat-6"
                      checked="checked"
                    />
                    <label class="custom-control-label" for="cat-6"
                      >Grid Categories Fullwidth</label
                    >
                  </div>

                  <span class="item-count">13</span>
                </div>

                <div class="sub-filter-items">
                  <div class="filter-item">
                    <div class="custom-control custom-checkbox">
                      <input
                        type="checkbox"
                        class="custom-control-input"
                        id="cat-7"
                      />
                      <label class="custom-control-label" for="cat-7"
                        >Dresses</label
                      >
                    </div>

                    <span class="item-count">3</span>
                  </div>

                  <div class="filter-item">
                    <div class="custom-control custom-checkbox">
                      <input
                        type="checkbox"
                        class="custom-control-input"
                        id="cat-8"
                      />
                      <label class="custom-control-label" for="cat-8"
                        >T-shirts</label
                      >
                    </div>

                    <span class="item-count">0</span>
                  </div>

                  <div class="filter-item">
                    <div class="custom-control custom-checkbox">
                      <input
                        type="checkbox"
                        class="custom-control-input"
                        id="cat-9"
                      />
                      <label class="custom-control-label" for="cat-9"
                        >Bags</label
                      >
                    </div>

                    <span class="item-count">4</span>
                  </div>

                  <div class="filter-item">
                    <div class="custom-control custom-checkbox">
                      <input
                        type="checkbox"
                        class="custom-control-input"
                        id="cat-10"
                      />
                      <label class="custom-control-label" for="cat-10"
                        >Jackets</label
                      >
                    </div>

                    <span class="item-count">2</span>
                  </div>

                  <div class="filter-item">
                    <div class="custom-control custom-checkbox">
                      <input
                        type="checkbox"
                        class="custom-control-input"
                        id="cat-11"
                      />
                      <label class="custom-control-label" for="cat-11"
                        >Shoes</label
                      >
                    </div>

                    <span class="item-count">2</span>
                  </div>

                  <div class="filter-item">
                    <div class="custom-control custom-checkbox">
                      <input
                        type="checkbox"
                        class="custom-control-input"
                        id="cat-12"
                      />
                      <label class="custom-control-label" for="cat-12"
                        >Jumpers</label
                      >
                    </div>

                    <span class="item-count">1</span>
                  </div>

                  <div class="filter-item">
                    <div class="custom-control custom-checkbox">
                      <input
                        type="checkbox"
                        class="custom-control-input"
                        id="cat-13"
                      />
                      <label class="custom-control-label" for="cat-13"
                        >Jeans</label
                      >
                    </div>

                    <span class="item-count">1</span>
                  </div>

                  <div class="filter-item">
                    <div class="custom-control custom-checkbox">
                      <input
                        type="checkbox"
                        class="custom-control-input"
                        id="cat-14"
                      />
                      <label class="custom-control-label" for="cat-14"
                        >Sportwear</label
                      >
                    </div>

                    <span class="item-count">0</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </aside>
    </div>
  </main>
</template>

<script>
import PageHeader from "~/components/elements/PageHeader";

export default {
  components: {
    PageHeader,
  },
  methods: {
    openSidebar: function () {
      document.querySelector("body").classList.add("sidebar-filter-active");
    },
    closeSidebar: function () {
      document.querySelector("body").classList.remove("sidebar-filter-active");
    },
    cleanAllFilter: function () {
      let allInputs = document.querySelectorAll(
        ".sidebar-filter input[type=checkbox]"
      );
      for (let i = 0; i < allInputs.length; i++) {
        allInputs[i].checked = false;
      }
    },
  },
};
</script>
