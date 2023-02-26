<template>
    <div class="main home-page">
        <new-collection :products="getItems"></new-collection>
        <blog-section :blogs="blogs"></blog-section>
    </div>
</template>
<script>
import NewCollection from '~/components/partial/home/newCollection';
import { homeData } from '~/utilities/data';
import { mapGetters, mapActions, mapMutations } from 'vuex';
import BlogSection from '~/components/partial/home/BlogSection.vue';

export default {
    components: {
        NewCollection,
        BlogSection
    },
    data: function() {
        return {
            trendyProducts: [],
            homeData: homeData
        };
    },
    computed: {
        ...mapGetters('product', ['getItems']),
        blogs() {
            return this.$store.getters['blog/getItems'] || [];
        }
    },
    mounted() {
        this.CLEAN_QUERY_OPTIONS();
        this.search({ company: this.$companyName, reload: true });
        this.searchBlogs({ company: this.$companyName, reload: true });
    },
    methods: {
        ...mapActions('product', ['search']),
        ...mapMutations('product', ['SET_PAGINATION', 'CLEAN_QUERY_OPTIONS']),
        ...mapActions('blog', { searchBlogs: 'search' })
    }
};
</script>
