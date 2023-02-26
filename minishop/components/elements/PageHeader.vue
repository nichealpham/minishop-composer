<template>
    <div
        class="page-header text-center"
        v-lazy:background-image="'./images/page-header-bg.jpg'"
    >
        <div class="container">
            <h1 class="page-title">
                {{ pageTitle }}
                <span>{{ getCompany.companyName }}</span>
            </h1>
        </div>
    </div>
</template>
<script>
import { mapGetters, mapActions } from 'vuex';

export default {
    props: {
        title: {
            type: String,
            required: true
        },
        subtitle: String
    },
    computed: {
        ...mapGetters('company', ['getCompany']),
        pageTitle: function() {
            if (this.$route.path.includes('wishlist')) return 'Yêu thích';
            if (this.$route.path.includes('account')) return 'Tài khoản';
            if (this.$route.path.includes('cart')) return 'Giỏ hàng';
            if (this.$route.path.includes('checkout')) return 'Đặt hàng';
            if (this.$route.path.includes('blog')) return 'Bài viết';
            if (this.$route.path.includes('about')) return 'Tài khoản';
            return this.title;
        }
    }
};
</script>
