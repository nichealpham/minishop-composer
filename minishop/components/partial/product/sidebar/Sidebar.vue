<template>
    <div class="skeleton-body">
        <div class="skel-pro-single" :class="{ loaded: loaded }">
            <div class="skel-widget"></div>
            <div class="skel-widget"></div>
            <div
                :class="
                    toggleState
                        ? 'sidebar-filter right sidebar-product'
                        : 'sidebar sidebar-product'
                "
            >
                <button
                    title="Close (Esc)"
                    type="button"
                    class="mfp-close"
                    @click="toggleSidebar"
                >
                    <span>×</span>
                </button>
                <related-products-two
                    :products="relatedProducts"
                ></related-products-two>

                <div class="widget widget-banner-sidebar">
                    <div class="banner-sidebar-title">ad box 280 x 280</div>

                    <div class="banner-sidebar banner-overlay">
                        <nuxt-link :to="'/shop/sidebar/4cols'">
                            <img
                                v-lazy="'./images/blog/sidebar/banner.jpg'"
                                alt="banner"
                            />
                        </nuxt-link>
                    </div>
                </div>
            </div>
            <button
                class="sidebar-fixed-toggler right"
                @click="toggleSidebar"
                v-if="toggleState"
            >
                <i class="icon-cog"></i>
            </button>
            <div class="sidebar-filter-overlay" @click="hideSidebar"></div>
        </div>
    </div>
</template>
<script>
import RelatedProductsTwo from '~/components/partial/product/related/RelatedProductsTwo';
export default {
    components: {
        RelatedProductsTwo
    },
    props: {
        products: Array,
        loaded: Boolean
    },
    computed: {
        relatedProducts: function() {
            return this.products.slice(0, 4);
        }
    },
    data: function() {
        return {
            toggleState: false
        };
    },
    mounted: function() {
        this.stickyHandle();
        window.addEventListener('resize', this.stickyHandle, { passive: true });
    },
    destroyed: function() {
        window.removeEventListener('resize', this.stickyHandle);
    },
    methods: {
        stickyHandle: function() {
            if (window.outerWidth < 992) {
                this.toggleState = true;
            } else {
                this.toggleState = false;
            }
        },

        toggleSidebar: function() {
            document
                .querySelector('body')
                .classList.toggle('sidebar-filter-active');
        },

        hideSidebar: function() {
            document
                .querySelector('body')
                .classList.remove('sidebar-filter-active');
        }
    }
};
</script>
