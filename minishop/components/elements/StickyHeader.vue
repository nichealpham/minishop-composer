<template>
    <div class="sticky-wrapper" :class="{ sticky: isSticky }">
        <slot></slot>
    </div>
</template>

<script>
export default {
    props: {
        wrapperClass: {
            type: String,
            default: 'header'
        },
        top: {
            type: Number,
            default: 210
        }
    },
    data: function() {
        return {
            isSticky: false,
            firstHeight: 0
        };
    },
    watch: {
        $route: function () {
            let stickyContent = this.$el.children[0];
            this.$el.style.height = '';
        }
    },
    mounted: function() {
        let stickyContent = this.$el.children[0];
        this.$el.style.height = stickyContent.offsetHeight + 'px';
        window.addEventListener('scroll', this.scrollHandler, {
            passive: true
        });
        window.addEventListener('resize', this.resizeHandler, {
            passive: true
        });
    },

    destroyed: function() {
        window.removeEventListener('scroll', this.scrollHandler);
        window.removeEventListener('resize', this.resizeHandler);
    },
    methods: {
        scrollHandler: function() {
            let stickyContent = this.$el.children[0];
            if (window.pageYOffset > this.top) {
                if (!this.isSticky) {
                    stickyContent.classList.add('fixed');
                    this.isSticky = true;
                }
            } else if (this.isSticky) {
                stickyContent.classList.remove('fixed');
                this.isSticky = false;
            } else {
                this.$el.style.height = stickyContent.offsetHeight + 'px';
            }
        },
        resizeHandler: function() {
            let stickyContent = this.$el.children[0];
            this.$el.style.height = stickyContent.offsetHeight + 'px';
            this.scrollHandler();
        }
    }
};
</script>