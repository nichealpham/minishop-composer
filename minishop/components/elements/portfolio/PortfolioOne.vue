<template>
    <div class="portfolio portfolio-overlay">
        <figure class="portfolio-media">
            <a href="#">
                <img :src="image" alt="item" />
            </a>
        </figure>
        <div class="portfolio-content">
            <h3 class="portfolio-title">
                <a href="#">Vestibulum auctor dapibus</a>
            </h3>

            <div class="portfolio-tags">
                <a href="#">{{ category }}</a>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props: {
        image: String,
        category: String
    }
};
</script>