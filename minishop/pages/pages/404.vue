<template>
    <main class="main">
        <div
            class="error-content text-center"
            v-lazy:background-image="'./images/backgrounds/error-bg.jpg'"
        >
            <div class="container">
                <h1 class="error-title">Error 404</h1>

                <p>We are sorry, the page you've requested is not available.</p>
                <nuxt-link
                    to="/"
                    class="btn btn-outline-primary-2 btn-minwidth-lg"
                >
                    <span>BACK TO HOMEPAGE</span>
                    <i class="icon-long-arrow-right"></i>
                </nuxt-link>
            </div>
        </div>
    </main>
</template>
<script>
export default {};
</script>
