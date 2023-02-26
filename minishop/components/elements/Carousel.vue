<template>
    <div class="swiper-carousel swiper-simple" :id="id">
        <div v-swiper:swiper="carouselSetting">
            <div class="swiper-wrapper">
                <slot></slot>
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
        <div class="swiper-pagination"></div>
    </div>
</template>
<script>
export default {
    props: {
        setting: Object
    },
    data: function() {
        return {
            id: Math.random()
                .toString(36)
                .substring(2, 15),
            carouselSetting: {
                loop: false,
                slidesPerView: 1,
                spaceBetween: 20,
                scrollbar: {
                    draggable: false
                }
            },
            loaded: false
        };
    },
    created: function() {
        this.carouselSetting = {
            ...this.carouselSetting,
            pagination: {
                el: '#' + this.id + ' .swiper-pagination',
                clickable: true
            },
            navigation: {
                nextEl: '#' + this.id + ' .swiper-next',
                preveEl: '#' + this.id + ' .swiper-prev'
            },
            ...this.setting
        };
        this.loaded = true;
    }
};
</script>