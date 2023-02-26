<template>
    <section v-lazy:background-image="bgImage">
        <slot></slot>
    </section>
</template>

<script>
export default {
    props: {
        options: {
            type: Object,
            default: function() {
                return {
                    offset: 20,
                    speed: 60
                };
            }
        },
        bgImage: {
            type: String,
            required: true
        }
    },

    mounted: function() {
        window.addEventListener('scroll', this.scrollHandler, {
            passive: true
        });
    },

    destroyed: function() {
        window.removeEventListener('scroll', this.scrollHandler);
    },

    methods: {
        scrollHandler: function() {
            let parallax = this.$el;
            let y =
                ((parallax.offsetTop - window.pageYOffset) *
                    this.options.speed) /
                    parallax.offsetTop +
                this.options.offset;
            parallax.style.backgroundPositionY = y + '%';
        }
    }
};
</script>