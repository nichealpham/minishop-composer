<template>
    <div class="product-gallery product-gallery-separated" v-if="product">
        <span class="product-label label-new" v-if="product.new">New</span>
        <span class="product-label label-sale" v-if="product.sale_price"
            >Sale</span
        >
        <span class="product-label label-top" v-if="product.top">Top</span>
        <span class="product-label label-out" v-if="product.stock === 0"
            >Out Of Stock</span
        >

        <figure
            class="product-main-image"
            v-for="(picture, index) in product.pictures"
            :key="index"
        >
            <img
                id="product-zoom"
                v-lazy="`${baseUrl}${picture.url}`"
                alt="product"
                :width="picture.width"
                :height="picture.height"
            />

            <a
                href="#"
                id="btn-product-gallery"
                class="btn-product-gallery"
                @click.prevent="openLightBox(index)"
            >
                <i class="icon-arrows"></i>
            </a>
        </figure>

        <light-box
            ref="lightbox"
            class="light-box"
            :media="lightBoxMedia"
            :show-caption="true"
            :show-light-box="false"
        ></light-box>
    </div>
</template>
<script>
import LightBox from 'vue-image-lightbox';
import { VueLazyloadImage } from 'vue-lazyload';
import 'vue-image-lightbox/dist/vue-image-lightbox.min.css';

import { baseUrl } from '~/repositories/repository.js';

export default {
    components: {
        LightBox
    },
    props: {
        product: {
            type: Object,
            default: function() {
                return {
                    pictures: []
                };
            }
        }
    },
    data: function() {
        return {
            baseUrl: baseUrl
        };
    },
    computed: {
        lightBoxMedia: function() {
            return this.product.pictures.reduce((acc, cur) => {
                return [
                    ...acc,
                    {
                        thumb: `${cur.url}`,
                        src: `${cur.url}`,
                        caption: this.product.name
                    }
                ];
            }, []);
        }
    },
    methods: {
        openLightBox: function(index) {
            this.$refs.lightbox.showImage(index);
        }
    }
};
</script>
