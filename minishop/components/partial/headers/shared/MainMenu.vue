<template>
    <nav class="main-nav">
        <ul class="menu">
            <li class="megamenu-container" :class="{ active: current == '/' }">
                <nuxt-link
                    :to="{
                        path: `/`
                    }"
                    class="sf-with-ul"
                    >Trang chủ</nuxt-link
                >
            </li>
            <li
                class="megamenu-container"
                :class="{ active: current == 'gallery' }"
            >
                <nuxt-link
                    :to="{
                        path: `/gallery`
                    }"
                    class="sf-with-ul"
                    >Thư viện ảnh</nuxt-link
                >
            </li>
            <li
                class="megamenu-container"
                :class="{ active: current == 'shop' }"
            >
                <nuxt-link
                    :to="{
                        path: `/shop/sidebar/4cols`
                    }"
                    class="sf-with-ul"
                    >Cửa hàng</nuxt-link
                >
            </li>
            <li
                class="megamenu-container"
                :class="{ active: current == 'blog' }"
            >
                <nuxt-link
                    :to="{
                        path: `/blog/listing`
                    }"
                    class="sf-with-ul"
                    >Bài viết</nuxt-link
                >
            </li>
            <li
                class="megamenu-container"
                :class="{ active: current == 'account' }"
            >
                <a
                    style="cursor:pointer;color:black"
                    @click="$router.push('/account')"
                    class="sf-with-ul"
                    >Tài khoản</a
                >
            </li>
        </ul>
    </nav>
</template>
<script>
import { mapGetters, mapMutations } from 'vuex';

export default {
    computed: {
        current: function() {
            if (this.$route.path.includes('shop')) return 'shop';
            if (this.$route.path.includes('gallery')) return 'gallery';
            if (this.$route.path.includes('blog')) return 'blog';
            if (this.$route.path.includes('about')) return 'about';
            if (this.$route.path.includes('account')) return 'account';
            return '/';
        },
        ...mapGetters('auth', ['token'])
    },
    methods: {
        ...mapMutations('auth', ['LOGOUT']),
        clickLogin() {
            // if (!this.token) return (window.location.href = this.$crmLoginUrl);
            if (!this.token) return this.openNewTab(this.$crmLoginUrl);
            // return this.openNewTab(this.$crmUrl);
            this.$router.push('/account');
        },
        async clickLogout() {
            this.LOGOUT();
            await this.sleep(50);
            return this.openNewTab(this.$crmLogoutUrl);
            // window.location.href = this.$crmLogoutUrl;
        },
        viewAllDemos: function(e) {
            var list = document.querySelectorAll('.demo-list .hidden');
            for (let i = 0; i < list.length; i++) {
                list[i].classList.add('show');
            }

            e.currentTarget.parentElement.classList.add('d-none');
        }
    }
};
</script>
